using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace DeskProImport
{
    public partial class UploadForm : Form
    {
        public UploadForm()
        {
            InitializeComponents();
            InitializeEvents();
            InitializeOthers();
        }

        private void InitializeOthers()
        {
            dataFileDialog.Filter = "CSV File (*.csv)|*.csv|JSON File (*.json)|*.json";
        }

        private void InitializeComponents()
        {            
            Width = 500;
            Height = 200;
            Text = "DeskPro Batch Article Upload";
            Font = formFont;
            MinimumSize = new Size(Width,Height);

            lblServerAddress.Text = "Server Address";
            lblServerAddress.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            lblServerAddress.Left = 10;
            lblServerAddress.Width = 50;
            lblServerAddress.Top = row(0) + lblTopPad;

            txtServerAddress.Left = lblServerAddress.Right + 10;
            txtServerAddress.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            txtServerAddress.Width = Width - lblServerAddress.Right - 20;
            var tmpHeight = txtServerAddress.Height;
            txtServerAddress.MinimumSize = new Size(200,tmpHeight);
            txtServerAddress.Top = row(0) + txtTopPad;

            lblAPIKey.Text = "API Key";
            lblAPIKey.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            lblAPIKey.Left = 10;
            lblAPIKey.Width = 50;
            lblAPIKey.Top = row(1) + lblTopPad;

            txtAPIKey.Left = lblAPIKey.Right + 10;
            txtAPIKey.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            txtAPIKey.Width = Width - lblAPIKey.Right - 20;
            tmpHeight = txtAPIKey.Height;
            txtAPIKey.MinimumSize = new Size(200,tmpHeight);
            txtAPIKey.Top = row(1) + txtTopPad;
            
            lblCSVDataFile.Text = "CSV File";
            lblCSVDataFile.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            lblCSVDataFile.Left = 10;
            lblCSVDataFile.Top = row(2) + lblTopPad;
            lblCSVDataFile.Width = 50;

            btnCSVDataFileDialog.Text = "Browse";
            btnCSVDataFileDialog.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCSVDataFileDialog.Top = row(2) + btnTopPad;
            btnCSVDataFileDialog.Width = 100;
            btnCSVDataFileDialog.Left = Width - 10 - 100;

            txtCSVDataFile.ReadOnly = true;
            txtCSVDataFile.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            txtCSVDataFile.Left = lblCSVDataFile.Right + 10;
            txtCSVDataFile.Top = row(2) + txtTopPad;
            txtCSVDataFile.Width = btnCSVDataFileDialog.Left - lblCSVDataFile.Right - 20;
            tmpHeight = txtCSVDataFile.Height;
            txtCSVDataFile.MinimumSize = new Size(300,tmpHeight);

            btnCSVDataFileDialog.Height = txtCSVDataFile.Height;

            btnCancel.Text = "Cancel";
            btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            btnCancel.Width = 100;
            btnCancel.Top = Height - 50 - 10;
            btnCancel.Left = Width - btnCancel.Width - 10;
            btnCancel.DialogResult = DialogResult.Cancel;
            
            btnUpload.Text = "Upload";
            btnUpload.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            btnUpload.Width = 100;
            btnUpload.Top = Height - 50 - 10;
            btnUpload.Left = btnCancel.Left - btnUpload.Width - 10;
            //btnUpload.DialogResult = DialogResult.OK;
            
            btnUpload.Enabled = txtCSVDataFile.Text.Length > 0;
            
            Controls.Add(lblServerAddress);
            Controls.Add(txtServerAddress);
            Controls.Add(lblAPIKey);
            Controls.Add(txtAPIKey);
            Controls.Add(lblCSVDataFile);
            Controls.Add(txtCSVDataFile);
            Controls.Add(btnCSVDataFileDialog);
            Controls.Add(btnUpload);
            Controls.Add(btnCancel);
        }

        private void InitializeEvents()
        {
            Load += OnLoad;
            Closing += OnClosing;
            txtServerAddress.TextChanged += TxtServerAddressOnTextChanged;
            txtAPIKey.TextChanged += TxtApiKeyOnTextChanged;
            txtCSVDataFile.TextChanged += TxtCsvDataFileOnTextChanged;
            btnCSVDataFileDialog.Click += BtnCsvDataFileDialogOnClick;
            btnUpload.Click += BtnUploadOnClick;
            btnCancel.Click += (sender, args) => Close();
        }

        

        private int row(int r)
        {
            return r * rowHeight;
        }



        private readonly Label lblServerAddress = new Label();
        private readonly TextBox txtServerAddress = new TextBox();
        
        private readonly Label lblAPIKey = new Label();
        private readonly TextBox txtAPIKey = new TextBox();
        
        private readonly Label lblCSVDataFile = new Label();
        private readonly TextBox txtCSVDataFile = new TextBox();
        private readonly Button btnCSVDataFileDialog = new Button();
        
        private readonly Button btnUpload = new Button();
        private readonly Button btnCancel = new Button();
        
        private readonly OpenFileDialog dataFileDialog = new OpenFileDialog();
        
        private readonly Font formFont = new Font("Arial", 11.0f);

        private readonly int rowHeight = 30;
        private readonly int lblTopPad = 13;
        private readonly int txtTopPad = 10;
        private readonly int btnTopPad = 12;
    }
}