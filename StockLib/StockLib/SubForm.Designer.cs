namespace StockLib
{
    partial class SubForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bs_sub = new System.Windows.Forms.BindingSource(this.components);
            this.gv_main = new System.Windows.Forms.DataGridView();
            this.MsMain = new System.Windows.Forms.MenuStrip();
            this.工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_ExportToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.gv_history = new System.Windows.Forms.DataGridView();
            this.bs_history = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bs_sub)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_main)).BeginInit();
            this.MsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_history)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_history)).BeginInit();
            this.SuspendLayout();
            // 
            // gv_main
            // 
            this.gv_main.AllowUserToAddRows = false;
            this.gv_main.AllowUserToDeleteRows = false;
            this.gv_main.AutoGenerateColumns = false;
            this.gv_main.ColumnHeadersHeight = 25;
            this.gv_main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gv_main.DataSource = this.bs_sub;
            this.gv_main.Location = new System.Drawing.Point(0, 47);
            this.gv_main.Margin = new System.Windows.Forms.Padding(2);
            this.gv_main.Name = "gv_main";
            this.gv_main.ReadOnly = true;
            this.gv_main.RowHeadersVisible = false;
            this.gv_main.RowTemplate.Height = 27;
            this.gv_main.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv_main.Size = new System.Drawing.Size(805, 222);
            this.gv_main.TabIndex = 0;
            // 
            // MsMain
            // 
            this.MsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.工具ToolStripMenuItem});
            this.MsMain.Location = new System.Drawing.Point(0, 0);
            this.MsMain.Name = "MsMain";
            this.MsMain.Size = new System.Drawing.Size(826, 25);
            this.MsMain.TabIndex = 1;
            this.MsMain.Text = "menuStrip1";
            // 
            // 工具ToolStripMenuItem
            // 
            this.工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_ExportToExcel});
            this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            this.工具ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.工具ToolStripMenuItem.Text = "工具";
            // 
            // MI_ExportToExcel
            // 
            this.MI_ExportToExcel.Name = "MI_ExportToExcel";
            this.MI_ExportToExcel.Size = new System.Drawing.Size(129, 22);
            this.MI_ExportToExcel.Text = "导出Excel";
            this.MI_ExportToExcel.Click += new System.EventHandler(this.MI_ExportToExcel_Click);
            // 
            // gv_history
            // 
            this.gv_history.AllowUserToAddRows = false;
            this.gv_history.AllowUserToDeleteRows = false;
            this.gv_history.AutoGenerateColumns = false;
            this.gv_history.ColumnHeadersHeight = 25;
            this.gv_history.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gv_history.DataSource = this.bs_history;
            this.gv_history.Location = new System.Drawing.Point(0, 290);
            this.gv_history.Margin = new System.Windows.Forms.Padding(2);
            this.gv_history.Name = "gv_history";
            this.gv_history.ReadOnly = true;
            this.gv_history.RowHeadersVisible = false;
            this.gv_history.RowTemplate.Height = 27;
            this.gv_history.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv_history.Size = new System.Drawing.Size(805, 222);
            this.gv_history.TabIndex = 2;
            // 
            // SubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 559);
            this.Controls.Add(this.gv_history);
            this.Controls.Add(this.gv_main);
            this.Controls.Add(this.MsMain);
            this.MainMenuStrip = this.MsMain;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SubForm";
            this.Text = "数据跟踪";
            this.Load += new System.EventHandler(this.SubForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bs_sub)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_main)).EndInit();
            this.MsMain.ResumeLayout(false);
            this.MsMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_history)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_history)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView gv_main;
        public System.Windows.Forms.BindingSource bs_sub;
        private System.Windows.Forms.MenuStrip MsMain;
        private System.Windows.Forms.ToolStripMenuItem 工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MI_ExportToExcel;
        private System.Windows.Forms.DataGridView gv_history;
        public System.Windows.Forms.BindingSource bs_history;
    }
}