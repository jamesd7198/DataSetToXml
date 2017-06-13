using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DataSetToXml.DataSettingsHelper;
using DataSetToXml.UI.Properties;
using Npgsql;

namespace DataSetToXml.UI
{
    internal enum FileType
    {
        Xml = 1,
        Xsd
    };

    public partial class DataSetToXmlForm : Form
    {
        FileType _fileType;

        public DataSetToXmlForm()
        {
            InitializeComponent();
            InitializeConnectionData();
        }

        void InitializeConnectionData()
        {
            var connections = ConfigurationManager.ConnectionStrings
                .Cast<ConnectionStringSettings>()
                .Select(s => new ConnectionStringSettings
                {
                    Name = string.IsNullOrWhiteSpace(s.Name) ? "Default" : s.Name,
                    ProviderName = string.IsNullOrWhiteSpace(s.ProviderName) ? "System.Data.SqlClient" : s.ProviderName,
                    ConnectionString = s.ConnectionString
                })
                .ToList();

            connectionDataDropdown.DataSource = connections;
            connectionDataDropdown.DisplayMember = "Name";
            connectionDataDropdown.ValueMember = "ConnectionString";
            connectionDataDropdown.SelectedIndex = 0;
        }

        void connectionDataDropdown_DropDown(object sender, EventArgs e)
        {
            var maxWidth = 0;

            foreach (ConnectionStringSettings item in connectionDataDropdown.Items)
            {
                var itemWidth = TextRenderer.MeasureText(item.Name, connectionDataDropdown.Font).Width;

                var newWidth = itemWidth > connectionDataDropdown.DropDownWidth
                    ? itemWidth
                    : connectionDataDropdown.DropDownWidth;

                maxWidth = newWidth > maxWidth ? newWidth : maxWidth;
            }

            if (connectionDataDropdown.Items.Count > connectionDataDropdown.MaxDropDownItems)
            {
                maxWidth += SystemInformation.VerticalScrollBarWidth;
            }

            connectionDataDropdown.DropDownWidth = maxWidth;
        }

        void sprocCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (sprocCheckbox.Checked)
            {
                queryBox.SetBounds(
                    queryBox.Bounds.X,
                    queryBox.Bounds.Y,
                    queryBox.Bounds.Width,
                    parametersGroup.Bounds.Y);

                parametersGroup.Visible = true;
            }
            else
            {
                queryBox.SetBounds(
                    queryBox.Bounds.X,
                    queryBox.Bounds.Y,
                    queryBox.Bounds.Width,
                    queryBox.Bounds.Y + parametersGroup.Bounds.Y);

                parametersGroup.Visible = false;
                parameterList.Items.Clear();
            }
        }

        void queryBox_Enter(object sender, EventArgs e)
        {
            if (queryBox.Text.StartsWith(Resources.DataSetToXmlForm_executeButton_Click_Enter_query_or_procedure_here_))
                queryBox.Text = "";
        }

        void queryBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(queryBox.Text))
                queryBox.Text = Resources.DataSetToXmlForm_executeButton_Click_Enter_query_or_procedure_here_;
        }

        void saveButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.OverwritePrompt = true;
                dialog.Filter = "Data Files (*.xml)|*.xml|Schema Files (*.xsd)|*.xsd";
                dialog.DefaultExt = "xml";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _fileType = (FileType) dialog.FilterIndex;

                    saveToFilepathField.Text = dialog.FileName;
                }
            }
        }

        void saveToFilepathField_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(saveToFilepathField.Text))
                fileExplorerLink.Visible = false;
            else
            {
                var fileInfo = new FileInfo(saveToFilepathField.Text);

                _fileType = fileInfo.Extension == ".xml" ? FileType.Xml : FileType.Xsd;

                if (Directory.Exists(fileInfo.DirectoryName))
                    fileExplorerLink.Visible = true;
            }
        }

        void fileExplorerLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var fileInfo = new FileInfo(saveToFilepathField.Text);

            if (string.IsNullOrWhiteSpace(fileInfo.DirectoryName))
                return;

            if (!Directory.Exists(fileInfo.DirectoryName))
                Directory.CreateDirectory(fileInfo.DirectoryName);

            Process.Start(fileInfo.DirectoryName);
        }

        void addButton_Click(object sender, EventArgs e)
        {
            var paramForm = new ParameterForm();
            var result = paramForm.ShowDialog();

            if (result == DialogResult.OK)
                parameterList.Items.Add(new KeyValuePair<string, object>(
                    paramForm.ParameterName,
                    paramForm.ParameterValue));
        }

        void removeButton_Click(object sender, EventArgs e)
        {
            var list = parameterList.SelectedIndices;

            for (var x = list.Count; x > 0; x--)
                parameterList.Items.RemoveAt(list[x - 1]);
        }

        void executeButton_Click(object sender, EventArgs e)
        {
            var errors = new List<string>();

            var connectionData = connectionDataDropdown.SelectedItem as ConnectionStringSettings;

            var dataSettings = GetDataSettings(connectionData);

            if (dataSettings == null)
                errors.Add("Connection provider name");

            if (string.IsNullOrWhiteSpace(queryBox.Text) ||
                queryBox.Text == Resources.DataSetToXmlForm_executeButton_Click_Enter_query_or_procedure_here_)
                errors.Add("Query text");

            if (string.IsNullOrWhiteSpace(saveToFilepathField.Text))
                errors.Add("Filepath to save file to");

            if (errors.Count > 0)
            {
                MessageBox.Show(this, string.Join("\n", errors), "Missing/incorrect required parameters!", MessageBoxButtons.OK);

                return;
            }

            var dataSet = sprocCheckbox.Checked 
                ? DataLoader.LoadDataSetFromProcedure(dataSettings, queryBox.Text) 
                : DataLoader.LoadDataSetFromQuery(dataSettings, queryBox.Text); 

            try
            {
                if (_fileType == FileType.Xml)
                    DataWriter.WriteDataSetToXmlFile(dataSet, saveToFilepathField.Text);
                else
                    DataWriter.WriteSchemaToXsdFile(dataSet, saveToFilepathField.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Error creating {saveToFilepathField.Text}:\n{ex.Message}", "Failed", MessageBoxButtons.OK);

                return;
            }

            MessageBox.Show(this, $"{saveToFilepathField.Text} created!", "Success", MessageBoxButtons.OK);
        }

        IDataSettings GetDataSettings(ConnectionStringSettings connectionData)
        {
            IDataSettings dataSettings;

            switch (connectionData.ProviderName)
            {
                case "System.Data.SqlClient":
                    dataSettings = new DataSettings(
                        new SqlConnection(connectionData.ConnectionString),
                        new SqlCommand(),
                        new SqlDataAdapter());

                    foreach (KeyValuePair<string, object> parameter in parameterList.Items)
                    {
                        dataSettings.AddParameter(new SqlParameter(parameter.Key, parameter.Value));
                    }

                    return dataSettings;

                case "Npgsql":
                    dataSettings = new DataSettings(
                        new NpgsqlConnection(connectionData.ConnectionString),
                        new NpgsqlCommand(),
                        new NpgsqlDataAdapter());

                    foreach (KeyValuePair<string, object> parameter in parameterList.Items)
                    {
                        dataSettings.AddParameter(new NpgsqlParameter(parameter.Key, parameter.Value));
                    }

                    return dataSettings;

                default:
                    return null;
            }
        }
    }
}
