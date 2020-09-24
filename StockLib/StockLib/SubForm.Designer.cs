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
            this.gv_main.DataSource = this.bs_sub;
            this.gv_main.Location = new System.Drawing.Point(0, 0);
            this.gv_main.Margin = new System.Windows.Forms.Padding(2);
            this.gv_main.Name = "gv_main";
            this.gv_main.ReadOnly = true;
            this.gv_main.RowHeadersVisible = false;
            this.gv_main.RowTemplate.Height = 27;
            this.gv_main.Size = new System.Drawing.Size(1144, 262);
            this.gv_main.TabIndex = 0;
            // 
            // SubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 271);
            this.Controls.Add(this.gv_main);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SubForm";
            this.Text = "数据跟踪";
            this.Load += new System.EventHandler(this.SubForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bs_sub)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_main)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView gv_main;
        public System.Windows.Forms.BindingSource bs_sub;
    }
}