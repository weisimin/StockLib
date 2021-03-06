﻿using System;
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
    public partial class SubForm : Form
    {
        public SubForm()
        {
            InitializeComponent();
        }

        private void SubForm_Load(object sender, EventArgs e)
        {
            // bs_sub.Filter = "issuppose='true'";
            gv_main.AutoGenerateColumns = true;
            gv_history.AutoGenerateColumns = true;
        }

        private void MI_ExportToExcel_Click(object sender, EventArgs e)
        {
             NetFramework.Util_XLS.ExportDataSetToExcel((bs_sub.DataSource as DataView).Table, Application.StartupPath+"\\export.xlsx");
            System.Diagnostics.Process.Start(Application.StartupPath);
        }
    }
}
