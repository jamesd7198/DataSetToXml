namespace DataSetToXml.UI
{
    partial class DataSetToXmlForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.connectionDataDropdown = new System.Windows.Forms.ComboBox();
            this.queryBox = new System.Windows.Forms.RichTextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.fileExplorerLink = new System.Windows.Forms.LinkLabel();
            this.executeButton = new System.Windows.Forms.Button();
            this.saveToFilepathField = new System.Windows.Forms.TextBox();
            this.parameterList = new System.Windows.Forms.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.parametersGroup = new System.Windows.Forms.GroupBox();
            this.sprocCheckbox = new System.Windows.Forms.CheckBox();
            this.parametersGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectionDataDropdown
            // 
            this.connectionDataDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.connectionDataDropdown.FormattingEnabled = true;
            this.connectionDataDropdown.Location = new System.Drawing.Point(24, 24);
            this.connectionDataDropdown.Name = "connectionDataDropdown";
            this.connectionDataDropdown.Size = new System.Drawing.Size(265, 21);
            this.connectionDataDropdown.TabIndex = 0;
            this.connectionDataDropdown.DropDown += new System.EventHandler(this.connectionDataDropdown_DropDown);
            // 
            // queryBox
            // 
            this.queryBox.Location = new System.Drawing.Point(24, 64);
            this.queryBox.Name = "queryBox";
            this.queryBox.Size = new System.Drawing.Size(406, 337);
            this.queryBox.TabIndex = 2;
            this.queryBox.Text = "Enter query or procedure here.";
            this.queryBox.Enter += new System.EventHandler(this.queryBox_Enter);
            this.queryBox.Leave += new System.EventHandler(this.queryBox_Leave);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(177, 444);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(112, 28);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save Results To";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // fileExplorerLink
            // 
            this.fileExplorerLink.AutoSize = true;
            this.fileExplorerLink.Location = new System.Drawing.Point(21, 444);
            this.fileExplorerLink.Name = "fileExplorerLink";
            this.fileExplorerLink.Size = new System.Drawing.Size(132, 13);
            this.fileExplorerLink.TabIndex = 5;
            this.fileExplorerLink.TabStop = true;
            this.fileExplorerLink.Text = "Open in Windows Explorer";
            this.fileExplorerLink.Visible = false;
            this.fileExplorerLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.fileExplorerLink_LinkClicked);
            // 
            // executeButton
            // 
            this.executeButton.Location = new System.Drawing.Point(318, 444);
            this.executeButton.Name = "executeButton";
            this.executeButton.Size = new System.Drawing.Size(112, 28);
            this.executeButton.TabIndex = 6;
            this.executeButton.Text = "Execute Query";
            this.executeButton.UseVisualStyleBackColor = true;
            this.executeButton.Click += new System.EventHandler(this.executeButton_Click);
            // 
            // saveToFilepathField
            // 
            this.saveToFilepathField.Location = new System.Drawing.Point(24, 418);
            this.saveToFilepathField.Name = "saveToFilepathField";
            this.saveToFilepathField.Size = new System.Drawing.Size(406, 20);
            this.saveToFilepathField.TabIndex = 7;
            this.saveToFilepathField.TextChanged += new System.EventHandler(this.saveToFilepathField_TextChanged);
            // 
            // parameterList
            // 
            this.parameterList.FormattingEnabled = true;
            this.parameterList.Location = new System.Drawing.Point(22, 19);
            this.parameterList.Name = "parameterList";
            this.parameterList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.parameterList.Size = new System.Drawing.Size(265, 82);
            this.parameterList.TabIndex = 8;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(293, 30);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(92, 26);
            this.addButton.TabIndex = 9;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(294, 62);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(92, 26);
            this.removeButton.TabIndex = 10;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // parametersGroup
            // 
            this.parametersGroup.Controls.Add(this.removeButton);
            this.parametersGroup.Controls.Add(this.addButton);
            this.parametersGroup.Controls.Add(this.parameterList);
            this.parametersGroup.Location = new System.Drawing.Point(24, 287);
            this.parametersGroup.Name = "parametersGroup";
            this.parametersGroup.Size = new System.Drawing.Size(406, 114);
            this.parametersGroup.TabIndex = 11;
            this.parametersGroup.TabStop = false;
            this.parametersGroup.Text = "Parameters";
            this.parametersGroup.Visible = false;
            // 
            // sprocCheckbox
            // 
            this.sprocCheckbox.AutoSize = true;
            this.sprocCheckbox.Location = new System.Drawing.Point(355, 28);
            this.sprocCheckbox.Name = "sprocCheckbox";
            this.sprocCheckbox.Size = new System.Drawing.Size(75, 17);
            this.sprocCheckbox.TabIndex = 12;
            this.sprocCheckbox.Text = "Procedure";
            this.sprocCheckbox.UseVisualStyleBackColor = true;
            this.sprocCheckbox.CheckedChanged += new System.EventHandler(this.sprocCheckbox_CheckedChanged);
            // 
            // DataSetToXmlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 510);
            this.Controls.Add(this.sprocCheckbox);
            this.Controls.Add(this.parametersGroup);
            this.Controls.Add(this.saveToFilepathField);
            this.Controls.Add(this.executeButton);
            this.Controls.Add(this.fileExplorerLink);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.queryBox);
            this.Controls.Add(this.connectionDataDropdown);
            this.Name = "DataSetToXmlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DataSet to Xml";
            this.parametersGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox connectionDataDropdown;
        private System.Windows.Forms.RichTextBox queryBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.LinkLabel fileExplorerLink;
        private System.Windows.Forms.Button executeButton;
        private System.Windows.Forms.TextBox saveToFilepathField;
        private System.Windows.Forms.ListBox parameterList;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.GroupBox parametersGroup;
        private System.Windows.Forms.CheckBox sprocCheckbox;
    }
}

