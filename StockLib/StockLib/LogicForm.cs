using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
           String Saves= NetFramework.Util_File.ReadToEnd(Application.StartupPath + "\\data.dat", Encoding.GetEncoding("GB2312"));
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

        }
    }
}
