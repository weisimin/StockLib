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
        SubForm sf = new SubForm();
        private void LogicForm_Load(object sender, EventArgs e)
        {
            //gv_list.AutoGenerateColumns = true;


            LoadDatas();
            bs_main.DataSource = listsource.AsDataView();
            bs_sub.Filter = "issuppose='true'";
            bs_sub.DataSource = listsource.AsDataView();

            sf.Show();
            sf.bs_sub.DataSource = listsource.AsDataView();

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

                    foreach (var stritem in relsitem.SendText)
                    {
                        SendCount += 1;
                        SendMsg += stritem + Environment.NewLine;
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

        private DownLoadSinaResult DownloadSinaData(string Codes)
        {
            String URL = "http://hq.sinajs.cn/list=" + Codes;
            System.Net.CookieCollection cookie = new System.Net.CookieCollection();
            string Result = NetFramework.Util_WEB.OpenUrl(URL, "", "", "GET", cookie, Encoding.GetEncoding("GB2312"));
            String[] Lines = Result.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);


            List<string> SendText = new List<string>();

            DateTime lasttime = DateTime.Now;
            DateTime? rowtime = null;
            foreach (var lineitem in Lines)
            {
                string[] infs = lineitem.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string codetype = infs[0].Substring(11, 2);
                string codevalue = infs[0].Substring(13, 6); ;
                DataRow[] rows = listsource.Select("codetype='" + codetype + "' and codevalue='" + codevalue + "'");
                if (rows.Count() > 0)
                {
                    rows[0].SetField<Decimal?>("minprice", Convert.ToDecimal(infs[5]));
                    rows[0].SetField<Decimal?>("nowprice", Convert.ToDecimal(infs[3]));
                    rows[0].SetField<Decimal?>("ytdprice", Convert.ToDecimal(infs[2]));


                    rows[0].SetField<Decimal?>("updown", CaculateGrowUp(rows[0].Field<Decimal>("nowprice"), rows[0].Field<Decimal>("ytdprice")));
                    rows[0].SetField<Decimal?>("growtoday", CaculateGrowUp(rows[0].Field<Decimal>("nowprice"), rows[0].Field<Decimal>("minprice")));

                    lasttime = Convert.ToDateTime(infs[30] + " " + infs[31]);

                    decimal? avg3min = (rows[0].Field<decimal?>("nowprice")
                        + rows[0].Field<decimal?>("lday03_end")
                        + rows[0].Field<decimal?>("lday02_end")
                        + rows[0].Field<decimal?>("lday01_end")
                        + rows[0].Field<decimal?>("lday04_end")
                        ) / 5;
                    decimal? avg3minytd = (rows[0].Field<decimal?>("lday01_end")
                        + rows[0].Field<decimal?>("lday02_end")
                        + rows[0].Field<decimal?>("lday03_end")
                        + rows[0].Field<decimal?>("lday04_end")
                        + rows[0].Field<decimal?>("lday05_end")) / 5;

                    decimal? avg3minytdbef = (
                       rows[0].Field<decimal?>("lday02_end")
                      + rows[0].Field<decimal?>("lday03_end")
                      + rows[0].Field<decimal?>("lday04_end")
                      + rows[0].Field<decimal?>("lday05_end")
                      + rows[0].Field<decimal?>("lday06_end")) / 5;

                    decimal? avg20 = (
                       rows[0].Field<decimal?>("lday01_end")
                      + rows[0].Field<decimal?>("lday02_end")
                      + rows[0].Field<decimal?>("lday03_end")
                      + rows[0].Field<decimal?>("lday04_end")
                      + rows[0].Field<decimal?>("lday05_end")
                       + rows[0].Field<decimal?>("lday06_end")
                      + rows[0].Field<decimal?>("lday07_end")
                      + rows[0].Field<decimal?>("lday08_end")
                      + rows[0].Field<decimal?>("lday09_end")
                      + rows[0].Field<decimal?>("lday10_end")
                       + rows[0].Field<decimal?>("lday11_end")
                      + rows[0].Field<decimal?>("lday12_end")
                      + rows[0].Field<decimal?>("lday13_end")
                      + rows[0].Field<decimal?>("lday14_end")
                      + rows[0].Field<decimal?>("lday15_end")
                       + rows[0].Field<decimal?>("lday16_end")
                      + rows[0].Field<decimal?>("lday17_end")
                      + rows[0].Field<decimal?>("lday18_end")
                      + rows[0].Field<decimal?>("lday19_end")
                      + rows[0].Field<decimal?>("lday20_end")

                      ) / 20;

                    decimal? nowprice = rows[0].Field<decimal?>("nowprice");
                    decimal? ytdprice = rows[0].Field<decimal?>("ytdprice");
                    decimal? growtoday = rows[0].Field<decimal?>("growtoday");
                    decimal? minprice = rows[0].Field<decimal?>("minprice");
                    decimal? lday01_min = rows[0].Field<decimal?>("lday01_min");
                    decimal? lday01_end = rows[0].Field<decimal?>("lday01_end");
                    //SetMax20Down(rows[0]);
                    rows[0].SetField<Decimal?>("avg3min", avg3min);
                    rows[0].SetField<Decimal?>("avg3minytd", avg3minytd);
                    rows[0].SetField<Decimal?>("avg20", avg20);
                    rowtime = rows[0].Field<DateTime?>("nowtime");
                    SetMinGrow(rows[0]);
                    #region 阶段更新
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



                        decimal? max20growday = rows[0].Field<decimal?>("max20growday");

                        if (max20growday == null)
                        {
                            max20growday = 0;
                            rows[0].SetField<decimal?>("max20growday", max20growday);
                        }
                        decimal? max10growmin = rows[0].Field<decimal?>("max10growmin");
                        if (max10growmin == null)
                        {
                            max10growmin = 0;
                            rows[0].SetField<decimal?>("max10growmin", max10growmin);
                        }


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

                            decimal? totalend = 0; int totalqty = 0;
                            if (rows[0].Field<decimal?>("lday01_end") > 0) { totalend += rows[0].Field<decimal?>("lday01_end"); totalqty += 1; }
                            if (rows[0].Field<decimal?>("lday02_end") > 0) { totalend += rows[0].Field<decimal?>("lday02_end"); totalqty += 1; }
                            if (rows[0].Field<decimal?>("lday03_end") > 0) { totalend += rows[0].Field<decimal?>("lday03_end"); totalqty += 1; }
                            if (rows[0].Field<decimal?>("lday04_end") > 0) { totalend += rows[0].Field<decimal?>("lday04_end"); totalqty += 1; }
                            if (rows[0].Field<decimal?>("lday05_end") > 0) { totalend += rows[0].Field<decimal?>("lday05_end"); totalqty += 1; }
                            if (rows[0].Field<decimal?>("lday06_end") > 0) { totalend += rows[0].Field<decimal?>("lday06_end"); totalqty += 1; }
                            if (rows[0].Field<decimal?>("lday07_end") > 0) { totalend += rows[0].Field<decimal?>("lday07_end"); totalqty += 1; }
                            if (rows[0].Field<decimal?>("lday08_end") > 0) { totalend += rows[0].Field<decimal?>("lday08_end"); totalqty += 1; }
                            if (rows[0].Field<decimal?>("lday09_end") > 0) { totalend += rows[0].Field<decimal?>("lday09_end"); totalqty += 1; }
                            if (rows[0].Field<decimal?>("lday10_end") > 0) { totalend += rows[0].Field<decimal?>("lday10_end"); totalqty += 1; }
                            //if (rows[0].Field<decimal?>("lday11_end") > 0) { totalend += rows[0].Field<decimal?>("lday11_end"); totalqty += 1; }
                            //if (rows[0].Field<decimal?>("lday12_end") > 0) { totalend += rows[0].Field<decimal?>("lday12_end"); totalqty += 1; }
                            //if (rows[0].Field<decimal?>("lday13_end") > 0) { totalend += rows[0].Field<decimal?>("lday13_end"); totalqty += 1; }
                            //if (rows[0].Field<decimal?>("lday14_end") > 0) { totalend += rows[0].Field<decimal?>("lday14_end"); totalqty += 1; }
                            //if (rows[0].Field<decimal?>("lday15_end") > 0) { totalend += rows[0].Field<decimal?>("lday15_end"); totalqty += 1; }



                            rows[0].SetField<decimal?>("day20_15avg", rows[0].Field<decimal?>("day19_15avg"));
                            rows[0].SetField<decimal?>("day19_15avg", rows[0].Field<decimal?>("day18_15avg"));
                            rows[0].SetField<decimal?>("day18_15avg", rows[0].Field<decimal?>("day17_15avg"));
                            rows[0].SetField<decimal?>("day17_15avg", rows[0].Field<decimal?>("day16_15avg"));
                            rows[0].SetField<decimal?>("day16_15avg", rows[0].Field<decimal?>("day15_15avg"));
                            rows[0].SetField<decimal?>("day15_15avg", rows[0].Field<decimal?>("day14_15avg"));
                            rows[0].SetField<decimal?>("day14_15avg", rows[0].Field<decimal?>("day13_15avg"));
                            rows[0].SetField<decimal?>("day13_15avg", rows[0].Field<decimal?>("day12_15avg"));
                            rows[0].SetField<decimal?>("day12_15avg", rows[0].Field<decimal?>("day11_15avg"));
                            rows[0].SetField<decimal?>("day11_15avg", rows[0].Field<decimal?>("day10_15avg"));
                            rows[0].SetField<decimal?>("day10_15avg", rows[0].Field<decimal?>("day09_15avg"));
                            rows[0].SetField<decimal?>("day09_15avg", rows[0].Field<decimal?>("day08_15avg"));
                            rows[0].SetField<decimal?>("day08_15avg", rows[0].Field<decimal?>("day07_15avg"));
                            rows[0].SetField<decimal?>("day07_15avg", rows[0].Field<decimal?>("day06_15avg"));
                            rows[0].SetField<decimal?>("day06_15avg", rows[0].Field<decimal?>("day05_15avg"));
                            rows[0].SetField<decimal?>("day05_15avg", rows[0].Field<decimal?>("day04_15avg"));
                            rows[0].SetField<decimal?>("day04_15avg", rows[0].Field<decimal?>("day03_15avg"));
                            rows[0].SetField<decimal?>("day03_15avg", rows[0].Field<decimal?>("day02_15avg"));
                            rows[0].SetField<decimal?>("day02_15avg", rows[0].Field<decimal?>("day01_15avg"));
                            rows[0].SetField<decimal?>("day01_15avg", totalend / totalqty);





                            rows[0].SetField<decimal?>("growday01", CaculateGrowUp(rows[0].Field<decimal?>("lday01_end"), rows[0].Field<decimal?>("lday01_min")));
                            rows[0].SetField<decimal?>("growday02", CaculateGrowUp(rows[0].Field<decimal?>("lday02_end"), rows[0].Field<decimal?>("lday02_min")));
                            rows[0].SetField<decimal?>("growday03", CaculateGrowUp(rows[0].Field<decimal?>("lday03_end"), rows[0].Field<decimal?>("lday03_min")));
                            rows[0].SetField<decimal?>("growday04", CaculateGrowUp(rows[0].Field<decimal?>("lday04_end"), rows[0].Field<decimal?>("lday04_min")));
                            rows[0].SetField<decimal?>("growday05", CaculateGrowUp(rows[0].Field<decimal?>("lday05_end"), rows[0].Field<decimal?>("lday05_min")));
                            rows[0].SetField<decimal?>("growday06", CaculateGrowUp(rows[0].Field<decimal?>("lday06_end"), rows[0].Field<decimal?>("lday06_min")));
                            rows[0].SetField<decimal?>("growday07", CaculateGrowUp(rows[0].Field<decimal?>("lday07_end"), rows[0].Field<decimal?>("lday07_min")));
                            rows[0].SetField<decimal?>("growday08", CaculateGrowUp(rows[0].Field<decimal?>("lday08_end"), rows[0].Field<decimal?>("lday08_min")));
                            rows[0].SetField<decimal?>("growday09", CaculateGrowUp(rows[0].Field<decimal?>("lday09_end"), rows[0].Field<decimal?>("lday09_min")));
                            rows[0].SetField<decimal?>("growday10", CaculateGrowUp(rows[0].Field<decimal?>("lday10_end"), rows[0].Field<decimal?>("lday10_min")));
                            rows[0].SetField<decimal?>("growday11", CaculateGrowUp(rows[0].Field<decimal?>("lday11_end"), rows[0].Field<decimal?>("lday11_min")));
                            rows[0].SetField<decimal?>("growday12", CaculateGrowUp(rows[0].Field<decimal?>("lday12_end"), rows[0].Field<decimal?>("lday12_min")));
                            rows[0].SetField<decimal?>("growday13", CaculateGrowUp(rows[0].Field<decimal?>("lday13_end"), rows[0].Field<decimal?>("lday13_min")));
                            rows[0].SetField<decimal?>("growday14", CaculateGrowUp(rows[0].Field<decimal?>("lday14_end"), rows[0].Field<decimal?>("lday14_min")));
                            rows[0].SetField<decimal?>("growday15", CaculateGrowUp(rows[0].Field<decimal?>("lday15_end"), rows[0].Field<decimal?>("lday15_min")));
                            rows[0].SetField<decimal?>("growday16", CaculateGrowUp(rows[0].Field<decimal?>("lday16_end"), rows[0].Field<decimal?>("lday16_min")));
                            rows[0].SetField<decimal?>("growday17", CaculateGrowUp(rows[0].Field<decimal?>("lday17_end"), rows[0].Field<decimal?>("lday17_min")));
                            rows[0].SetField<decimal?>("growday18", CaculateGrowUp(rows[0].Field<decimal?>("lday18_end"), rows[0].Field<decimal?>("lday18_min")));
                            rows[0].SetField<decimal?>("growday19", CaculateGrowUp(rows[0].Field<decimal?>("lday19_end"), rows[0].Field<decimal?>("lday19_min")));
                            rows[0].SetField<decimal?>("growday20", CaculateGrowUp(rows[0].Field<decimal?>("lday20_end"), rows[0].Field<decimal?>("lday20_min")));

                            rows[0].SetField<DateTime?>("lday_time", lasttime);




                            SetDayGrow(rows[0]);


                        }//下午3点处理结束


                    }//下载的分钟时间与之前记录的部一样
                    #endregion
                    #region 实时更新
                    //string Codet = rows[0].Field<String>("codevalue");
                    if (
                        avg3minytd >= lday01_end
                        && avg3min <= nowprice
                        &&avg20>= lday01_end
                        )
                    {
                        rows[0].SetField<string>("supposename", "买入");
                    }
                    else if (
                      avg3minytd <= lday01_end
                      && avg3min >= nowprice
                      && avg20 <= lday01_end
                      )
                    {
                        rows[0].SetField<string>("supposename", "卖出");
                    }
                    else
                    {
                        if (rows[0].Field<string>("supposename").Contains("观望") == false)
                        { 
                           rows[0].SetField<string>("supposename", rows[0].Field<string>("supposename")+"观望");
                        }
                     
                    }


                    if (
                          avg20 >= lday01_end
                        //&& max20down * 100.0M <= -3.0M
                        //&& max20down * 100.0M >= -18.0M
                        && avg3minytd >= lday01_end
                        && avg3min <= nowprice

    && rows[0].Field<bool?>("issuppose") == false
    )
                    {
                        rows[0].SetField<bool?>("issuppose", true);




                        SendText.Add(rows[0].Field<String>("stockname")
                            + "[" + rows[0].Field<String>("codevalue") + "] "
                            + rows[0].Field<decimal?>("growtoday").Value.ToString("0.00%")
                              + "[" + rows[0].Field<decimal?>("Max20Down").Value.ToString("0.00%") + "] "
                            );


                    }
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

        public class DownLoadSinaResult
        {
            public DateTime? LastTime { get; set; }
            public List<String> SendText { get; set; }
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
            string TemplateURL = "http://43.push2his.eastmoney.com/api/qt/stock/kline/get?cb=jQuery&secid={0}.{1}&ut=fa5fd1943c7b386f172d6893dbfba10b&fields1=f1%2Cf2%2Cf3%2Cf4%2Cf5&fields2=f51%2Cf52%2Cf53%2Cf54%2Cf55%2Cf56%2Cf57%2Cf58&klt=101&fqt=0&end=20500101&lmt=21&_=1596726494793";
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

                    switch (Lines.Count - containday - i)
                    {
                        case 0:
                            item.SetField<decimal?>("lday01_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday01_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("growday01", CaculateGrowUp(item.Field<decimal?>("lday01_end"), item.Field<decimal?>("lday01_min")));

                            break;
                        case 1:
                            item.SetField<decimal?>("lday02_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday02_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("growday02", CaculateGrowUp(item.Field<decimal?>("lday02_end"), item.Field<decimal?>("lday02_min")));

                            break;
                        case 2:
                            item.SetField<decimal?>("lday03_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday03_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("growday03", CaculateGrowUp(item.Field<decimal?>("lday03_end"), item.Field<decimal?>("lday03_min")));

                            break;
                        case 3:
                            item.SetField<decimal?>("lday04_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday04_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("growday04", CaculateGrowUp(item.Field<decimal?>("lday04_end"), item.Field<decimal?>("lday04_min")));

                            break;
                        case 4:
                            item.SetField<decimal?>("lday05_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday05_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("growday05", CaculateGrowUp(item.Field<decimal?>("lday05_end"), item.Field<decimal?>("lday05_min")));

                            break;
                        case 5:
                            item.SetField<decimal?>("lday06_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday06_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("growday06", CaculateGrowUp(item.Field<decimal?>("lday06_end"), item.Field<decimal?>("lday06_min")));

                            break;
                        case 6:
                            item.SetField<decimal?>("lday07_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday07_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("growday07", CaculateGrowUp(item.Field<decimal?>("lday07_end"), item.Field<decimal?>("lday07_min")));

                            break;
                        case 7:
                            item.SetField<decimal?>("lday08_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday08_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("growday08", CaculateGrowUp(item.Field<decimal?>("lday08_end"), item.Field<decimal?>("lday08_min")));

                            break;
                        case 8:
                            item.SetField<decimal?>("lday09_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday09_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("growday09", CaculateGrowUp(item.Field<decimal?>("lday09_end"), item.Field<decimal?>("lday09_min")));

                            break;
                        case 9:
                            item.SetField<decimal?>("lday10_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday10_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("growday10", CaculateGrowUp(item.Field<decimal?>("lday10_end"), item.Field<decimal?>("lday10_min")));

                            break;
                        case 10:
                            item.SetField<decimal?>("lday11_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday11_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("growday11", CaculateGrowUp(item.Field<decimal?>("lday11_end"), item.Field<decimal?>("lday11_min")));

                            break;
                        case 11:
                            item.SetField<decimal?>("lday12_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday12_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("growday12", CaculateGrowUp(item.Field<decimal?>("lday12_end"), item.Field<decimal?>("lday12_min")));

                            break;
                        case 12:
                            item.SetField<decimal?>("lday13_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday13_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("growday13", CaculateGrowUp(item.Field<decimal?>("lday13_end"), item.Field<decimal?>("lday13_min")));

                            break;
                        case 13:
                            item.SetField<decimal?>("lday14_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday14_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("growday14", CaculateGrowUp(item.Field<decimal?>("lday14_end"), item.Field<decimal?>("lday14_min")));

                            break;
                        case 14:
                            item.SetField<decimal?>("lday15_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday15_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("growday15", CaculateGrowUp(item.Field<decimal?>("lday15_end"), item.Field<decimal?>("lday15_min")));

                            break;
                        case 15:
                            item.SetField<decimal?>("lday16_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday16_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("growday16", CaculateGrowUp(item.Field<decimal?>("lday16_end"), item.Field<decimal?>("lday16_min")));

                            break;
                        case 16:
                            item.SetField<decimal?>("lday17_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday17_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("growday17", CaculateGrowUp(item.Field<decimal?>("lday17_end"), item.Field<decimal?>("lday17_min")));

                            break;
                        case 17:
                            item.SetField<decimal?>("lday18_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday18_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("growday18", CaculateGrowUp(item.Field<decimal?>("lday18_end"), item.Field<decimal?>("lday18_min")));

                            break;
                        case 18:
                            item.SetField<decimal?>("lday19_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday19_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("growday19", CaculateGrowUp(item.Field<decimal?>("lday19_end"), item.Field<decimal?>("lday19_min")));

                            break;
                        case 19:
                            item.SetField<decimal?>("lday20_min", Convert.ToDecimal(infs[4]));
                            item.SetField<decimal?>("lday20_end", Convert.ToDecimal(infs[2]));
                            item.SetField<decimal?>("growday20", CaculateGrowUp(item.Field<decimal?>("lday20_end"), item.Field<decimal?>("lday20_min")));

                            break;

                        default:

                            break;

                    }
                    if (Lines.Count - containday - i > 20)
                    {
                        break;
                    }

                }//日K线循环结束
                SetDayGrow(item);
                Application.DoEvents();
            }//行循环结束
            ss_mian_label.Text = "下载完成";
        }
        private void SetDayGrow(DataRow item)
        {
            Decimal? max20growday = 0;
            Decimal? max20growday_15avg = 0;
            decimal? test20growday = item.Field<decimal?>("growday01");
            if (test20growday > max20growday || max20growday == null)
            {
                max20growday = test20growday;
                max20growday_15avg = item.Field<decimal?>("day01_15avg");
            }

            test20growday = item.Field<decimal?>("growday02");
            if (test20growday > max20growday)
            {
                max20growday = test20growday;
                max20growday_15avg = item.Field<decimal?>("day02_15avg");
            }
            test20growday = item.Field<decimal?>("growday03");
            if (test20growday > max20growday)
            {
                max20growday = test20growday;
                max20growday_15avg = item.Field<decimal?>("day03_15avg");
            }
            test20growday = item.Field<decimal?>("growday04");
            if (test20growday > max20growday)
            {
                max20growday = test20growday;
                max20growday_15avg = item.Field<decimal?>("day04_15avg");
            }
            test20growday = item.Field<decimal?>("growday05");
            if (test20growday > max20growday)
            {
                max20growday = test20growday;
                max20growday_15avg = item.Field<decimal?>("day05_15avg");
            }
            test20growday = item.Field<decimal?>("growday06");
            if (test20growday > max20growday)
            {
                max20growday = test20growday;
                max20growday_15avg = item.Field<decimal?>("day06_15avg");
            }
            test20growday = item.Field<decimal?>("growday07");
            if (test20growday > max20growday)
            {
                max20growday = test20growday;
                max20growday_15avg = item.Field<decimal?>("day07_15avg");
            }
            test20growday = item.Field<decimal?>("growday08");
            if (test20growday > max20growday)
            {
                max20growday = test20growday;
                max20growday_15avg = item.Field<decimal?>("day08_15avg");
            }
            test20growday = item.Field<decimal?>("growday09");
            if (test20growday > max20growday)
            {
                max20growday = test20growday;
                max20growday_15avg = item.Field<decimal?>("day09_15avg");
            }
            test20growday = item.Field<decimal?>("growday10");
            if (test20growday > max20growday)
            {
                max20growday = test20growday;
                max20growday_15avg = item.Field<decimal?>("day10_15avg");
            }
            test20growday = item.Field<decimal?>("growday11");
            if (test20growday > max20growday)
            {
                max20growday = test20growday;
                max20growday_15avg = item.Field<decimal?>("day11_15avg");
            }
            test20growday = item.Field<decimal?>("growday12");
            if (test20growday > max20growday)
            {
                max20growday = test20growday;
                max20growday_15avg = item.Field<decimal?>("day12_15avg");
            }
            test20growday = item.Field<decimal?>("growday13");
            if (test20growday > max20growday)
            {
                max20growday = test20growday;
                max20growday_15avg = item.Field<decimal?>("day13_15avg");
            }
            test20growday = item.Field<decimal?>("growday14");
            if (test20growday > max20growday)
            {
                max20growday = test20growday;
                max20growday_15avg = item.Field<decimal?>("day14_15avg");
            }
            test20growday = item.Field<decimal?>("growday15");
            if (test20growday > max20growday)
            {
                max20growday = test20growday;
                max20growday_15avg = item.Field<decimal?>("day15_15avg");
            }
            test20growday = item.Field<decimal?>("growday16");
            if (test20growday > max20growday)
            {
                max20growday = test20growday;
                max20growday_15avg = item.Field<decimal?>("day16_15avg");
            }
            test20growday = item.Field<decimal?>("growday17");
            if (test20growday > max20growday)
            {
                max20growday = test20growday;
                max20growday_15avg = item.Field<decimal?>("day17_15avg");
            }
            test20growday = item.Field<decimal?>("growday18");
            if (test20growday > max20growday)
            {
                max20growday = test20growday;
                max20growday_15avg = item.Field<decimal?>("day18_15avg");
            }
            test20growday = item.Field<decimal?>("growday19");
            if (test20growday > max20growday)
            {
                max20growday = test20growday;
                max20growday_15avg = item.Field<decimal?>("day19_15avg");
            }
            test20growday = item.Field<decimal?>("growday20");
            if (test20growday > max20growday)
            {
                max20growday = test20growday;
                max20growday_15avg = item.Field<decimal?>("day20_15avg");
            }


            item.SetField<decimal?>("max20growday", max20growday);
            item.SetField<decimal?>("max20growday_avg15", max20growday);
            ss_mian_label.Text = "正在更新Max20growDay" + item.Field<string>("stockname");
            SetMax20Down(item);
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
                  && (DateTime.Now.Hour * 60 + DateTime.Now.Minute <= 15 * 60))
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
            listsource.Rows[0].SetField<bool?>("issuppose", true);
            listsource.Rows[1].SetField<bool?>("issuppose", true);

            string access = msg.AccessToken;
            msg.SendTextMsg(listsource.Rows[0].Field<String>("stockname")
                + "[" + listsource.Rows[0].Field<String>("codevalue") + "]"
                 + listsource.Rows[0].Field<decimal?>("growtoday").Value.ToString("0.00%")
                );
        }

        DateTime? LastOpenDay = null;
        private void Set_DatyOpen_Click(object sender, EventArgs e)
        {
            if ((LastOpenDay == null || LastOpenDay != DateTime.Today))
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
                    SetMinGrow(item);
                    SetDayGrow(item);

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

                                    + (tb_rerise.Text == "" ? "" : " and growtoday>= " + tb_rerise.Text + "/100.0 ")
                                    + (tb_PriceFrom.Text == "" ? "" : " and nowprice>= " + tb_PriceFrom.Text + " ")
                                      + (tb_PriceTo.Text == "" ? "" : " and nowprice<= " + tb_PriceTo.Text + " ")
                                    + (tb_max20growup.Text == "" ? "" : " and max20growday <= " + tb_max20growup.Text + "/100.0 ")
                               + (tb_max20downto.Text == "" ? "" : " and max20down <= " + tb_max20downto.Text + "/100.0 ")
                                 + (tb_max20downfrom.Text == "" ? "" : " and max20down >= " + tb_max20downfrom.Text + "/100.0 ")
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
                    gv_list.ClearSelection();
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
            sf.Left = this.Left;
            sf.Top = this.Top + this.Height - 8;
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
            //rowqty
        }

        private void tb_max20downfrom_TextChanged(object sender, EventArgs e)
        {
            ReloadFilter();
        }

        private void Test_Restore_Click(object sender, EventArgs e)
        {

            //listsource.Columns.Add("avg20", typeof(decimal));
            //return;
            foreach (DataRow item in listsource.Rows)
            {
                //item.SetField("issuppose", false);
                item.SetField("supposename","");
            }

        }

        private void gv_main_SelectionChanged(object sender, EventArgs e)
        {


            if (gv_main.SelectedRows.Count > 0)
            {
                DataGridViewRow sr = gv_main.SelectedRows[0];
                string Code = sr.Cells["codetype2"].Value.ToString() + sr.Cells["codevalue2"].Value.ToString();
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
    }//class
}//namespace
