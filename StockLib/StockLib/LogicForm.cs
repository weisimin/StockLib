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
            LoadDatas();
            bs_main.DataSource = listsource;
            SubForm sf = new SubForm();
            sf.Show();
            sf.bs_main.DataSource = listsource;
        }

        #region 事件代码
        private void Download_Now_Click(object sender, EventArgs e)
        {

        }

        private void Download_History_Click(object sender, EventArgs e)
        {

        }

        private void Download_AllCode_Click(object sender, EventArgs e)
        {
            listsource.Rows.Clear();

            DownloadCode("https://www.banban.cn/gupiao/list_sh.html", CodeTppe.sh);
            DownloadCode("https://www.banban.cn/gupiao/list_sz.html", CodeTppe.sz);

        }


        private void Save_Data_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region 保存Datatable
        private void LoadDatas()
        {
            if (System.IO.File.Exists(Application.StartupPath + "\\data.txt"))
            {
                String Saves = NetFramework.Util_File.ReadToEnd(Application.StartupPath + "\\data.txt", Encoding.GetEncoding("GB2312"));
                DataTable loads = NetFramework.Util_DataTable.DeserializerTable(Saves);
                listsource = loads;
            }

        }

        private void SaveData()
        {
            String ToSave = NetFramework.Util_DataTable.SerializeDataTable(listsource);
            NetFramework.Util_File.SaveToFile(ToSave, Application.StartupPath + "\\data.txt", Encoding.GetEncoding("GB2312"));
        }
        #endregion

        #region 下载网络
        private enum CodeTppe { sz, sh }
        public DataTable listsource = new DataTable();
        private void DownloadCode(string URL, CodeTppe ct)
        {
            //String URL = "https://www.banban.cn/gupiao/list_sh.html";
            //    URL = "https://www.banban.cn/gupiao/list_sz.html";
            System.Net.CookieCollection cookie = new System.Net.CookieCollection();
            Regex findcontent = new Regex("<div class=\"u-postcontent cz\" id=\"ctrlfscont\">((?!(</div>))[\\s\\S])+</div>", RegexOptions.IgnoreCase);
            Regex findcodeandname = new Regex(">((?!<)(?!\n)[\\s\\S])+<", RegexOptions.IgnoreCase);

            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie);

            String res = findcontent.Match(Result).Value;

            MatchCollection codes = findcodeandname.Matches(res);
            foreach (Match codeitem in codes)
            {
                String NameAndCode = codeitem.Value;
                String Code = NameAndCode.Substring(NameAndCode.Length - 8, 6);
                String Name = NameAndCode.Substring(1, NameAndCode.Length - 10);

                DataRow newr = listsource.NewRow();
                newr.SetField<String>("codetype", ct.ToString()) ;
                newr.SetField<String>("codevalue", Code) ;
                newr.SetField<String>("stockname", Name) ;
                listsource.Rows.Add(newr);
            }
        }
        #endregion

        private void LogicForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData();
        }
    }
}
