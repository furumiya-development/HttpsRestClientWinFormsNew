using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpsRestClientWinForms.FormDesign
{
    public class FormAuthDesign : FormDesign
    {
        private TextBox textBoxUserName = new TextBox();
        private TextBox textBoxPassword = new TextBox();
        private Button buttonAuth = new Button();
        private Button buttonCancel = new Button();

        public FormAuthDesign()
        {
            FormDesignSetting();
        }

        public void FormDesignSetting()
        {
            LabelsSetting(@"labelMessage", @"認証が必要です。ユーザー名とパスワードを入力して下さい。", 25, 25, 350, 25);
            LabelsSetting(@"labelUserName", @"ユーザー:", 25, 50, 80, 25);
            LabelsSetting(@"labelPassword", @"パスワード:", 25, 100, 80, 25);

            textBoxUserName.TabIndex = 0;
            textBoxUserName = (TextBox)ControlsSetting(textBoxUserName, @"textBoxUserName", 120, 50, 250, 25);

            textBoxPassword.TabIndex = 1;
            textBoxPassword.PasswordChar = '*';
            textBoxPassword = (TextBox)ControlsSetting(textBoxPassword, @"textBoxPassword", 120, 100, 250, 25);

            buttonAuth.Text = @"認証";
            buttonAuth.TabIndex = 2;
            buttonAuth.UseVisualStyleBackColor = true;
            buttonAuth = (Button)ControlsSetting(buttonAuth, @"buttonAuth", 25, 150, 150, 50);

            buttonCancel.Text = @"キャンセル";
            buttonCancel.TabIndex = 3;
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel = (Button)ControlsSetting(buttonCancel, @"buttonCancel", 250, 150, 150, 50);
        }

        public Dictionary<string, Label> LabelDic
        {
            get => labelDic;
            set => labelDic = value;
        }

        public TextBox TextBoxUserName
        {
            get => textBoxUserName;
            set => textBoxUserName = value;
        }

        public TextBox TextBoxPassword
        {
            get => textBoxPassword;
            set => textBoxPassword = value;
        }

        public Button ButtonAuth
        {
            get => buttonAuth;
            set => buttonAuth = value;
        }

        public Button ButtonCancel
        {
            get => buttonCancel;
            set => buttonCancel = value;
        }
    }
}