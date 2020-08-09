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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bs_sub = new System.Windows.Forms.BindingSource(this.components);
            this.gv_main = new System.Windows.Forms.DataGridView();
            this.codevalue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nowprice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.growtoday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.max10growmin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bs_sub)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_main)).BeginInit();
            this.SuspendLayout();
            // 
            // gv_main
            // 
            this.gv_main.AllowUserToAddRows = false;
            this.gv_main.AllowUserToDeleteRows = false;
            this.gv_main.AutoGenerateColumns = false;
            this.gv_main.ColumnHeadersHeight = 25;
            this.gv_main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gv_main.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codevalue,
            this.stockname,
            this.nowprice,
            this.growtoday,
            this.max10growmin});
            this.gv_main.DataSource = this.bs_sub;
            this.gv_main.Location = new System.Drawing.Point(0, 0);
            this.gv_main.Name = "gv_main";
            this.gv_main.ReadOnly = true;
            this.gv_main.RowHeadersVisible = false;
            this.gv_main.RowTemplate.Height = 27;
            this.gv_main.Size = new System.Drawing.Size(371, 327);
            this.gv_main.TabIndex = 0;
            // 
            // codevalue
            // 
            this.codevalue.DataPropertyName = "codevalue";
            this.codevalue.HeaderText = "代码";
            this.codevalue.Name = "codevalue";
            this.codevalue.ReadOnly = true;
            this.codevalue.Width = 80;
            // 
            // stockname
            // 
            this.stockname.DataPropertyName = "stockname";
            this.stockname.HeaderText = "名称";
            this.stockname.Name = "stockname";
            this.stockname.ReadOnly = true;
            // 
            // nowprice
            // 
            this.nowprice.DataPropertyName = "nowprice";
            this.nowprice.HeaderText = "价格";
            this.nowprice.Name = "nowprice";
            this.nowprice.ReadOnly = true;
            this.nowprice.Width = 50;
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
            // max10growmin
            // 
            this.max10growmin.DataPropertyName = "max10growmin";
            dataGridViewCellStyle2.Format = "0.00%";
            this.max10growmin.DefaultCellStyle = dataGridViewCellStyle2;
            this.max10growmin.HeaderText = "大10分";
            this.max10growmin.Name = "max10growmin";
            this.max10growmin.ReadOnly = true;
            this.max10growmin.Width = 60;
            // 
            // SubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 339);
            this.Controls.Add(this.gv_main);
            this.Name = "SubForm";
            this.Text = "推荐股";
            this.Load += new System.EventHandler(this.SubForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bs_sub)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_main)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView gv_main;
        public System.Windows.Forms.BindingSource bs_sub;
        private System.Windows.Forms.DataGridViewTextBoxColumn codevalue;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockname;
        private System.Windows.Forms.DataGridViewTextBoxColumn nowprice;
        private System.Windows.Forms.DataGridViewTextBoxColumn growtoday;
        private System.Windows.Forms.DataGridViewTextBoxColumn max10growmin;
    }
}