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
              bs_main.DataSource = new ds_main.dt_newsDataTable();
            LoadDatas();
          
            SubForm sf = new SubForm();
            sf.Show();
            sf.bs_sub.DataSource = ((DataTable)bs_main.DataSource).AsDataView();
        }

        #region 事件代码
        private void Download_Now_Click(object sender, EventArgs e)
        {
            int runcount = 0;
            string Dats = "";
            int indexcount = 0;
            foreach (DataRow item in ((DataTable)bs_main.DataSource).Rows)
            {
                runcount += 1;
                if (runcount == 1)
                {
                    Dats += item.Field<String>("codetype") + item.Field<String>("codevalue");
                }
                else
                {
                    Dats += "," + item.Field<String>("codetype") + item.Field<String>("codevalue");
                }

                if (runcount == 100)
                {
                    ss_mian_label.Text = "正在刷新"+ Dats;
                    DownloadSinaData(Dats);
                    this.Refresh();
                    runcount = 0;
                    Dats = "";
                }
                if (indexcount == ((DataTable)bs_main.DataSource).Rows.Count - 1)
                {
                    ss_mian_label.Text = "正在刷新" + Dats;
                    DownloadSinaData(Dats);
                    this.Refresh();
                }
                indexcount += 1;
                Application.DoEvents();
            }
            ss_mian_label.Text = "刷新完成";
        }
        private void DownloadSinaData(string Codes)
        {
            String URL = "http://hq.sinajs.cn/list=" + Codes;
            System.Net.CookieCollection cookie = new System.Net.CookieCollection();
            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie, Encoding.GetEncoding("GB2312"));
            String[] Lines = Result.Split(Environment.NewLine.ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
            foreach (var lineitem in Lines)
            {
                string[] infs = lineitem.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string codetype = infs[0].Substring(11, 2);
                string codevalue = infs[0].Substring(13, 6); ;
                DataRow[] rows= ((DataTable)bs_main.DataSource).Select("codetype='" + codetype + "' and codevalue='" + codevalue + "'");
                if (rows.Count()>0)
                {
                    rows[0].SetField<Decimal>("minprice",Convert.ToDecimal( infs[5]));
                    rows[0].SetField<Decimal>("nowprice",Convert.ToDecimal( infs[3]));
                    if (rows[0].Field<Object>("nowtime") == null)
                    {
                        rows[0].SetField<DateTime>("nowtime", Convert.ToDateTime(infs[30]+" "+ infs[31]));
                    }
                    else if (rows[0].Field<DateTime>("nowtime").ToString("yyyy-MM-dd HH:mm")!=DateTime.Now.ToString("yyyy-MM-dd HH:mm")
                      
                        
                        )
                    {

                        rows[0].SetField<decimal>("lmin10", rows[0].Field<decimal>("lmin09"));
                        rows[0].SetField<decimal>("lmin09", rows[0].Field<decimal>("lmin08"));
                        rows[0].SetField<decimal>("lmin08", rows[0].Field<decimal>("lmin07"));
                        rows[0].SetField<decimal>("lmin07", rows[0].Field<decimal>("lmin06"));
                        rows[0].SetField<decimal>("lmin06", rows[0].Field<decimal>("lmin05"));
                        rows[0].SetField<decimal>("lmin05", rows[0].Field<decimal>("lmin04"));
                        rows[0].SetField<decimal>("lmin04", rows[0].Field<decimal>("lmin03"));
                        rows[0].SetField<decimal>("lmin03", rows[0].Field<decimal>("lmin02"));
                        rows[0].SetField<decimal>("lmin02", rows[0].Field<decimal>("lmin01"));

                        rows[0].SetField<decimal>("lmin01", Convert.ToDecimal(infs[3]));

                    }

                }
                Application.DoEvents();
            }
        }
     

        private void Download_History_Click(object sender, EventArgs e)
        {

        }

        private void Download_AllCode_Click(object sender, EventArgs e)
        {
            //listsource.Rows.Clear();
            ss_mian_label.Text = "开始下载sh";
            DownloadCode("https://www.banban.cn/gupiao/list_sh.html", CodeTppe.sh);
            ss_mian_label.Text = "开始下载sz";
            DownloadCode("https://www.banban.cn/gupiao/list_sz.html", CodeTppe.sz);
            ss_mian_label.Text = "下载完成";
        }


        private void Save_Data_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        #endregion

        #region 保存Datatable
        private void LoadDatas()
        {
            if (System.IO.File.Exists(Application.StartupPath + "\\data.txt"))
            {
                ss_mian_label.Text = "正在读取数据";
                this.Refresh();
                String Saves = NetFramework.Util_File.ReadToEnd(Application.StartupPath + "\\data.txt", Encoding.GetEncoding("GB2312"));
                DataTable loads = NetFramework.Util_DataTable.DeserializeDataTableXml(Saves);
                (bs_main.DataSource) = loads;
                ss_mian_label.Text = "读取数据完毕";
                this.Refresh();
            }

        }

        private void SaveData()
        {
            ss_mian_label.Text = "正在保存数据";
            this.Refresh();
            String ToSave = NetFramework.Util_DataTable.SerializeDataTableXml(((DataTable)bs_main.DataSource));
            NetFramework.Util_File.SaveToFile(ToSave, Application.StartupPath + "\\data.txt", Encoding.GetEncoding("GB2312"));
            ss_mian_label.Text = "保存数据完毕";
            this.Refresh();
        }
        #endregion

        #region 下载网络
        private enum CodeTppe { sz, sh }
        //public DataTable listsource = new ds_main.dt_newsDataTable();
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

            Int32 cunrcont = 0;
            foreach (Match codeitem in codes)
            {
                cunrcont += 1;
                ss_mian_label.Text = "共找到" + codes.Count.ToString() + "个,当前第" + cunrcont.ToString() + "个";
                this.Refresh();
                String NameAndCode = codeitem.Value;
                String Code = NameAndCode.Substring(NameAndCode.Length - 8, 6);
                String Name = NameAndCode.Substring(1, NameAndCode.Length - 10);



                DataRow[] find = ((DataTable)bs_main.DataSource).Select("codetype='" + ct.ToString() + "' and codevalue='" + Code + "'");
                if (find.Count() == 0)
                {
                    DataRow newr = ((DataTable)bs_main.DataSource).NewRow();
                    newr.SetField<String>("codetype", ct.ToString());
                    newr.SetField<String>("codevalue", Code);
                    newr.SetField<String>("stockname", Name);
                    ((DataTable)bs_main.DataSource).Rows.Add(newr);
                }
                Application.DoEvents();

            }
        }
        #endregion

        private void LogicForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData();
        }

        private void Download_DeleteAndReDown_Click(object sender, EventArgs e)
        {
            (bs_main.DataSource) = new ds_main.dt_newsDataTable();
            Download_AllCode_Click(sender, e);
        }

        private void time_refresh_Tick(object sender, EventArgs e)
        {
            time_refresh.Enabled = false;
            Download_Now_Click(sender, e);
            time_refresh.Enabled = true;
        }

        private void Set_DayNoTrans_Click(object sender, EventArgs e)
        {

        }
    }
}
