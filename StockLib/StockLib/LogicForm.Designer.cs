﻿namespace StockLib
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bs_main = new System.Windows.Forms.BindingSource(this.components);
            this.gv_list = new System.Windows.Forms.DataGridView();
            this.Menu_Main = new System.Windows.Forms.MenuStrip();
            this.MI_System = new System.Windows.Forms.ToolStripMenuItem();
            this.Download_History = new System.Windows.Forms.ToolStripMenuItem();
            this.Download_Now = new System.Windows.Forms.ToolStripMenuItem();
            this.Download_AllCode = new System.Windows.Forms.ToolStripMenuItem();
            this.Save_Data = new System.Windows.Forms.ToolStripMenuItem();
            this.Download_DeleteAndReDown = new System.Windows.Forms.ToolStripMenuItem();
            this.Set_DayNoTrans = new System.Windows.Forms.ToolStripMenuItem();
            this.Set_DatyOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.Test_Func = new System.Windows.Forms.ToolStripMenuItem();
            this.Test_SetSuppose = new System.Windows.Forms.ToolStripMenuItem();
            this.Test_Restore = new System.Windows.Forms.ToolStripMenuItem();
            this.ss_main = new System.Windows.Forms.StatusStrip();
            this.ss_mian_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.time_refresh = new System.Windows.Forms.Timer(this.components);
            this.gb_filter = new System.Windows.Forms.GroupBox();
            this.fil_cb_focus = new System.Windows.Forms.CheckBox();
            this.fil_tb_name = new System.Windows.Forms.TextBox();
            this.fil_name = new System.Windows.Forms.Label();
            this.fil_tbcode = new System.Windows.Forms.TextBox();
            this.fil_code = new System.Windows.Forms.Label();
            this.pop_m_grid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pop_m_grid_setisfoucus = new System.Windows.Forms.ToolStripMenuItem();
            this.gb_control = new System.Windows.Forms.GroupBox();
            this.con_cb_wechat = new System.Windows.Forms.CheckBox();
            this.gv_main = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bs_sub = new System.Windows.Forms.BindingSource(this.components);
            this.lbl_suppose = new System.Windows.Forms.Label();
            this.lbl_watch = new System.Windows.Forms.Label();
            this.codevalue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nowprice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isfocus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.growtoday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.高次 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.bs_main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_list)).BeginInit();
            this.Menu_Main.SuspendLayout();
            this.ss_main.SuspendLayout();
            this.gb_filter.SuspendLayout();
            this.pop_m_grid.SuspendLayout();
            this.gb_control.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_sub)).BeginInit();
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
            this.isfocus,
            this.growtoday,
            this.高次,
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
            this.gv_list.Location = new System.Drawing.Point(11, 50);
            this.gv_list.Margin = new System.Windows.Forms.Padding(2);
            this.gv_list.Name = "gv_list";
            this.gv_list.ReadOnly = true;
            this.gv_list.RowHeadersVisible = false;
            this.gv_list.RowTemplate.Height = 27;
            this.gv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv_list.Size = new System.Drawing.Size(419, 171);
            this.gv_list.TabIndex = 0;
            this.gv_list.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gv_list_CellMouseUp);
            // 
            // Menu_Main
            // 
            this.Menu_Main.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Menu_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_System,
            this.Test_Func});
            this.Menu_Main.Location = new System.Drawing.Point(0, 0);
            this.Menu_Main.Name = "Menu_Main";
            this.Menu_Main.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.Menu_Main.Size = new System.Drawing.Size(750, 25);
            this.Menu_Main.TabIndex = 1;
            this.Menu_Main.Text = "menuStrip1";
            this.Menu_Main.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Menu_Main_ItemClicked);
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
            this.MI_System.Size = new System.Drawing.Size(82, 21);
            this.MI_System.Text = "MI_System";
            // 
            // Download_History
            // 
            this.Download_History.Name = "Download_History";
            this.Download_History.Size = new System.Drawing.Size(248, 22);
            this.Download_History.Text = "Download_History";
            this.Download_History.Click += new System.EventHandler(this.Download_History_Click);
            // 
            // Download_Now
            // 
            this.Download_Now.Name = "Download_Now";
            this.Download_Now.Size = new System.Drawing.Size(248, 22);
            this.Download_Now.Text = "Download_Now";
            this.Download_Now.Click += new System.EventHandler(this.Download_Now_Click);
            // 
            // Download_AllCode
            // 
            this.Download_AllCode.Name = "Download_AllCode";
            this.Download_AllCode.Size = new System.Drawing.Size(248, 22);
            this.Download_AllCode.Text = "Download_AllCode";
            this.Download_AllCode.Click += new System.EventHandler(this.Download_AllCode_Click);
            // 
            // Save_Data
            // 
            this.Save_Data.Name = "Save_Data";
            this.Save_Data.Size = new System.Drawing.Size(248, 22);
            this.Save_Data.Text = "Save_Data";
            this.Save_Data.Click += new System.EventHandler(this.Save_Data_Click);
            // 
            // Download_DeleteAndReDown
            // 
            this.Download_DeleteAndReDown.Name = "Download_DeleteAndReDown";
            this.Download_DeleteAndReDown.Size = new System.Drawing.Size(248, 22);
            this.Download_DeleteAndReDown.Text = "Download_DeleteAndReDown";
            this.Download_DeleteAndReDown.Click += new System.EventHandler(this.Download_DeleteAndReDown_Click);
            // 
            // Set_DayNoTrans
            // 
            this.Set_DayNoTrans.Name = "Set_DayNoTrans";
            this.Set_DayNoTrans.Size = new System.Drawing.Size(248, 22);
            this.Set_DayNoTrans.Text = "Set_DayNoTrans";
            this.Set_DayNoTrans.Click += new System.EventHandler(this.Set_DayNoTrans_Click);
            // 
            // Set_DatyOpen
            // 
            this.Set_DatyOpen.Name = "Set_DatyOpen";
            this.Set_DatyOpen.Size = new System.Drawing.Size(248, 22);
            this.Set_DatyOpen.Text = "Set_DatyOpen";
            this.Set_DatyOpen.Click += new System.EventHandler(this.Set_DatyOpen_Click);
            // 
            // Test_Func
            // 
            this.Test_Func.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Test_SetSuppose,
            this.Test_Restore});
            this.Test_Func.Name = "Test_Func";
            this.Test_Func.Size = new System.Drawing.Size(75, 21);
            this.Test_Func.Text = "Test_Func";
            // 
            // Test_SetSuppose
            // 
            this.Test_SetSuppose.Name = "Test_SetSuppose";
            this.Test_SetSuppose.Size = new System.Drawing.Size(174, 22);
            this.Test_SetSuppose.Text = "Test_SetSuppose";
            this.Test_SetSuppose.Click += new System.EventHandler(this.Test_SetSuppose_Click);
            // 
            // Test_Restore
            // 
            this.Test_Restore.Name = "Test_Restore";
            this.Test_Restore.Size = new System.Drawing.Size(174, 22);
            this.Test_Restore.Text = "Test_Restore";
            // 
            // ss_main
            // 
            this.ss_main.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ss_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ss_mian_label});
            this.ss_main.Location = new System.Drawing.Point(0, 394);
            this.ss_main.Name = "ss_main";
            this.ss_main.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.ss_main.Size = new System.Drawing.Size(750, 25);
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
            // gb_filter
            // 
            this.gb_filter.Controls.Add(this.fil_cb_focus);
            this.gb_filter.Controls.Add(this.fil_tb_name);
            this.gb_filter.Controls.Add(this.fil_name);
            this.gb_filter.Controls.Add(this.fil_tbcode);
            this.gb_filter.Controls.Add(this.fil_code);
            this.gb_filter.Location = new System.Drawing.Point(8, 225);
            this.gb_filter.Name = "gb_filter";
            this.gb_filter.Size = new System.Drawing.Size(411, 98);
            this.gb_filter.TabIndex = 9;
            this.gb_filter.TabStop = false;
            this.gb_filter.Text = "过滤条件";
            // 
            // fil_cb_focus
            // 
            this.fil_cb_focus.AutoSize = true;
            this.fil_cb_focus.Location = new System.Drawing.Point(55, 77);
            this.fil_cb_focus.Name = "fil_cb_focus";
            this.fil_cb_focus.Size = new System.Drawing.Size(48, 16);
            this.fil_cb_focus.TabIndex = 14;
            this.fil_cb_focus.Text = "关注";
            this.fil_cb_focus.UseVisualStyleBackColor = true;
            this.fil_cb_focus.CheckedChanged += new System.EventHandler(this.fil_cb_focus_CheckedChanged);
            // 
            // fil_tb_name
            // 
            this.fil_tb_name.Location = new System.Drawing.Point(55, 49);
            this.fil_tb_name.Name = "fil_tb_name";
            this.fil_tb_name.Size = new System.Drawing.Size(100, 21);
            this.fil_tb_name.TabIndex = 12;
            this.fil_tb_name.TextChanged += new System.EventHandler(this.fil_tb_name_TextChanged);
            // 
            // fil_name
            // 
            this.fil_name.AutoSize = true;
            this.fil_name.Location = new System.Drawing.Point(20, 53);
            this.fil_name.Name = "fil_name";
            this.fil_name.Size = new System.Drawing.Size(29, 12);
            this.fil_name.TabIndex = 11;
            this.fil_name.Text = "名称";
            // 
            // fil_tbcode
            // 
            this.fil_tbcode.Location = new System.Drawing.Point(55, 25);
            this.fil_tbcode.Name = "fil_tbcode";
            this.fil_tbcode.Size = new System.Drawing.Size(100, 21);
            this.fil_tbcode.TabIndex = 10;
            this.fil_tbcode.TextChanged += new System.EventHandler(this.fil_tbcode_TextChanged);
            // 
            // fil_code
            // 
            this.fil_code.AutoSize = true;
            this.fil_code.Location = new System.Drawing.Point(20, 29);
            this.fil_code.Name = "fil_code";
            this.fil_code.Size = new System.Drawing.Size(29, 12);
            this.fil_code.TabIndex = 9;
            this.fil_code.Text = "代码";
            // 
            // pop_m_grid
            // 
            this.pop_m_grid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pop_m_grid_setisfoucus});
            this.pop_m_grid.Name = "pop_m_grid";
            this.pop_m_grid.Size = new System.Drawing.Size(101, 26);
            this.pop_m_grid.Opening += new System.ComponentModel.CancelEventHandler(this.pop_m_grid_Opening);
            // 
            // pop_m_grid_setisfoucus
            // 
            this.pop_m_grid_setisfoucus.Name = "pop_m_grid_setisfoucus";
            this.pop_m_grid_setisfoucus.Size = new System.Drawing.Size(100, 22);
            this.pop_m_grid_setisfoucus.Text = "关注";
            this.pop_m_grid_setisfoucus.Click += new System.EventHandler(this.pop_m_grid_setisfoucus_Click);
            // 
            // gb_control
            // 
            this.gb_control.Controls.Add(this.con_cb_wechat);
            this.gb_control.Location = new System.Drawing.Point(13, 330);
            this.gb_control.Name = "gb_control";
            this.gb_control.Size = new System.Drawing.Size(406, 61);
            this.gb_control.TabIndex = 10;
            this.gb_control.TabStop = false;
            this.gb_control.Text = "控制";
            // 
            // con_cb_wechat
            // 
            this.con_cb_wechat.AutoSize = true;
            this.con_cb_wechat.Location = new System.Drawing.Point(50, 20);
            this.con_cb_wechat.Name = "con_cb_wechat";
            this.con_cb_wechat.Size = new System.Drawing.Size(72, 16);
            this.con_cb_wechat.TabIndex = 16;
            this.con_cb_wechat.Text = "微信消息";
            this.con_cb_wechat.UseVisualStyleBackColor = true;
            // 
            // gv_main
            // 
            this.gv_main.AllowUserToAddRows = false;
            this.gv_main.AllowUserToDeleteRows = false;
            this.gv_main.AutoGenerateColumns = false;
            this.gv_main.ColumnHeadersHeight = 25;
            this.gv_main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gv_main.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.gv_main.DataSource = this.bs_sub;
            this.gv_main.Location = new System.Drawing.Point(431, 50);
            this.gv_main.Margin = new System.Windows.Forms.Padding(2);
            this.gv_main.Name = "gv_main";
            this.gv_main.ReadOnly = true;
            this.gv_main.RowHeadersVisible = false;
            this.gv_main.RowTemplate.Height = 27;
            this.gv_main.Size = new System.Drawing.Size(308, 341);
            this.gv_main.TabIndex = 11;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "codevalue";
            this.dataGridViewTextBoxColumn1.HeaderText = "代码";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 60;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "stockname";
            this.dataGridViewTextBoxColumn2.HeaderText = "名称";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "nowprice";
            this.dataGridViewTextBoxColumn3.HeaderText = "价格";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 50;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "growtoday";
            dataGridViewCellStyle5.Format = "0.00%";
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn4.HeaderText = "涨跌";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 50;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "max10growmin";
            dataGridViewCellStyle6.Format = "0.00%";
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn5.HeaderText = "大10分";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 60;
            // 
            // lbl_suppose
            // 
            this.lbl_suppose.AutoSize = true;
            this.lbl_suppose.Location = new System.Drawing.Point(433, 29);
            this.lbl_suppose.Name = "lbl_suppose";
            this.lbl_suppose.Size = new System.Drawing.Size(35, 12);
            this.lbl_suppose.TabIndex = 12;
            this.lbl_suppose.Text = "推荐:";
            // 
            // lbl_watch
            // 
            this.lbl_watch.AutoSize = true;
            this.lbl_watch.Location = new System.Drawing.Point(12, 29);
            this.lbl_watch.Name = "lbl_watch";
            this.lbl_watch.Size = new System.Drawing.Size(35, 12);
            this.lbl_watch.TabIndex = 13;
            this.lbl_watch.Text = "监控:";
            // 
            // codevalue
            // 
            this.codevalue.DataPropertyName = "codevalue";
            this.codevalue.Frozen = true;
            this.codevalue.HeaderText = "代码";
            this.codevalue.Name = "codevalue";
            this.codevalue.ReadOnly = true;
            this.codevalue.Width = 50;
            // 
            // stockname
            // 
            this.stockname.DataPropertyName = "stockname";
            this.stockname.Frozen = true;
            this.stockname.HeaderText = "名称";
            this.stockname.Name = "stockname";
            this.stockname.ReadOnly = true;
            this.stockname.Width = 60;
            // 
            // nowprice
            // 
            this.nowprice.DataPropertyName = "nowprice";
            this.nowprice.Frozen = true;
            this.nowprice.HeaderText = "价格";
            this.nowprice.Name = "nowprice";
            this.nowprice.ReadOnly = true;
            this.nowprice.Width = 50;
            // 
            // isfocus
            // 
            this.isfocus.DataPropertyName = "isfocus";
            this.isfocus.HeaderText = "关注";
            this.isfocus.Name = "isfocus";
            this.isfocus.ReadOnly = true;
            this.isfocus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isfocus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.isfocus.Width = 50;
            // 
            // growtoday
            // 
            this.growtoday.DataPropertyName = "growtoday";
            dataGridViewCellStyle1.Format = "0.00%";
            this.growtoday.DefaultCellStyle = dataGridViewCellStyle1;
            this.growtoday.HeaderText = "涨跌";
            this.growtoday.Name = "growtoday";
            this.growtoday.ReadOnly = true;
            this.growtoday.Width = 50;
            // 
            // 高次
            // 
            this.高次.DataPropertyName = "MaxHighQty";
            this.高次.HeaderText = "高次";
            this.高次.Name = "高次";
            this.高次.ReadOnly = true;
            // 
            // max10growmin
            // 
            this.max10growmin.DataPropertyName = "max10growmin";
            dataGridViewCellStyle2.Format = "0.00%";
            this.max10growmin.DefaultCellStyle = dataGridViewCellStyle2;
            this.max10growmin.HeaderText = "大10分";
            this.max10growmin.Name = "max10growmin";
            this.max10growmin.ReadOnly = true;
            this.max10growmin.Width = 70;
            // 
            // max20growday
            // 
            this.max20growday.DataPropertyName = "max20growday";
            dataGridViewCellStyle3.Format = "0.00%";
            this.max20growday.DefaultCellStyle = dataGridViewCellStyle3;
            this.max20growday.HeaderText = "大20天";
            this.max20growday.Name = "max20growday";
            this.max20growday.ReadOnly = true;
            this.max20growday.Width = 70;
            // 
            // nowtime
            // 
            this.nowtime.DataPropertyName = "nowtime";
            dataGridViewCellStyle4.Format = "HH:mm";
            this.nowtime.DefaultCellStyle = dataGridViewCellStyle4;
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
            this.lmin01.Width = 50;
            // 
            // lmin02
            // 
            this.lmin02.DataPropertyName = "lmin02";
            this.lmin02.HeaderText = "分2";
            this.lmin02.Name = "lmin02";
            this.lmin02.ReadOnly = true;
            this.lmin02.Width = 50;
            // 
            // lmin03
            // 
            this.lmin03.DataPropertyName = "lmin03";
            this.lmin03.HeaderText = "分3";
            this.lmin03.Name = "lmin03";
            this.lmin03.ReadOnly = true;
            this.lmin03.Width = 50;
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
            // LogicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 419);
            this.Controls.Add(this.lbl_watch);
            this.Controls.Add(this.lbl_suppose);
            this.Controls.Add(this.gv_main);
            this.Controls.Add(this.gb_control);
            this.Controls.Add(this.gb_filter);
            this.Controls.Add(this.ss_main);
            this.Controls.Add(this.gv_list);
            this.Controls.Add(this.Menu_Main);
            this.MainMenuStrip = this.Menu_Main;
            this.Name = "LogicForm";
            this.Text = "最新行情";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogicForm_FormClosing);
            this.Load += new System.EventHandler(this.LogicForm_Load);
            this.Shown += new System.EventHandler(this.LogicForm_Shown);
            this.Move += new System.EventHandler(this.LogicForm_Move);
            ((System.ComponentModel.ISupportInitialize)(this.bs_main)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_list)).EndInit();
            this.Menu_Main.ResumeLayout(false);
            this.Menu_Main.PerformLayout();
            this.ss_main.ResumeLayout(false);
            this.ss_main.PerformLayout();
            this.gb_filter.ResumeLayout(false);
            this.gb_filter.PerformLayout();
            this.pop_m_grid.ResumeLayout(false);
            this.gb_control.ResumeLayout(false);
            this.gb_control.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_main)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_sub)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem Test_Restore;
        private System.Windows.Forms.ToolStripMenuItem Set_DatyOpen;
        private System.Windows.Forms.GroupBox gb_filter;
        private System.Windows.Forms.CheckBox fil_cb_focus;
        private System.Windows.Forms.TextBox fil_tb_name;
        private System.Windows.Forms.Label fil_name;
        private System.Windows.Forms.TextBox fil_tbcode;
        private System.Windows.Forms.Label fil_code;
        private System.Windows.Forms.ContextMenuStrip pop_m_grid;
        private System.Windows.Forms.ToolStripMenuItem pop_m_grid_setisfoucus;
        private System.Windows.Forms.GroupBox gb_control;
        private System.Windows.Forms.CheckBox con_cb_wechat;
        private System.Windows.Forms.DataGridView gv_main;
        public System.Windows.Forms.BindingSource bs_sub;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Label lbl_suppose;
        private System.Windows.Forms.Label lbl_watch;
        private System.Windows.Forms.DataGridViewTextBoxColumn codevalue;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockname;
        private System.Windows.Forms.DataGridViewTextBoxColumn nowprice;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isfocus;
        private System.Windows.Forms.DataGridViewTextBoxColumn growtoday;
        private System.Windows.Forms.DataGridViewTextBoxColumn 高次;
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
    }
}

