using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using HttpsRestClientWinForms.FormDesign;
using HttpsRestClientWinForms.Http;
using HttpsRestClientWinForms.Model;
using HttpsRestClientWinForms.Model.Service;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HttpsRestClientWinForms
{
    /// <summary>メインフォーム</summary>
    /// <remarks>Nuget Package : Install-Package Microsoft.Extensions.Http -Version 6.0.0</remarks>
    public partial class Form1Control : Form
    {
        private static HttpClient? httpClient;
        private SynchronizationContext synchronizationContext;
        private HttpSetting Settings = new HttpSetting();
        private ShohinService service;
        private Form1Design fDesign = new Form1Design();
        private bool authenticated = false; //認証済みフラグ
        
        public Form1Control(IHttpClientFactory factory)
        {
            InitializeComponent();
            CreateForm();
            synchronizationContext = SynchronizationContext.Current!;
            httpClient = factory.CreateClient();
            httpClient.Timeout = TimeSpan.FromMilliseconds(-1);
            service = new ShohinService(httpClient);
            //FormAuthControl f = new();
            //f.Show();
        }

        private void Form1Control_Load(object sender, EventArgs e)
        {
            fDesign.CmbSslProtocol.DataSource = Settings.SslProtocol;
            fDesign.CmbSslProtocol.DataBindings.Add(new Binding("SelectedItem", Settings, "SslProtocolVerStr"));
            fDesign.CmbHttpVer.DataSource = Settings.HttpVersion;
            fDesign.CmbHttpVer.DataBindings.Add(new Binding("SelectedItem", Settings, "HttpVerStr"));
            fDesign.CmbHttpVer.SelectedIndex = 2;
            fDesign.CmbSslProtocol.SelectedIndex = 2;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            fDesign.GetGridViewRowSetTextBox();
        }

        private async void ButtonRead_Click(object sender, EventArgs e)
        {
            var uri = fDesign.TextBoxUri.Text;
            fDesign.BindingSource1.DataSource = await service.HttpGet(uri);
            if (AuthCheck(false) == true)
            {
                await service.HttpGet(uri);
                AuthCheck(true);
            }

            if (service.LastStatusCode == HttpStatusCode.OK)
            {
                var list = JsonSerializer.Deserialize<List<ShohinEntity>>(service.LastBody)!;
                fDesign.BindingSource1.DataSource = list;
                fDesign.DataGridView1.DataSource = fDesign.BindingSource1;
                fDesign.DataGridSetting();
                fDesign.RichTextBox1.AppendText($"データを全件取得しました。{Environment.NewLine}");
            }
        }

        private async void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (PatternMatch(fDesign.TextBoxShohinCode.Text) == false)
            {
                MsgDialogOK(MSG_MESSAGE_SHOHIN_NUM_NG, MSG_TITLE_SHOHIN_NUM_NG, MessageBoxIcon.Warning);
                return;
            }

            var uri = fDesign.TextBoxUri.Text;
            string JsonStr = CreateJsonStr();
            await service.HttpPost(uri, JsonStr);
            if (AuthCheck(false) == true)
            {
                await service.HttpPost(uri, JsonStr);
                AuthCheck(true);
            }

            if (service.LastStatusCode == HttpStatusCode.Created)
                fDesign.RichTextBox1.AppendText($"データを1件追加しました。{Environment.NewLine}");
        }

        private async void ButtonChange_Click(object sender, EventArgs e)
        {
            if (fDesign.DataGridView1.Rows.Count <= 0 || fDesign.LabelUniqueId.Text == "")
            {
                MsgDialogOK(MSG_MESSAGE_GYO_NG, MSG_TITLE_GYO_NG, MessageBoxIcon.Warning);
                return;
            }

            if (PatternMatch(fDesign.TextBoxShohinCode.Text) == false)
            {
                MsgDialogOK(MSG_MESSAGE_SHOHIN_NUM_NG, MSG_TITLE_SHOHIN_NUM_NG, MessageBoxIcon.Warning);
                return;
            }

            var uri = $@"{fDesign.TextBoxUri.Text}/{fDesign.LabelUniqueId.Text}";
            string JsonStr = CreateJsonStr();
            await service.HttpPut(uri, JsonStr);
            if (AuthCheck(false) == true)
            {
                await service.HttpPut(uri, JsonStr);
                AuthCheck(true);
            }

            if (service.LastStatusCode == HttpStatusCode.OK)
                fDesign.RichTextBox1.AppendText($"選択されたレコードを1件更新しました。{Environment.NewLine}");
        }

        private async void ButtonErase_Click(object sender, EventArgs e)
        {
            if (fDesign.DataGridView1.Rows.Count <= 0 || fDesign.LabelUniqueId.Text == "")
            {
                MessageBox.Show("削除する商品行が選択がされていません", "商品IDなし", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (PatternMatch(fDesign.TextBoxShohinCode.Text) == false)
            {
                MsgDialogOK(MSG_MESSAGE_SHOHIN_NUM_NG, MSG_TITLE_SHOHIN_NUM_NG, MessageBoxIcon.Warning);
                return;
            }

            var uri = $@"{fDesign.TextBoxUri.Text}/{fDesign.LabelUniqueId.Text}";
            await service.HttpDelete(uri, @"dummy text");
            if (AuthCheck(false) == true)
            {
                await service.HttpDelete(uri, @"dummy text");
                AuthCheck(true);
            }

            if (service.LastStatusCode == HttpStatusCode.NoContent)
                fDesign.RichTextBox1.AppendText($"選択されたレコードを1件削除しました。{Environment.NewLine}");
        }

        private bool AuthCheck(bool retry)
        {
            var authRetryFlag = false;

            fDesign.TextBoxReqBody.Text = service.LastBody;
            fDesign.RichTextBox1.AppendText(service.LastHeaders.ToString() + Environment.NewLine);
            if (service.LastStatusCode == HttpStatusCode.Unauthorized)
            {
                if (retry == false)
                {
                    //ID、パスワード未設定でのUnauthorizedなので認証処理
                    var dialog = new FormAuthControl();
                    dialog.ShowDialog(); //キャンセルを押した場合リトライフラグをONしないことも必要
                    authRetryFlag = true;
                }
                else
                {
                    //リトライ(ID、パスワードで試す)でもUnauthorizedなので認証失敗
                    authenticated = false;
                }
            }
            else
            {
                if (authenticated == false) //既に認証済みか？
                    authenticated = true; //認証済みにする

                if (service.LastStatusCode != HttpStatusCode.OK &&
                        service.LastStatusCode != HttpStatusCode.Created &&
                        service.LastStatusCode != HttpStatusCode.NoContent)
                {
                    //HTTP_BAD_REQUESTを含むその他のコード
                    fDesign.RichTextBox1.AppendText($"別のステータスコードが返りました。{Environment.NewLine}");
                    MsgDialogOK(service.LastHeaders.ToString(), service.LastStatusCode.ToString(), MessageBoxIcon.Error);
                }
            }

            return authRetryFlag;
        }

        private string CreateJsonStr()
        {
            string Str = "{ \"shohinCode\":" + fDesign.TextBoxShohinCode.Text;
            Str += ", \"shohinName\": \"" + fDesign.TextBoxShohinName.Text + "\", \"note\": \"" + fDesign.TextBoxRemarks.Text + "\" }";

            return Str;
        }

        private void CreateForm()
        {
            this.Name = "Form1Control";
            this.Text = "RESTful API クライアント(HttpClient)";
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(500, 200);
            this.Size = new Size(800, 600);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Load += new EventHandler(this.Form1Control_Load!);

            Controls.Add(fDesign.DataGridView1);
            fDesign.DataGridView1.CellClick += new DataGridViewCellEventHandler(DataGridView1_CellClick!);

            Controls.Add(fDesign.TextBoxUri);
            Controls.Add(fDesign.CmbHttpVer);
            Controls.Add(fDesign.CmbSslProtocol);
            Controls.Add(fDesign.TextBoxReqBody);
            Controls.Add(fDesign.RichTextBox1);

            Controls.Add(fDesign.LabelUniqueId);
            Controls.Add(fDesign.TextBoxShohinCode);
            Controls.Add(fDesign.TextBoxShohinName);
            Controls.Add(fDesign.TextBoxRemarks);

            Controls.Add(fDesign.ButtonRead);
            fDesign.ButtonRead.Click += new EventHandler(ButtonRead_Click!);
            Controls.Add(fDesign.ButtonAdd);
            fDesign.ButtonAdd.Click += new EventHandler(ButtonAdd_Click!);
            Controls.Add(fDesign.ButtonChange);
            fDesign.ButtonChange.Click += new EventHandler(ButtonChange_Click!);
            Controls.Add(fDesign.ButtonErase);
            fDesign.ButtonErase.Click += new EventHandler(ButtonErase_Click!);

            Controls.Add(fDesign.LabelFoot);

            foreach (var label in fDesign.LabelDic)
                Controls.Add(label.Value);
        }

        private const string MSG_TITLE_SHOHIN_NUM_NG = "メッセージ";
        private const string MSG_MESSAGE_SHOHIN_NUM_NG = "商品番号は半角数値の0～9999でなければなりません。";
        private const string MSG_TITLE_GYO_NG = "商品IDなし";
        private const string MSG_MESSAGE_GYO_NG = "更新する商品行が選択できていません。";
        private const string SU_0_TO_9999 = "^[0-9]{1,4}$";

        private bool PatternMatch(string numid)
        {
            return Regex.IsMatch(numid, SU_0_TO_9999);
        }

        private void MsgDialogOK(string message, string title, MessageBoxIcon type)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, type);
        }
    }
}