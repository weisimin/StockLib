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
            this.bs_main = new System.Windows.Forms.BindingSource(this.components);
            this.gv_list = new System.Windows.Forms.DataGridView();
            this.Menu_Main = new System.Windows.Forms.MenuStrip();
            this.MI_System = new System.Windows.Forms.ToolStripMenuItem();
            this.Download_History = new System.Windows.Forms.ToolStripMenuItem();
            this.Download_Now = new System.Windows.Forms.ToolStripMenuItem();
            this.Download_AllCode = new System.Windows.Forms.ToolStripMenuItem();
            this.Save_Data = new System.Windows.Forms.ToolStripMenuItem();
            this.codevalue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nowprice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.max10growmin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.max20growday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bs_main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_list)).BeginInit();
            this.Menu_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // gv_list
            // 
            this.gv_list.AllowUserToAddRows = false;
            this.gv_list.AllowUserToDeleteRows = false;
            this.gv_list.AllowUserToOrderColumns = true;
            this.gv_list.AutoGenerateColumns = false;
            this.gv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codevalue,
            this.stockname,
            this.nowprice,
            this.max10growmin,
            this.max20growday});
            this.gv_list.DataSource = this.bs_main;
            this.gv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gv_list.Location = new System.Drawing.Point(0, 28);
            this.gv_list.Name = "gv_list";
            this.gv_list.ReadOnly = true;
            this.gv_list.RowHeadersVisible = false;
            this.gv_list.RowTemplate.Height = 27;
            this.gv_list.Size = new System.Drawing.Size(1262, 405);
            this.gv_list.TabIndex = 0;
            // 
            // Menu_Main
            // 
            this.Menu_Main.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Menu_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_System});
            this.Menu_Main.Location = new System.Drawing.Point(0, 0);
            this.Menu_Main.Name = "Menu_Main";
            this.Menu_Main.Size = new System.Drawing.Size(1262, 28);
            this.Menu_Main.TabIndex = 1;
            this.Menu_Main.Text = "menuStrip1";
            // 
            // MI_System
            // 
            this.MI_System.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Download_History,
            this.Download_Now,
            this.Download_AllCode,
            this.Save_Data});
            this.MI_System.Name = "MI_System";
            this.MI_System.Size = new System.Drawing.Size(100, 24);
            this.MI_System.Text = "MI_System";
            // 
            // Download_History
            // 
            this.Download_History.Name = "Download_History";
            this.Download_History.Size = new System.Drawing.Size(223, 26);
            this.Download_History.Text = "Download_History";
            this.Download_History.Click += new System.EventHandler(this.Download_History_Click);
            // 
            // Download_Now
            // 
            this.Download_Now.Name = "Download_Now";
            this.Download_Now.Size = new System.Drawing.Size(223, 26);
            this.Download_Now.Text = "Download_Now";
            this.Download_Now.Click += new System.EventHandler(this.Download_Now_Click);
            // 
            // Download_AllCode
            // 
            this.Download_AllCode.Name = "Download_AllCode";
            this.Download_AllCode.Size = new System.Drawing.Size(223, 26);
            this.Download_AllCode.Text = "Download_AllCode";
            this.Download_AllCode.Click += new System.EventHandler(this.Download_AllCode_Click);
            // 
            // Save_Data
            // 
            this.Save_Data.Name = "Save_Data";
            this.Save_Data.Size = new System.Drawing.Size(223, 26);
            this.Save_Data.Text = "Save_Data";
            this.Save_Data.Click += new System.EventHandler(this.Save_Data_Click);
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
            this.nowprice.Width = 75;
            // 
            // max10growmin
            // 
            this.max10growmin.DataPropertyName = "max10growmin";
            this.max10growmin.HeaderText = "大10分";
            this.max10growmin.Name = "max10growmin";
            this.max10growmin.ReadOnly = true;
            this.max10growmin.Width = 80;
            // 
            // max20growday
            // 
            this.max20growday.DataPropertyName = "max20growday";
            this.max20growday.HeaderText = "大20天";
            this.max20growday.Name = "max20growday";
            this.max20growday.ReadOnly = true;
            this.max20growday.Width = 80;
            // 
            // LogicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 433);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn codevalue;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockname;
        private System.Windows.Forms.DataGridViewTextBoxColumn nowprice;
        private System.Windows.Forms.DataGridViewTextBoxColumn max10growmin;
        private System.Windows.Forms.DataGridViewTextBoxColumn max20growday;
    }
}

