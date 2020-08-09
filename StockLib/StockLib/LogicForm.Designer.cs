namespace StockLib
{
    partial class LogicForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bs_main = new System.Windows.Forms.BindingSource(this.components);
            this.gv_list = new System.Windows.Forms.DataGridView();
            this.codevalue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nowprice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.growtoday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.max10growmin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.max20growday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nowtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lmin01 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lmin02 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lmin03 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lday01_min = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lday01_end = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lday02_min = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lday02_end = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lday03_min = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lday03_end = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Menu_Main = new System.Windows.Forms.MenuStrip();
            this.MI_System = new System.Windows.Forms.ToolStripMenuItem();
            this.Download_History = new System.Windows.Forms.ToolStripMenuItem();
            this.Download_Now = new System.Windows.Forms.ToolStripMenuItem();
            this.Download_AllCode = new System.Windows.Forms.ToolStripMenuItem();
            this.Save_Data = new System.Windows.Forms.ToolStripMenuItem();
            this.Download_DeleteAndReDown = new System.Windows.Forms.ToolStripMenuItem();
            this.Set_DayNoTrans = new System.Windows.Forms.ToolStripMenuItem();
            this.Test_Func = new System.Windows.Forms.ToolStripMenuItem();
            this.Test_SetSuppose = new System.Windows.Forms.ToolStripMenuItem();
            this.ss_main = new System.Windows.Forms.StatusStrip();
            this.ss_mian_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.time_refresh = new System.Windows.Forms.Timer(this.components);
            this.Test_Restore = new System.Windows.Forms.ToolStripMenuItem();
            this.Set_DatyOpen = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.bs_main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_list)).BeginInit();
            this.Menu_Main.SuspendLayout();
            this.ss_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // gv_list
            // 
            this.gv_list.AllowUserToAddRows = false;
            this.gv_list.AllowUserToDeleteRows = false;
            this.gv_list.AllowUserToOrderColumns = true;
            this.gv_list.AutoGenerateColumns = false;
            this.gv_list.ColumnHeadersHeight = 25;
            this.gv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codevalue,
            this.stockname,
            this.nowprice,
            this.growtoday,
            this.max10growmin,
            this.max20growday,
            this.nowtime,
            this.lmin01,
            this.lmin02,
            this.lmin03,
            this.lday01_min,
            this.lday01_end,
            this.lday02_min,
            this.lday02_end,
            this.lday03_min,
            this.lday03_end});
            this.gv_list.DataSource = this.bs_main;
            this.gv_list.Location = new System.Drawing.Point(0, 28);
            this.gv_list.Name = "gv_list";
            this.gv_list.ReadOnly = true;
            this.gv_list.RowHeadersVisible = false;
            this.gv_list.RowTemplate.Height = 27;
            this.gv_list.Size = new System.Drawing.Size(1175, 377);
            this.gv_list.TabIndex = 0;
            // 
            // codevalue
            // 
            this.codevalue.DataPropertyName = "codevalue";
            this.codevalue.Frozen = true;
            this.codevalue.HeaderText = "代码";
            this.codevalue.Name = "codevalue";
            this.codevalue.ReadOnly = true;
            // 
            // stockname
            // 
            this.stockname.DataPropertyName = "stockname";
            this.stockname.Frozen = true;
            this.stockname.HeaderText = "名称";
            this.stockname.Name = "stockname";
            this.stockname.ReadOnly = true;
            // 
            // nowprice
            // 
            this.nowprice.DataPropertyName = "nowprice";
            this.nowprice.Frozen = true;
            this.nowprice.HeaderText = "价格";
            this.nowprice.Name = "nowprice";
            this.nowprice.ReadOnly = true;
            this.nowprice.Width = 60;
            // 
            // growtoday
            // 
            this.growtoday.DataPropertyName = "growtoday";
            dataGridViewCellStyle5.Format = "0.00%";
            this.growtoday.DefaultCellStyle = dataGridViewCellStyle5;
            this.growtoday.HeaderText = "涨跌";
            this.growtoday.Name = "growtoday";
            this.growtoday.ReadOnly = true;
            this.growtoday.Width = 50;
            // 
            // max10growmin
            // 
            this.max10growmin.DataPropertyName = "max10growmin";
            dataGridViewCellStyle6.Format = "0.00%";
            this.max10growmin.DefaultCellStyle = dataGridViewCellStyle6;
            this.max10growmin.HeaderText = "大10分";
            this.max10growmin.Name = "max10growmin";
            this.max10growmin.ReadOnly = true;
            this.max10growmin.Width = 70;
            // 
            // max20growday
            // 
            this.max20growday.DataPropertyName = "max20growday";
            dataGridViewCellStyle7.Format = "0.00%";
            this.max20growday.DefaultCellStyle = dataGridViewCellStyle7;
            this.max20growday.HeaderText = "大20天";
            this.max20growday.Name = "max20growday";
            this.max20growday.ReadOnly = true;
            this.max20growday.Width = 70;
            // 
            // nowtime
            // 
            this.nowtime.DataPropertyName = "nowtime";
            dataGridViewCellStyle8.Format = "HH:mm";
            this.nowtime.DefaultCellStyle = dataGridViewCellStyle8;
            this.nowtime.HeaderText = "时间";
            this.nowtime.Name = "nowtime";
            this.nowtime.ReadOnly = true;
            this.nowtime.Width = 50;
            // 
            // lmin01
            // 
            this.lmin01.DataPropertyName = "lmin01";
            this.lmin01.HeaderText = "分1";
            this.lmin01.Name = "lmin01";
            this.lmin01.ReadOnly = true;
            this.lmin01.Width = 60;
            // 
            // lmin02
            // 
            this.lmin02.DataPropertyName = "lmin02";
            this.lmin02.HeaderText = "分2";
            this.lmin02.Name = "lmin02";
            this.lmin02.ReadOnly = true;
            this.lmin02.Width = 60;
            // 
            // lmin03
            // 
            this.lmin03.DataPropertyName = "lmin03";
            this.lmin03.HeaderText = "分3";
            this.lmin03.Name = "lmin03";
            this.lmin03.ReadOnly = true;
            this.lmin03.Width = 60;
            // 
            // lday01_min
            // 
            this.lday01_min.DataPropertyName = "lday01_min";
            this.lday01_min.HeaderText = "日1小";
            this.lday01_min.Name = "lday01_min";
            this.lday01_min.ReadOnly = true;
            this.lday01_min.Width = 80;
            // 
            // lday01_end
            // 
            this.lday01_end.DataPropertyName = "lday01_end";
            this.lday01_end.HeaderText = "日1终";
            this.lday01_end.Name = "lday01_end";
            this.lday01_end.ReadOnly = true;
            this.lday01_end.Width = 80;
            // 
            // lday02_min
            // 
            this.lday02_min.DataPropertyName = "lday02_min";
            this.lday02_min.HeaderText = "日2小";
            this.lday02_min.Name = "lday02_min";
            this.lday02_min.ReadOnly = true;
            this.lday02_min.Width = 80;
            // 
            // lday02_end
            // 
            this.lday02_end.DataPropertyName = "lday02_end";
            this.lday02_end.HeaderText = "日2终";
            this.lday02_end.Name = "lday02_end";
            this.lday02_end.ReadOnly = true;
            this.lday02_end.Width = 80;
            // 
            // lday03_min
            // 
            this.lday03_min.DataPropertyName = "lday03_min";
            this.lday03_min.HeaderText = "日3小";
            this.lday03_min.Name = "lday03_min";
            this.lday03_min.ReadOnly = true;
            this.lday03_min.Width = 80;
            // 
            // lday03_end
            // 
            this.lday03_end.DataPropertyName = "lday03_end";
            this.lday03_end.HeaderText = "日3终";
            this.lday03_end.Name = "lday03_end";
            this.lday03_end.ReadOnly = true;
            this.lday03_end.Width = 80;
            // 
            // Menu_Main
            // 
            this.Menu_Main.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Menu_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_System,
            this.Test_Func});
            this.Menu_Main.Location = new System.Drawing.Point(0, 0);
            this.Menu_Main.Name = "Menu_Main";
            this.Menu_Main.Size = new System.Drawing.Size(1197, 28);
            this.Menu_Main.TabIndex = 1;
            this.Menu_Main.Text = "menuStrip1";
            // 
            // MI_System
            // 
            this.MI_System.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Download_History,
            this.Download_Now,
            this.Download_AllCode,
            this.Save_Data,
            this.Download_DeleteAndReDown,
            this.Set_DayNoTrans,
            this.Set_DatyOpen});
            this.MI_System.Name = "MI_System";
            this.MI_System.Size = new System.Drawing.Size(100, 24);
            this.MI_System.Text = "MI_System";
            // 
            // Download_History
            // 
            this.Download_History.Name = "Download_History";
            this.Download_History.Size = new System.Drawing.Size(304, 26);
            this.Download_History.Text = "Download_History";
            this.Download_History.Click += new System.EventHandler(this.Download_History_Click);
            // 
            // Download_Now
            // 
            this.Download_Now.Name = "Download_Now";
            this.Download_Now.Size = new System.Drawing.Size(304, 26);
            this.Download_Now.Text = "Download_Now";
            this.Download_Now.Click += new System.EventHandler(this.Download_Now_Click);
            // 
            // Download_AllCode
            // 
            this.Download_AllCode.Name = "Download_AllCode";
            this.Download_AllCode.Size = new System.Drawing.Size(304, 26);
            this.Download_AllCode.Text = "Download_AllCode";
            this.Download_AllCode.Click += new System.EventHandler(this.Download_AllCode_Click);
            // 
            // Save_Data
            // 
            this.Save_Data.Name = "Save_Data";
            this.Save_Data.Size = new System.Drawing.Size(304, 26);
            this.Save_Data.Text = "Save_Data";
            this.Save_Data.Click += new System.EventHandler(this.Save_Data_Click);
            // 
            // Download_DeleteAndReDown
            // 
            this.Download_DeleteAndReDown.Name = "Download_DeleteAndReDown";
            this.Download_DeleteAndReDown.Size = new System.Drawing.Size(304, 26);
            this.Download_DeleteAndReDown.Text = "Download_DeleteAndReDown";
            this.Download_DeleteAndReDown.Click += new System.EventHandler(this.Download_DeleteAndReDown_Click);
            // 
            // Set_DayNoTrans
            // 
            this.Set_DayNoTrans.Name = "Set_DayNoTrans";
            this.Set_DayNoTrans.Size = new System.Drawing.Size(304, 26);
            this.Set_DayNoTrans.Text = "Set_DayNoTrans";
            this.Set_DayNoTrans.Click += new System.EventHandler(this.Set_DayNoTrans_Click);
            // 
            // Test_Func
            // 
            this.Test_Func.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Test_SetSuppose,
            this.Test_Restore});
            this.Test_Func.Name = "Test_Func";
            this.Test_Func.Size = new System.Drawing.Size(93, 24);
            this.Test_Func.Text = "Test_Func";
            // 
            // Test_SetSuppose
            // 
            this.Test_SetSuppose.Name = "Test_SetSuppose";
            this.Test_SetSuppose.Size = new System.Drawing.Size(210, 26);
            this.Test_SetSuppose.Text = "Test_SetSuppose";
            this.Test_SetSuppose.Click += new System.EventHandler(this.Test_SetSuppose_Click);
            // 
            // ss_main
            // 
            this.ss_main.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ss_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ss_mian_label});
            this.ss_main.Location = new System.Drawing.Point(0, 408);
            this.ss_main.Name = "ss_main";
            this.ss_main.Size = new System.Drawing.Size(1197, 25);
            this.ss_main.TabIndex = 2;
            this.ss_main.Text = "statusStrip1";
            // 
            // ss_mian_label
            // 
            this.ss_mian_label.AutoSize = false;
            this.ss_mian_label.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ss_mian_label.Name = "ss_mian_label";
            this.ss_mian_label.Size = new System.Drawing.Size(200, 20);
            this.ss_mian_label.Text = "状态：";
            this.ss_mian_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // time_refresh
            // 
            this.time_refresh.Enabled = true;
            this.time_refresh.Interval = 5000;
            this.time_refresh.Tick += new System.EventHandler(this.time_refresh_Tick);
            // 
            // Test_Restore
            // 
            this.Test_Restore.Name = "Test_Restore";
            this.Test_Restore.Size = new System.Drawing.Size(210, 26);
            this.Test_Restore.Text = "Test_Restore";
            // 
            // Set_DatyOpen
            // 
            this.Set_DatyOpen.Name = "Set_DatyOpen";
            this.Set_DatyOpen.Size = new System.Drawing.Size(304, 26);
            this.Set_DatyOpen.Text = "Set_DatyOpen";
            this.Set_DatyOpen.Click += new System.EventHandler(this.Set_DatyOpen_Click);
            // 
            // LogicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1197, 433);
            this.Controls.Add(this.ss_main);
            this.Controls.Add(this.gv_list);
            this.Controls.Add(this.Menu_Main);
            this.MainMenuStrip = this.Menu_Main;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LogicForm";
            this.Text = "最新行情";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogicForm_FormClosing);
            this.Load += new System.EventHandler(this.LogicForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bs_main)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_list)).EndInit();
            this.Menu_Main.ResumeLayout(false);
            this.Menu_Main.PerformLayout();
            this.ss_main.ResumeLayout(false);
            this.ss_main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bs_main;
        private System.Windows.Forms.DataGridView gv_list;
        private System.Windows.Forms.MenuStrip Menu_Main;
        private System.Windows.Forms.ToolStripMenuItem MI_System;
        private System.Windows.Forms.ToolStripMenuItem Download_History;
        private System.Windows.Forms.ToolStripMenuItem Download_Now;
        private System.Windows.Forms.ToolStripMenuItem Download_AllCode;
        private System.Windows.Forms.ToolStripMenuItem Save_Data;
        private System.Windows.Forms.StatusStrip ss_main;
        private System.Windows.Forms.ToolStripStatusLabel ss_mian_label;
        private System.Windows.Forms.ToolStripMenuItem Download_DeleteAndReDown;
        private System.Windows.Forms.Timer time_refresh;
        private System.Windows.Forms.ToolStripMenuItem Set_DayNoTrans;
        private System.Windows.Forms.ToolStripMenuItem Test_Func;
        private System.Windows.Forms.ToolStripMenuItem Test_SetSuppose;
        private System.Windows.Forms.DataGridViewTextBoxColumn codevalue;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockname;
        private System.Windows.Forms.DataGridViewTextBoxColumn nowprice;
        private System.Windows.Forms.DataGridViewTextBoxColumn growtoday;
        private System.Windows.Forms.DataGridViewTextBoxColumn max10growmin;
        private System.Windows.Forms.DataGridViewTextBoxColumn max20growday;
        private System.Windows.Forms.DataGridViewTextBoxColumn nowtime;
        private System.Windows.Forms.DataGridViewTextBoxColumn lmin01;
        private System.Windows.Forms.DataGridViewTextBoxColumn lmin02;
        private System.Windows.Forms.DataGridViewTextBoxColumn lmin03;
        private System.Windows.Forms.DataGridViewTextBoxColumn lday01_min;
        private System.Windows.Forms.DataGridViewTextBoxColumn lday01_end;
        private System.Windows.Forms.DataGridViewTextBoxColumn lday02_min;
        private System.Windows.Forms.DataGridViewTextBoxColumn lday02_end;
        private System.Windows.Forms.DataGridViewTextBoxColumn lday03_min;
        private System.Windows.Forms.DataGridViewTextBoxColumn lday03_end;
        private System.Windows.Forms.ToolStripMenuItem Test_Restore;
        private System.Windows.Forms.ToolStripMenuItem Set_DatyOpen;
    }
}

