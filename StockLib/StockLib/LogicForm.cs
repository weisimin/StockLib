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

        DataTable listsource = new ds_main.dt_newsDataTable();
        DataTable historysource = new ds_main.dt_historyDataTable();
        SubForm sf = new SubForm();
        private void LogicForm_Load(object sender, EventArgs e)
        {
            //gv_list.AutoGenerateColumns = true;


            LoadDatas();

            bs_main.DataSource = listsource.AsDataView();
            bs_main.Sort = "max20growday desc";
            bs_sub.DataSource = listsource.AsDataView();
            bs_sub.Filter = "issuppose='true'";
            bs_sub.Sort = "noticetime DESC";




            sf.Show();
            sf.bs_sub.DataSource = listsource.AsDataView();

            sf.bs_history.DataSource = historysource.AsDataView();
            sf.bs_history.Sort = "transday DESC";

            ReloadFilter();
        }

        #region 事件代码
        DateTime? LastTime = null;
        private void Download_Now_Click(object sender, EventArgs e)
        {
            int runcount = 0;
            string Dats = "";
            int indexcount = 0;
            List<DownLoadSinaResult> rels = new List<DownLoadSinaResult>();
            foreach (DataRow item in listsource.Rows)
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

                if (runcount == 200)
                {
                    ss_mian_label.Text = "正在刷新" + Dats;
                    rels.Add(DownloadSinaData(Dats));
                    this.Refresh();
                    runcount = 0;
                    Dats = "";
                }
                if (indexcount == listsource.Rows.Count - 1)
                {
                    ss_mian_label.Text = "正在刷新" + Dats;
                    rels.Add(DownloadSinaData(Dats));
                    this.Refresh();
                }
                indexcount += 1;
                Application.DoEvents();
            }
            #region 发送和保存
            if (rels.Count > 0
                && rels.First().LastTime.Value.ToString("HH:mm") == "15:00"
                && (LastTime == null || LastTime.Value.ToString("yyyy-MM-dd HH:mm") != rels.First().LastTime.Value.ToString("yyyy-MM-dd HH:mm")))
            {
                SaveData();
                LastTime = rels.First().LastTime;
            }
            bool SendWechat = false;
            this.Invoke(new Action(() =>
            {
                SendWechat = con_cb_wechat.Checked;
            }));
            String SendMsg = "";
            Int32 SendCount = 0;
            foreach (var relsitem in rels)
            {
                if (relsitem.SendText.Count != 0 && SendWechat == true)
                {

                    foreach (WetchatJob stritem in relsitem.SendText)
                    {
                        SendCount += 1;
                        SendMsg += stritem.MsgContent + Environment.NewLine;
                        if (SendCount >= 50)
                        {
                            msg.SendTextMsg(SendMsg);
                            SendCount = 0;
                            SendMsg = "";
                        }
                    }

                }
            }//循环叠加，有足够的数量就发出
            if (SendMsg != "")
            {
                msg.SendTextMsg(SendMsg); SendCount = 0;
                SendMsg = "";
            }//剩下数量不够的发送

            #endregion
            ss_mian_label.Text = "刷新完成";
        }



        public decimal? grow159915 = 0;
        public decimal? grow399001 = 0;
        private DownLoadSinaResult DownloadSinaData(string Codes)
        {
            String URL = "http://hq.sinajs.cn/list=" + Codes;
            System.Net.CookieCollection cookie = new System.Net.CookieCollection();
            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie, Encoding.GetEncoding("GB2312"));
            String[] Lines = Result.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);


            List<WetchatJob> SendText = new List<WetchatJob>();

            DateTime lasttime = DateTime.Now;
            DateTime? rowtime = null;
            foreach (var lineitem in Lines)
            {
                string[] infs = lineitem.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string codetype = infs[0].Substring(11, 2);
                string codevalue = infs[0].Substring(13, 6);

                DataRow[] rows = listsource.Select("codetype='" + codetype + "' and codevalue='" + codevalue + "'");
                string codet = rows[0].Field<String>("codevalue");
                if (rows.Count() > 0)
                {
                    rows[0].SetField<Decimal?>("minprice", Convert.ToDecimal(infs[5]));
                    rows[0].SetField<Decimal?>("nowprice", Convert.ToDecimal(infs[3]));
                    rows[0].SetField<Decimal?>("ytdprice", Convert.ToDecimal(infs[2]));
                    decimal? highprice = rows[0].Field<decimal?>("highprice");
                    decimal? breakratio = rows[0].Field<Decimal?>("breakratio");
                    if (Convert.ToDecimal(infs[4]) > highprice && highprice != 0)
                    {
                        breakratio += 1;
                        rows[0].SetField<Decimal?>("breakratio", breakratio);
                    }

                    rows[0].SetField<Decimal?>("highprice", Convert.ToDecimal(infs[4]));

                    rows[0].SetField<Decimal?>("volumn", Convert.ToDecimal(infs[8]));
                    decimal? updown = CaculateGrowUp(rows[0].Field<Decimal>("nowprice"), rows[0].Field<Decimal>("ytdprice"));
                    rows[0].SetField<Decimal?>("updown", updown);
                    decimal? growtoday = CaculateGrowUp(rows[0].Field<Decimal>("nowprice"), rows[0].Field<Decimal>("minprice"));
                    decimal? openprice = Convert.ToDecimal(infs[1]);
                    rows[0].SetField<Decimal?>("openprice", openprice);
                    if (codevalue == "159915")
                    {
                        grow159915 = updown;
                        sl_diff.Text = "势头:" + ((grow159915 > 0 ? 1 : -1) * Math.Abs((grow159915 - grow399001).Value)).ToString("0.00%");

                    }
                    if (codevalue == "399001")
                    {
                        grow399001 = updown;
                        sl_diff.Text = "势头:" + ((grow159915 > 0 ? 1 : -1) * Math.Abs((grow159915 - grow399001).Value)).ToString("0.00%");
                    }
                    rows[0].SetField<Decimal?>("growtoday", growtoday);

                    lasttime = Convert.ToDateTime(infs[30] + " " + infs[31]);


                    decimal? nowprice = rows[0].Field<decimal?>("nowprice");
                    decimal? ytdprice = rows[0].Field<decimal?>("ytdprice");
                    decimal? volumn = rows[0].Field<decimal?>("volumn");

                    decimal? minprice = rows[0].Field<decimal?>("minprice");
                    highprice = rows[0].Field<decimal?>("highprice");
                    decimal? lday01_min = rows[0].Field<decimal?>("lday01_min");
                    decimal? lday01_end = rows[0].Field<decimal?>("lday01_end");
                    decimal? lday02_min = rows[0].Field<decimal?>("lday02_min");
                    decimal? lday02_end = rows[0].Field<decimal?>("lday02_end");
                    decimal? lday03_min = rows[0].Field<decimal?>("lday03_min");
                    decimal? lday03_end = rows[0].Field<decimal?>("lday03_end");
                    decimal? lday04_min = rows[0].Field<decimal?>("lday04_min");
                    decimal? lday04_end = rows[0].Field<decimal?>("lday04_end");
                    decimal? lday05_min = rows[0].Field<decimal?>("lday05_min");
                    decimal? lday05_end = rows[0].Field<decimal?>("lday05_end");
                    decimal? lday06_min = rows[0].Field<decimal?>("lday06_min");
                    decimal? lday06_end = rows[0].Field<decimal?>("lday06_end");
                    decimal? lday07_min = rows[0].Field<decimal?>("lday07_min");
                    decimal? lday07_end = rows[0].Field<decimal?>("lday07_end");
                    decimal? lday08_min = rows[0].Field<decimal?>("lday08_min");
                    decimal? lday08_end = rows[0].Field<decimal?>("lday08_end");
                    decimal? lday09_min = rows[0].Field<decimal?>("lday09_min");
                    decimal? lday09_end = rows[0].Field<decimal?>("lday09_end");
                    decimal? lday10_min = rows[0].Field<decimal?>("lday10_min");
                    decimal? lday10_end = rows[0].Field<decimal?>("lday10_end");
                    decimal? lday11_min = rows[0].Field<decimal?>("lday11_min");
                    decimal? lday11_end = rows[0].Field<decimal?>("lday11_end");
                    decimal? lday12_min = rows[0].Field<decimal?>("lday12_min");
                    decimal? lday12_end = rows[0].Field<decimal?>("lday12_end");
                    decimal? lday13_min = rows[0].Field<decimal?>("lday13_min");
                    decimal? lday13_end = rows[0].Field<decimal?>("lday13_end");
                    decimal? lday14_min = rows[0].Field<decimal?>("lday14_min");
                    decimal? lday14_end = rows[0].Field<decimal?>("lday14_end");
                    decimal? lday15_min = rows[0].Field<decimal?>("lday15_min");
                    decimal? lday15_end = rows[0].Field<decimal?>("lday15_end");
                    decimal? lday16_min = rows[0].Field<decimal?>("lday16_min");
                    decimal? lday16_end = rows[0].Field<decimal?>("lday16_end");
                    decimal? lday17_min = rows[0].Field<decimal?>("lday17_min");
                    decimal? lday17_end = rows[0].Field<decimal?>("lday17_end");
                    decimal? lday18_min = rows[0].Field<decimal?>("lday18_min");
                    decimal? lday18_end = rows[0].Field<decimal?>("lday18_end");
                    decimal? lday19_min = rows[0].Field<decimal?>("lday19_min");
                    decimal? lday19_end = rows[0].Field<decimal?>("lday19_end");
                    decimal? lday20_min = rows[0].Field<decimal?>("lday20_min");
                    decimal? lday20_end = rows[0].Field<decimal?>("lday20_end");

                    decimal? lday01_high = rows[0].Field<decimal?>("lday01_high");
                    decimal? lday02_high = rows[0].Field<decimal?>("lday02_high");
                    decimal? lday03_high = rows[0].Field<decimal?>("lday03_high");
                    decimal? lday04_high = rows[0].Field<decimal?>("lday04_high");
                    decimal? lday05_high = rows[0].Field<decimal?>("lday05_high");
                    decimal? lday06_high = rows[0].Field<decimal?>("lday06_high");
                    decimal? lday07_high = rows[0].Field<decimal?>("lday07_high");
                    decimal? lday08_high = rows[0].Field<decimal?>("lday08_high");
                    decimal? lday09_high = rows[0].Field<decimal?>("lday09_high");
                    decimal? lday10_high = rows[0].Field<decimal?>("lday10_high");
                    decimal? lday11_high = rows[0].Field<decimal?>("lday11_high");
                    decimal? lday12_high = rows[0].Field<decimal?>("lday12_high");
                    decimal? lday13_high = rows[0].Field<decimal?>("lday13_high");
                    decimal? lday14_high = rows[0].Field<decimal?>("lday14_high");
                    decimal? lday15_high = rows[0].Field<decimal?>("lday15_high");
                    decimal? lday16_high = rows[0].Field<decimal?>("lday16_high");
                    decimal? lday17_high = rows[0].Field<decimal?>("lday17_high");
                    decimal? lday18_high = rows[0].Field<decimal?>("lday18_high");
                    decimal? lday19_high = rows[0].Field<decimal?>("lday19_high");
                    decimal? lday20_high = rows[0].Field<decimal?>("lday20_high");

                    decimal? lday01_vol = rows[0].Field<decimal?>("lday01_vol");
                    decimal? lday02_vol = rows[0].Field<decimal?>("lday02_vol");
                    decimal? lday03_vol = rows[0].Field<decimal?>("lday03_vol");
                    decimal? lday04_vol = rows[0].Field<decimal?>("lday04_vol");
                    decimal? lday05_vol = rows[0].Field<decimal?>("lday05_vol");
                    decimal? lday06_vol = rows[0].Field<decimal?>("lday06_vol");
                    decimal? lday07_vol = rows[0].Field<decimal?>("lday07_vol");
                    decimal? lday08_vol = rows[0].Field<decimal?>("lday08_vol");
                    decimal? lday09_vol = rows[0].Field<decimal?>("lday09_vol");
                    decimal? lday10_vol = rows[0].Field<decimal?>("lday10_vol");
                    decimal? lday11_vol = rows[0].Field<decimal?>("lday11_vol");
                    decimal? lday12_vol = rows[0].Field<decimal?>("lday12_vol");
                    decimal? lday13_vol = rows[0].Field<decimal?>("lday13_vol");
                    decimal? lday14_vol = rows[0].Field<decimal?>("lday14_vol");
                    decimal? lday15_vol = rows[0].Field<decimal?>("lday15_vol");
                    decimal? lday16_vol = rows[0].Field<decimal?>("lday16_vol");
                    decimal? lday17_vol = rows[0].Field<decimal?>("lday17_vol");
                    decimal? lday18_vol = rows[0].Field<decimal?>("lday18_vol");
                    decimal? lday19_vol = rows[0].Field<decimal?>("lday19_vol");
                    decimal? lday20_vol = rows[0].Field<decimal?>("lday20_vol");


                    rowtime = rows[0].Field<DateTime?>("nowtime");
                    ss_mian_label.Text = "正在更新" + rows[0].Field<string>("stockname");
                    #region 实时更新
                    if (rows[0].Field<string>("supposename") == null)
                    {
                        rows[0].SetField<string>("supposename", "");
                    }


                    int befpos = gv_list.FirstDisplayedScrollingRowIndex;

                    //SetDayGrowDeltaMaDif(rows[0]);
                    // SetDayGrowDoubleSuper(rows[0]);

                    YTDSuperAndNow(rows[0]);

                    #region Better is  better
                    /*
                        if (minprice >= lday01_min && lday01_min >= lday02_min && lday02_min >= lday03_min
                            && highprice >= lday01_high && lday01_high >= lday02_high && lday02_high >= lday03_high
                            && (lday03_high <= lday04_high || lday03_min <= lday04_min)
                            )
                        {


                            rows[0].SetField<decimal?>("max20growday", growtoday);
                        }

                    */
                    #endregion

                    Decimal? logprice = rows[0].Field<decimal?>("logprice");

                    if (nowprice > logprice)
                    {
                        rows[0].SetField<decimal?>("logprice", nowprice);
                        rows[0].SetField<decimal?>("logbreakqty", rows[0].Field<decimal?>("logbreakqty") + 1);
                    }
                    try
                    {
                        gv_list.FirstDisplayedScrollingRowIndex = befpos;
                    }
                    catch (Exception)
                    {


                    }



                    decimal? max20growday = rows[0].Field<decimal?>("max20growday");
                    decimal? max20growday_avg15 = rows[0].Field<decimal?>("max20growday_avg15");
                    decimal? jump = (nowprice == 0 ? 0 : (highprice / nowprice - 1));
                    decimal? jumpytd = (lday01_end == 0 ? 0 : (lday01_high / lday01_end - 1));
                    decimal? growytd = (lday01_min == 0 ? 0 : (lday01_end / lday01_min - 1));
                    decimal? lmin01 = rows[0].Field<decimal?>("lmin01");

                    rows[0].SetField<decimal?>("Max20Down", lday01_vol == 0 ? 0 : (volumn / lday01_vol));



                    string strstrong = rows[0].Field<string>("strong");
                    if (jump >= growytd && strstrong.Contains("弱") == false)
                    {
                        strstrong = "弱" + strstrong;
                    }
                    if (growtoday >= jumpytd && strstrong.Contains("强") == false)
                    {
                        strstrong = "强" + strstrong;
                    }

                    //string strytdstrong = "";
                    //if (lday01_min < lday02_min)
                    //{
                    //    strytdstrong += "弱";
                    //}
                    //if (lday01_high > lday02_high)
                    //{
                    //    strytdstrong += "强";
                    //}

                    //max20growday = lday01_vol == 0 ? 0 : (((growtoday - jump) + (updown - jump)) / 2 * volumn / lday01_vol);

                    decimal? minutegrow = (lmin01 == 0 ? 0 : (nowprice / lmin01 - 1));

                    /* max20growday = (ytdprice == 0 ? 0 : (openprice / ytdprice - 1))
                         + (growtoday)
                         + (lmin01 == 0 ? 0 : (nowprice / lmin01 - 1))
                         - 3 * (nowprice == 0 ? 0 : (highprice / nowprice - 1));
                    

                    int befpos = gv_list.FirstDisplayedScrollingRowIndex;

                    rows[0].SetField<decimal?>("max20growday", max20growday);

                    gv_list.FirstDisplayedScrollingRowIndex = befpos;*/

                    //SetMinGrow(rows[0]);

                    //decimal? max10growmin = rows[0].Field<decimal?>("max10growmin");
                    rows[0].SetField<string>("strong", strstrong);

                    DateTime? breakday1 = rows[0].Field<DateTime?>("breakday1");
                    if (
                    //lday01_end != 0 && lday02_end != 0 && nowprice != 0 &&
                    //(
                    // lday01_high / lday01_end <= 1.0075M
                    // && lday02_high / lday02_end <= 1.0075M
                    //  && highprice / nowprice <= 1.0075M
                    //  && growtoday * 100 >= 3.5M
                    // && rows[0].Field<bool?>("issuppose") == false
                    //)

                    /*( strytdstrong==""|| strytdstrong.Contains("弱"))
                     && strstrong.Contains("强") && updown * 100 >= 3.0M&&growtoday*100>=3.0M

                     )*/

                    /* max20growday*100>3.5M&& max10growmin*100 >= 3.5M*/

                    //lday01_vol != 0 && max20growday * 100 >= 4.0M && volumn / lday01_vol >= 1.0M



                    //(breakday1 == rowtime.Value.Date)
                    //&& 
                    (max20growday*100 >= 5M)
                    && rows[0].Field<bool?>("issuppose") == false
                        )

                    {
                        rows[0].SetField<bool?>("issuppose", true);
                        rows[0].SetField<DateTime?>("noticetime", DateTime.Now);
                        if (System.IO.Directory.Exists(Application.StartupPath + "\\Download") == false)
                        {
                            System.IO.Directory.CreateDirectory(Application.StartupPath + "\\Download");
                        }
                        //下载图片

                        //  String SaveFile = Application.StartupPath + "\\Download\\" + rows[0].Field<String>("codetype") + rows[0].Field<String>("codevalue") + ".gif";
                        // NetFramework.Util_WEB.DownloadFile("http://image.sinajs.cn/newchart/daily/n/" + rows[0].Field<String>("codetype") + rows[0].Field<String>("codevalue") + ".gif"
                        //   , SaveFile
                        // );

                        //上传临时素材

                        //  String ImageResult = msg.UploadTempJpg(SaveFile);



                        SendText.Add(new WetchatJob(
                           "image", "<a href='http://image.sinajs.cn/newchart/min/n/" + rows[0].Field<String>("codetype") + rows[0].Field<String>("codevalue") + ".gif'>"
                            + rows[0].Field<String>("stockname")
                            + "[" + rows[0].Field<String>("codevalue") + "] "
                            + rows[0].Field<decimal?>("updown").Value.ToString("0.00%") + "</a>" + Environment.NewLine
                            + "<a href='http://image.sinajs.cn/newchart/daily/n/" + rows[0].Field<String>("codetype") + rows[0].Field<String>("codevalue") + ".gif' >日K线</a>"
                            ));


                    }
                    #endregion

                    #region 阶段更新/分钟
                    if (rowtime == null)
                    {
                        rows[0].SetField<DateTime>("nowtime", Convert.ToDateTime(infs[30] + " " + infs[31]));
                    }
                    else if (rowtime.Value.ToString("yyyy-MM-dd HH:mm") != lasttime.ToString("yyyy-MM-dd HH:mm"))
                    {

                        rows[0].SetField<decimal?>("lmin10", rows[0].Field<decimal?>("lmin09"));
                        rows[0].SetField<decimal?>("lmin09", rows[0].Field<decimal?>("lmin08"));
                        rows[0].SetField<decimal?>("lmin08", rows[0].Field<decimal?>("lmin07"));
                        rows[0].SetField<decimal?>("lmin07", rows[0].Field<decimal?>("lmin06"));
                        rows[0].SetField<decimal?>("lmin06", rows[0].Field<decimal?>("lmin05"));
                        rows[0].SetField<decimal?>("lmin05", rows[0].Field<decimal?>("lmin04"));
                        rows[0].SetField<decimal?>("lmin04", rows[0].Field<decimal?>("lmin03"));
                        rows[0].SetField<decimal?>("lmin03", rows[0].Field<decimal?>("lmin02"));
                        rows[0].SetField<decimal?>("lmin02", rows[0].Field<decimal?>("lmin01"));
                        rows[0].SetField<decimal?>("lmin01", Convert.ToDecimal(infs[3]));




                        //decimal? max20growday = rows[0].Field<decimal?>("max20growday");

                        if (max20growday == null)
                        {
                            max20growday = 0;
                            rows[0].SetField<decimal?>("max20growday", max20growday);
                        }
                        //if (max10growmin == null)
                        //{
                        //    max10growmin = 0;
                        //    rows[0].SetField<decimal?>("max10growmin", max10growmin);
                        //}


                        decimal? max20down = rows[0].Field<decimal?>("Max20Down");
                        if (max20down == null)
                        {
                            max20down = 0;
                            rows[0].SetField<decimal?>("max20down", max20down);
                        }

                        if (lasttime.ToString("HH:mm") == "15:00")
                        {

                            rows[0].SetField<decimal?>("lday20_min", rows[0].Field<decimal?>("lday19_min"));
                            rows[0].SetField<decimal?>("lday19_min", rows[0].Field<decimal?>("lday18_min"));
                            rows[0].SetField<decimal?>("lday18_min", rows[0].Field<decimal?>("lday17_min"));
                            rows[0].SetField<decimal?>("lday17_min", rows[0].Field<decimal?>("lday16_min"));
                            rows[0].SetField<decimal?>("lday16_min", rows[0].Field<decimal?>("lday15_min"));
                            rows[0].SetField<decimal?>("lday15_min", rows[0].Field<decimal?>("lday14_min"));
                            rows[0].SetField<decimal?>("lday14_min", rows[0].Field<decimal?>("lday13_min"));
                            rows[0].SetField<decimal?>("lday13_min", rows[0].Field<decimal?>("lday12_min"));
                            rows[0].SetField<decimal?>("lday12_min", rows[0].Field<decimal?>("lday11_min"));
                            rows[0].SetField<decimal?>("lday11_min", rows[0].Field<decimal?>("lday10_min"));
                            rows[0].SetField<decimal?>("lday10_min", rows[0].Field<decimal?>("lday09_min"));
                            rows[0].SetField<decimal?>("lday09_min", rows[0].Field<decimal?>("lday08_min"));
                            rows[0].SetField<decimal?>("lday08_min", rows[0].Field<decimal?>("lday07_min"));
                            rows[0].SetField<decimal?>("lday07_min", rows[0].Field<decimal?>("lday06_min"));
                            rows[0].SetField<decimal?>("lday06_min", rows[0].Field<decimal?>("lday05_min"));
                            rows[0].SetField<decimal?>("lday05_min", rows[0].Field<decimal?>("lday04_min"));
                            rows[0].SetField<decimal?>("lday04_min", rows[0].Field<decimal?>("lday03_min"));
                            rows[0].SetField<decimal?>("lday03_min", rows[0].Field<decimal?>("lday02_min"));
                            rows[0].SetField<decimal?>("lday02_min", rows[0].Field<decimal?>("lday01_min"));
                            rows[0].SetField<decimal?>("lday01_min", rows[0].Field<decimal?>("minprice"));

                            rows[0].SetField<decimal?>("lday20_end", rows[0].Field<decimal?>("lday19_end"));
                            rows[0].SetField<decimal?>("lday19_end", rows[0].Field<decimal?>("lday18_end"));
                            rows[0].SetField<decimal?>("lday18_end", rows[0].Field<decimal?>("lday17_end"));
                            rows[0].SetField<decimal?>("lday17_end", rows[0].Field<decimal?>("lday16_end"));
                            rows[0].SetField<decimal?>("lday16_end", rows[0].Field<decimal?>("lday15_end"));
                            rows[0].SetField<decimal?>("lday15_end", rows[0].Field<decimal?>("lday14_end"));
                            rows[0].SetField<decimal?>("lday14_end", rows[0].Field<decimal?>("lday13_end"));
                            rows[0].SetField<decimal?>("lday13_end", rows[0].Field<decimal?>("lday12_end"));
                            rows[0].SetField<decimal?>("lday12_end", rows[0].Field<decimal?>("lday11_end"));
                            rows[0].SetField<decimal?>("lday11_end", rows[0].Field<decimal?>("lday10_end"));
                            rows[0].SetField<decimal?>("lday10_end", rows[0].Field<decimal?>("lday09_end"));
                            rows[0].SetField<decimal?>("lday09_end", rows[0].Field<decimal?>("lday08_end"));
                            rows[0].SetField<decimal?>("lday08_end", rows[0].Field<decimal?>("lday07_end"));
                            rows[0].SetField<decimal?>("lday07_end", rows[0].Field<decimal?>("lday06_end"));
                            rows[0].SetField<decimal?>("lday06_end", rows[0].Field<decimal?>("lday05_end"));
                            rows[0].SetField<decimal?>("lday05_end", rows[0].Field<decimal?>("lday04_end"));
                            rows[0].SetField<decimal?>("lday04_end", rows[0].Field<decimal?>("lday03_end"));
                            rows[0].SetField<decimal?>("lday03_end", rows[0].Field<decimal?>("lday02_end"));
                            rows[0].SetField<decimal?>("lday02_end", rows[0].Field<decimal?>("lday01_end"));
                            rows[0].SetField<decimal?>("lday01_end", rows[0].Field<decimal?>("nowprice"));

                            rows[0].SetField<decimal?>("lday20_high", rows[0].Field<decimal?>("lday19_high"));
                            rows[0].SetField<decimal?>("lday19_high", rows[0].Field<decimal?>("lday18_high"));
                            rows[0].SetField<decimal?>("lday18_high", rows[0].Field<decimal?>("lday17_high"));
                            rows[0].SetField<decimal?>("lday17_high", rows[0].Field<decimal?>("lday16_high"));
                            rows[0].SetField<decimal?>("lday16_high", rows[0].Field<decimal?>("lday15_high"));
                            rows[0].SetField<decimal?>("lday15_high", rows[0].Field<decimal?>("lday14_high"));
                            rows[0].SetField<decimal?>("lday14_high", rows[0].Field<decimal?>("lday13_high"));
                            rows[0].SetField<decimal?>("lday13_high", rows[0].Field<decimal?>("lday12_high"));
                            rows[0].SetField<decimal?>("lday12_high", rows[0].Field<decimal?>("lday11_high"));
                            rows[0].SetField<decimal?>("lday11_high", rows[0].Field<decimal?>("lday10_high"));
                            rows[0].SetField<decimal?>("lday10_high", rows[0].Field<decimal?>("lday09_high"));
                            rows[0].SetField<decimal?>("lday09_high", rows[0].Field<decimal?>("lday08_high"));
                            rows[0].SetField<decimal?>("lday08_high", rows[0].Field<decimal?>("lday07_high"));
                            rows[0].SetField<decimal?>("lday07_high", rows[0].Field<decimal?>("lday06_high"));
                            rows[0].SetField<decimal?>("lday06_high", rows[0].Field<decimal?>("lday05_high"));
                            rows[0].SetField<decimal?>("lday05_high", rows[0].Field<decimal?>("lday04_high"));
                            rows[0].SetField<decimal?>("lday04_high", rows[0].Field<decimal?>("lday03_high"));
                            rows[0].SetField<decimal?>("lday03_high", rows[0].Field<decimal?>("lday02_high"));
                            rows[0].SetField<decimal?>("lday02_high", rows[0].Field<decimal?>("lday01_high"));
                            rows[0].SetField<decimal?>("lday01_high", rows[0].Field<decimal?>("highprice"));

                            rows[0].SetField<decimal?>("lday20_vol", rows[0].Field<decimal?>("lday19_vol"));
                            rows[0].SetField<decimal?>("lday19_vol", rows[0].Field<decimal?>("lday18_vol"));
                            rows[0].SetField<decimal?>("lday18_vol", rows[0].Field<decimal?>("lday17_vol"));
                            rows[0].SetField<decimal?>("lday17_vol", rows[0].Field<decimal?>("lday16_vol"));
                            rows[0].SetField<decimal?>("lday16_vol", rows[0].Field<decimal?>("lday15_vol"));
                            rows[0].SetField<decimal?>("lday15_vol", rows[0].Field<decimal?>("lday14_vol"));
                            rows[0].SetField<decimal?>("lday14_vol", rows[0].Field<decimal?>("lday13_vol"));
                            rows[0].SetField<decimal?>("lday13_vol", rows[0].Field<decimal?>("lday12_vol"));
                            rows[0].SetField<decimal?>("lday12_vol", rows[0].Field<decimal?>("lday11_vol"));
                            rows[0].SetField<decimal?>("lday11_vol", rows[0].Field<decimal?>("lday10_vol"));
                            rows[0].SetField<decimal?>("lday10_vol", rows[0].Field<decimal?>("lday09_vol"));
                            rows[0].SetField<decimal?>("lday09_vol", rows[0].Field<decimal?>("lday08_vol"));
                            rows[0].SetField<decimal?>("lday08_vol", rows[0].Field<decimal?>("lday07_vol"));
                            rows[0].SetField<decimal?>("lday07_vol", rows[0].Field<decimal?>("lday06_vol"));
                            rows[0].SetField<decimal?>("lday06_vol", rows[0].Field<decimal?>("lday05_vol"));
                            rows[0].SetField<decimal?>("lday05_vol", rows[0].Field<decimal?>("lday04_vol"));
                            rows[0].SetField<decimal?>("lday04_vol", rows[0].Field<decimal?>("lday03_vol"));
                            rows[0].SetField<decimal?>("lday03_vol", rows[0].Field<decimal?>("lday02_vol"));
                            rows[0].SetField<decimal?>("lday02_vol", rows[0].Field<decimal?>("lday01_vol"));
                            rows[0].SetField<decimal?>("lday01_vol", rows[0].Field<decimal?>("volumn"));


                            rows[0].SetField<decimal?>("lday20_open", rows[0].Field<decimal?>("lday19_open"));
                            rows[0].SetField<decimal?>("lday19_open", rows[0].Field<decimal?>("lday18_open"));
                            rows[0].SetField<decimal?>("lday18_open", rows[0].Field<decimal?>("lday17_open"));
                            rows[0].SetField<decimal?>("lday17_open", rows[0].Field<decimal?>("lday16_open"));
                            rows[0].SetField<decimal?>("lday16_open", rows[0].Field<decimal?>("lday15_open"));
                            rows[0].SetField<decimal?>("lday15_open", rows[0].Field<decimal?>("lday14_open"));
                            rows[0].SetField<decimal?>("lday14_open", rows[0].Field<decimal?>("lday13_open"));
                            rows[0].SetField<decimal?>("lday13_open", rows[0].Field<decimal?>("lday12_open"));
                            rows[0].SetField<decimal?>("lday12_open", rows[0].Field<decimal?>("lday11_open"));
                            rows[0].SetField<decimal?>("lday11_open", rows[0].Field<decimal?>("lday10_open"));
                            rows[0].SetField<decimal?>("lday10_open", rows[0].Field<decimal?>("lday09_open"));
                            rows[0].SetField<decimal?>("lday09_open", rows[0].Field<decimal?>("lday08_open"));
                            rows[0].SetField<decimal?>("lday08_open", rows[0].Field<decimal?>("lday07_open"));
                            rows[0].SetField<decimal?>("lday07_open", rows[0].Field<decimal?>("lday06_open"));
                            rows[0].SetField<decimal?>("lday06_open", rows[0].Field<decimal?>("lday05_open"));
                            rows[0].SetField<decimal?>("lday05_open", rows[0].Field<decimal?>("lday04_open"));
                            rows[0].SetField<decimal?>("lday04_open", rows[0].Field<decimal?>("lday03_open"));
                            rows[0].SetField<decimal?>("lday03_open", rows[0].Field<decimal?>("lday02_open"));
                            rows[0].SetField<decimal?>("lday02_open", rows[0].Field<decimal?>("lday01_open"));
                            rows[0].SetField<decimal?>("lday01_open", rows[0].Field<decimal?>("openprice"));





                            DataRow newr = historysource.NewRow();
                            newr.SetField<decimal?>("min", minprice);
                            newr.SetField<decimal?>("high", highprice);
                            newr.SetField<decimal?>("close", nowprice);
                            newr.SetField<decimal?>("open", openprice);
                            newr.SetField<decimal?>("volumn", volumn);

                            newr.SetField<string>("codetype", codetype);
                            newr.SetField<string>("codevalue", codevalue);
                            newr.SetField<DateTime?>("transday", lasttime.Date);
                            try
                            {
                                historysource.Rows.Add(newr);
                            }
                            catch (Exception anyerror)
                            {

                                ss_mian_label.Text = anyerror.Message;
                            }


                            String fileter = "transday <= #" + lasttime.Date.AddMonths(-3).Date.ToString("yyyy-MM-dd") + "# ";
                            DataRow[] todel = historysource.Select(fileter);
                            foreach (DataRow delitem in todel)
                            {
                                historysource.Rows.Remove(delitem);
                            }



                            rows[0].SetField<DateTime?>("lday_time", lasttime);







                        }//下午3点处理结束


                    }//下载的分钟时间与之前记录的部一样
                    #endregion


                    rows[0].SetField<DateTime>("nowtime", lasttime);


                }//找到数据库有行
                Application.DoEvents();

            }//循环结束

            DownLoadSinaResult RtnResult = new DownLoadSinaResult();
            RtnResult.LastTime = lasttime;
            RtnResult.SendText = SendText;
            return RtnResult;

        }

        public class WetchatJob
        {
            public WetchatJob(string _MsgType, string _MsgContent)
            {
                MsgContent = _MsgContent;
                MsgType = _MsgType;
            }

            public string MsgType { get; set; }
            public string MsgContent { get; set; }
        }

        public class DownLoadSinaResult
        {
            public DateTime? LastTime { get; set; }
            public List<WetchatJob> SendText { get; set; }
        }

        private Boolean HaveBreak(Decimal?[] EndArray, int StartIndex, decimal scaler = 1.0M)
        {
            if (StartIndex >= EndArray.Length - 2)
            {
                return false;
            }
            decimal? Highprice = 0;
            for (int i = StartIndex + 2; i < EndArray.Length; i++)
            {
                if (EndArray[i] > Highprice)
                {
                    Highprice = EndArray[i];
                }
            }
            if (
                Highprice < EndArray[StartIndex] * scaler
               && Highprice > EndArray[StartIndex + 1]
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private decimal? CaculateGrowUp(decimal? Now, Decimal? minprice)
        {
            if (minprice == 0 || minprice == null)
            {
                return 0;
            }
            else
            {
                return (Now / minprice - 1.0M);
            }
        }

        private void Download_History_Click(object sender, EventArgs e)
        {
            string TemplateURL = "http://43.push2his.eastmoney.com/api/qt/stock/kline/get?cb=jQuery&secid={0}.{1}&ut=fa5fd1943c7b386f172d6893dbfba10b&fields1=f1%2Cf2%2Cf3%2Cf4%2Cf5&fields2=f51%2Cf52%2Cf53%2Cf54%2Cf55%2Cf56%2Cf57%2Cf58&klt=101&fqt=0&end=20500101&lmt=60&_=1596726494793";
            System.Net.CookieCollection cookie = new System.Net.CookieCollection();
            foreach (DataRow item in listsource.Rows)
            {
                //"sh=>1,sz=>0"
                string codetype = "";
                if (item.Field<String>("codetype") == "sh")
                {
                    codetype = "1";
                }
                else if (item.Field<String>("codetype") == "sz")
                {
                    codetype = "0";
                }
                string URL = string.Format(TemplateURL, new object[] { codetype, item.Field<String>("codevalue") });
                String LineDatas = NetFramework.Util_WEB.OpenUrl(
                                         URL, "", "", "GET", cookie);
                if (LineDatas.Length > 9)
                {
                    LineDatas = LineDatas.Substring(7, LineDatas.Length - 9);
                }
                else
                {
                    continue;
                }
                ss_mian_label.Text = "下载日线" + item.Field<String>("codevalue");
                Newtonsoft.Json.Linq.JObject jr = Newtonsoft.Json.Linq.JObject.Parse(LineDatas);
                if (jr["data"].ToString() == "")
                {
                    continue;
                }
                Newtonsoft.Json.Linq.JArray Lines = (Newtonsoft.Json.Linq.JArray)jr["data"]["klines"];
                //跳过最后一天，不要当天
                //假设4个K线，从，2，1，0开始
                Int32 containday = 2;
                if (DateTime.Now.Hour >= 15 || DateTime.Now.Hour <= 8)
                {
                    containday = 1;
                }
                for (int i = Lines.Count - containday; i >= 0; i--)
                {
                    string[] infs = Lines[i].ToString().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                    try
                    {
                        DataRow newr = historysource.NewRow();
                        newr.SetField<decimal?>("min", Convert.ToDecimal(infs[4]));
                        newr.SetField<decimal?>("high", Convert.ToDecimal(infs[3]));
                        newr.SetField<decimal?>("close", Convert.ToDecimal(infs[2]));
                        newr.SetField<decimal?>("open", Convert.ToDecimal(infs[1]));
                        newr.SetField<decimal?>("volumn", Convert.ToDecimal(infs[6]));

                        newr.SetField<string>("codetype", item.Field<String>("codetype"));
                        newr.SetField<string>("codevalue", item.Field<String>("codevalue"));
                        newr.SetField<DateTime?>("transday", Convert.ToDateTime(infs[0]));
                        historysource.Rows.Add(newr);
                    }
                    catch (Exception anyerror2)
                    {
                        ss_mian_label.Text = item.Field<String>("codevalue") + infs[0] + anyerror2.Message;

                    }


                    switch (Lines.Count - containday - i)
                    {
                        case 0:
                            item.SetField<decimal?>("lday01_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday01_high", Convert.ToDecimal(infs[3]));
                            item.SetField<decimal?>("lday01_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("lday01_open", Convert.ToDecimal(infs[1]));
                            break;
                        case 1:
                            item.SetField<decimal?>("lday02_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday02_high", Convert.ToDecimal(infs[3]));
                            item.SetField<decimal?>("lday02_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("lday02_open", Convert.ToDecimal(infs[1]));
                            break;
                        case 2:
                            item.SetField<decimal?>("lday03_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday03_high", Convert.ToDecimal(infs[3]));
                            item.SetField<decimal?>("lday03_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("lday03_open", Convert.ToDecimal(infs[1]));
                            break;
                        case 3:
                            item.SetField<decimal?>("lday04_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday04_high", Convert.ToDecimal(infs[3]));
                            item.SetField<decimal?>("lday04_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("lday04_open", Convert.ToDecimal(infs[1]));
                            break;
                        case 4:
                            item.SetField<decimal?>("lday05_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday05_high", Convert.ToDecimal(infs[3]));
                            item.SetField<decimal?>("lday05_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("lday05_open", Convert.ToDecimal(infs[1]));
                            break;
                        case 5:
                            item.SetField<decimal?>("lday06_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday06_high", Convert.ToDecimal(infs[3]));
                            item.SetField<decimal?>("lday06_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("lday06_open", Convert.ToDecimal(infs[1]));
                            break;
                        case 6:
                            item.SetField<decimal?>("lday07_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday07_high", Convert.ToDecimal(infs[3]));
                            item.SetField<decimal?>("lday07_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("lday07_open", Convert.ToDecimal(infs[1]));
                            break;
                        case 7:
                            item.SetField<decimal?>("lday08_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday08_high", Convert.ToDecimal(infs[3]));
                            item.SetField<decimal?>("lday08_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("lday08_open", Convert.ToDecimal(infs[1]));
                            break;
                        case 8:
                            item.SetField<decimal?>("lday09_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday09_high", Convert.ToDecimal(infs[3]));
                            item.SetField<decimal?>("lday09_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("lday09_open", Convert.ToDecimal(infs[1]));
                            break;
                        case 9:
                            item.SetField<decimal?>("lday10_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday10_high", Convert.ToDecimal(infs[3]));
                            item.SetField<decimal?>("lday10_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("lday10_open", Convert.ToDecimal(infs[1]));
                            break;
                        case 10:
                            item.SetField<decimal?>("lday11_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday11_high", Convert.ToDecimal(infs[3]));
                            item.SetField<decimal?>("lday11_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("lday11_open", Convert.ToDecimal(infs[1]));
                            break;
                        case 11:
                            item.SetField<decimal?>("lday12_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday12_high", Convert.ToDecimal(infs[3]));
                            item.SetField<decimal?>("lday12_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("lday12_open", Convert.ToDecimal(infs[1]));
                            break;
                        case 12:
                            item.SetField<decimal?>("lday13_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday13_high", Convert.ToDecimal(infs[3]));
                            item.SetField<decimal?>("lday13_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("lday13_open", Convert.ToDecimal(infs[1]));
                            break;
                        case 13:
                            item.SetField<decimal?>("lday14_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday14_high", Convert.ToDecimal(infs[3]));
                            item.SetField<decimal?>("lday14_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("lday14_open", Convert.ToDecimal(infs[1]));
                            break;
                        case 14:
                            item.SetField<decimal?>("lday15_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday15_high", Convert.ToDecimal(infs[3]));
                            item.SetField<decimal?>("lday15_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("lday15_open", Convert.ToDecimal(infs[1]));
                            break;
                        case 15:
                            item.SetField<decimal?>("lday16_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday16_high", Convert.ToDecimal(infs[3]));
                            item.SetField<decimal?>("lday16_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("lday16_open", Convert.ToDecimal(infs[1]));
                            break;
                        case 16:
                            item.SetField<decimal?>("lday17_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday17_high", Convert.ToDecimal(infs[3]));
                            item.SetField<decimal?>("lday17_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("lday17_open", Convert.ToDecimal(infs[1]));
                            break;
                        case 17:
                            item.SetField<decimal?>("lday18_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday18_high", Convert.ToDecimal(infs[3]));
                            item.SetField<decimal?>("lday18_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("lday18_open", Convert.ToDecimal(infs[1]));
                            break;
                        case 18:
                            item.SetField<decimal?>("lday19_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday19_high", Convert.ToDecimal(infs[3]));
                            item.SetField<decimal?>("lday19_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("lday19_open", Convert.ToDecimal(infs[1]));
                            break;
                        case 19:
                            item.SetField<decimal?>("lday20_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday20_high", Convert.ToDecimal(infs[3]));
                            item.SetField<decimal?>("lday20_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("lday20_open", Convert.ToDecimal(infs[1]));
                            break;

                        default:

                            break;

                    }


                }//日K线循环结束
                YTDSuperAndNow(item);
                //SetDayGrowDoubleSuper(item);
                //SetDayGrowTwoDaySuper(item);
                Application.DoEvents();
            }//行循环结束
            ss_mian_label.Text = "下载完成";
        }

        private void SetDayGrowVmin(DataRow item)
        {

            Decimal? max20min = 0;
            Decimal? max20min_day = 0;
            decimal? lday01_min = item.Field<decimal?>("lday01_min");
            decimal? test20min = item.Field<decimal?>("lday01_min");

            decimal? nowprice = item.Field<decimal?>("nowprice");

            if (test20min != 0 && test20min != null)
            {
                max20min_day = 1;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday02_min");
            if (test20min < max20min)
            {
                max20min_day = 2;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday03_min");
            if (test20min < max20min)
            {
                max20min_day = 3;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday04_min");
            if (test20min < max20min)
            {
                max20min_day = 4;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday05_min");
            if (test20min < max20min)
            {
                max20min_day = 5;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday06_min");
            if (test20min < max20min)
            {
                max20min_day = 6;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday07_min");
            if (test20min < max20min)
            {
                max20min_day = 7;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday08_min");
            if (test20min < max20min)
            {
                max20min_day = 8;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday09_min");
            if (test20min < max20min)
            {
                max20min_day = 9;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday10_min");
            if (test20min < max20min)
            {
                max20min_day = 10;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday11_min");
            if (test20min < max20min)
            {
                max20min_day = 11;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday12_min");
            if (test20min < max20min)
            {
                max20min_day = 12;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday13_min");
            if (test20min < max20min)
            {
                max20min_day = 13;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday14_min");
            if (test20min < max20min)
            {
                max20min_day = 14;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday15_min");
            if (test20min < max20min)
            {
                max20min_day = 15;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday16_min");
            if (test20min < max20min)
            {
                max20min_day = 16;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday17_min");
            if (test20min < max20min)
            {
                max20min_day = 17;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday18_min");
            if (test20min < max20min)
            {
                max20min_day = 18;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday19_min");
            if (test20min < max20min)
            {
                max20min_day = 19;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday20_min");
            if (test20min < max20min)
            {
                max20min_day = 20;
                max20min = test20min;
            }
            Decimal? max20high = 0;
            Decimal? max20high_day = 0;
            decimal? test20high = item.Field<decimal?>("lday01_high");
            if (test20high != 0 && test20high != null)
            {
                max20high_day = 1;
                max20high = test20high;
            }
            test20high = item.Field<decimal?>("lday02_high");
            if (test20high > max20high)
            {
                max20high_day = 2;
                max20high = test20high;
            }
            test20high = item.Field<decimal?>("lday03_high");
            if (test20high > max20high)
            {
                max20high_day = 3;
                max20high = test20high;
            }
            test20high = item.Field<decimal?>("lday04_high");
            if (test20high > max20high)
            {
                max20high_day = 4;
                max20high = test20high;
            }
            test20high = item.Field<decimal?>("lday05_high");
            if (test20high > max20high)
            {
                max20high_day = 5;
                max20high = test20high;
            }
            test20high = item.Field<decimal?>("lday06_high");
            if (test20high > max20high)
            {
                max20high_day = 6;
                max20high = test20high;
            }
            test20high = item.Field<decimal?>("lday07_high");
            if (test20high > max20high)
            {
                max20high_day = 7;
                max20high = test20high;
            }
            test20high = item.Field<decimal?>("lday08_high");
            if (test20high > max20high)
            {
                max20high_day = 8;
                max20high = test20high;
            }
            test20high = item.Field<decimal?>("lday09_high");
            if (test20high > max20high)
            {
                max20high_day = 9;
                max20high = test20high;
            }
            test20high = item.Field<decimal?>("lday10_high");
            if (test20high > max20high)
            {
                max20high_day = 10;
                max20high = test20high;
            }
            test20high = item.Field<decimal?>("lday11_high");
            if (test20high > max20high)
            {
                max20high_day = 11;
                max20high = test20high;
            }
            test20high = item.Field<decimal?>("lday12_high");
            if (test20high > max20high)
            {
                max20high_day = 12;
                max20high = test20high;
            }
            test20high = item.Field<decimal?>("lday13_high");
            if (test20high > max20high)
            {
                max20high_day = 13;
                max20high = test20high;
            }
            test20high = item.Field<decimal?>("lday14_high");
            if (test20high > max20high)
            {
                max20high_day = 14;
                max20high = test20high;
            }
            test20high = item.Field<decimal?>("lday15_high");
            if (test20high > max20high)
            {
                max20high_day = 15;
                max20high = test20high;
            }
            test20high = item.Field<decimal?>("lday16_high");
            if (test20high > max20high)
            {
                max20high_day = 16;
                max20high = test20high;
            }
            test20high = item.Field<decimal?>("lday17_high");
            if (test20high > max20high)
            {
                max20high_day = 17;
                max20high = test20high;
            }
            test20high = item.Field<decimal?>("lday18_high");
            if (test20high > max20high)
            {
                max20high_day = 18;
                max20high = test20high;
            }
            test20high = item.Field<decimal?>("lday19_high");
            if (test20high > max20high)
            {
                max20high_day = 19;
                max20high = test20high;
            }
            test20high = item.Field<decimal?>("lday20_high");
            if (test20high > max20high)
            {
                max20high_day = 20;
                max20high = test20high;
            }



            item.SetField<decimal?>("max20growday", (max20min == 0 || max20min_day == 0 || max20min_day >= 3) ? 0 : ((max20high / max20min - 1)));
            item.SetField<decimal?>("max20growday_avg15", max20min_day);
        }

        private void SetDayGrowThreeDayJump(DataRow item)
        {
            decimal? ThreeDayJump = ((item.Field<decimal?>("nowprice") == 0 || item.Field<decimal?>("highprice") == 0) ? 0 : (item.Field<decimal?>("highprice") / item.Field<decimal?>("nowprice") - 1))
                + ((item.Field<decimal?>("lday01_end") == 0 || item.Field<decimal?>("lday01_high") == 0) ? 0 : (item.Field<decimal?>("lday01_high") / item.Field<decimal?>("lday01_end") - 1))
                  + ((item.Field<decimal?>("lday02_end") == 0 || item.Field<decimal?>("lday02_high") == 0) ? 0 : (item.Field<decimal?>("lday02_high") / item.Field<decimal?>("lday02_end") - 1))
                    + ((item.Field<decimal?>("lday03_end") == 0 || item.Field<decimal?>("lday03_high") == 0) ? 0 : (item.Field<decimal?>("lday03_high") / item.Field<decimal?>("lday03_end") - 1))
                ;




            item.SetField<decimal?>("max20growday", ThreeDayJump
                );
            //item.SetField<decimal?>("max20growday_avg15", max20min_day);
        }

        private void SetDayGrowVminPerDay(DataRow item)
        {
            Decimal? max20max = 0;
            Decimal? max20max_day = 0;

            decimal? test20max = item.Field<decimal?>("highprice");

            if (test20max != 0 && test20max != null)
            {
                max20max_day = 1;
                max20max = test20max;
            }
            test20max = item.Field<decimal?>("lday01_high");

            if (test20max != 0 && test20max != null)
            {
                max20max_day = 1;
                max20max = test20max;
            }
            test20max = item.Field<decimal?>("lday02_high");
            if (test20max > max20max)
            {
                max20max_day = 2;
                max20max = test20max;
            }
            test20max = item.Field<decimal?>("lday03_high");
            if (test20max > max20max)
            {
                max20max_day = 3;
                max20max = test20max;
            }
            test20max = item.Field<decimal?>("lday04_high");
            if (test20max > max20max)
            {
                max20max_day = 4;
                max20max = test20max;
            }
            test20max = item.Field<decimal?>("lday05_high");
            if (test20max > max20max)
            {
                max20max_day = 5;
                max20max = test20max;
            }
            test20max = item.Field<decimal?>("lday06_high");
            if (test20max > max20max)
            {
                max20max_day = 6;
                max20max = test20max;
            }
            test20max = item.Field<decimal?>("lday07_high");
            if (test20max > max20max)
            {
                max20max_day = 7;
                max20max = test20max;
            }
            test20max = item.Field<decimal?>("lday08_high");
            if (test20max > max20max)
            {
                max20max_day = 8;
                max20max = test20max;
            }
            test20max = item.Field<decimal?>("lday09_high");
            if (test20max > max20max)
            {
                max20max_day = 9;
                max20max = test20max;
            }
            test20max = item.Field<decimal?>("lday10_high");
            if (test20max > max20max)
            {
                max20max_day = 10;
                max20max = test20max;
            }
            test20max = item.Field<decimal?>("lday11_high");
            if (test20max > max20max)
            {
                max20max_day = 11;
                max20max = test20max;
            }
            test20max = item.Field<decimal?>("lday12_high");
            if (test20max > max20max)
            {
                max20max_day = 12;
                max20max = test20max;
            }
            test20max = item.Field<decimal?>("lday13_high");
            if (test20max > max20max)
            {
                max20max_day = 13;
                max20max = test20max;
            }
            test20max = item.Field<decimal?>("lday14_high");
            if (test20max > max20max)
            {
                max20max_day = 14;
                max20max = test20max;
            }
            test20max = item.Field<decimal?>("lday15_high");
            if (test20max > max20max)
            {
                max20max_day = 15;
                max20max = test20max;
            }
            test20max = item.Field<decimal?>("lday16_high");
            if (test20max > max20max)
            {
                max20max_day = 16;
                max20max = test20max;
            }
            test20max = item.Field<decimal?>("lday17_high");
            if (test20max > max20max)
            {
                max20max_day = 17;
                max20max = test20max;
            }
            test20max = item.Field<decimal?>("lday18_high");
            if (test20max > max20max)
            {
                max20max_day = 18;
                max20max = test20max;
            }
            test20max = item.Field<decimal?>("lday19_high");
            if (test20max > max20max)
            {
                max20max_day = 19;
                max20max = test20max;
            }
            test20max = item.Field<decimal?>("lday20_high");
            if (test20max > max20max)
            {
                max20max_day = 20;
                max20max = test20max;
            }


            Decimal? max20min = 0;
            Decimal? max20min_day = 0;

            decimal? test20min = item.Field<decimal?>("minprice");

            decimal? nowprice = item.Field<decimal?>("nowprice");

            if (test20min != 0 && test20min != null)
            {
                max20min_day = 1;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday01_min");

            if (test20min != 0 && test20min != null)
            {
                max20min_day = 1;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday02_min");
            if (test20min < max20min)
            {
                max20min_day = 2;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday03_min");
            if (test20min < max20min)
            {
                max20min_day = 3;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday04_min");
            if (test20min < max20min)
            {
                max20min_day = 4;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday05_min");
            if (test20min < max20min)
            {
                max20min_day = 5;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday06_min");
            if (test20min < max20min)
            {
                max20min_day = 6;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday07_min");
            if (test20min < max20min)
            {
                max20min_day = 7;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday08_min");
            if (test20min < max20min)
            {
                max20min_day = 8;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday09_min");
            if (test20min < max20min)
            {
                max20min_day = 9;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday10_min");
            if (test20min < max20min)
            {
                max20min_day = 10;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday11_min");
            if (test20min < max20min)
            {
                max20min_day = 11;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday12_min");
            if (test20min < max20min)
            {
                max20min_day = 12;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday13_min");
            if (test20min < max20min)
            {
                max20min_day = 13;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday14_min");
            if (test20min < max20min)
            {
                max20min_day = 14;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday15_min");
            if (test20min < max20min)
            {
                max20min_day = 15;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday16_min");
            if (test20min < max20min)
            {
                max20min_day = 16;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday17_min");
            if (test20min < max20min)
            {
                max20min_day = 17;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday18_min");
            if (test20min < max20min)
            {
                max20min_day = 18;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday19_min");
            if (test20min < max20min)
            {
                max20min_day = 19;
                max20min = test20min;
            }
            test20min = item.Field<decimal?>("lday20_min");
            if (test20min < max20min)
            {
                max20min_day = 20;
                max20min = test20min;
            }



            item.SetField<decimal?>("max20growday", (max20min == 0 || max20min_day == 0 || nowprice == 0 || max20max_day == 0) ? 0 : (
                (nowprice / max20min - 1) / max20min_day
                - (max20max / nowprice - 1) / max20max_day

                ));
            //item.SetField<decimal?>("max20growday_avg15", max20min_day);
        }

        private void SetDayGrowVMax(DataRow item)
        {

            Decimal? max20high = 0;
            Decimal? max20high_day = 0;
            decimal? lday01_high = item.Field<decimal?>("lday01_high");
            decimal? test20high = item.Field<decimal?>("lday01_high");
            decimal? nowprice = item.Field<decimal?>("nowprice");
            decimal? openprice = item.Field<decimal?>("openprice");
            decimal? max20vol = 0;
            decimal? volumn = item.Field<decimal?>("volumn");

            if (test20high != 0 && test20high != null)
            {
                max20high_day = 1;
                max20high = test20high;
                max20vol = item.Field<decimal?>("lday01_vol");
            }
            test20high = item.Field<decimal?>("lday02_high");
            if (test20high > max20high)
            {
                max20high_day = 2;
                max20high = test20high;
                max20vol = item.Field<decimal?>("lday02_vol");
            }
            test20high = item.Field<decimal?>("lday03_high");
            if (test20high > max20high)
            {
                max20high_day = 3;
                max20high = test20high;
                max20vol = item.Field<decimal?>("lday03_vol");
            }
            test20high = item.Field<decimal?>("lday04_high");
            if (test20high > max20high)
            {
                max20high_day = 4;
                max20high = test20high;
                max20vol = item.Field<decimal?>("lday04_vol");
            }
            test20high = item.Field<decimal?>("lday05_high");
            if (test20high > max20high)
            {
                max20high_day = 5;
                max20high = test20high;
                max20vol = item.Field<decimal?>("lday05_vol");
            }
            test20high = item.Field<decimal?>("lday06_high");
            if (test20high > max20high)
            {
                max20high_day = 6;
                max20high = test20high;
                max20vol = item.Field<decimal?>("lday06_vol");
            }
            test20high = item.Field<decimal?>("lday07_high");
            if (test20high > max20high)
            {
                max20high_day = 7;
                max20high = test20high;
                max20vol = item.Field<decimal?>("lday07_vol");
            }
            test20high = item.Field<decimal?>("lday08_high");
            if (test20high > max20high)
            {
                max20high_day = 8;
                max20high = test20high;
                max20vol = item.Field<decimal?>("lday08_vol");
            }
            test20high = item.Field<decimal?>("lday09_high");
            if (test20high > max20high)
            {
                max20high_day = 9;
                max20high = test20high;
                max20vol = item.Field<decimal?>("lday09_vol");
            }
            test20high = item.Field<decimal?>("lday10_high");
            if (test20high > max20high)
            {
                max20high_day = 10;
                max20high = test20high;
                max20vol = item.Field<decimal?>("lday10_vol");
            }
            test20high = item.Field<decimal?>("lday11_high");
            if (test20high > max20high)
            {
                max20high_day = 11;
                max20high = test20high;
                max20vol = item.Field<decimal?>("lday11_vol");
            }
            test20high = item.Field<decimal?>("lday12_high");
            if (test20high > max20high)
            {
                max20high_day = 12;
                max20high = test20high;
                max20vol = item.Field<decimal?>("lday12_vol");
            }
            test20high = item.Field<decimal?>("lday13_high");
            if (test20high > max20high)
            {
                max20high_day = 13;
                max20high = test20high;
                max20vol = item.Field<decimal?>("lday13_vol");
            }
            test20high = item.Field<decimal?>("lday14_high");
            if (test20high > max20high)
            {
                max20high_day = 14;
                max20high = test20high;
                max20vol = item.Field<decimal?>("lday14_vol");
            }
            test20high = item.Field<decimal?>("lday15_high");
            if (test20high > max20high)
            {
                max20high_day = 15;
                max20high = test20high;
                max20vol = item.Field<decimal?>("lday15_vol");
            }
            //test20high = item.Field<decimal?>("lday16_high");
            //if (test20high > max20high)
            //{
            //    max20high_day = 16;
            //    max20high = test20high;
            //}
            //test20high = item.Field<decimal?>("lday17_high");
            //if (test20high > max20high)
            //{
            //    max20high_day = 17;
            //    max20high = test20high;
            //}
            //test20high = item.Field<decimal?>("lday18_high");
            //if (test20high > max20high)
            //{
            //    max20high_day = 18;
            //    max20high = test20high;
            //}
            //test20high = item.Field<decimal?>("lday19_high");
            //if (test20high > max20high)
            //{
            //    max20high_day = 19;
            //    max20high = test20high;
            //}
            //test20high = item.Field<decimal?>("lday20_high");
            //if (test20high > max20high)
            //{
            //    max20high_day = 20;
            //    max20high = test20high;
            //}

            item.SetField<decimal?>("max20growday", (max20high == 0 || max20vol == 0) ? 0 : (Math.Abs((nowprice / max20high - 1).Value) * (nowprice < openprice || nowprice < max20high ? -1 : 1)));
            //item.SetField<decimal?>("max20growday_avg15", max20high_day);
        }

        private void AfterSuperLittle(DataRow item)
        {
            decimal? lday01_high = item.Field<decimal?>("lday01_high");

            decimal? nowprice = item.Field<decimal?>("nowprice");


            decimal? lday02_high = item.Field<decimal?>("lday02_high");

            decimal? highprice = item.Field<decimal?>("highprice");

            decimal? lday01_end = item.Field<decimal?>("lday01_end");

            decimal? lday02_end = item.Field<decimal?>("lday02_end");

            decimal? morehighprice = highprice;
            if (morehighprice < lday01_high)
            {
                morehighprice = lday01_high;
            }
            if (morehighprice < lday01_high)
            {
                morehighprice = lday01_high;
            }
            if (lday02_end != 0 && lday01_end / lday02_end >= 1.06M && morehighprice / nowprice <= 1.03M)
            {
                item.SetField<decimal?>("max20growday",
                  (nowprice / lday01_end - 1)
                  );
            }
            else
            {
                item.SetField<decimal?>("max20growday",
                     -99
                      );
            }

        }
        private void SetDayGrowVMaxToYtdAndBreak(DataRow item)
        {



            decimal? lday01_end = item.Field<decimal?>("lday01_end");
            decimal? nowprice = item.Field<decimal?>("nowprice");


            List<decimal?> TestPrice = new List<decimal?>();
            if (DateTime.Now.Hour * 60 + DateTime.Now.Minute < 570 || DateTime.Now.Hour * 60 + DateTime.Now.Minute > 900)
            {
                lday01_end = item.Field<decimal?>("lday02_end");
                nowprice = item.Field<decimal?>("lday01_end");
            }
            else
            {
                TestPrice.Add(item.Field<decimal?>("highprice"));
            }
            TestPrice.Add(item.Field<decimal?>("lday01_high"));
            TestPrice.Add(item.Field<decimal?>("lday02_high"));
            TestPrice.Add(item.Field<decimal?>("lday03_high"));
            TestPrice.Add(item.Field<decimal?>("lday04_high"));
            TestPrice.Add(item.Field<decimal?>("lday05_high"));
            TestPrice.Add(item.Field<decimal?>("lday06_high"));
            TestPrice.Add(item.Field<decimal?>("lday07_high"));
            TestPrice.Add(item.Field<decimal?>("lday08_high"));
            TestPrice.Add(item.Field<decimal?>("lday09_high"));
            TestPrice.Add(item.Field<decimal?>("lday10_high"));
            TestPrice.Add(item.Field<decimal?>("lday11_high"));
            TestPrice.Add(item.Field<decimal?>("lday12_high"));
            TestPrice.Add(item.Field<decimal?>("lday13_high"));
            TestPrice.Add(item.Field<decimal?>("lday14_high"));
            TestPrice.Add(item.Field<decimal?>("lday15_high"));
            TestPrice.Add(item.Field<decimal?>("lday16_high"));
            TestPrice.Add(item.Field<decimal?>("lday17_high"));
            //TestPrice.Add(item.Field<decimal?>("lday18_high"));
            //TestPrice.Add(item.Field<decimal?>("lday19_high"));
            //TestPrice.Add(item.Field<decimal?>("lday20_high"));
            //TestPrice.Add(item.Field<decimal?>("lday02_high"));
            decimal?[] p_test = TestPrice.ToArray();

            decimal? highprice = item.Field<decimal?>("highprice");
            if (highprice < item.Field<decimal?>("lday01_high"))
            {
                highprice = item.Field<decimal?>("lday01_high");
            }
            if (highprice < item.Field<decimal?>("lday02_high"))
            {
                highprice = item.Field<decimal?>("lday02_high");
            }
            if (HaveBreak(p_test, 0))
            {
                DateTime? breakday1 = item.Field<DateTime?>("breakday1");
                if (breakday1 != DateTime.Today)
                {
                    item.SetField<DateTime?>("breakday2", breakday1);
                    item.SetField<DateTime?>("breakday1", DateTime.Today);
                }
            }
            if (HaveBreak(p_test, 0)
                && (HaveBreak(p_test, 1) == false)
                && (HaveBreak(p_test, 2) == false)
                && (HaveBreak(p_test, 3) == false)
                && (HaveBreak(p_test, 4) == false)
                 && (HaveBreak(p_test, 5) == false)
                  && (HaveBreak(p_test, 6) == false)
                   && (HaveBreak(p_test, 7) == false)
                    && (HaveBreak(p_test, 8) == false)
                     && (HaveBreak(p_test, 9) == false)
                && (lday01_end != 0))
            {
                item.SetField<decimal?>("max20growday",
                   (nowprice / lday01_end - 1)
                   );


            }
            else
            {
                item.SetField<decimal?>("max20growday",
                     99
                      );
                DateTime? breakday1 = item.Field<DateTime?>("breakday1");
                DateTime? breakday2 = item.Field<DateTime?>("breakday2");
                if (breakday1 == DateTime.Today)
                {
                    item.SetField<DateTime?>("breakday1", breakday2);
                    item.SetField<DateTime?>("breakday2", null);

                }
            }


            //item.SetField<decimal?>("max20growday_avg15", max20high_day);
        }

        private void MA5CrossMA15(DataRow item)
        {
            decimal? MA5 = item.Field<decimal?>("nowprice") + item.Field<decimal?>("lday01_end") + item.Field<decimal?>("lday02_end") + item.Field<decimal?>("lday03_end") + item.Field<decimal?>("lday04_end");
            MA5 = MA5 / 5;
            decimal? MA5Bef = item.Field<decimal?>("lday01_end") + item.Field<decimal?>("lday02_end") + item.Field<decimal?>("lday03_end") + item.Field<decimal?>("lday04_end") + item.Field<decimal?>("lday05_end");
            MA5Bef = MA5Bef / 5;

            decimal? MA5Bef5 = item.Field<decimal?>("lday05_end") + item.Field<decimal?>("lday06_end") + item.Field<decimal?>("lday07_end") + item.Field<decimal?>("lday08_end") + item.Field<decimal?>("lday09_end");
            MA5Bef5 = MA5Bef5 / 5;

            decimal? MA15 = item.Field<decimal?>("nowprice") + item.Field<decimal?>("lday01_end") + item.Field<decimal?>("lday02_end") + item.Field<decimal?>("lday03_end") + item.Field<decimal?>("lday04_end")
                + item.Field<decimal?>("lday05_end") + item.Field<decimal?>("lday06_end") + item.Field<decimal?>("lday07_end") + item.Field<decimal?>("lday08_end") + item.Field<decimal?>("lday09_end")
                + item.Field<decimal?>("lday10_end") + item.Field<decimal?>("lday11_end") + item.Field<decimal?>("lday12_end") + item.Field<decimal?>("lday13_end") + item.Field<decimal?>("lday14_end");
            MA15 = MA15 / 15;
            decimal? MA15Bef = item.Field<decimal?>("lday01_end") + item.Field<decimal?>("lday02_end") + item.Field<decimal?>("lday03_end") + item.Field<decimal?>("lday04_end") + item.Field<decimal?>("lday05_end")
                + item.Field<decimal?>("lday06_end") + item.Field<decimal?>("lday07_end") + item.Field<decimal?>("lday08_end") + item.Field<decimal?>("lday09_end")
                + item.Field<decimal?>("lday10_end") + item.Field<decimal?>("lday11_end") + item.Field<decimal?>("lday12_end") + item.Field<decimal?>("lday13_end")
                + item.Field<decimal?>("lday14_end") + item.Field<decimal?>("lday15_end");
            MA15Bef = MA15Bef / 15;

            decimal? MA15Bef5 = item.Field<decimal?>("lday05_end")
                + item.Field<decimal?>("lday06_end") + item.Field<decimal?>("lday07_end") + item.Field<decimal?>("lday08_end") + item.Field<decimal?>("lday09_end")
                + item.Field<decimal?>("lday10_end") + item.Field<decimal?>("lday11_end") + item.Field<decimal?>("lday12_end") + item.Field<decimal?>("lday13_end")
                + item.Field<decimal?>("lday14_end") + item.Field<decimal?>("lday15_end") + item.Field<decimal?>("lday16_end") + item.Field<decimal?>("lday17_end")
                 + item.Field<decimal?>("lday18_end") + item.Field<decimal?>("lday19_end") + item.Field<decimal?>("lday20_end");
            MA15Bef5 = MA15Bef5 / 15;
            if (MA5Bef <= MA15Bef && MA5 >= MA15)
            {
                item.SetField<decimal?>("max20growday",
                   (MA5Bef5 == 0 ? 0 : ((MA15Bef5 - MA5Bef5) / MA5Bef5))
                     );
                DateTime? breakday1 = item.Field<DateTime?>("breakday1");
                if (breakday1 != DateTime.Today)
                {
                    item.SetField<DateTime?>("breakday2", breakday1);
                    item.SetField<DateTime?>("breakday1", DateTime.Today);
                }
            }
            else
            {
                item.SetField<decimal?>("max20growday", 99);
            }

        }
        private void CloseCrossMA3(DataRow item)
        {
            decimal? MA3 = item.Field<decimal?>("nowprice") + item.Field<decimal?>("lday01_end") + item.Field<decimal?>("lday02_end");
            MA3 = MA3 / 3;
            decimal? nowprice = item.Field<decimal?>("nowprice");
            decimal? minprice = item.Field<decimal?>("minprice");
            decimal? lday01_end = item.Field<decimal?>("lday01_end");
            decimal? volumn = item.Field<decimal?>("volumn");
            decimal? lday01_vol = item.Field<decimal?>("lday01_vol");

            decimal? MA3bef = item.Field<decimal?>("lday01_end") + item.Field<decimal?>("lday02_end") + item.Field<decimal?>("lday03_end");
            MA3bef = MA3bef / 3;

            decimal? nowpricebef = item.Field<decimal?>("lday01_end");

            decimal? MA15 = item.Field<decimal?>("nowprice") + item.Field<decimal?>("lday01_end") + item.Field<decimal?>("lday02_end") + item.Field<decimal?>("lday03_end") + item.Field<decimal?>("lday04_end")
               + item.Field<decimal?>("lday05_end") + item.Field<decimal?>("lday06_end") + item.Field<decimal?>("lday07_end") + item.Field<decimal?>("lday08_end") + item.Field<decimal?>("lday09_end")
               + item.Field<decimal?>("lday10_end") + item.Field<decimal?>("lday11_end") + item.Field<decimal?>("lday12_end") + item.Field<decimal?>("lday13_end") + item.Field<decimal?>("lday14_end");
            MA15 = MA15 / 15;
            decimal? MA15Bef = item.Field<decimal?>("lday01_end") + item.Field<decimal?>("lday02_end") + item.Field<decimal?>("lday03_end") + item.Field<decimal?>("lday04_end") + item.Field<decimal?>("lday05_end")
                + item.Field<decimal?>("lday06_end") + item.Field<decimal?>("lday07_end") + item.Field<decimal?>("lday08_end") + item.Field<decimal?>("lday09_end")
                + item.Field<decimal?>("lday10_end") + item.Field<decimal?>("lday11_end") + item.Field<decimal?>("lday12_end") + item.Field<decimal?>("lday13_end")
                + item.Field<decimal?>("lday14_end") + item.Field<decimal?>("lday15_end");
            MA15Bef = MA15Bef / 15;

            decimal? MA5 = item.Field<decimal?>("nowprice") + item.Field<decimal?>("lday01_end") + item.Field<decimal?>("lday02_end") + item.Field<decimal?>("lday03_end") + item.Field<decimal?>("lday04_end");
            MA5 = MA5 / 5;
            decimal? MA5Bef = item.Field<decimal?>("lday01_end") + item.Field<decimal?>("lday02_end") + item.Field<decimal?>("lday03_end") + item.Field<decimal?>("lday04_end") + item.Field<decimal?>("lday05_end");
            MA5Bef = MA5Bef / 5;


            decimal? MA9 = item.Field<decimal?>("nowprice") + item.Field<decimal?>("lday01_end") + item.Field<decimal?>("lday02_end") + item.Field<decimal?>("lday03_end") + item.Field<decimal?>("lday04_end")
                + item.Field<decimal?>("lday05_end") + item.Field<decimal?>("lday06_end") + item.Field<decimal?>("lday07_end")
                + item.Field<decimal?>("lday08_end") + item.Field<decimal?>("lday09_end")
                ;

            MA9 = MA9 / 9;
            decimal? MA9Bef = item.Field<decimal?>("lday01_end") + item.Field<decimal?>("lday02_end") + item.Field<decimal?>("lday03_end") + item.Field<decimal?>("lday04_end") + item.Field<decimal?>("lday05_end")
                 + item.Field<decimal?>("lday06_end") + item.Field<decimal?>("lday07_end") + item.Field<decimal?>("lday08_end")
                 + item.Field<decimal?>("lday09_end") + item.Field<decimal?>("lday10_end");
            ;
            MA9Bef = MA9Bef / 9;

            decimal? MA10 = item.Field<decimal?>("nowprice") + item.Field<decimal?>("lday01_end") + item.Field<decimal?>("lday02_end") + item.Field<decimal?>("lday03_end") + item.Field<decimal?>("lday04_end")
               + item.Field<decimal?>("lday05_end") + item.Field<decimal?>("lday06_end") + item.Field<decimal?>("lday07_end")
               + item.Field<decimal?>("lday08_end") + item.Field<decimal?>("lday09_end")
               ;

            MA10 = MA10 / 10;
            decimal? MA10Bef = item.Field<decimal?>("lday01_end") + item.Field<decimal?>("lday02_end") + item.Field<decimal?>("lday03_end") + item.Field<decimal?>("lday04_end") + item.Field<decimal?>("lday05_end")
                 + item.Field<decimal?>("lday06_end") + item.Field<decimal?>("lday07_end") + item.Field<decimal?>("lday08_end")
                 + item.Field<decimal?>("lday09_end") + item.Field<decimal?>("lday10_end");
            ;
            MA10Bef = MA10Bef / 10;

            DateTime? nowtime = item.Field<DateTime?>("nowtime");

            decimal? maxhighprice = item.Field<decimal?>("highprice");
            if (maxhighprice < item.Field<decimal?>("lday01_high"))
            {
                maxhighprice = item.Field<decimal?>("lday01_high");
            }
            if (maxhighprice < item.Field<decimal?>("lday02_high"))
            {
                maxhighprice = item.Field<decimal?>("lday02_high");
            }
            if (maxhighprice < item.Field<decimal?>("lday03_high"))
            {
                maxhighprice = item.Field<decimal?>("lday03_high");
            }
            if (maxhighprice < item.Field<decimal?>("lday04_high"))
            {
                maxhighprice = item.Field<decimal?>("lday04_high");
            }
            if (maxhighprice < item.Field<decimal?>("lday05_high"))
            {
                maxhighprice = item.Field<decimal?>("lday05_high");
            }
            item.SetField<decimal?>("max20growday_avg15", (nowprice == 0 ? 0 : (maxhighprice / nowprice - 1)));

            if (MA5Bef <= MA15Bef && MA5 >= MA15)
            {
                DateTime? breakday1 = item.Field<DateTime?>("breakday1");
                if (breakday1 != (nowtime.HasValue ? nowtime.Value.Date : nowtime))
                {
                    item.SetField<DateTime?>("breakday2", breakday1);
                    item.SetField<DateTime?>("breakday1", nowtime.Value.Date);
                }
            }

            if (lday01_end <= MA10Bef && nowprice >= MA10)
            {

                item.SetField<decimal?>("max20growday",
                  (lday01_vol == 0 ? 0 : volumn / lday01_vol)
                        //(minprice == 0 ? 0 : (
                        //(nowprice - minprice)
                        /// minprice))
                        //MA3bef==0?0:(((MA3- nowprice) - (MA3bef - nowpricebef))/ MA3bef)
                        );

            }
            else
            {
                item.SetField<decimal?>("max20growday", -99);
                item.SetField<decimal?>("max20growday_avg15", 0);
            }

        }

        private void GrowDivousMA1To10(DataRow item)
        {

            decimal? nowprice = item.Field<decimal?>("nowprice");
            decimal? lday01_End = item.Field<decimal?>("lday01_end");


            decimal? MA10 = item.Field<decimal?>("nowprice") + item.Field<decimal?>("lday01_end") + item.Field<decimal?>("lday02_end") + item.Field<decimal?>("lday03_end") + item.Field<decimal?>("lday04_end") + item.Field<decimal?>("lday05_end")
                 + item.Field<decimal?>("lday06_end") + item.Field<decimal?>("lday07_end") + item.Field<decimal?>("lday08_end")
                 + item.Field<decimal?>("lday09_end");
            ;
            MA10 = MA10 / 10;

            decimal? nowgrow = item.Field<decimal?>("openprice") == 0 ? 0 : (item.Field<decimal?>("nowprice") / item.Field<decimal?>("openprice") - 1);
            decimal? lday01grow = item.Field<decimal?>("lday01_open") == 0 ? 0 : (item.Field<decimal?>("lday01_end") / item.Field<decimal?>("lday01_open") - 1);
            decimal? lday02grow = item.Field<decimal?>("lday02_open") == 0 ? 0 : (item.Field<decimal?>("lday02_end") / item.Field<decimal?>("lday02_open") - 1);
            decimal? lday03grow = item.Field<decimal?>("lday03_open") == 0 ? 0 : (item.Field<decimal?>("lday03_end") / item.Field<decimal?>("lday03_open") - 1);
            decimal? lday04grow = item.Field<decimal?>("lday04_open") == 0 ? 0 : (item.Field<decimal?>("lday04_end") / item.Field<decimal?>("lday04_open") - 1);
            decimal? lday05grow = item.Field<decimal?>("lday05_open") == 0 ? 0 : (item.Field<decimal?>("lday05_end") / item.Field<decimal?>("lday05_open") - 1);
            decimal? lday06grow = item.Field<decimal?>("lday06_open") == 0 ? 0 : (item.Field<decimal?>("lday06_end") / item.Field<decimal?>("lday06_open") - 1);
            decimal? lday07grow = item.Field<decimal?>("lday07_open") == 0 ? 0 : (item.Field<decimal?>("lday07_end") / item.Field<decimal?>("lday07_open") - 1);
            decimal? lday08grow = item.Field<decimal?>("lday08_open") == 0 ? 0 : (item.Field<decimal?>("lday08_end") / item.Field<decimal?>("lday08_open") - 1);
            decimal? lday09grow = item.Field<decimal?>("lday09_open") == 0 ? 0 : (item.Field<decimal?>("lday09_end") / item.Field<decimal?>("lday09_open") - 1);
            decimal? lday10grow = item.Field<decimal?>("lday10_open") == 0 ? 0 : (item.Field<decimal?>("lday10_end") / item.Field<decimal?>("lday10_open") - 1);
            decimal? lday11grow = item.Field<decimal?>("lday11_open") == 0 ? 0 : (item.Field<decimal?>("lday11_end") / item.Field<decimal?>("lday11_open") - 1);
            decimal? lday12grow = item.Field<decimal?>("lday12_open") == 0 ? 0 : (item.Field<decimal?>("lday12_end") / item.Field<decimal?>("lday12_open") - 1);
            decimal? lday13grow = item.Field<decimal?>("lday13_open") == 0 ? 0 : (item.Field<decimal?>("lday13_end") / item.Field<decimal?>("lday13_open") - 1);
            decimal? lday14grow = item.Field<decimal?>("lday14_open") == 0 ? 0 : (item.Field<decimal?>("lday14_end") / item.Field<decimal?>("lday14_open") - 1);

            decimal? lday01_vol = item.Field<decimal?>("lday01_vol");
            decimal? volumn = item.Field<decimal?>("volumn");



            decimal? magrow = nowprice / MA10 - 1;
            if (magrow.HasValue == false)
            {
                magrow = 0;
            }
            if (nowgrow > lday01grow
                && nowgrow > lday02grow
                && nowgrow > lday03grow
                && nowgrow > lday04grow
                && nowgrow > lday05grow
                && nowgrow > lday06grow
                && nowgrow > lday07grow
                && nowgrow > lday08grow
                && nowgrow > lday09grow
                && nowgrow > lday10grow
                && nowgrow > lday11grow
                && nowgrow > lday12grow
                && nowgrow > lday13grow
                && nowgrow > lday14grow
                )
            {
                item.SetField<decimal?>("max20growday", nowgrow);
                item.SetField<decimal?>("max20growday_avg15", volumn/ lday01_vol);
            }
            else
            {
                item.SetField<decimal?>("max20growday", -99.0M);
                item.SetField<decimal?>("max20growday_avg15", 0);
            }
        }

        private void YTDSuperAndNow(DataRow item)
        {

            decimal? nowprice = item.Field<decimal?>("nowprice");
            decimal? lday01_End = item.Field<decimal?>("lday01_end");


            decimal? MA10 = item.Field<decimal?>("nowprice") + item.Field<decimal?>("lday01_end") + item.Field<decimal?>("lday02_end") + item.Field<decimal?>("lday03_end") + item.Field<decimal?>("lday04_end") + item.Field<decimal?>("lday05_end")
                 + item.Field<decimal?>("lday06_end") + item.Field<decimal?>("lday07_end") + item.Field<decimal?>("lday08_end")
                 + item.Field<decimal?>("lday09_end");
            ;
            MA10 = MA10 / 10;

            decimal? nowgrow = item.Field<decimal?>("openprice") == 0 ? 0 : (item.Field<decimal?>("nowprice") / item.Field<decimal?>("openprice") - 1);
            decimal? lday01grow = item.Field<decimal?>("lday01_open") == 0 ? 0 : (item.Field<decimal?>("lday01_end") / item.Field<decimal?>("lday01_open") - 1);
            decimal? lday02grow = item.Field<decimal?>("lday02_open") == 0 ? 0 : (item.Field<decimal?>("lday02_end") / item.Field<decimal?>("lday02_open") - 1);
            decimal? lday03grow = item.Field<decimal?>("lday03_open") == 0 ? 0 : (item.Field<decimal?>("lday03_end") / item.Field<decimal?>("lday03_open") - 1);
            decimal? lday04grow = item.Field<decimal?>("lday04_open") == 0 ? 0 : (item.Field<decimal?>("lday04_end") / item.Field<decimal?>("lday04_open") - 1);
            decimal? lday05grow = item.Field<decimal?>("lday05_open") == 0 ? 0 : (item.Field<decimal?>("lday05_end") / item.Field<decimal?>("lday05_open") - 1);
            decimal? lday06grow = item.Field<decimal?>("lday06_open") == 0 ? 0 : (item.Field<decimal?>("lday06_end") / item.Field<decimal?>("lday06_open") - 1);
            decimal? lday07grow = item.Field<decimal?>("lday07_open") == 0 ? 0 : (item.Field<decimal?>("lday07_end") / item.Field<decimal?>("lday07_open") - 1);
            decimal? lday08grow = item.Field<decimal?>("lday08_open") == 0 ? 0 : (item.Field<decimal?>("lday08_end") / item.Field<decimal?>("lday08_open") - 1);
            decimal? lday09grow = item.Field<decimal?>("lday09_open") == 0 ? 0 : (item.Field<decimal?>("lday09_end") / item.Field<decimal?>("lday09_open") - 1);
            decimal? lday10grow = item.Field<decimal?>("lday10_open") == 0 ? 0 : (item.Field<decimal?>("lday10_end") / item.Field<decimal?>("lday10_open") - 1);
            decimal? lday11grow = item.Field<decimal?>("lday11_open") == 0 ? 0 : (item.Field<decimal?>("lday11_end") / item.Field<decimal?>("lday11_open") - 1);
            decimal? lday12grow = item.Field<decimal?>("lday12_open") == 0 ? 0 : (item.Field<decimal?>("lday12_end") / item.Field<decimal?>("lday12_open") - 1);
            decimal? lday13grow = item.Field<decimal?>("lday13_open") == 0 ? 0 : (item.Field<decimal?>("lday13_end") / item.Field<decimal?>("lday13_open") - 1);
            decimal? lday14grow = item.Field<decimal?>("lday14_open") == 0 ? 0 : (item.Field<decimal?>("lday14_end") / item.Field<decimal?>("lday14_open") - 1);

            decimal? lday01_vol = item.Field<decimal?>("lday01_vol");
            decimal? volumn = item.Field<decimal?>("volumn");



            decimal? magrow = nowprice / MA10 - 1;
            if (magrow.HasValue == false)
            {
                magrow = 0;
            }
            if (nowgrow > 0.03M
                && lday01grow > 0.03M
                && lday02grow < 0.015M
                              )
            {
                item.SetField<decimal?>("max20growday", nowgrow);
                item.SetField<decimal?>("max20growday_avg15", volumn / lday01_vol);
            }
            else
            {
                item.SetField<decimal?>("max20growday", -99.0M);
                item.SetField<decimal?>("max20growday_avg15", 0);
            }
        }
        private void BrwakDayDif(DataRow item)
        {

            decimal? nowprice = item.Field<decimal?>("highprice");
            decimal? minprice = item.Field<decimal?>("minprice");
            decimal? lday01_end = item.Field<decimal?>("lday01_high");
            Int32 nowbreak = 0;
            Int32 lday01break = 0;
            if (nowprice >= item.Field<decimal?>("lday01_high"))
            {
                nowbreak += 1;
            }
            else
            {

            }
            if (nowprice >= item.Field<decimal?>("lday02_high"))
            {
                nowbreak += 1;
            }
            else
            {

            }
            if (nowprice >= item.Field<decimal?>("lday03_high"))
            {
                nowbreak += 1;
            }
            else
            {

            }
            if (nowprice >= item.Field<decimal?>("lday04_high"))
            {
                nowbreak += 1;
            }
            else
            {

            }
            if (nowprice >= item.Field<decimal?>("lday05_high"))
            {
                nowbreak += 1;
            }
            else
            {

            }
            if (nowprice >= item.Field<decimal?>("lday06_high"))
            {
                nowbreak += 1;
            }
            else
            {

            }
            if (nowprice >= item.Field<decimal?>("lday07_high"))
            {
                nowbreak += 1;
            }
            else
            {

            }
            if (nowprice >= item.Field<decimal?>("lday08_high"))
            {
                nowbreak += 1;
            }
            else
            {

            }
            if (nowprice >= item.Field<decimal?>("lday09_high"))
            {
                nowbreak += 1;
            }
            else
            {

            }
            if (nowprice >= item.Field<decimal?>("lday10_high"))
            {
                nowbreak += 1;
            }
            else
            {

            }


            if (lday01_end >= item.Field<decimal?>("lday02_high"))
            {
                lday01break += 1;
            }
            else
            {

            }
            if (lday01_end >= item.Field<decimal?>("lday03_high"))
            {
                lday01break += 1;
            }
            else
            {

            }
            if (lday01_end >= item.Field<decimal?>("lday04_high"))
            {
                lday01break += 1;
            }
            else
            {

            }
            if (lday01_end >= item.Field<decimal?>("lday05_high"))
            {
                lday01break += 1;
            }
            else
            {

            }
            if (lday01_end >= item.Field<decimal?>("lday06_high"))
            {
                lday01break += 1;
            }
            else
            {

            }
            if (lday01_end >= item.Field<decimal?>("lday07_high"))
            {
                lday01break += 1;
            }
            else
            {

            }
            if (lday01_end >= item.Field<decimal?>("lday08_high"))
            {
                lday01break += 1;
            }
            else
            {

            }
            if (lday01_end >= item.Field<decimal?>("lday09_high"))
            {
                lday01break += 1;
            }
            else
            {

            }
            if (lday01_end >= item.Field<decimal?>("lday10_high"))
            {
                lday01break += 1;
            }
            else
            {

            }




            item.SetField<decimal?>("max20growday",
                 nowbreak - lday01break
                    //MA3bef==0?0:(((MA3- nowprice) - (MA3bef - nowpricebef))/ MA3bef)
                    );

            item.SetField<decimal?>("max20growday_avg15", nowbreak);

        }



        private void TodayReboundDivousHistory(DataRow item)
        {
            decimal? historyminprice = item.Field<decimal?>("minprice");
            decimal? minprice = item.Field<decimal?>("minprice");
            decimal? nowprice = item.Field<decimal?>("nowprice");

            decimal? todaygrow = (minprice == 0 ? 0 : (nowprice / minprice - 1));

            if (historyminprice > item.Field<decimal?>("lday01_min"))
            {
                historyminprice = item.Field<decimal?>("lday01_min");
            }
            if (historyminprice > item.Field<decimal?>("lday02_min"))
            {
                historyminprice = item.Field<decimal?>("lday02_min");
            }
            if (historyminprice > item.Field<decimal?>("lday03_min"))
            {
                historyminprice = item.Field<decimal?>("lday03_min");
            }
            if (historyminprice > item.Field<decimal?>("lday04_min"))
            {
                historyminprice = item.Field<decimal?>("lday04_min");
            }
            if (historyminprice > item.Field<decimal?>("lday05_min"))
            {
                historyminprice = item.Field<decimal?>("lday05_min");
            }
            if (historyminprice > item.Field<decimal?>("lday06_min"))
            {
                historyminprice = item.Field<decimal?>("lday06_min");
            }
            if (historyminprice > item.Field<decimal?>("lday07_min"))
            {
                historyminprice = item.Field<decimal?>("lday07_min");
            }
            if (historyminprice > item.Field<decimal?>("lday08_min"))
            {
                historyminprice = item.Field<decimal?>("lday08_min");
            }
            if (historyminprice > item.Field<decimal?>("lday09_min"))
            {
                historyminprice = item.Field<decimal?>("lday09_min");
            }
            if (historyminprice > item.Field<decimal?>("lday10_min"))
            {
                historyminprice = item.Field<decimal?>("lday10_min");
            }

            decimal? historygrow = (historyminprice == 0 ? 0 : (nowprice / historyminprice - 1));

            decimal? highprice = item.Field<decimal?>("highprice");

            decimal? highjump = (nowprice == 0 ? 0 : (highprice / nowprice - 1));

            item.SetField<decimal?>("max20growday",
                     todaygrow - historygrow
                        );

            item.SetField<decimal?>("max20growday_avg15",
                   highjump
                      );
        }


        private void SetDayGrowVEverHigh(DataRow item)
        {

            Boolean EverHigh = false;
            Int32 EverHighDay = 0;
            bool ShortLow = false;

            decimal? growtoday = item.Field<decimal?>("growtoday");

            decimal? test20open = item.Field<decimal?>("lday01_min");
            decimal? test20end = item.Field<decimal?>("lday01_end");


            if (test20open != 0 && test20end / test20open > 1.07M && EverHighDay == 0)
            {
                EverHigh = true;
                EverHighDay = 1;
            }
            test20open = item.Field<decimal?>("lday02_min");
            test20end = item.Field<decimal?>("lday02_end");


            if (test20open != 0 && test20end / test20open > 1.07M && EverHighDay == 0)
            {
                EverHigh = true;
                EverHighDay = 2;
            }
            test20open = item.Field<decimal?>("lday03_min");
            test20end = item.Field<decimal?>("lday03_end");


            if (test20open != 0 && test20end / test20open > 1.07M && EverHighDay == 0)
            {
                EverHigh = true;
                EverHighDay = 3;
            }
            test20open = item.Field<decimal?>("lday04_min");
            test20end = item.Field<decimal?>("lday04_end");


            if (test20open != 0 && test20end / test20open > 1.07M && EverHighDay == 0)
            {
                EverHigh = true;
                EverHighDay = 4;
            }


            test20open = item.Field<decimal?>("lday05_min");
            test20end = item.Field<decimal?>("lday05_end");


            if (test20open != 0 && test20end / test20open > 1.07M && EverHighDay == 0)
            {
                EverHigh = true;
                EverHighDay = 5;
            }

            test20open = item.Field<decimal?>("lday06_min");
            test20end = item.Field<decimal?>("lday06_end");


            if (test20open != 0 && test20end / test20open > 1.07M && EverHighDay == 0)
            {
                EverHigh = true;
                EverHighDay = 6;
            }

            test20open = item.Field<decimal?>("lday07_min");
            test20end = item.Field<decimal?>("lday07_end");


            if (test20open != 0 && test20end / test20open > 1.07M && EverHighDay == 0)
            {
                EverHigh = true;
                EverHighDay = 7;
            }
            test20open = item.Field<decimal?>("lday08_min");
            test20end = item.Field<decimal?>("lday08_end");


            if (test20open != 0 && test20end / test20open > 1.07M && EverHighDay == 0)
            {
                EverHigh = true;
                EverHighDay = 8;
            }

            test20open = item.Field<decimal?>("lday09_min");
            test20end = item.Field<decimal?>("lday09_end");


            if (test20open != 0 && test20end / test20open > 1.07M && EverHighDay == 0)
            {
                EverHigh = true;
                EverHighDay = 9;
            }

            test20open = item.Field<decimal?>("lday10_min");
            test20end = item.Field<decimal?>("lday10_end");


            if (test20open != 0 && test20end / test20open > 1.07M && EverHighDay == 0)
            {
                EverHigh = true;
                EverHighDay = 10;
            }

            switch (EverHighDay)
            {
                case 2:
                    if (item.Field<decimal?>("lday01_min") <= item.Field<decimal?>("minprice"))
                    {
                        ShortLow = true;
                    }
                    break;
                case 3:
                    if (
                         item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("minprice")
                        && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday01_min")
                        )
                    {
                        ShortLow = true;
                    }
                    break;
                case 4:
                    if (
                        item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("minprice")
                       && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday01_min")
                       && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday03_min")
                      )
                    {
                        ShortLow = true;
                    }
                    break;
                case 5:
                    if (
                        item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("minprice")
                       && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday01_min")
                       && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday03_min")
                       && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday04_min")
                      )
                    {
                        ShortLow = true;
                    }
                    break;
                case 6:
                    if (
                      item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("minprice")
                     && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday01_min")
                     && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday03_min")
                     && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday04_min")
                      && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday05_min")
                    )
                    {
                        ShortLow = true;
                    }
                    break;
                case 7:
                    if (
                    item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("minprice")
                   && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday01_min")
                   && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday03_min")
                   && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday04_min")
                    && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday05_min")
                      && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday06_min")
                  )
                    {
                        ShortLow = true;
                    }
                    break;
                case 8:
                    if (
                  item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("minprice")
                 && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday01_min")
                 && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday03_min")
                 && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday04_min")
                  && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday05_min")
                    && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday06_min")
                     && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday07_min")
                )
                    {
                        ShortLow = true;
                    }
                    break;
                case 9:
                    if (
               item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("minprice")
              && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday01_min")
              && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday03_min")
              && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday04_min")
              && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday05_min")
              && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday06_min")
              && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday07_min")
              && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday08_min")
             )
                    {
                        ShortLow = true;
                    }
                    break;
                case 10:
                    if (
            item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("minprice")
           && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday01_min")
           && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday03_min")
           && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday04_min")
           && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday05_min")
           && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday06_min")
           && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday07_min")
           && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday08_min")
           && item.Field<decimal?>("lday02_min") <= item.Field<decimal?>("lday09_min")
          )
                    {
                        ShortLow = true;
                    }
                    break;
                default:
                    break;
            }





            item.SetField<decimal?>("max20growday", (EverHigh && ShortLow) == true ? growtoday : 0);
            item.SetField<bool>("ShortLow", ShortLow);
        }
        private void SetDayGrowTurnAndStrong(DataRow item)
        {
            Decimal? TurnUp = 0;
            Decimal? TurnDown = 0;


            String PreDirect = "";
            Int32 TurnQty = 0;




            decimal? test20open = item.Field<decimal?>("lday01_open");
            decimal? test20end = item.Field<decimal?>("lday01_end");

            if (test20end > test20open)
            {
                if (PreDirect != "ASC")
                {
                    PreDirect = "ASC";
                    TurnQty += 1;
                }
                TurnUp += test20open == 0 ? 0 : (test20end / test20open - 1);
            }
            else if (test20end < test20open)
            {
                if (PreDirect != "DESC")
                {
                    PreDirect = "DESC";
                    TurnQty += 1;
                }
                TurnDown += test20open == 0 ? 0 : (test20end / test20open - 1);
            }
            if (TurnQty == 3)
            {
                goto EndDoing;
            }
            test20open = item.Field<decimal?>("lday02_open");
            test20end = item.Field<decimal?>("lday02_end");




            if (test20end > test20open)
            {
                if (PreDirect != "ASC")
                {
                    PreDirect = "ASC";
                    TurnQty += 1;
                }
                TurnUp += test20open == 0 ? 0 : (test20end / test20open - 1);
            }
            else if (test20end < test20open)
            {
                if (PreDirect != "DESC")
                {
                    PreDirect = "DESC";
                    TurnQty += 1;
                }
                TurnDown += test20open == 0 ? 0 : (test20end / test20open - 1);
            }
            if (TurnQty == 3)
            {
                goto EndDoing;
            }
            test20open = item.Field<decimal?>("lday03_open");
            test20end = item.Field<decimal?>("lday03_end");




            if (test20end > test20open)
            {
                if (PreDirect != "ASC")
                {
                    PreDirect = "ASC";
                    TurnQty += 1;
                }
                TurnUp += test20open == 0 ? 0 : (test20end / test20open - 1);
            }
            else if (test20end < test20open)
            {
                if (PreDirect != "DESC")
                {
                    PreDirect = "DESC";
                    TurnQty += 1;
                }
                TurnDown += test20open == 0 ? 0 : (test20end / test20open - 1);
            }
            if (TurnQty == 3)
            {
                goto EndDoing;
            }
            test20open = item.Field<decimal?>("lday04_open");
            test20end = item.Field<decimal?>("lday04_end");




            if (test20end > test20open)
            {
                if (PreDirect != "ASC")
                {
                    PreDirect = "ASC";
                    TurnQty += 1;
                }
                TurnUp += test20open == 0 ? 0 : (test20end / test20open - 1);
            }
            else if (test20end < test20open)
            {
                if (PreDirect != "DESC")
                {
                    PreDirect = "DESC";
                    TurnQty += 1;
                }
                TurnDown += test20open == 0 ? 0 : (test20end / test20open - 1);
            }
            if (TurnQty == 3)
            {
                goto EndDoing;
            }


            test20open = item.Field<decimal?>("lday05_open");
            test20end = item.Field<decimal?>("lday05_end");




            if (test20end > test20open)
            {
                if (PreDirect != "ASC")
                {
                    PreDirect = "ASC";
                    TurnQty += 1;
                }
                TurnUp += test20open == 0 ? 0 : (test20end / test20open - 1);
            }
            else if (test20end < test20open)
            {
                if (PreDirect != "DESC")
                {
                    PreDirect = "DESC";
                    TurnQty += 1;
                }
                TurnDown += test20open == 0 ? 0 : (test20end / test20open - 1);
            }
            if (TurnQty == 3)
            {
                goto EndDoing;
            }

            test20open = item.Field<decimal?>("lday06_open");
            test20end = item.Field<decimal?>("lday06_end");



            if (test20end > test20open)
            {
                if (PreDirect != "ASC")
                {
                    PreDirect = "ASC";
                    TurnQty += 1;
                }
                TurnUp += test20open == 0 ? 0 : (test20end / test20open - 1);
            }
            else if (test20end < test20open)
            {
                if (PreDirect != "DESC")
                {
                    PreDirect = "DESC";
                    TurnQty += 1;
                }
                TurnDown += test20open == 0 ? 0 : (test20end / test20open - 1);
            }
            if (TurnQty == 3)
            {
                goto EndDoing;
            }

            test20open = item.Field<decimal?>("lday07_open");
            test20end = item.Field<decimal?>("lday07_end");




            if (test20end > test20open)
            {
                if (PreDirect != "ASC")
                {
                    PreDirect = "ASC";
                    TurnQty += 1;
                }
                TurnUp += test20open == 0 ? 0 : (test20end / test20open - 1);
            }
            else if (test20end < test20open)
            {
                if (PreDirect != "DESC")
                {
                    PreDirect = "DESC";
                    TurnQty += 1;
                }
                TurnDown += test20open == 0 ? 0 : (test20end / test20open - 1);
            }
            if (TurnQty == 3)
            {
                goto EndDoing;
            }
            test20open = item.Field<decimal?>("lday08_open");
            test20end = item.Field<decimal?>("lday08_end");




            if (test20end > test20open)
            {
                if (PreDirect != "ASC")
                {
                    PreDirect = "ASC";
                    TurnQty += 1;
                }
                TurnUp += test20open == 0 ? 0 : (test20end / test20open - 1);
            }
            else if (test20end < test20open)
            {
                if (PreDirect != "DESC")
                {
                    PreDirect = "DESC";
                    TurnQty += 1;
                }
                TurnDown += test20open == 0 ? 0 : (test20end / test20open - 1);
            }
            if (TurnQty == 3)
            {
                goto EndDoing;
            }

            test20open = item.Field<decimal?>("lday09_open");
            test20end = item.Field<decimal?>("lday09_end");




            if (test20end > test20open)
            {
                if (PreDirect != "ASC")
                {
                    PreDirect = "ASC";
                    TurnQty += 1;
                }
                TurnUp += test20open == 0 ? 0 : (test20end / test20open - 1);
            }
            else if (test20end < test20open)
            {
                if (PreDirect != "DESC")
                {
                    PreDirect = "DESC";
                    TurnQty += 1;
                }
                TurnDown += test20open == 0 ? 0 : (test20end / test20open - 1);
            }
            if (TurnQty == 3)
            {
                goto EndDoing;
            }

            test20open = item.Field<decimal?>("lday10_open");
            test20end = item.Field<decimal?>("lday10_end");




            if (test20end > test20open)
            {
                if (PreDirect != "ASC")
                {
                    PreDirect = "ASC";
                    TurnQty += 1;
                }
                TurnUp += test20open == 0 ? 0 : (test20end / test20open - 1);
            }
            else if (test20end < test20open)
            {
                if (PreDirect != "DESC")
                {
                    PreDirect = "DESC";
                    TurnQty += 1;
                }
                TurnDown += test20open == 0 ? 0 : (test20end / test20open - 1);
            }
            if (TurnQty == 3)
            {
                goto EndDoing;
            }



        EndDoing:



            item.SetField<decimal?>("max20growday", TurnDown + TurnUp);

        }
        private void SetDayGrowDoubleSuper(DataRow item)
        {

            Int32 Superqty = 0;


            decimal? openprice = item.Field<decimal?>("lday01_end");
            decimal? nowprice = item.Field<decimal?>("nowprice");

            decimal? test20open = item.Field<decimal?>("lday02_end");
            decimal? test20end = item.Field<decimal?>("lday01_end");
            Int32 HighIndex = 0;
            if ((test20open != 0) && (test20end / test20open >= 1.06M))
            {
                Superqty += 1;
                HighIndex = 1;
            }
            test20open = item.Field<decimal?>("lday03_end");
            test20end = item.Field<decimal?>("lday02_end");

            if ((test20open != 0) && (test20end / test20open >= 1.06M) && HighIndex <= 0)
            {
                Superqty += 1;
                HighIndex = 2;
            }
            test20open = item.Field<decimal?>("lday04_end");
            test20end = item.Field<decimal?>("lday03_end");

            if ((test20open != 0) && (test20end / test20open >= 1.06M) && HighIndex <= 0)
            {
                Superqty += 1;
            }
            test20open = item.Field<decimal?>("lday05_end");
            test20end = item.Field<decimal?>("lday04_end");

            if ((test20open != 0) && (test20end / test20open >= 1.06M) && HighIndex <= 1)
            {
                Superqty += 1;
                HighIndex = 3;
            }

            test20open = item.Field<decimal?>("lday06_end");
            test20end = item.Field<decimal?>("lday05_end");

            if ((test20open != 0) && (test20end / test20open >= 1.06M) && HighIndex <= 2)
            {
                Superqty += 1;
                HighIndex = 4;
            }
            test20open = item.Field<decimal?>("lday07_end");
            test20end = item.Field<decimal?>("lday06_end");

            if ((test20open != 0) && (test20end / test20open >= 1.06M) && HighIndex <= 3)
            {
                Superqty += 1;
                HighIndex = 5;
            }
            test20open = item.Field<decimal?>("lday08_end");
            test20end = item.Field<decimal?>("lday07_end");





            if (Superqty == 1 && (openprice != 0) && (nowprice / openprice >= 1.05M))
            {
                item.SetField<decimal?>("max20growday", (nowprice / openprice - 1));
            }
            else
            {
                item.SetField<decimal?>("max20growday", 0);
            }


        }
        private void SetDayGrowTwoDaySuper(DataRow item)
        {
            decimal? nowprice = item.Field<decimal?>("nowprice");
            decimal? lday02_end = item.Field<decimal?>("lday02_end");

            decimal? lday04_end = item.Field<decimal?>("lday04_end");

            decimal? highprice = item.Field<decimal?>("highprice");


            item.SetField<decimal?>("max20growday",
                (lday02_end == 0 || lday04_end == 0 || nowprice == 0) ? 0 : (
                (nowprice / lday02_end - 1)
                - (lday02_end / lday04_end - 1) / 2
                - (highprice / nowprice - 1)

                )
                );




        }

        private void SetDayGrowFirstSuper(DataRow item)
        {

            Int32 Superqty = 0;


            decimal? openprice = item.Field<decimal?>("lday01_end");
            decimal? nowprice = item.Field<decimal?>("nowprice");
            decimal? minprice = item.Field<decimal?>("minprice");


            decimal? test20open = item.Field<decimal?>("lday02_end");
            decimal? test20end = item.Field<decimal?>("lday01_end");
            Int32 HighIndex = 0;
            if ((test20open != 0) && (test20end / test20open >= 1.06M))
            {
                Superqty += 1;
                HighIndex = 1;
            }
            test20open = item.Field<decimal?>("lday03_end");
            test20end = item.Field<decimal?>("lday02_end");

            if ((test20open != 0) && (test20end / test20open >= 1.06M) && HighIndex <= 0)
            {
                Superqty += 1;
                HighIndex = 2;
            }
            test20open = item.Field<decimal?>("lday04_end");
            test20end = item.Field<decimal?>("lday03_end");

            if ((test20open != 0) && (test20end / test20open >= 1.06M) && HighIndex <= 0)
            {
                Superqty += 1;
            }
            test20open = item.Field<decimal?>("lday05_end");
            test20end = item.Field<decimal?>("lday04_end");

            if ((test20open != 0) && (test20end / test20open >= 1.06M) && HighIndex <= 1)
            {
                Superqty += 1;
                HighIndex = 3;
            }

            test20open = item.Field<decimal?>("lday06_end");
            test20end = item.Field<decimal?>("lday05_end");

            if ((test20open != 0) && (test20end / test20open >= 1.06M) && HighIndex <= 2)
            {
                Superqty += 1;
                HighIndex = 4;
            }
            test20open = item.Field<decimal?>("lday07_end");
            test20end = item.Field<decimal?>("lday06_end");

            if ((test20open != 0) && (test20end / test20open >= 1.06M) && HighIndex <= 3)
            {
                Superqty += 1;
                HighIndex = 5;
            }
            test20open = item.Field<decimal?>("lday08_end");
            test20end = item.Field<decimal?>("lday07_end");

            if ((test20open != 0) && (test20end / test20open >= 1.06M) && HighIndex <= 4)
            {
                Superqty += 1;
                HighIndex = 6;
            }
            test20open = item.Field<decimal?>("lday09_end");
            test20end = item.Field<decimal?>("lday08_end");

            if ((test20open != 0) && (test20end / test20open >= 1.06M) && HighIndex <= 5)
            {
                Superqty += 1;
                HighIndex = 7;
            }
            test20open = item.Field<decimal?>("lday10_end");
            test20end = item.Field<decimal?>("lday09_end");

            if ((test20open != 0) && (test20end / test20open >= 1.06M) && HighIndex <= 6)
            {
                Superqty += 1;
                HighIndex = 8;
            }

            test20open = item.Field<decimal?>("lday11_end");
            test20end = item.Field<decimal?>("lday10_end");

            if ((test20open != 0) && (test20end / test20open >= 1.06M) && HighIndex <= 7)
            {
                Superqty += 1;
                HighIndex = 9;
            }

            if (Superqty == 0 && (minprice != 0) && (nowprice / minprice >= 1.05M))
            {
                item.SetField<decimal?>("max20growday", (nowprice / minprice - 1));
            }
            else
            {
                item.SetField<decimal?>("max20growday", 0);
            }


        }

        private void SetDayGrowYtdSuper(DataRow item)
        {




            decimal? lday01_end = item.Field<decimal?>("lday01_end");
            decimal? lday02_end = item.Field<decimal?>("lday02_end");
            decimal? lday03_end = item.Field<decimal?>("lday03_end");
            decimal? lday01_high = item.Field<decimal?>("lday01_high");

            decimal? nowprice = item.Field<decimal?>("nowprice");

            decimal? minprice = item.Field<decimal?>("minprice");
            decimal? highprice = item.Field<decimal?>("highprice");

            decimal? testprice = highprice;
            if (testprice <= lday01_high)
            {
                testprice = lday01_high;
            }



            if (
                (lday01_end != 0)
                && (lday02_end != 0)
                && (nowprice != 0)
                && (lday01_end / lday02_end >= 1.035M)
                && (highprice >= lday01_high)
                )
            {
                item.SetField<decimal?>("max20growday", (testprice / nowprice - 1));
            }
            else
            {
                item.SetField<decimal?>("max20growday", 999);
            }


        }

        private void SetDayGrowSuperLowRatio(DataRow item)
        {




            decimal? lday01_end = item.Field<decimal?>("lday01_end");
            decimal? lday02_end = item.Field<decimal?>("lday02_end");
            decimal? lday03_end = item.Field<decimal?>("lday03_end");

            decimal? nowprice = item.Field<decimal?>("nowprice");


            decimal? lday01_vol = item.Field<decimal?>("lday01_vol");
            decimal? lday02_vol = item.Field<decimal?>("lday02_vol");
            decimal? lday03_vol = item.Field<decimal?>("lday03_vol");

            decimal? volumn = item.Field<decimal?>("volumn");




            if (
                lday01_vol != 0 && lday01_end != 0 && nowprice / lday02_end >= 1.03M
                )
            {
                item.SetField<decimal?>("max20growday", nowprice / lday02_end * lday02_vol / volumn - 1);
            }
            else
            {
                item.SetField<decimal?>("max20growday", 0);
            }


        }
        private void SetDayGrowMaValue(DataRow item)
        {




            decimal? openprice = item.Field<decimal?>("lday01_end");
            decimal? nowprice = item.Field<decimal?>("nowprice");
            decimal? minprice = item.Field<decimal?>("minprice");

            decimal? lday01_min = item.Field<decimal?>("lday01_min");
            decimal? lday01_end = item.Field<decimal?>("lday01_end");
            decimal? lday02_min = item.Field<decimal?>("lday02_min");
            decimal? lday02_end = item.Field<decimal?>("lday02_end");
            decimal? lday03_min = item.Field<decimal?>("lday03_min");
            decimal? lday03_end = item.Field<decimal?>("lday03_end");
            decimal? lday04_min = item.Field<decimal?>("lday04_min");
            decimal? lday04_end = item.Field<decimal?>("lday04_end");
            decimal? lday05_min = item.Field<decimal?>("lday05_min");
            decimal? lday05_end = item.Field<decimal?>("lday05_end");
            decimal? lday06_min = item.Field<decimal?>("lday06_min");
            decimal? lday06_end = item.Field<decimal?>("lday06_end");
            decimal? lday07_min = item.Field<decimal?>("lday07_min");
            decimal? lday07_end = item.Field<decimal?>("lday07_end");
            decimal? lday08_min = item.Field<decimal?>("lday08_min");
            decimal? lday08_end = item.Field<decimal?>("lday08_end");
            decimal? lday09_min = item.Field<decimal?>("lday09_min");
            decimal? lday09_end = item.Field<decimal?>("lday09_end");
            decimal? lday10_min = item.Field<decimal?>("lday10_min");
            decimal? lday10_end = item.Field<decimal?>("lday10_end");
            decimal? lday11_min = item.Field<decimal?>("lday11_min");
            decimal? lday11_end = item.Field<decimal?>("lday11_end");
            decimal? lday12_min = item.Field<decimal?>("lday12_min");
            decimal? lday12_end = item.Field<decimal?>("lday12_end");
            decimal? lday13_min = item.Field<decimal?>("lday13_min");
            decimal? lday13_end = item.Field<decimal?>("lday13_end");
            decimal? lday14_min = item.Field<decimal?>("lday14_min");
            decimal? lday14_end = item.Field<decimal?>("lday14_end");
            decimal? lday15_min = item.Field<decimal?>("lday15_min");
            decimal? lday15_end = item.Field<decimal?>("lday15_end");
            decimal? lday16_min = item.Field<decimal?>("lday16_min");
            decimal? lday16_end = item.Field<decimal?>("lday16_end");
            decimal? lday17_min = item.Field<decimal?>("lday17_min");
            decimal? lday17_end = item.Field<decimal?>("lday17_end");
            decimal? lday18_min = item.Field<decimal?>("lday18_min");
            decimal? lday18_end = item.Field<decimal?>("lday18_end");
            decimal? lday19_min = item.Field<decimal?>("lday19_min");
            decimal? lday19_end = item.Field<decimal?>("lday19_end");
            decimal? lday20_min = item.Field<decimal?>("lday20_min");
            decimal? lday20_end = item.Field<decimal?>("lday20_end");

            decimal? lday01_high = item.Field<decimal?>("lday01_high");
            decimal? lday02_high = item.Field<decimal?>("lday02_high");
            decimal? lday03_high = item.Field<decimal?>("lday03_high");
            decimal? lday04_high = item.Field<decimal?>("lday04_high");
            decimal? lday05_high = item.Field<decimal?>("lday05_high");
            decimal? lday06_high = item.Field<decimal?>("lday06_high");
            decimal? lday07_high = item.Field<decimal?>("lday07_high");
            decimal? lday08_high = item.Field<decimal?>("lday08_high");
            decimal? lday09_high = item.Field<decimal?>("lday09_high");
            decimal? lday10_high = item.Field<decimal?>("lday10_high");
            decimal? lday11_high = item.Field<decimal?>("lday11_high");
            decimal? lday12_high = item.Field<decimal?>("lday12_high");
            decimal? lday13_high = item.Field<decimal?>("lday13_high");
            decimal? lday14_high = item.Field<decimal?>("lday14_high");
            decimal? lday15_high = item.Field<decimal?>("lday15_high");
            decimal? lday16_high = item.Field<decimal?>("lday16_high");
            decimal? lday17_high = item.Field<decimal?>("lday17_high");
            decimal? lday18_high = item.Field<decimal?>("lday18_high");
            decimal? lday19_high = item.Field<decimal?>("lday19_high");
            decimal? lday20_high = item.Field<decimal?>("lday20_high");

            decimal? lday01_vol = item.Field<decimal?>("lday01_vol");
            decimal? lday02_vol = item.Field<decimal?>("lday02_vol");
            decimal? lday03_vol = item.Field<decimal?>("lday03_vol");
            decimal? lday04_vol = item.Field<decimal?>("lday04_vol");
            decimal? lday05_vol = item.Field<decimal?>("lday05_vol");
            decimal? lday06_vol = item.Field<decimal?>("lday06_vol");
            decimal? lday07_vol = item.Field<decimal?>("lday07_vol");
            decimal? lday08_vol = item.Field<decimal?>("lday08_vol");
            decimal? lday09_vol = item.Field<decimal?>("lday09_vol");
            decimal? lday10_vol = item.Field<decimal?>("lday10_vol");
            decimal? lday11_vol = item.Field<decimal?>("lday11_vol");
            decimal? lday12_vol = item.Field<decimal?>("lday12_vol");
            decimal? lday13_vol = item.Field<decimal?>("lday13_vol");
            decimal? lday14_vol = item.Field<decimal?>("lday14_vol");
            decimal? lday15_vol = item.Field<decimal?>("lday15_vol");
            decimal? lday16_vol = item.Field<decimal?>("lday16_vol");
            decimal? lday17_vol = item.Field<decimal?>("lday17_vol");
            decimal? lday18_vol = item.Field<decimal?>("lday18_vol");
            decimal? lday19_vol = item.Field<decimal?>("lday19_vol");
            decimal? lday20_vol = item.Field<decimal?>("lday20_vol");

            try
            {
                decimal? F5MA15 = lday05_end + lday06_end + lday07_end + lday08_end + lday09_end
                               + lday10_end + lday11_end + lday12_end + lday13_end + lday14_end
                               + lday15_end + lday16_end + lday17_end + lday18_end + lday19_end;
                F5MA15 = (lday05_end - F5MA15 / 15) / lday05_end;

                decimal? F0MA15 = nowprice + lday01_end + lday02_end + lday03_end + lday04_end
                   + lday05_end + lday06_end + lday07_end + lday08_end + lday09_end
                   + lday10_end + lday11_end + lday12_end + lday13_end + lday14_end;

                F0MA15 = (nowprice - F0MA15 / 15) / nowprice;


                //F0MA15 = F5MA15+ F0MA15;

                item.SetField<decimal?>("max20growday", Math.Abs(F0MA15.Value));
                item.SetField<decimal?>("logbreakqty", (nowprice / minprice - 1) - Math.Abs((nowprice / openprice - 1).Value));
            }
            catch (Exception)
            {
                item.SetField<decimal?>("max20growday", 0);

            }



        }

        private void SetDayGrowAfterSuperLittleChange(DataRow item)
        {




            decimal? openprice = item.Field<decimal?>("lday01_end");
            decimal? nowprice = item.Field<decimal?>("nowprice");

            decimal? lday01_min = item.Field<decimal?>("lday01_min");
            decimal? lday01_end = item.Field<decimal?>("lday01_end");
            decimal? lday02_min = item.Field<decimal?>("lday02_min");
            decimal? lday02_end = item.Field<decimal?>("lday02_end");
            decimal? lday03_min = item.Field<decimal?>("lday03_min");
            decimal? lday03_end = item.Field<decimal?>("lday03_end");
            decimal? lday04_min = item.Field<decimal?>("lday04_min");
            decimal? lday04_end = item.Field<decimal?>("lday04_end");
            decimal? lday05_min = item.Field<decimal?>("lday05_min");
            decimal? lday05_end = item.Field<decimal?>("lday05_end");
            decimal? lday06_min = item.Field<decimal?>("lday06_min");
            decimal? lday06_end = item.Field<decimal?>("lday06_end");
            decimal? lday07_min = item.Field<decimal?>("lday07_min");
            decimal? lday07_end = item.Field<decimal?>("lday07_end");
            decimal? lday08_min = item.Field<decimal?>("lday08_min");
            decimal? lday08_end = item.Field<decimal?>("lday08_end");
            decimal? lday09_min = item.Field<decimal?>("lday09_min");
            decimal? lday09_end = item.Field<decimal?>("lday09_end");
            decimal? lday10_min = item.Field<decimal?>("lday10_min");
            decimal? lday10_end = item.Field<decimal?>("lday10_end");
            decimal? lday11_min = item.Field<decimal?>("lday11_min");
            decimal? lday11_end = item.Field<decimal?>("lday11_end");
            decimal? lday12_min = item.Field<decimal?>("lday12_min");
            decimal? lday12_end = item.Field<decimal?>("lday12_end");
            decimal? lday13_min = item.Field<decimal?>("lday13_min");
            decimal? lday13_end = item.Field<decimal?>("lday13_end");
            decimal? lday14_min = item.Field<decimal?>("lday14_min");
            decimal? lday14_end = item.Field<decimal?>("lday14_end");
            decimal? lday15_min = item.Field<decimal?>("lday15_min");
            decimal? lday15_end = item.Field<decimal?>("lday15_end");
            decimal? lday16_min = item.Field<decimal?>("lday16_min");
            decimal? lday16_end = item.Field<decimal?>("lday16_end");
            decimal? lday17_min = item.Field<decimal?>("lday17_min");
            decimal? lday17_end = item.Field<decimal?>("lday17_end");
            decimal? lday18_min = item.Field<decimal?>("lday18_min");
            decimal? lday18_end = item.Field<decimal?>("lday18_end");
            decimal? lday19_min = item.Field<decimal?>("lday19_min");
            decimal? lday19_end = item.Field<decimal?>("lday19_end");
            decimal? lday20_min = item.Field<decimal?>("lday20_min");
            decimal? lday20_end = item.Field<decimal?>("lday20_end");

            decimal? lday01_high = item.Field<decimal?>("lday01_high");
            decimal? lday02_high = item.Field<decimal?>("lday02_high");
            decimal? lday03_high = item.Field<decimal?>("lday03_high");
            decimal? lday04_high = item.Field<decimal?>("lday04_high");
            decimal? lday05_high = item.Field<decimal?>("lday05_high");
            decimal? lday06_high = item.Field<decimal?>("lday06_high");
            decimal? lday07_high = item.Field<decimal?>("lday07_high");
            decimal? lday08_high = item.Field<decimal?>("lday08_high");
            decimal? lday09_high = item.Field<decimal?>("lday09_high");
            decimal? lday10_high = item.Field<decimal?>("lday10_high");
            decimal? lday11_high = item.Field<decimal?>("lday11_high");
            decimal? lday12_high = item.Field<decimal?>("lday12_high");
            decimal? lday13_high = item.Field<decimal?>("lday13_high");
            decimal? lday14_high = item.Field<decimal?>("lday14_high");
            decimal? lday15_high = item.Field<decimal?>("lday15_high");
            decimal? lday16_high = item.Field<decimal?>("lday16_high");
            decimal? lday17_high = item.Field<decimal?>("lday17_high");
            decimal? lday18_high = item.Field<decimal?>("lday18_high");
            decimal? lday19_high = item.Field<decimal?>("lday19_high");
            decimal? lday20_high = item.Field<decimal?>("lday20_high");

            decimal? lday01_vol = item.Field<decimal?>("lday01_vol");
            decimal? lday02_vol = item.Field<decimal?>("lday02_vol");
            decimal? lday03_vol = item.Field<decimal?>("lday03_vol");
            decimal? lday04_vol = item.Field<decimal?>("lday04_vol");
            decimal? lday05_vol = item.Field<decimal?>("lday05_vol");
            decimal? lday06_vol = item.Field<decimal?>("lday06_vol");
            decimal? lday07_vol = item.Field<decimal?>("lday07_vol");
            decimal? lday08_vol = item.Field<decimal?>("lday08_vol");
            decimal? lday09_vol = item.Field<decimal?>("lday09_vol");
            decimal? lday10_vol = item.Field<decimal?>("lday10_vol");
            decimal? lday11_vol = item.Field<decimal?>("lday11_vol");
            decimal? lday12_vol = item.Field<decimal?>("lday12_vol");
            decimal? lday13_vol = item.Field<decimal?>("lday13_vol");
            decimal? lday14_vol = item.Field<decimal?>("lday14_vol");
            decimal? lday15_vol = item.Field<decimal?>("lday15_vol");
            decimal? lday16_vol = item.Field<decimal?>("lday16_vol");
            decimal? lday17_vol = item.Field<decimal?>("lday17_vol");
            decimal? lday18_vol = item.Field<decimal?>("lday18_vol");
            decimal? lday19_vol = item.Field<decimal?>("lday19_vol");
            decimal? lday20_vol = item.Field<decimal?>("lday20_vol");


            decimal? test20open = item.Field<decimal?>("lday02_end");
            decimal? test20end = item.Field<decimal?>("lday01_high");

            item.SetField<decimal?>("max20growday_avg15", 9999
                  );

            if ((test20open != 0) && (test20end / test20open >= 1.06M))
            {
                item.SetField<decimal?>("max20growday_avg15", Math.Abs(((nowprice == 0) ? 0 : (test20end / nowprice - 1)).Value)
                    );

            }
            test20open = item.Field<decimal?>("lday03_end");
            test20end = item.Field<decimal?>("lday02_high");

            if ((test20open != 0) && (test20end / test20open >= 1.06M))
            {
                item.SetField<decimal?>("max20growday_avg15", Math.Abs(((nowprice == 0) ? 0 : (test20end / nowprice - 1)).Value)
    );

            }
            test20open = item.Field<decimal?>("lday04_end");
            test20end = item.Field<decimal?>("lday03_high");

            if ((test20open != 0) && (test20end / test20open >= 1.06M))
            {
                item.SetField<decimal?>("max20growday_avg15", Math.Abs(((nowprice == 0) ? 0 : (test20end / nowprice - 1)).Value)
                        );
            }
            test20open = item.Field<decimal?>("lday05_end");
            test20end = item.Field<decimal?>("lday04_high");

            if ((test20open != 0) && (test20end / test20open >= 1.06M))
            {
                item.SetField<decimal?>("max20growday_avg15", Math.Abs(((nowprice == 0) ? 0 : (test20end / nowprice - 1)).Value)
            );
            }

            test20open = item.Field<decimal?>("lday06_end");
            test20end = item.Field<decimal?>("lday05_high");

            if ((test20open != 0) && (test20end / test20open >= 1.06M))
            {
                item.SetField<decimal?>("max20growday_avg15", Math.Abs(((nowprice == 0) ? 0 : (test20end / nowprice - 1)).Value)

    );
            }
            test20open = item.Field<decimal?>("lday07_end");
            test20end = item.Field<decimal?>("lday06_high");

            if ((test20open != 0) && (test20end / test20open >= 1.06M))
            {
                item.SetField<decimal?>("max20growday_avg15", Math.Abs(((nowprice == 0) ? 0 : (test20end / nowprice - 1)).Value)

    );

            }
            test20open = item.Field<decimal?>("lday08_end");
            test20end = item.Field<decimal?>("lday07_high");

            if ((test20open != 0) && (test20end / test20open >= 1.06M))
            {
                item.SetField<decimal?>("max20growday_avg15", Math.Abs(((nowprice == 0) ? 0 : (test20end / nowprice - 1)).Value)
                        );

            }
            test20open = item.Field<decimal?>("lday09_end");
            test20end = item.Field<decimal?>("lday08_high");

            if ((test20open != 0) && (test20end / test20open >= 1.06M))
            {
                item.SetField<decimal?>("max20growday_avg15", Math.Abs(((nowprice == 0) ? 0 : (test20end / nowprice - 1)).Value)
                        );

            }
            test20open = item.Field<decimal?>("lday10_end");
            test20end = item.Field<decimal?>("lday09_high");

            if ((test20open != 0) && (test20end / test20open >= 1.06M))
            {
                item.SetField<decimal?>("max20growday_avg15", Math.Abs(((nowprice == 0) ? 0 : (test20end / nowprice - 1)).Value)
                        );


            }

            test20open = item.Field<decimal?>("lday11_end");
            test20end = item.Field<decimal?>("lday10_high");

            if ((test20open != 0) && (test20end / test20open >= 1.06M))
            {
                item.SetField<decimal?>("max20growday_avg15", Math.Abs(((nowprice == 0) ? 0 : (test20end / nowprice - 1)).Value)
                        );

            }



        }

        private void SetDayGrowDeltaMaDif(DataRow item)
        {




            decimal? openprice = item.Field<decimal?>("lday01_end");
            decimal? nowprice = item.Field<decimal?>("nowprice");

            decimal? lday01_min = item.Field<decimal?>("lday01_min");
            decimal? lday01_end = item.Field<decimal?>("lday01_end");
            decimal? lday02_min = item.Field<decimal?>("lday02_min");
            decimal? lday02_end = item.Field<decimal?>("lday02_end");
            decimal? lday03_min = item.Field<decimal?>("lday03_min");
            decimal? lday03_end = item.Field<decimal?>("lday03_end");
            decimal? lday04_min = item.Field<decimal?>("lday04_min");
            decimal? lday04_end = item.Field<decimal?>("lday04_end");
            decimal? lday05_min = item.Field<decimal?>("lday05_min");
            decimal? lday05_end = item.Field<decimal?>("lday05_end");
            decimal? lday06_min = item.Field<decimal?>("lday06_min");
            decimal? lday06_end = item.Field<decimal?>("lday06_end");
            decimal? lday07_min = item.Field<decimal?>("lday07_min");
            decimal? lday07_end = item.Field<decimal?>("lday07_end");
            decimal? lday08_min = item.Field<decimal?>("lday08_min");
            decimal? lday08_end = item.Field<decimal?>("lday08_end");
            decimal? lday09_min = item.Field<decimal?>("lday09_min");
            decimal? lday09_end = item.Field<decimal?>("lday09_end");
            decimal? lday10_min = item.Field<decimal?>("lday10_min");
            decimal? lday10_end = item.Field<decimal?>("lday10_end");
            decimal? lday11_min = item.Field<decimal?>("lday11_min");
            decimal? lday11_end = item.Field<decimal?>("lday11_end");
            decimal? lday12_min = item.Field<decimal?>("lday12_min");
            decimal? lday12_end = item.Field<decimal?>("lday12_end");
            decimal? lday13_min = item.Field<decimal?>("lday13_min");
            decimal? lday13_end = item.Field<decimal?>("lday13_end");
            decimal? lday14_min = item.Field<decimal?>("lday14_min");
            decimal? lday14_end = item.Field<decimal?>("lday14_end");
            decimal? lday15_min = item.Field<decimal?>("lday15_min");
            decimal? lday15_end = item.Field<decimal?>("lday15_end");
            decimal? lday16_min = item.Field<decimal?>("lday16_min");
            decimal? lday16_end = item.Field<decimal?>("lday16_end");
            decimal? lday17_min = item.Field<decimal?>("lday17_min");
            decimal? lday17_end = item.Field<decimal?>("lday17_end");
            decimal? lday18_min = item.Field<decimal?>("lday18_min");
            decimal? lday18_end = item.Field<decimal?>("lday18_end");
            decimal? lday19_min = item.Field<decimal?>("lday19_min");
            decimal? lday19_end = item.Field<decimal?>("lday19_end");
            decimal? lday20_min = item.Field<decimal?>("lday20_min");
            decimal? lday20_end = item.Field<decimal?>("lday20_end");

            decimal? lday01_high = item.Field<decimal?>("lday01_high");
            decimal? lday02_high = item.Field<decimal?>("lday02_high");
            decimal? lday03_high = item.Field<decimal?>("lday03_high");
            decimal? lday04_high = item.Field<decimal?>("lday04_high");
            decimal? lday05_high = item.Field<decimal?>("lday05_high");
            decimal? lday06_high = item.Field<decimal?>("lday06_high");
            decimal? lday07_high = item.Field<decimal?>("lday07_high");
            decimal? lday08_high = item.Field<decimal?>("lday08_high");
            decimal? lday09_high = item.Field<decimal?>("lday09_high");
            decimal? lday10_high = item.Field<decimal?>("lday10_high");
            decimal? lday11_high = item.Field<decimal?>("lday11_high");
            decimal? lday12_high = item.Field<decimal?>("lday12_high");
            decimal? lday13_high = item.Field<decimal?>("lday13_high");
            decimal? lday14_high = item.Field<decimal?>("lday14_high");
            decimal? lday15_high = item.Field<decimal?>("lday15_high");
            decimal? lday16_high = item.Field<decimal?>("lday16_high");
            decimal? lday17_high = item.Field<decimal?>("lday17_high");
            decimal? lday18_high = item.Field<decimal?>("lday18_high");
            decimal? lday19_high = item.Field<decimal?>("lday19_high");
            decimal? lday20_high = item.Field<decimal?>("lday20_high");

            decimal? lday01_vol = item.Field<decimal?>("lday01_vol");
            decimal? lday02_vol = item.Field<decimal?>("lday02_vol");
            decimal? lday03_vol = item.Field<decimal?>("lday03_vol");
            decimal? lday04_vol = item.Field<decimal?>("lday04_vol");
            decimal? lday05_vol = item.Field<decimal?>("lday05_vol");
            decimal? lday06_vol = item.Field<decimal?>("lday06_vol");
            decimal? lday07_vol = item.Field<decimal?>("lday07_vol");
            decimal? lday08_vol = item.Field<decimal?>("lday08_vol");
            decimal? lday09_vol = item.Field<decimal?>("lday09_vol");
            decimal? lday10_vol = item.Field<decimal?>("lday10_vol");
            decimal? lday11_vol = item.Field<decimal?>("lday11_vol");
            decimal? lday12_vol = item.Field<decimal?>("lday12_vol");
            decimal? lday13_vol = item.Field<decimal?>("lday13_vol");
            decimal? lday14_vol = item.Field<decimal?>("lday14_vol");
            decimal? lday15_vol = item.Field<decimal?>("lday15_vol");
            decimal? lday16_vol = item.Field<decimal?>("lday16_vol");
            decimal? lday17_vol = item.Field<decimal?>("lday17_vol");
            decimal? lday18_vol = item.Field<decimal?>("lday18_vol");
            decimal? lday19_vol = item.Field<decimal?>("lday19_vol");
            decimal? lday20_vol = item.Field<decimal?>("lday20_vol");

            decimal? MA15 = (nowprice + lday01_end + lday02_end + lday03_end + lday04_end + lday05_end + lday06_end + lday07_end
                         + lday08_end + lday09_end + lday10_end + lday11_end + lday12_end + lday13_end + lday14_end) / 15;

            decimal? ytdMA15 = (lday01_end + lday02_end + lday03_end + lday04_end + lday05_end + lday06_end + lday07_end
                         + lday08_end + lday09_end + lday10_end + lday11_end + lday12_end + lday13_end + lday14_end + lday15_end) / 15;




            item.SetField<decimal?>("max20growday_avg15", (MA15 == 0 || ytdMA15 == 0) ? 0 : ((nowprice - MA15) / MA15 - (lday01_end - ytdMA15) / ytdMA15));






        }
        private void SetDayGrowLittleChangeFirstSuper(DataRow item)
        {

            Decimal? max20high = 0;
            Decimal? max20high_day = 0;
            decimal? lday01_end = item.Field<decimal?>("lday01_end");
            ;
            decimal? nowprice = item.Field<decimal?>("nowprice");

            decimal? test20high = item.Field<decimal?>("lday01_high");
            if (test20high > max20high)
            {
                max20high_day = 1;
                max20high = test20high;
            }

            test20high = item.Field<decimal?>("lday02_high");
            if (test20high > max20high)
            {
                max20high_day = 2;
                max20high = test20high;
            }

            test20high = item.Field<decimal?>("lday03_high");
            if (test20high > max20high)
            {
                max20high_day = 3;
                max20high = test20high;
            }

            test20high = item.Field<decimal?>("lday04_high");
            if (test20high > max20high)
            {
                max20high_day = 4;
                max20high = test20high;
            }

            test20high = item.Field<decimal?>("lday05_high");
            if (test20high > max20high)
            {
                max20high_day = 5;
                max20high = test20high;
            }

            test20high = item.Field<decimal?>("lday06_high");
            if (test20high > max20high)
            {
                max20high_day = 6;
                max20high = test20high;
            }

            test20high = item.Field<decimal?>("lday07_high");
            if (test20high > max20high)
            {
                max20high_day = 7;
                max20high = test20high;
            }

            test20high = item.Field<decimal?>("lday08_high");
            if (test20high > max20high)
            {
                max20high_day = 8;
                max20high = test20high;
            }

            test20high = item.Field<decimal?>("lday09_high");
            if (test20high > max20high)
            {
                max20high_day = 9;
                max20high = test20high;
            }

            test20high = item.Field<decimal?>("lday10_high");
            if (test20high > max20high)
            {
                max20high_day = 10;
                max20high = test20high;
            }

            test20high = item.Field<decimal?>("lday11_high");
            if (test20high > max20high)
            {
                max20high_day = 11;
                max20high = test20high;
            }

            test20high = item.Field<decimal?>("lday12_high");
            if (test20high > max20high)
            {
                max20high_day = 12;
                max20high = test20high;
            }

            test20high = item.Field<decimal?>("lday13_high");
            if (test20high > max20high)
            {
                max20high_day = 13;
                max20high = test20high;
            }

            test20high = item.Field<decimal?>("lday14_high");
            if (test20high > max20high)
            {
                max20high_day = 14;
                max20high = test20high;
            }

            test20high = item.Field<decimal?>("lday15_high");
            if (test20high > max20high)
            {
                max20high_day = 15;
                max20high = test20high;
            }


            item.SetField<decimal?>("max20growday_avg15", (lday01_end == null || test20high == null || lday01_end == 0) ? 0 : ((nowprice / lday01_end - 1) - Math.Abs((test20high / lday01_end - 1).Value)));



        }



        private void SetDayGrowFiveday(DataRow item)
        {

            Decimal? max20high = 0;
            Decimal? max20low = 0;

            decimal? lday01_end = item.Field<decimal?>("lday01_end");
            ;
            decimal? nowprice = item.Field<decimal?>("nowprice");

            decimal? test20high = item.Field<decimal?>("highprice");
            if (test20high > max20high)
            {

                max20high = test20high;
            }

            test20high = item.Field<decimal?>("lday01_high");
            if (test20high > max20high)
            {

                max20high = test20high;
            }

            test20high = item.Field<decimal?>("lday02_high");
            if (test20high > max20high)
            {

                max20high = test20high;
            }

            test20high = item.Field<decimal?>("lday03_high");
            if (test20high > max20high)
            {

                max20high = test20high;
            }

            test20high = item.Field<decimal?>("lday04_high");
            if (test20high > max20high)
            {

                max20high = test20high;
            }

            test20high = item.Field<decimal?>("lday05_high");
            if (test20high > max20high)
            {

                max20high = test20high;
            }
            decimal? test20low = item.Field<decimal?>("minprice");
            max20low = test20low;
            if (max20low > test20low)
            {

                max20low = test20low;
            }
            test20low = item.Field<decimal?>("lday01_min");
            if (max20low > test20low)
            {

                max20low = test20low;
            }

            test20low = item.Field<decimal?>("lday02_min");
            if (max20low > test20low)
            {

                max20low = test20low;
            }

            test20low = item.Field<decimal?>("lday03_min");
            if (max20low > test20low)
            {

                max20low = test20low;
            }

            test20low = item.Field<decimal?>("lday04_min");
            if (max20low > test20low)
            {

                max20low = test20low;
            }

            test20low = item.Field<decimal?>("lday05_min");
            if (max20low > test20low)
            {

                max20low = test20low;
            }

            item.SetField<decimal?>("max20growday_avg15", (lday01_end == null || test20high == null || lday01_end == 0) ? 0 : ((nowprice / lday01_end - 1) - Math.Abs((test20high / lday01_end - 1).Value)));



        }

        private void SetMax20Down(DataRow item)
        {
            Decimal? Max20End = 0;
            decimal? LastEnd = item.Field<decimal?>("lday01_end");
            decimal? TestMax20End = item.Field<decimal?>("lday01_end");
            if (TestMax20End > Max20End || Max20End == null)
            {
                Max20End = TestMax20End;

            }
            if (LastEnd == 0)
            {
                LastEnd = TestMax20End;
            }
            TestMax20End = item.Field<decimal?>("lday02_end");
            if (TestMax20End > Max20End)
            {
                Max20End = TestMax20End;

            }
            if (LastEnd == 0)
            {
                LastEnd = TestMax20End;
            }
            TestMax20End = item.Field<decimal?>("lday03_end");
            if (TestMax20End > Max20End)
            {
                Max20End = TestMax20End;

            }
            if (LastEnd == 0)
            {
                LastEnd = TestMax20End;
            }
            TestMax20End = item.Field<decimal?>("lday04_end");
            if (TestMax20End > Max20End)
            {
                Max20End = TestMax20End;

            }
            if (LastEnd == 0)
            {
                LastEnd = TestMax20End;
            }
            TestMax20End = item.Field<decimal?>("lday05_end");
            if (TestMax20End > Max20End)
            {
                Max20End = TestMax20End;

            }
            if (LastEnd == 0)
            {
                LastEnd = TestMax20End;
            }
            TestMax20End = item.Field<decimal?>("lday06_end");
            if (TestMax20End > Max20End)
            {
                Max20End = TestMax20End;

            }
            if (LastEnd == 0)
            {
                LastEnd = TestMax20End;
            }
            TestMax20End = item.Field<decimal?>("lday07_end");
            if (TestMax20End > Max20End)
            {
                Max20End = TestMax20End;

            }
            if (LastEnd == 0)
            {
                LastEnd = TestMax20End;
            }
            TestMax20End = item.Field<decimal?>("lday08_end");
            if (TestMax20End > Max20End)
            {
                Max20End = TestMax20End;

            }
            if (LastEnd == 0)
            {
                LastEnd = TestMax20End;
            }
            TestMax20End = item.Field<decimal?>("lday09_end");
            if (TestMax20End > Max20End)
            {
                Max20End = TestMax20End;

            }
            if (LastEnd == 0)
            {
                LastEnd = TestMax20End;
            }
            TestMax20End = item.Field<decimal?>("lday10_end");
            if (TestMax20End > Max20End)
            {
                Max20End = TestMax20End;

            }
            if (LastEnd == 0)
            {
                LastEnd = TestMax20End;
            }
            TestMax20End = item.Field<decimal?>("lday11_end");
            if (TestMax20End > Max20End)
            {
                Max20End = TestMax20End;

            }
            if (LastEnd == 0)
            {
                LastEnd = TestMax20End;
            }
            TestMax20End = item.Field<decimal?>("lday12_end");
            if (TestMax20End > Max20End)
            {
                Max20End = TestMax20End;

            }
            if (LastEnd == 0)
            {
                LastEnd = TestMax20End;
            }
            TestMax20End = item.Field<decimal?>("lday13_end");
            if (TestMax20End > Max20End)
            {
                Max20End = TestMax20End;

            }
            if (LastEnd == 0)
            {
                LastEnd = TestMax20End;
            }
            TestMax20End = item.Field<decimal?>("lday14_end");
            if (TestMax20End > Max20End)
            {
                Max20End = TestMax20End;

            }
            if (LastEnd == 0)
            {
                LastEnd = TestMax20End;
            }
            TestMax20End = item.Field<decimal?>("lday15_end");
            if (TestMax20End > Max20End)
            {
                Max20End = TestMax20End;

            }
            if (LastEnd == 0)
            {
                LastEnd = TestMax20End;
            }
            TestMax20End = item.Field<decimal?>("lday16_end");
            if (TestMax20End > Max20End)
            {
                Max20End = TestMax20End;

            }
            if (LastEnd == 0)
            {
                LastEnd = TestMax20End;
            }
            TestMax20End = item.Field<decimal?>("lday17_end");
            if (TestMax20End > Max20End)
            {
                Max20End = TestMax20End;

            }
            if (LastEnd == 0)
            {
                LastEnd = TestMax20End;
            }
            TestMax20End = item.Field<decimal?>("lday18_end");
            if (TestMax20End > Max20End)
            {
                Max20End = TestMax20End;

            }
            if (LastEnd == 0)
            {
                LastEnd = TestMax20End;
            }
            TestMax20End = item.Field<decimal?>("lday19_end");
            if (TestMax20End > Max20End)
            {
                Max20End = TestMax20End;

            }
            if (LastEnd == 0)
            {
                LastEnd = TestMax20End;
            }
            TestMax20End = item.Field<decimal?>("lday20_end");
            if (TestMax20End > Max20End)
            {
                Max20End = TestMax20End;

            }


            item.SetField<decimal?>("Max20Down", LastEnd == 0 ? 0 : (-(Max20End / LastEnd - 1)));
            ss_mian_label.Text = "正在更新Max20Down" + item.Field<string>("stockname");
        }

        private void SetMinGrow(DataRow item)
        {

            decimal? max10growmin = 0;
            decimal? testgrow = CaculateGrowUp(item.Field<decimal?>("nowprice"), item.Field<decimal?>("lmin01"));
            if (testgrow > max10growmin || max10growmin == null)
            {
                max10growmin = testgrow;
            }
            testgrow = CaculateGrowUp(item.Field<decimal?>("nowprice"), item.Field<decimal?>("lmin02"));
            if (testgrow > max10growmin)
            {
                max10growmin = testgrow;
            }
            testgrow = CaculateGrowUp(item.Field<decimal?>("nowprice"), item.Field<decimal?>("lmin03"));
            if (testgrow > max10growmin)
            {
                max10growmin = testgrow;
            }
            testgrow = CaculateGrowUp(item.Field<decimal?>("nowprice"), item.Field<decimal?>("lmin04"));
            if (testgrow > max10growmin)
            {
                max10growmin = testgrow;
            }
            testgrow = CaculateGrowUp(item.Field<decimal?>("nowprice"), item.Field<decimal?>("lmin05"));
            if (testgrow > max10growmin)
            {
                max10growmin = testgrow;
            }
            testgrow = CaculateGrowUp(item.Field<decimal?>("nowprice"), item.Field<decimal?>("lmin06"));
            if (testgrow > max10growmin)
            {
                max10growmin = testgrow;
            }
            testgrow = CaculateGrowUp(item.Field<decimal?>("nowprice"), item.Field<decimal?>("lmin07"));
            if (testgrow > max10growmin)
            {
                max10growmin = testgrow;
            }
            testgrow = CaculateGrowUp(item.Field<decimal?>("nowprice"), item.Field<decimal?>("lmin08"));
            if (testgrow > max10growmin)
            {
                max10growmin = testgrow;
            }
            testgrow = CaculateGrowUp(item.Field<decimal?>("nowprice"), item.Field<decimal?>("lmin09"));
            if (testgrow > max10growmin)
            {
                max10growmin = testgrow;
            }
            testgrow = CaculateGrowUp(item.Field<decimal?>("nowprice"), item.Field<decimal?>("lmin10"));
            if (testgrow > max10growmin)
            {
                max10growmin = testgrow;
            }
            item.SetField<decimal?>("max10growmin", max10growmin);
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
                listsource = loads;


                Saves = NetFramework.Util_File.ReadToEnd(Application.StartupPath + "\\history.txt", Encoding.GetEncoding("GB2312"));
                loads = NetFramework.Util_DataTable.DeserializeDataTableXml(Saves);
                historysource = loads;
                ss_mian_label.Text = "读取数据完毕";
                this.Refresh();
            }

        }

        private void SaveData()
        {
            ss_mian_label.Text = "正在保存数据";
            this.Refresh();
            String ToSave = NetFramework.Util_DataTable.SerializeDataTableXml(listsource);
            NetFramework.Util_File.SaveToFile(ToSave, Application.StartupPath + "\\data.txt", Encoding.GetEncoding("GB2312"));

            ToSave = NetFramework.Util_DataTable.SerializeDataTableXml(historysource);
            NetFramework.Util_File.SaveToFile(ToSave, Application.StartupPath + "\\history.txt", Encoding.GetEncoding("GB2312"));


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



                DataRow[] find = listsource.Select("codetype='" + ct.ToString() + "' and codevalue='" + Code + "'");
                if (find.Count() == 0)
                {
                    DataRow newr = listsource.NewRow();
                    newr.SetField<String>("codetype", ct.ToString());
                    newr.SetField<String>("codevalue", Code);
                    newr.SetField<String>("stockname", Name);
                    listsource.Rows.Add(newr);
                }
                Application.DoEvents();

            }
        }
        #endregion

        private void LogicForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData();
            Environment.Exit(1);
        }

        private void Download_DeleteAndReDown_Click(object sender, EventArgs e)
        {
            listsource = new ds_main.dt_newsDataTable();
            bs_main.DataSource = listsource.AsDataView();
            sf.bs_sub.DataSource = listsource.AsDataView();
            Download_AllCode_Click(sender, e);
        }

        private void time_refresh_Tick(object sender, EventArgs e)
        {
            time_refresh.Enabled = false;
            try
            {
                if ((DateTime.Now.Hour * 60 + DateTime.Now.Minute >= 9 * 60 + 30)
                  && (DateTime.Now.Hour * 60 + DateTime.Now.Minute <= 15 * 60)
                  && cb_stop.Checked == false
                  )
                {
                    Download_Now_Click(sender, e);
                }
                #region
                if (DateTime.Now.Hour == 9
                    && DateTime.Now.Minute < 30
                                       )
                {
                    Set_DatyOpen_Click(sender, e);
                }
                #endregion
            }
            catch (Exception AnyError)
            {
                ss_mian_label.Text = AnyError.Message;
                NetFramework.Console.Write(AnyError.Message);
                NetFramework.Console.Write(AnyError.StackTrace);

            }



            time_refresh.Enabled = true;
        }

        private void Set_DayNoTrans_Click(object sender, EventArgs e)
        {

        }

        NetFramework.Util_WeChatEnterpriseMsg msg = new NetFramework.Util_WeChatEnterpriseMsg("wx48e213f50ad641c8", "RysOH8IiVWHvi5kWyyheee7YAlvE6Z4q9uDRvHrYxqI", "1000005");
        private void Test_SetSuppose_Click(object sender, EventArgs e)
        {
            //listsource.Rows[0].SetField<bool?>("issuppose", true);
            //listsource.Rows[1].SetField<bool?>("issuppose", true);

            //string access = msg.AccessToken;
            //msg.SendTextMsg(listsource.Rows[0].Field<String>("stockname")
            //    + "[" + listsource.Rows[0].Field<String>("codevalue") + "]"
            //     + listsource.Rows[0].Field<decimal?>("growtoday").Value.ToString("0.00%")
            //    );
            DataRow newr = listsource.NewRow();
            newr.SetField<String>("codetype", "sz");
            newr.SetField<String>("codevalue", "399001");
            newr.SetField<String>("stockname", "深证成指");
            listsource.Rows.Add(newr);
        }

        DateTime? LastOpenDay = null;
        private void Set_DatyOpen_Click(object sender, EventArgs e)
        {
            //if ((LastOpenDay == null || LastOpenDay != DateTime.Today))
            {
                LastOpenDay = DateTime.Today;
                foreach (DataRow item in listsource.Rows)
                {
                    item.SetField<Boolean>("issuppose", false);
                    item.SetField<decimal>("lmin01", 0);
                    item.SetField<decimal>("lmin02", 0);
                    item.SetField<decimal>("lmin03", 0);
                    item.SetField<decimal>("lmin04", 0);
                    item.SetField<decimal>("lmin05", 0);
                    item.SetField<decimal>("lmin06", 0);
                    item.SetField<decimal>("lmin07", 0);
                    item.SetField<decimal>("lmin08", 0);
                    item.SetField<decimal>("lmin09", 0);
                    item.SetField<decimal>("lmin10", 0);
                    item.SetField<decimal>("breakratio", 0);
                    item.SetField<DateTime?>("noticetime", null);

                    item.SetField<decimal?>("logprice", 0);
                    item.SetField<decimal?>("logbreakqty", 0);

                    if (item.Field<string>("strong").Length > 1)
                    {
                        item.SetField<string>("strong", item.Field<string>("strong").Substring(0, 1));
                    }
                    //SetMinGrow(item);
                    //SetDayGrowVMax(item);

                }
            }
        }

        private void ReloadFilter()
        {
            try
            {
                if (fil_cb_focus.Checked == true)
                {
                    bs_main.Filter = (fil_cb_focus.Checked == true ? "  isfocus =1 " : " ");

                }
                else
                {
                    bs_main.Filter = "codevalue like '%" + fil_tbcode.Text + "%' "
                                   + " and stockname like '%" + fil_tb_name.Text + "%' "

                                    + (tb_updownmin.Text == "" ? "" : " and updown>= " + tb_updownmin.Text + "/100.0 ")
                                    + (tb_updownmax.Text == "" ? "" : " and updown<= " + tb_updownmax.Text + "/100.0 ")

                                    + (tb_PriceFrom.Text == "" ? "" : " and nowprice>= " + tb_PriceFrom.Text + " ")
                                      + (tb_PriceTo.Text == "" ? "" : " and nowprice<= " + tb_PriceTo.Text + " ")
                                    + (tb_max20growup.Text == "" ? "" : " and max20growday >= " + tb_max20growup.Text + "/100.0 ")
                               + (tb_max20downto.Text == "" ? "" : " and max20down <= " + tb_max20downto.Text + "/100.0 ")
                                 + (tb_max20downfrom.Text == "" ? "" : " and max20down >= " + tb_max20downfrom.Text + "/100.0 ")
                                    + (tb_max20growday_avg15.Text == "" ? "" : " and max20growday_avg15 <= " + tb_max20growday_avg15.Text + " ")
                                + (tb_growmin.Text == "" ? "" : " and growtoday*100 >= " + tb_growmin.Text + " ")
                                 + (tb_growmax.Text == "" ? "" : " and growtoday*100 <= " + tb_growmax.Text + " ")

                                    ;
                }

                sf.bs_sub.Filter = bs_main.Filter;
            }
            catch (Exception AnyError)
            {


            }


        }



        private void pop_m_grid_setisfoucus_Click(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)(sender)).Checked = !((ToolStripMenuItem)(sender)).Checked;
            (((DataRowView)((DataGridViewRow)pop_m_grid.Tag).DataBoundItem)).Row["isfocus"] = ((ToolStripMenuItem)(sender)).Checked;
        }



        private void fil_tbcode_TextChanged(object sender, EventArgs e)
        {
            ReloadFilter();
        }

        private void fil_tb_name_TextChanged(object sender, EventArgs e)
        {
            ReloadFilter();
        }

        private void fil_cb_focus_CheckedChanged(object sender, EventArgs e)
        {
            ReloadFilter();
        }

        private void gv_list_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    gv_list.Rows[e.RowIndex].Selected = true;
                    gv_list.CurrentCell = gv_list.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    pop_m_grid.Tag = gv_list.Rows[e.RowIndex];
                    pop_m_grid.Show(MousePosition.X, MousePosition.Y);

                }
            }
        }

        private void pop_m_grid_Opening(object sender, CancelEventArgs e)
        {
            pop_m_grid_setisfoucus.Checked = Convert.ToBoolean(
                (((DataRowView)((DataGridViewRow)pop_m_grid.Tag).DataBoundItem))["isfocus"] == DBNull.Value ? false :
                (((DataRowView)((DataGridViewRow)pop_m_grid.Tag).DataBoundItem))["isfocus"]
                );

        }

        private void LogicForm_Move(object sender, EventArgs e)
        {
            sf.Top = this.Top;
            sf.Left = this.Left + this.Width - 16;
            sf.Height = this.Height;
        }

        private void Menu_Main_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void LogicForm_Shown(object sender, EventArgs e)
        {
            LogicForm_Move(sender, e);
        }

        private void tb_rerise_TextChanged(object sender, EventArgs e)
        {
            ReloadFilter();
        }

        private void tb_PriceFrom_TextChanged(object sender, EventArgs e)
        {
            ReloadFilter();
        }

        private void tb_PriceTo_TextChanged(object sender, EventArgs e)
        {
            ReloadFilter();
        }

        private void tb_max20growup_TextChanged(object sender, EventArgs e)
        {
            ReloadFilter();
        }

        private void gv_list_RowEnter(object sender, DataGridViewCellEventArgs e)
        {




        }

        private void gv_list_SelectionChanged(object sender, EventArgs e)
        {
            if (gv_list.SelectedRows.Count > 0)
            {
                DataGridViewRow sr = gv_list.SelectedRows[0];
                string Code = sr.Cells["codetype"].Value.ToString() + sr.Cells["codevalue"].Value.ToString();
                try
                {
                    sf.bs_history.Filter = "codevalue ='" + sr.Cells["codevalue"].Value.ToString() + "' ";
                    if (cb_showjpg.Checked == true)
                    {
                        pic_day.Image = Image.FromStream(System.Net.WebRequest.Create("http://image.sinajs.cn/newchart/daily/n/" + Code + ".gif").GetResponse().GetResponseStream());
                        pic_minute.Image = Image.FromStream(System.Net.WebRequest.Create("http://image.sinajs.cn/newchart/min/n/" + Code + ".gif").GetResponse().GetResponseStream());


                    }

                }
                catch (Exception anyerror)
                {

                    ss_mian_label.Text = anyerror.Message;
                }
            }//rowqty
        }//function

        private void tb_max20down_TextChanged(object sender, EventArgs e)
        {
            ReloadFilter();
        }

        private void cb_showjpg_CheckedChanged(object sender, EventArgs e)
        {
            if (gv_list.SelectedRows.Count > 0 && cb_showjpg.Checked == true)
            {
                DataGridViewRow sr = gv_list.SelectedRows[0];
                string Code = sr.Cells["codetype"].Value.ToString() + sr.Cells["codevalue"].Value.ToString();
                try
                {
                    if (cb_showjpg.Checked == true)
                    {
                        pic_day.Image = Image.FromStream(System.Net.WebRequest.Create("http://image.sinajs.cn/newchart/daily/n/" + Code + ".gif").GetResponse().GetResponseStream());
                        pic_minute.Image = Image.FromStream(System.Net.WebRequest.Create("http://image.sinajs.cn/newchart/min/n/" + Code + ".gif").GetResponse().GetResponseStream());

                    }

                }
                catch (Exception anyerror)
                {

                    ss_mian_label.Text = anyerror.Message;
                }
            }
            else
            {
                pic_day.Image = null;
                pic_minute.Image = null;
            }


            if (cb_showjpg.Checked)
            {
                gv_list.Height -= pic_day.Height;
                pic_day.Visible = true;
                pic_minute.Visible = true;
            }
            else
            {
                gv_list.Height += pic_day.Height;
                pic_day.Visible = false;
                pic_minute.Visible = false;
            }

            //rowqty
        }

        private void tb_max20downfrom_TextChanged(object sender, EventArgs e)
        {
            ReloadFilter();
        }

        private void Test_Restore_Click(object sender, EventArgs e)
        {

            //listsource.Columns.Add("breakday1", typeof(DateTime));
            //listsource.Columns.Add("breakday2", typeof(DateTime));
            //listsource.Columns.Add("lday02_open", typeof(decimal));
            //listsource.Columns.Add("lday03_open", typeof(decimal));
            //listsource.Columns.Add("lday04_open", typeof(decimal));
            //listsource.Columns.Add("lday05_open", typeof(decimal));
            //listsource.Columns.Add("lday06_open", typeof(decimal));
            //listsource.Columns.Add("lday07_open", typeof(decimal));
            //listsource.Columns.Add("lday08_open", typeof(decimal));
            //listsource.Columns.Add("lday09_open", typeof(decimal));
            //listsource.Columns.Add("lday10_open", typeof(decimal));
            //listsource.Columns.Add("lday11_open", typeof(decimal));
            //listsource.Columns.Add("lday12_open", typeof(decimal));
            //listsource.Columns.Add("lday13_open", typeof(decimal));
            //listsource.Columns.Add("lday14_open", typeof(decimal));
            //listsource.Columns.Add("lday15_open", typeof(decimal));
            //listsource.Columns.Add("lday16_open", typeof(decimal));
            //listsource.Columns.Add("lday17_open", typeof(decimal));
            //listsource.Columns.Add("lday18_open", typeof(decimal));
            //listsource.Columns.Add("lday19_open", typeof(decimal));
            //listsource.Columns.Add("lday20_open", typeof(decimal));

            // return;

            foreach (DataRow item in listsource.Rows)
            {


                item.SetField<DateTime?>("breakday2", null);
                item.SetField<DateTime?>("breakday1", null);
            }
            //historysource.Rows.Clear();

        }

        private void gv_main_SelectionChanged(object sender, EventArgs e)
        {


            if (gv_main.SelectedRows.Count > 0)
            {
                DataGridViewRow sr = gv_main.SelectedRows[0];
                string Code = sr.Cells["b_codetype"].Value.ToString() + sr.Cells["b_codevalue"].Value.ToString();
                try
                {
                    if (cb_showjpg.Checked == true)
                    {
                        pic_day.Image = Image.FromStream(System.Net.WebRequest.Create("http://image.sinajs.cn/newchart/daily/n/" + Code + ".gif").GetResponse().GetResponseStream());
                        pic_minute.Image = Image.FromStream(System.Net.WebRequest.Create("http://image.sinajs.cn/newchart/min/n/" + Code + ".gif").GetResponse().GetResponseStream());

                    }

                }
                catch (Exception anyerror)
                {

                    ss_mian_label.Text = anyerror.Message;
                }
            }//rowqty
        }

        private void tb_max20growday_avg15_TextChanged(object sender, EventArgs e)
        {
            ReloadFilter();
        }

        private void gv_main_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    gv_main.ClearSelection();
                    gv_main.Rows[e.RowIndex].Selected = true;
                    gv_main.CurrentCell = gv_main.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    pop_m_grid.Tag = gv_main.Rows[e.RowIndex];
                    pop_m_grid.Show(MousePosition.X, MousePosition.Y);

                }
            }
        }

        private void tb_growmin_TextChanged(object sender, EventArgs e)
        {
            ReloadFilter();
        }

        private void tb_growmax_TextChanged(object sender, EventArgs e)
        {
            ReloadFilter();
        }

        private void tb_updownmax_TextChanged(object sender, EventArgs e)
        {
            ReloadFilter();
        }

        private void rebuildMA5CrossMA15ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataRow item in listsource.Rows)
            {
                String codetype = item.Field<string>("codetype");
                string codevalue = item.Field<string>("codevalue");
                ss_mian_label.Text = "正在更新breakday" + codetype + codevalue;
                DataRow[] itemhistory = historysource.Select("codetype='" + codetype + "' and codevalue='" + codevalue + "' ", "transday desc");
                item.SetField<DateTime?>("breakday2", null);
                item.SetField<DateTime?>("breakday1", null);
                DateTime? breakday1 = null;
                DateTime? breakday2 = null;

                for (int i = 0; i < 30; i++)
                {
                    if (itemhistory.Length < i + 1)
                    {
                        continue;
                    }
                    DateTime? transday = itemhistory[i].Field<DateTime?>("transday");
                    if (i < 20)
                    {
                        item.SetField<decimal?>("lday" + (i + 1).ToString("00") + "_end", itemhistory[i].Field<decimal?>("close"));
                        item.SetField<decimal?>("lday" + (i + 1).ToString("00") + "_vol", itemhistory[i].Field<decimal?>("volumn"));
                    }

                    if (breakday1 != null && breakday2 != null)
                    {
                        break;
                    }

                    if (itemhistory.Length > i + 16)
                    {
                        decimal? MA5 = itemhistory[i].Field<decimal?>("close")
                            + itemhistory[i + 1].Field<decimal?>("close")
                             + itemhistory[i + 2].Field<decimal?>("close")
                              + itemhistory[i + 3].Field<decimal?>("close")
                               + itemhistory[i + 4].Field<decimal?>("close")
                            ;
                        MA5 = MA5 / 5;
                        decimal? MA15 = itemhistory[i].Field<decimal?>("close")
                           + itemhistory[i + 1].Field<decimal?>("close")
                           + itemhistory[i + 2].Field<decimal?>("close")
                           + itemhistory[i + 3].Field<decimal?>("close")
                           + itemhistory[i + 4].Field<decimal?>("close")
                           + itemhistory[i + 5].Field<decimal?>("close")
                           + itemhistory[i + 6].Field<decimal?>("close")
                           + itemhistory[i + 7].Field<decimal?>("close")
                           + itemhistory[i + 8].Field<decimal?>("close")
                           + itemhistory[i + 9].Field<decimal?>("close")
                           + itemhistory[i + 10].Field<decimal?>("close")
                           + itemhistory[i + 11].Field<decimal?>("close")
                           + itemhistory[i + 12].Field<decimal?>("close")
                           + itemhistory[i + 13].Field<decimal?>("close")
                           + itemhistory[i + 14].Field<decimal?>("close")
                           ;
                        MA15 = MA15 / 15;

                        decimal? MA5bef = itemhistory[i + 1].Field<decimal?>("close")
                             + itemhistory[i + 2].Field<decimal?>("close")
                              + itemhistory[i + 3].Field<decimal?>("close")
                               + itemhistory[i + 4].Field<decimal?>("close")
                                + itemhistory[i + 5].Field<decimal?>("close")
                            ;
                        MA5bef = MA5bef / 5;
                        decimal? MA15bef =
                            itemhistory[i + 1].Field<decimal?>("close")
                           + itemhistory[i + 2].Field<decimal?>("close")
                           + itemhistory[i + 3].Field<decimal?>("close")
                           + itemhistory[i + 4].Field<decimal?>("close")
                           + itemhistory[i + 5].Field<decimal?>("close")
                           + itemhistory[i + 6].Field<decimal?>("close")
                           + itemhistory[i + 7].Field<decimal?>("close")
                           + itemhistory[i + 8].Field<decimal?>("close")
                           + itemhistory[i + 9].Field<decimal?>("close")
                           + itemhistory[i + 10].Field<decimal?>("close")
                           + itemhistory[i + 11].Field<decimal?>("close")
                           + itemhistory[i + 12].Field<decimal?>("close")
                           + itemhistory[i + 13].Field<decimal?>("close")
                           + itemhistory[i + 14].Field<decimal?>("close")
                           + itemhistory[i + 15].Field<decimal?>("close")
                           ;
                        MA15bef = MA15bef / 15;
                        if (MA5bef < MA15bef && MA5 > MA15bef)
                        {
                            if (breakday1 != null && breakday2 == null)
                            {
                                breakday2 = transday;
                                item.SetField<DateTime?>("breakday2", breakday2);
                                //item.SetField<Decimal?>("logbreakqty", Convert.ToDecimal((breakday2 - breakday1).Value.TotalDays));
                            }
                            if (breakday1 == null)
                            {
                                breakday1 = transday;
                                item.SetField<DateTime?>("breakday1", breakday1);

                            }

                        }

                    }//30天+16天计算MA
                }//查找30天


            }
            ss_mian_label.Text = "正在更新breakday完成";
        }

        private void rebuildLday01To20ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }//class
    public class DayInfo
    {

        public DayInfo(CheckTurnDesc _TunDesc)
        {
            TurnDesc = _TunDesc;
        }
        public DataRow Dayn5 { get; set; }
        public DataRow Dayn4 { get; set; }
        public DataRow Dayn3 { get; set; }
        public DataRow Dayn2 { get; set; }
        public DataRow Dayn1 { get; set; }

        public DataRow DayTurnDay { get; set; }


        public DataRow Dayf5 { get; set; }
        public DataRow Dayf4 { get; set; }
        public DataRow Dayf3 { get; set; }
        public DataRow Dayf2 { get; set; }
        public DataRow Dayf1 { get; set; }

        public bool MoveFar(DataRow newDay, bool LastDay)
        {


            string TmpCodeValue = newDay.Field<string>("codevalue");
            decimal? TmpClose = newDay.Field<decimal?>("close");
            DateTime TmpTransDay = newDay.Field<DateTime>("transday");

            if (TurnDesc == CheckTurnDesc.Top && TmpClose > TurnClose)
            {
                TurnClose = TmpClose;
                Dayn5 = Dayn4;
                Dayn4 = Dayn3;
                Dayn3 = Dayn2;
                Dayn2 = Dayn1;
                Dayn1 = DayTurnDay;
                DayTurnDay = newDay;

                Dayf5 = null;
                Dayf4 = null;
                Dayf3 = null;
                Dayf2 = null;
                Dayf1 = null;
            }
            else if (TurnDesc == CheckTurnDesc.Top)
            {
                if (Dayf1 == null)
                {
                    Dayf1 = newDay;
                    goto TopFarDay;
                }
                if (Dayf2 == null)
                {
                    Dayf2 = newDay;
                    goto TopFarDay;
                }
                if (Dayf3 == null)
                {
                    Dayf3 = newDay;
                    goto TopFarDay;
                }
                if (Dayf4 == null)
                {
                    Dayf4 = newDay;
                    goto TopFarDay;
                }
                if (Dayf5 == null)
                {
                    Dayf5 = newDay;
                    goto TopFarDay;
                }




            }//顶部模式
        TopFarDay:

            if (TurnDesc == CheckTurnDesc.Botton && (TmpClose < TurnClose || TurnClose == 0))
            {
                TurnClose = TmpClose;
                Dayn5 = Dayn4;
                Dayn4 = Dayn3;
                Dayn3 = Dayn2;
                Dayn2 = Dayn1;
                Dayn1 = DayTurnDay;
                DayTurnDay = newDay;

                Dayf5 = null;
                Dayf4 = null;
                Dayf3 = null;
                Dayf2 = null;
                Dayf1 = null;
            }
            else if (TurnDesc == CheckTurnDesc.Botton)
            {
                if (Dayf1 == null)
                {
                    Dayf1 = newDay;
                    goto BottonFarDay;
                }
                if (Dayf2 == null)
                {
                    Dayf2 = newDay;
                    goto BottonFarDay;
                }
                if (Dayf3 == null)
                {
                    Dayf3 = newDay;
                    goto BottonFarDay;
                }
                if (Dayf4 == null)
                {
                    Dayf4 = newDay;
                    goto BottonFarDay;
                }
                if (Dayf5 == null)
                {
                    Dayf5 = newDay;
                    goto BottonFarDay;
                }




            }//底部模式
        BottonFarDay:

            #region 检查是否是极点
            if ((Dayf5 != null && Dayf4 != null && Dayf3 != null && Dayf2 != null && Dayf1 != null) || LastDay == true)
            {
                TurnClose = TmpClose;
                TurnTransDay = TmpTransDay;

                return true;
            }
            else
            {
                return false;
            }
            #endregion


        }

        decimal? TurnClose = 0;
        DateTime? TurnTransDay = null;
        public enum CheckTurnDesc { Top, Botton }

        public CheckTurnDesc TurnDesc { get; set; }



    }//class

}//namespace
