using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace StockLib
{
    public partial class LogicForm : Form
    {
        public LogicForm()
        {
            InitializeComponent();
        }

        private void LogicForm_Load(object sender, EventArgs e)
        {

        }
        private void LoadDatas()
        {
            String Saves = NetFramework.Util_File.ReadToEnd(Application.StartupPath + "\\data.dat", Encoding.GetEncoding("GB2312"));
            string[] Lines = Saves.Split(Environment.NewLine.ToCharArray());
            ds_main.dt_newsDataTable result = new ds_main.dt_newsDataTable();
            foreach (var item in Lines)
            {
                ds_main.dt_newsRow newr = result.Newdt_newsRow();
                result.Adddt_newsRow(newr);
            }
        }

        private void Download_Now_Click(object sender, EventArgs e)
        {

        }

        private void Download_History_Click(object sender, EventArgs e)
        {

        }

        private void Download_AllCode_Click(object sender, EventArgs e)
        {
            String URL = "https://www.banban.cn/gupiao/list_sh.html";
            System.Net.CookieCollection cookie = new System.Net.CookieCollection();
            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie);
            Regex findcontent = new Regex("<div class=\"u-postcontent cz\" id=\"ctrlfscont\">((?!(</div>))[\\s\\S])+</div>", RegexOptions.IgnoreCase);
            String res= findcontent.Match(Result).Value;
            Regex findcodeandname = new Regex("<((?!(<))[\\s\\S])+<", RegexOptions.IgnoreCase);
            MatchCollection codes = findcodeandname.Matches(res);
            foreach (Match codeitem in codes)
            {
                String NameAndCode = codeitem.Value;
            }
            //改一下URL代码再跑一次

        }
    }
}
