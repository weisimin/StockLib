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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bs_main = new System.Windows.Forms.BindingSource(this.components);
            this.gv_list = new System.Windows.Forms.DataGridView();
            this.codetype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codevalue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nowprice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isfocus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.反弹 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updown = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.最近 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.上次 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.max20growday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.max20growday_avg15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.突破 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShortLow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.跌20天 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.max10growmin = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.Set_DatyOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.Test_Func = new System.Windows.Forms.ToolStripMenuItem();
            this.Test_SetSuppose = new System.Windows.Forms.ToolStripMenuItem();
            this.Test_Restore = new System.Windows.Forms.ToolStripMenuItem();
            this.ss_main = new System.Windows.Forms.StatusStrip();
            this.ss_mian_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.sl_diff = new System.Windows.Forms.ToolStripStatusLabel();
            this.time_refresh = new System.Windows.Forms.Timer(this.components);
            this.gb_filter = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_updownmax = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_growmin = new System.Windows.Forms.TextBox();
            this.tb_growmax = new System.Windows.Forms.TextBox();
            this.lbl_grow = new System.Windows.Forms.Label();
            this.lbl_max20growday_avg15 = new System.Windows.Forms.Label();
            this.tb_max20growday_avg15 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_max20downfrom = new System.Windows.Forms.TextBox();
            this.tb_max20downto = new System.Windows.Forms.TextBox();
            this.lbl_max20down = new System.Windows.Forms.Label();
            this.tb_max20growup = new System.Windows.Forms.TextBox();
            this.lbl_max20growup = new System.Windows.Forms.Label();
            this.tb_PriceTo = new System.Windows.Forms.TextBox();
            this.lbl_PriceTo = new System.Windows.Forms.Label();
            this.tb_PriceFrom = new System.Windows.Forms.TextBox();
            this.lbl_PriceFrom = new System.Windows.Forms.Label();
            this.tb_updownmin = new System.Windows.Forms.TextBox();
            this.lbl_updown = new System.Windows.Forms.Label();
            this.fil_cb_focus = new System.Windows.Forms.CheckBox();
            this.fil_tb_name = new System.Windows.Forms.TextBox();
            this.fil_name = new System.Windows.Forms.Label();
            this.fil_tbcode = new System.Windows.Forms.TextBox();
            this.fil_code = new System.Windows.Forms.Label();
            this.pop_m_grid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pop_m_grid_setisfoucus = new System.Windows.Forms.ToolStripMenuItem();
            this.gb_control = new System.Windows.Forms.GroupBox();
            this.cb_stop = new System.Windows.Forms.CheckBox();
            this.cb_showjpg = new System.Windows.Forms.CheckBox();
            this.con_cb_wechat = new System.Windows.Forms.CheckBox();
            this.gv_main = new System.Windows.Forms.DataGridView();
            this.bs_sub = new System.Windows.Forms.BindingSource(this.components);
            this.lbl_watch = new System.Windows.Forms.Label();
            this.pic_day = new System.Windows.Forms.PictureBox();
            this.pic_minute = new System.Windows.Forms.PictureBox();
            this.b_codetype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.b_codevalue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.b_stockname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.涨跌 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.最近2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.上次2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.b_max20growday_avg15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.b_max20growday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.b_supposename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bs_main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_list)).BeginInit();
            this.Menu_Main.SuspendLayout();
            this.ss_main.SuspendLayout();
            this.gb_filter.SuspendLayout();
            this.pop_m_grid.SuspendLayout();
            this.gb_control.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_sub)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_day)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_minute)).BeginInit();
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
            this.codetype,
            this.codevalue,
            this.stockname,
            this.nowprice,
            this.isfocus,
            this.反弹,
            this.updown,
            this.最近,
            this.上次,
            this.max20growday,
            this.max20growday_avg15,
            this.突破,
            this.ShortLow,
            this.跌20天,
            this.max10growmin,
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
            this.gv_list.Size = new System.Drawing.Size(585, 399);
            this.gv_list.TabIndex = 0;
            this.gv_list.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gv_list_CellMouseUp);
            this.gv_list.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gv_list_RowEnter);
            this.gv_list.SelectionChanged += new System.EventHandler(this.gv_list_SelectionChanged);
            // 
            // codetype
            // 
            this.codetype.DataPropertyName = "codetype";
            this.codetype.Frozen = true;
            this.codetype.HeaderText = "codetype";
            this.codetype.Name = "codetype";
            this.codetype.ReadOnly = true;
            this.codetype.Visible = false;
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
            // 反弹
            // 
            this.反弹.DataPropertyName = "growtoday";
            dataGridViewCellStyle1.Format = "0.00%";
            this.反弹.DefaultCellStyle = dataGridViewCellStyle1;
            this.反弹.HeaderText = "反弹";
            this.反弹.Name = "反弹";
            this.反弹.ReadOnly = true;
            this.反弹.Width = 50;
            // 
            // updown
            // 
            this.updown.DataPropertyName = "updown";
            dataGridViewCellStyle2.Format = "0.00%";
            this.updown.DefaultCellStyle = dataGridViewCellStyle2;
            this.updown.HeaderText = "涨跌";
            this.updown.Name = "updown";
            this.updown.ReadOnly = true;
            this.updown.Width = 50;
            // 
            // 最近
            // 
            this.最近.DataPropertyName = "breakday1";
            dataGridViewCellStyle3.Format = "MM-dd";
            this.最近.DefaultCellStyle = dataGridViewCellStyle3;
            this.最近.HeaderText = "最近";
            this.最近.Name = "最近";
            this.最近.ReadOnly = true;
            this.最近.Width = 50;
            // 
            // 上次
            // 
            this.上次.DataPropertyName = "breakday2";
            dataGridViewCellStyle4.Format = "MM-dd";
            this.上次.DefaultCellStyle = dataGridViewCellStyle4;
            this.上次.HeaderText = "上次";
            this.上次.Name = "上次";
            this.上次.ReadOnly = true;
            this.上次.Width = 50;
            // 
            // max20growday
            // 
            this.max20growday.DataPropertyName = "max20growday";
            dataGridViewCellStyle5.Format = "0.00%";
            this.max20growday.DefaultCellStyle = dataGridViewCellStyle5;
            this.max20growday.HeaderText = "大20天";
            this.max20growday.Name = "max20growday";
            this.max20growday.ReadOnly = true;
            this.max20growday.Width = 70;
            // 
            // max20growday_avg15
            // 
            this.max20growday_avg15.DataPropertyName = "max20growday_avg15";
            dataGridViewCellStyle6.Format = "0.00%";
            this.max20growday_avg15.DefaultCellStyle = dataGridViewCellStyle6;
            this.max20growday_avg15.HeaderText = "大回调";
            this.max20growday_avg15.Name = "max20growday_avg15";
            this.max20growday_avg15.ReadOnly = true;
            this.max20growday_avg15.Width = 50;
            // 
            // 突破
            // 
            this.突破.DataPropertyName = "logbreakqty";
            dataGridViewCellStyle7.Format = "0.00%";
            this.突破.DefaultCellStyle = dataGridViewCellStyle7;
            this.突破.HeaderText = "突破";
            this.突破.Name = "突破";
            this.突破.ReadOnly = true;
            this.突破.Width = 50;
            // 
            // ShortLow
            // 
            this.ShortLow.DataPropertyName = "ShortLow";
            this.ShortLow.HeaderText = "短";
            this.ShortLow.Name = "ShortLow";
            this.ShortLow.ReadOnly = true;
            this.ShortLow.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ShortLow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ShortLow.Width = 50;
            // 
            // 跌20天
            // 
            this.跌20天.DataPropertyName = "Max20Down";
            dataGridViewCellStyle8.Format = "0.00%";
            this.跌20天.DefaultCellStyle = dataGridViewCellStyle8;
            this.跌20天.HeaderText = "跌20天";
            this.跌20天.Name = "跌20天";
            this.跌20天.ReadOnly = true;
            this.跌20天.Width = 50;
            // 
            // max10growmin
            // 
            this.max10growmin.DataPropertyName = "max10growmin";
            dataGridViewCellStyle9.Format = "0.00%";
            this.max10growmin.DefaultCellStyle = dataGridViewCellStyle9;
            this.max10growmin.HeaderText = "大10分";
            this.max10growmin.Name = "max10growmin";
            this.max10growmin.ReadOnly = true;
            this.max10growmin.Width = 70;
            // 
            // nowtime
            // 
            this.nowtime.DataPropertyName = "nowtime";
            dataGridViewCellStyle10.Format = "HH:mm";
            this.nowtime.DefaultCellStyle = dataGridViewCellStyle10;
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
            // Menu_Main
            // 
            this.Menu_Main.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Menu_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_System,
            this.Test_Func});
            this.Menu_Main.Location = new System.Drawing.Point(0, 0);
            this.Menu_Main.Name = "Menu_Main";
            this.Menu_Main.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.Menu_Main.Size = new System.Drawing.Size(952, 25);
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
            this.Test_Restore.Click += new System.EventHandler(this.Test_Restore_Click);
            // 
            // ss_main
            // 
            this.ss_main.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ss_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ss_mian_label,
            this.sl_diff});
            this.ss_main.Location = new System.Drawing.Point(0, 548);
            this.ss_main.Name = "ss_main";
            this.ss_main.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.ss_main.Size = new System.Drawing.Size(952, 25);
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
            // sl_diff
            // 
            this.sl_diff.Name = "sl_diff";
            this.sl_diff.Size = new System.Drawing.Size(44, 20);
            this.sl_diff.Text = "势头：";
            // 
            // time_refresh
            // 
            this.time_refresh.Enabled = true;
            this.time_refresh.Interval = 5000;
            this.time_refresh.Tick += new System.EventHandler(this.time_refresh_Tick);
            // 
            // gb_filter
            // 
            this.gb_filter.Controls.Add(this.label3);
            this.gb_filter.Controls.Add(this.tb_updownmax);
            this.gb_filter.Controls.Add(this.label2);
            this.gb_filter.Controls.Add(this.tb_growmin);
            this.gb_filter.Controls.Add(this.tb_growmax);
            this.gb_filter.Controls.Add(this.lbl_grow);
            this.gb_filter.Controls.Add(this.lbl_max20growday_avg15);
            this.gb_filter.Controls.Add(this.tb_max20growday_avg15);
            this.gb_filter.Controls.Add(this.label1);
            this.gb_filter.Controls.Add(this.tb_max20downfrom);
            this.gb_filter.Controls.Add(this.tb_max20downto);
            this.gb_filter.Controls.Add(this.lbl_max20down);
            this.gb_filter.Controls.Add(this.tb_max20growup);
            this.gb_filter.Controls.Add(this.lbl_max20growup);
            this.gb_filter.Controls.Add(this.tb_PriceTo);
            this.gb_filter.Controls.Add(this.lbl_PriceTo);
            this.gb_filter.Controls.Add(this.tb_PriceFrom);
            this.gb_filter.Controls.Add(this.lbl_PriceFrom);
            this.gb_filter.Controls.Add(this.tb_updownmin);
            this.gb_filter.Controls.Add(this.lbl_updown);
            this.gb_filter.Controls.Add(this.fil_cb_focus);
            this.gb_filter.Controls.Add(this.fil_tb_name);
            this.gb_filter.Controls.Add(this.fil_name);
            this.gb_filter.Controls.Add(this.fil_tbcode);
            this.gb_filter.Controls.Add(this.fil_code);
            this.gb_filter.Location = new System.Drawing.Point(12, 459);
            this.gb_filter.Name = "gb_filter";
            this.gb_filter.Size = new System.Drawing.Size(581, 85);
            this.gb_filter.TabIndex = 9;
            this.gb_filter.TabStop = false;
            this.gb_filter.Text = "过滤条件";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 34;
            this.label3.Text = "~";
            // 
            // tb_updownmax
            // 
            this.tb_updownmax.Location = new System.Drawing.Point(196, 30);
            this.tb_updownmax.Name = "tb_updownmax";
            this.tb_updownmax.Size = new System.Drawing.Size(40, 21);
            this.tb_updownmax.TabIndex = 33;
            this.tb_updownmax.TextChanged += new System.EventHandler(this.tb_updownmax_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(512, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 32;
            this.label2.Text = "~";
            // 
            // tb_growmin
            // 
            this.tb_growmin.Location = new System.Drawing.Point(466, 54);
            this.tb_growmin.Name = "tb_growmin";
            this.tb_growmin.Size = new System.Drawing.Size(40, 21);
            this.tb_growmin.TabIndex = 31;
            this.tb_growmin.TextChanged += new System.EventHandler(this.tb_growmin_TextChanged);
            // 
            // tb_growmax
            // 
            this.tb_growmax.Location = new System.Drawing.Point(523, 55);
            this.tb_growmax.Name = "tb_growmax";
            this.tb_growmax.Size = new System.Drawing.Size(40, 21);
            this.tb_growmax.TabIndex = 30;
            this.tb_growmax.TextChanged += new System.EventHandler(this.tb_growmax_TextChanged);
            // 
            // lbl_grow
            // 
            this.lbl_grow.AutoSize = true;
            this.lbl_grow.Location = new System.Drawing.Point(431, 59);
            this.lbl_grow.Name = "lbl_grow";
            this.lbl_grow.Size = new System.Drawing.Size(29, 12);
            this.lbl_grow.TabIndex = 29;
            this.lbl_grow.Text = "反弹";
            // 
            // lbl_max20growday_avg15
            // 
            this.lbl_max20growday_avg15.AutoSize = true;
            this.lbl_max20growday_avg15.Location = new System.Drawing.Point(451, 37);
            this.lbl_max20growday_avg15.Name = "lbl_max20growday_avg15";
            this.lbl_max20growday_avg15.Size = new System.Drawing.Size(41, 12);
            this.lbl_max20growday_avg15.TabIndex = 28;
            this.lbl_max20growday_avg15.Text = "大天数";
            // 
            // tb_max20growday_avg15
            // 
            this.tb_max20growday_avg15.Location = new System.Drawing.Point(497, 33);
            this.tb_max20growday_avg15.Name = "tb_max20growday_avg15";
            this.tb_max20growday_avg15.Size = new System.Drawing.Size(40, 21);
            this.tb_max20growday_avg15.TabIndex = 27;
            this.tb_max20growday_avg15.TextChanged += new System.EventHandler(this.tb_max20growday_avg15_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(393, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "~";
            // 
            // tb_max20downfrom
            // 
            this.tb_max20downfrom.Location = new System.Drawing.Point(347, 31);
            this.tb_max20downfrom.Name = "tb_max20downfrom";
            this.tb_max20downfrom.Size = new System.Drawing.Size(40, 21);
            this.tb_max20downfrom.TabIndex = 25;
            this.tb_max20downfrom.TextChanged += new System.EventHandler(this.tb_max20downfrom_TextChanged);
            // 
            // tb_max20downto
            // 
            this.tb_max20downto.Location = new System.Drawing.Point(404, 32);
            this.tb_max20downto.Name = "tb_max20downto";
            this.tb_max20downto.Size = new System.Drawing.Size(40, 21);
            this.tb_max20downto.TabIndex = 24;
            this.tb_max20downto.TextChanged += new System.EventHandler(this.tb_max20down_TextChanged);
            // 
            // lbl_max20down
            // 
            this.lbl_max20down.AutoSize = true;
            this.lbl_max20down.Location = new System.Drawing.Point(312, 36);
            this.lbl_max20down.Name = "lbl_max20down";
            this.lbl_max20down.Size = new System.Drawing.Size(29, 12);
            this.lbl_max20down.TabIndex = 23;
            this.lbl_max20down.Text = "跌20";
            // 
            // tb_max20growup
            // 
            this.tb_max20growup.Location = new System.Drawing.Point(270, 31);
            this.tb_max20growup.Name = "tb_max20growup";
            this.tb_max20growup.Size = new System.Drawing.Size(40, 21);
            this.tb_max20growup.TabIndex = 22;
            this.tb_max20growup.TextChanged += new System.EventHandler(this.tb_max20growup_TextChanged);
            // 
            // lbl_max20growup
            // 
            this.lbl_max20growup.AutoSize = true;
            this.lbl_max20growup.Location = new System.Drawing.Point(240, 35);
            this.lbl_max20growup.Name = "lbl_max20growup";
            this.lbl_max20growup.Size = new System.Drawing.Size(29, 12);
            this.lbl_max20growup.TabIndex = 21;
            this.lbl_max20growup.Text = "大20";
            // 
            // tb_PriceTo
            // 
            this.tb_PriceTo.Location = new System.Drawing.Point(250, 57);
            this.tb_PriceTo.Name = "tb_PriceTo";
            this.tb_PriceTo.Size = new System.Drawing.Size(40, 21);
            this.tb_PriceTo.TabIndex = 20;
            this.tb_PriceTo.Text = "100";
            this.tb_PriceTo.TextChanged += new System.EventHandler(this.tb_PriceTo_TextChanged);
            // 
            // lbl_PriceTo
            // 
            this.lbl_PriceTo.AutoSize = true;
            this.lbl_PriceTo.Location = new System.Drawing.Point(235, 60);
            this.lbl_PriceTo.Name = "lbl_PriceTo";
            this.lbl_PriceTo.Size = new System.Drawing.Size(11, 12);
            this.lbl_PriceTo.TabIndex = 19;
            this.lbl_PriceTo.Text = "~";
            // 
            // tb_PriceFrom
            // 
            this.tb_PriceFrom.Location = new System.Drawing.Point(189, 55);
            this.tb_PriceFrom.Name = "tb_PriceFrom";
            this.tb_PriceFrom.Size = new System.Drawing.Size(40, 21);
            this.tb_PriceFrom.TabIndex = 18;
            this.tb_PriceFrom.TextChanged += new System.EventHandler(this.tb_PriceFrom_TextChanged);
            // 
            // lbl_PriceFrom
            // 
            this.lbl_PriceFrom.AutoSize = true;
            this.lbl_PriceFrom.Location = new System.Drawing.Point(154, 59);
            this.lbl_PriceFrom.Name = "lbl_PriceFrom";
            this.lbl_PriceFrom.Size = new System.Drawing.Size(29, 12);
            this.lbl_PriceFrom.TabIndex = 17;
            this.lbl_PriceFrom.Text = "价格";
            // 
            // tb_updownmin
            // 
            this.tb_updownmin.Location = new System.Drawing.Point(143, 31);
            this.tb_updownmin.Name = "tb_updownmin";
            this.tb_updownmin.Size = new System.Drawing.Size(40, 21);
            this.tb_updownmin.TabIndex = 16;
            this.tb_updownmin.TextChanged += new System.EventHandler(this.tb_rerise_TextChanged);
            // 
            // lbl_updown
            // 
            this.lbl_updown.AutoSize = true;
            this.lbl_updown.Location = new System.Drawing.Point(111, 35);
            this.lbl_updown.Name = "lbl_updown";
            this.lbl_updown.Size = new System.Drawing.Size(29, 12);
            this.lbl_updown.TabIndex = 15;
            this.lbl_updown.Text = "涨跌";
            // 
            // fil_cb_focus
            // 
            this.fil_cb_focus.AutoSize = true;
            this.fil_cb_focus.Location = new System.Drawing.Point(296, 60);
            this.fil_cb_focus.Name = "fil_cb_focus";
            this.fil_cb_focus.Size = new System.Drawing.Size(132, 16);
            this.fil_cb_focus.TabIndex = 14;
            this.fil_cb_focus.Text = "关注[忽略其他条件]";
            this.fil_cb_focus.UseVisualStyleBackColor = true;
            this.fil_cb_focus.CheckedChanged += new System.EventHandler(this.fil_cb_focus_CheckedChanged);
            // 
            // fil_tb_name
            // 
            this.fil_tb_name.Location = new System.Drawing.Point(39, 51);
            this.fil_tb_name.Name = "fil_tb_name";
            this.fil_tb_name.Size = new System.Drawing.Size(100, 21);
            this.fil_tb_name.TabIndex = 12;
            this.fil_tb_name.TextChanged += new System.EventHandler(this.fil_tb_name_TextChanged);
            // 
            // fil_name
            // 
            this.fil_name.AutoSize = true;
            this.fil_name.Location = new System.Drawing.Point(4, 55);
            this.fil_name.Name = "fil_name";
            this.fil_name.Size = new System.Drawing.Size(29, 12);
            this.fil_name.TabIndex = 11;
            this.fil_name.Text = "名称";
            // 
            // fil_tbcode
            // 
            this.fil_tbcode.Location = new System.Drawing.Point(39, 27);
            this.fil_tbcode.Name = "fil_tbcode";
            this.fil_tbcode.Size = new System.Drawing.Size(58, 21);
            this.fil_tbcode.TabIndex = 10;
            this.fil_tbcode.TextChanged += new System.EventHandler(this.fil_tbcode_TextChanged);
            // 
            // fil_code
            // 
            this.fil_code.AutoSize = true;
            this.fil_code.Location = new System.Drawing.Point(4, 31);
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
            this.gb_control.Controls.Add(this.cb_stop);
            this.gb_control.Controls.Add(this.cb_showjpg);
            this.gb_control.Controls.Add(this.con_cb_wechat);
            this.gb_control.Location = new System.Drawing.Point(600, 454);
            this.gb_control.Name = "gb_control";
            this.gb_control.Size = new System.Drawing.Size(340, 91);
            this.gb_control.TabIndex = 10;
            this.gb_control.TabStop = false;
            this.gb_control.Text = "控制";
            // 
            // cb_stop
            // 
            this.cb_stop.AutoSize = true;
            this.cb_stop.Location = new System.Drawing.Point(167, 20);
            this.cb_stop.Name = "cb_stop";
            this.cb_stop.Size = new System.Drawing.Size(72, 16);
            this.cb_stop.TabIndex = 18;
            this.cb_stop.Text = "停止更新";
            this.cb_stop.UseVisualStyleBackColor = true;
            // 
            // cb_showjpg
            // 
            this.cb_showjpg.AutoSize = true;
            this.cb_showjpg.Location = new System.Drawing.Point(95, 20);
            this.cb_showjpg.Name = "cb_showjpg";
            this.cb_showjpg.Size = new System.Drawing.Size(66, 16);
            this.cb_showjpg.TabIndex = 17;
            this.cb_showjpg.Text = "K线图片";
            this.cb_showjpg.UseVisualStyleBackColor = true;
            this.cb_showjpg.CheckedChanged += new System.EventHandler(this.cb_showjpg_CheckedChanged);
            // 
            // con_cb_wechat
            // 
            this.con_cb_wechat.AutoSize = true;
            this.con_cb_wechat.Location = new System.Drawing.Point(17, 20);
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
            this.b_codetype,
            this.b_codevalue,
            this.b_stockname,
            this.涨跌,
            this.最近2,
            this.上次2,
            this.b_max20growday_avg15,
            this.b_max20growday,
            this.b_supposename});
            this.gv_main.DataSource = this.bs_sub;
            this.gv_main.Location = new System.Drawing.Point(600, 50);
            this.gv_main.Margin = new System.Windows.Forms.Padding(2);
            this.gv_main.Name = "gv_main";
            this.gv_main.ReadOnly = true;
            this.gv_main.RowHeadersVisible = false;
            this.gv_main.RowTemplate.Height = 27;
            this.gv_main.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv_main.Size = new System.Drawing.Size(341, 399);
            this.gv_main.TabIndex = 11;
            this.gv_main.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gv_main_CellMouseUp);
            this.gv_main.SelectionChanged += new System.EventHandler(this.gv_main_SelectionChanged);
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
            // pic_day
            // 
            this.pic_day.Location = new System.Drawing.Point(14, 226);
            this.pic_day.Name = "pic_day";
            this.pic_day.Size = new System.Drawing.Size(308, 223);
            this.pic_day.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_day.TabIndex = 15;
            this.pic_day.TabStop = false;
            this.pic_day.Visible = false;
            // 
            // pic_minute
            // 
            this.pic_minute.Location = new System.Drawing.Point(326, 226);
            this.pic_minute.Name = "pic_minute";
            this.pic_minute.Size = new System.Drawing.Size(269, 223);
            this.pic_minute.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_minute.TabIndex = 16;
            this.pic_minute.TabStop = false;
            this.pic_minute.Visible = false;
            // 
            // b_codetype
            // 
            this.b_codetype.DataPropertyName = "codetype";
            this.b_codetype.HeaderText = "codetype";
            this.b_codetype.Name = "b_codetype";
            this.b_codetype.ReadOnly = true;
            this.b_codetype.Visible = false;
            // 
            // b_codevalue
            // 
            this.b_codevalue.DataPropertyName = "codevalue";
            this.b_codevalue.HeaderText = "代码";
            this.b_codevalue.Name = "b_codevalue";
            this.b_codevalue.ReadOnly = true;
            this.b_codevalue.Width = 50;
            // 
            // b_stockname
            // 
            this.b_stockname.DataPropertyName = "stockname";
            this.b_stockname.HeaderText = "名称";
            this.b_stockname.Name = "b_stockname";
            this.b_stockname.ReadOnly = true;
            this.b_stockname.Width = 60;
            // 
            // 涨跌
            // 
            this.涨跌.DataPropertyName = "updown";
            dataGridViewCellStyle11.Format = "0.00%";
            this.涨跌.DefaultCellStyle = dataGridViewCellStyle11;
            this.涨跌.HeaderText = "涨跌";
            this.涨跌.Name = "涨跌";
            this.涨跌.ReadOnly = true;
            this.涨跌.Width = 50;
            // 
            // 最近2
            // 
            this.最近2.DataPropertyName = "breakday1";
            dataGridViewCellStyle12.Format = "MM-dd";
            this.最近2.DefaultCellStyle = dataGridViewCellStyle12;
            this.最近2.HeaderText = "最近";
            this.最近2.Name = "最近2";
            this.最近2.ReadOnly = true;
            this.最近2.Width = 50;
            // 
            // 上次2
            // 
            this.上次2.DataPropertyName = "breakday2";
            dataGridViewCellStyle13.Format = "MM-dd";
            this.上次2.DefaultCellStyle = dataGridViewCellStyle13;
            this.上次2.HeaderText = "上次";
            this.上次2.Name = "上次2";
            this.上次2.ReadOnly = true;
            this.上次2.Width = 50;
            // 
            // b_max20growday_avg15
            // 
            this.b_max20growday_avg15.DataPropertyName = "max20growday_avg15";
            this.b_max20growday_avg15.HeaderText = "大天数";
            this.b_max20growday_avg15.Name = "b_max20growday_avg15";
            this.b_max20growday_avg15.ReadOnly = true;
            this.b_max20growday_avg15.Width = 50;
            // 
            // b_max20growday
            // 
            this.b_max20growday.DataPropertyName = "max20growday";
            dataGridViewCellStyle14.Format = "0.00%";
            this.b_max20growday.DefaultCellStyle = dataGridViewCellStyle14;
            this.b_max20growday.HeaderText = "大20天";
            this.b_max20growday.Name = "b_max20growday";
            this.b_max20growday.ReadOnly = true;
            this.b_max20growday.Width = 50;
            // 
            // b_supposename
            // 
            this.b_supposename.DataPropertyName = "supposename";
            dataGridViewCellStyle15.Format = "0.00%";
            this.b_supposename.DefaultCellStyle = dataGridViewCellStyle15;
            this.b_supposename.HeaderText = "建议";
            this.b_supposename.Name = "b_supposename";
            this.b_supposename.ReadOnly = true;
            this.b_supposename.Width = 60;
            // 
            // LogicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 573);
            this.Controls.Add(this.pic_minute);
            this.Controls.Add(this.pic_day);
            this.Controls.Add(this.lbl_watch);
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
            ((System.ComponentModel.ISupportInitialize)(this.pic_day)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_minute)).EndInit();
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
        private System.Windows.Forms.Label lbl_watch;
        private System.Windows.Forms.TextBox tb_updownmin;
        private System.Windows.Forms.Label lbl_updown;
        private System.Windows.Forms.TextBox tb_PriceTo;
        private System.Windows.Forms.Label lbl_PriceTo;
        private System.Windows.Forms.TextBox tb_PriceFrom;
        private System.Windows.Forms.Label lbl_PriceFrom;
        private System.Windows.Forms.TextBox tb_max20growup;
        private System.Windows.Forms.Label lbl_max20growup;
        private System.Windows.Forms.PictureBox pic_day;
        private System.Windows.Forms.PictureBox pic_minute;
        private System.Windows.Forms.TextBox tb_max20downto;
        private System.Windows.Forms.Label lbl_max20down;
        private System.Windows.Forms.CheckBox cb_showjpg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_max20downfrom;
        private System.Windows.Forms.Label lbl_max20growday_avg15;
        private System.Windows.Forms.TextBox tb_max20growday_avg15;
        private System.Windows.Forms.ToolStripStatusLabel sl_diff;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_growmin;
        private System.Windows.Forms.TextBox tb_growmax;
        private System.Windows.Forms.Label lbl_grow;
        private System.Windows.Forms.CheckBox cb_stop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_updownmax;
        private System.Windows.Forms.DataGridViewTextBoxColumn codetype;
        private System.Windows.Forms.DataGridViewTextBoxColumn codevalue;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockname;
        private System.Windows.Forms.DataGridViewTextBoxColumn nowprice;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isfocus;
        private System.Windows.Forms.DataGridViewTextBoxColumn 反弹;
        private System.Windows.Forms.DataGridViewTextBoxColumn updown;
        private System.Windows.Forms.DataGridViewTextBoxColumn 最近;
        private System.Windows.Forms.DataGridViewTextBoxColumn 上次;
        private System.Windows.Forms.DataGridViewTextBoxColumn max20growday;
        private System.Windows.Forms.DataGridViewTextBoxColumn max20growday_avg15;
        private System.Windows.Forms.DataGridViewTextBoxColumn 突破;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ShortLow;
        private System.Windows.Forms.DataGridViewTextBoxColumn 跌20天;
        private System.Windows.Forms.DataGridViewTextBoxColumn max10growmin;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn b_codetype;
        private System.Windows.Forms.DataGridViewTextBoxColumn b_codevalue;
        private System.Windows.Forms.DataGridViewTextBoxColumn b_stockname;
        private System.Windows.Forms.DataGridViewTextBoxColumn 涨跌;
        private System.Windows.Forms.DataGridViewTextBoxColumn 最近2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 上次2;
        private System.Windows.Forms.DataGridViewTextBoxColumn b_max20growday_avg15;
        private System.Windows.Forms.DataGridViewTextBoxColumn b_max20growday;
        private System.Windows.Forms.DataGridViewTextBoxColumn b_supposename;
    }
}

