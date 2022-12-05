using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpsRestClientWinForms.FormDesign
{
    public class FormDesign
    {
        protected Dictionary<string, Label> labelDic = new Dictionary<string, Label>();
        protected Label LabelsSetting(string name, string txt, int x, int y, int w, int h)
        {
            var label = new Label();
            label.Name = name;
            label.AutoSize = false;
            label.Text = txt;
            label.Location = new Point(x, y);
            label.Size = new Size(w, h);
            labelDic.Add(label.Name, label);
            //Controls.Add(label);

            return label;
        }

        protected Control ControlsSetting(Control ctl, string name, int x, int y, int w, int h)
        {
            ctl.Name = name;
            ctl.Location = new Point(x, y);
            ctl.Size = new Size(w, h);
            //Controls.Add(ctl);

            return ctl;
        }
    }
}