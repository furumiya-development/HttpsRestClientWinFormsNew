using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpsRestClientWinForms.FormDesign
{
    public class Form1Design : FormDesign
    {
        private DataGridView dataGridView1 = new DataGridView();
        private BindingSource bindingSource1 = new BindingSource();
        private TextBox textBoxUri = new TextBox();
        private ComboBox cmbHttpVer = new ComboBox();
        private ComboBox cmbSslProtocol = new ComboBox();
        private TextBox textBoxReqBody = new TextBox();
        private RichTextBox richTextBox1 = new RichTextBox();
        private Label labelUuid = new Label();
        private TextBox textBoxShohinCode = new TextBox();
        private TextBox textBoxShohinName = new TextBox();
        private TextBox textBoxRemarks = new TextBox();
        private Button buttonRead = new Button();
        private Button buttonAdd = new Button();
        private Button buttonChange = new Button();
        private Button buttonErase = new Button();
        private Label labelFoot = new Label();

        public Form1Design()
        {
            FormDesignSetting();
        }

        public void FormDesignSetting()
        {
            dataGridView1.TabIndex = 7;
            dataGridView1 = (DataGridView)ControlsSetting(dataGridView1, "dataGridView1", 25, 25, 730, 200);

            LabelsSetting(@"labelUri", @"URI:", 25, 235, 50, 25);
            textBoxUri.TabIndex = 0;
            textBoxUri.Text = @"https://localhost:8443/rest/read"; //@"https://localhost:7213/api/ShohinEntities";
            textBoxUri = (TextBox)ControlsSetting(textBoxUri, @"textBoxUri", 75, 235, 300, 25);

            LabelsSetting(@"labelHttpVer", @"HTTPバージョン：", 375, 235, 110, 25);
            cmbHttpVer = (ComboBox)ControlsSetting(cmbHttpVer, @"cmbHttpVer", 485, 235, 80, 25);
            LabelsSetting(@"labelSslProtocol", @"SSLプロトコル：", 575, 235, 100, 25);
            cmbSslProtocol = (ComboBox)ControlsSetting(cmbSslProtocol, @"cmbSslProtocol", 675, 235, 80, 25);

            LabelsSetting(@"labelReqBody", @"レスポンスBody:", 25, 260, 80, 25);
            textBoxReqBody.TabIndex = 1;
            textBoxReqBody = (TextBox)ControlsSetting(textBoxReqBody, @"textBoxReqBody", 105, 260, 600, 25);
            richTextBox1.TabIndex = 8;
            richTextBox1 = (RichTextBox)ControlsSetting(richTextBox1, @"richTextBox1", 25, 285, 350, 200);

            LabelsSetting(@"label1", @"ユニークID:", 400, 300, 100, 25);
            LabelsSetting(@"label2", @"商品番号:", 400, 350, 100, 25);
            LabelsSetting(@"label3", @"商品名:", 400, 400, 100, 25);
            LabelsSetting(@"label4", @"備考:", 400, 450, 50, 25);

            labelUuid.AutoSize = false;
            labelUuid.TextAlign = ContentAlignment.TopRight;
            labelUuid = (Label)ControlsSetting(labelUuid, @"labelUuid", 500, 300, 250, 25);
            textBoxShohinCode.TabIndex = 2;
            textBoxShohinCode = (TextBox)ControlsSetting(textBoxShohinCode, @"textBoxShohinCode", 600, 350, 150, 25);
            textBoxShohinName.TabIndex = 3;
            textBoxShohinName = (TextBox)ControlsSetting(textBoxShohinName, @"textBoxShohinName", 550, 400, 200, 25);
            textBoxRemarks.TabIndex = 4;
            textBoxRemarks = (TextBox)ControlsSetting(textBoxRemarks, @"textBoxRemarks", 450, 450, 300, 25);

            buttonRead.Text = @"抽出(GET)";
            buttonRead.TabIndex = 5;
            buttonRead.UseVisualStyleBackColor = true;
            buttonRead = (Button)ControlsSetting(buttonRead, @"buttonRead", 25, 490, 150, 50);

            buttonAdd.Text = @"追加(POST)";
            buttonAdd.TabIndex = 6;
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd = (Button)ControlsSetting(buttonAdd, @"buttonAdd", 220, 490, 150, 50);

            buttonChange.Text = @"更新(PUT)";
            buttonChange.TabIndex = 7;
            buttonChange.UseVisualStyleBackColor = true;
            buttonChange = (Button)ControlsSetting(buttonChange, @"buttonChange", 410, 490, 150, 50);

            buttonErase.Text = @"削除(DELETE)";
            buttonErase.TabIndex = 8;
            buttonErase.UseVisualStyleBackColor = true;
            buttonErase = (Button)ControlsSetting(buttonErase, @"buttonErase", 600, 490, 150, 50);

            labelFoot.Text = @"Copyright (c)  2021-2022  furumiya-development";
            labelFoot.AutoSize = false;
            labelFoot = (Label)ControlsSetting(labelFoot, @"labelFoot", 30, 540, 400, 25);
        }

        public void GetGridViewRowSetTextBox()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                labelUuid.Text = dataGridView1.CurrentRow.Cells["UniqueId"].Value.ToString();
                textBoxShohinCode.Text = dataGridView1.CurrentRow.Cells["ShohinCode"].Value.ToString();
                textBoxShohinName.Text = dataGridView1.CurrentRow.Cells["ShohinName"].Value.ToString();
                textBoxRemarks.Text = dataGridView1.CurrentRow.Cells["Remarks"].Value.ToString();
            }
        }

        public void DataGridSetting()
        {
            dataGridView1.Columns["UniqueId"].HeaderText = "ユニークID";
            dataGridView1.Columns["ShohinCode"].HeaderText = "商品番号";
            dataGridView1.Columns["ShohinName"].HeaderText = "商品名";
            dataGridView1.Columns["EditDate"].HeaderText = "編集日付";
            dataGridView1.Columns["EditTime"].HeaderText = "編集時刻";
            dataGridView1.Columns["Remarks"].HeaderText = "備考";
            dataGridView1.Columns["UniqueId"].Width = 230;
            dataGridView1.Columns["ShohinCode"].Width = 70;
            dataGridView1.Columns["EditDate"].Width = 80;
            dataGridView1.Columns["EditTime"].Width = 80;
            dataGridView1.Columns["Remarks"].Width = 160;
            dataGridView1.Columns["EditDate"].DefaultCellStyle.Format = "0000/00/00";
            dataGridView1.Columns["EditTime"].DefaultCellStyle.Format = "00:00:00";
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
        }

        public void TextBoxClear()
        {
            labelUuid.Text = "";
            textBoxShohinCode.Text = "";
            textBoxShohinName.Text = "";
            textBoxRemarks.Text = "";
        }

        public Dictionary<string, Label> LabelDic
        {
            get => labelDic;
            set => labelDic = value;
        }

        public DataGridView DataGridView1
        {
            get => dataGridView1;
            set => dataGridView1 = value;
        }

        public BindingSource BindingSource1
        {
            get => bindingSource1;
            set => bindingSource1 = value;
        }

        public TextBox TextBoxUri
        {
            get => textBoxUri;
            set => textBoxUri = value;
        }

        public ComboBox CmbHttpVer
        {
            get => cmbHttpVer;
            set => cmbHttpVer = value;
        }

        public ComboBox CmbSslProtocol
        {
            get => cmbSslProtocol;
            set => cmbSslProtocol = value;
        }

        public TextBox TextBoxReqBody
        {
            get => textBoxReqBody;
            set => textBoxReqBody = value;
        }

        public RichTextBox RichTextBox1
        {
            get => richTextBox1;
            set => richTextBox1 = value;
        }

        public Label LabelUniqueId
        {
            get => labelUuid;
            set => labelUuid = value;
        }

        public TextBox TextBoxShohinCode
        {
            get => textBoxShohinCode;
            set => textBoxShohinCode = value;
        }

        public TextBox TextBoxShohinName
        {
            get => textBoxShohinName;
            set => textBoxShohinName = value;
        }

        public TextBox TextBoxRemarks
        {
            get => textBoxRemarks;
            set => textBoxRemarks = value;
        }

        public Button ButtonRead
        {
            get => buttonRead;
            set => buttonRead = value;
        }

        public Button ButtonAdd
        {
            get => buttonAdd;
            set => buttonAdd = value;
        }

        public Button ButtonChange
        {
            get => buttonChange;
            set => buttonChange = value;
        }

        public Button ButtonErase
        {
            get => buttonErase;
            set => buttonErase = value;
        }

        public Label LabelFoot
        {
            get => labelFoot;
            set => labelFoot = value;
        }
    }
}