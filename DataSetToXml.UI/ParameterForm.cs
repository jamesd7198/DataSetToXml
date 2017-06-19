using System;
using System.Windows.Forms;

namespace DataSetToXml.UI
{
    public partial class ParameterForm : Form
    {
        public string ParameterName { get; private set; }
        public object ParameterValue { get; private set; }

        public ParameterForm()
        {
            InitializeComponent();
            okButton.Enabled = false;
        }
        
        void nameAndValueField_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameField.Text) && !string.IsNullOrWhiteSpace(valueField.Text))
                okButton.Enabled = true;
            else
                okButton.Enabled = false;

            var field = (TextBox) sender;

            if (!string.IsNullOrWhiteSpace(field.Text))
                ParameterValue = field.Text;
        }

        void nameAndValueField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CompleteParameterAssignment();
            }
        }

        void okButton_Click(object sender, EventArgs e)
        {
            CompleteParameterAssignment();
        }

        void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        void CompleteParameterAssignment()
        {
            if (string.IsNullOrWhiteSpace(nameField.Text) || string.IsNullOrWhiteSpace(valueField.Text))
                return;

            ParameterName = nameField.Text;
            ParameterValue = valueField.Text;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
