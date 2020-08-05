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
            this.bs_main = new System.Windows.Forms.BindingSource(this.components);
            this.gv_main = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.bs_main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_main)).BeginInit();
            this.SuspendLayout();
            // 
            // gv_main
            // 
            this.gv_main.AllowUserToAddRows = false;
            this.gv_main.AllowUserToDeleteRows = false;
            this.gv_main.AutoGenerateColumns = false;
            this.gv_main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_main.DataSource = this.bs_main;
            this.gv_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gv_main.Location = new System.Drawing.Point(0, 0);
            this.gv_main.Name = "gv_main";
            this.gv_main.ReadOnly = true;
            this.gv_main.RowTemplate.Height = 27;
            this.gv_main.Size = new System.Drawing.Size(282, 339);
            this.gv_main.TabIndex = 0;
            // 
            // SubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 339);
            this.Controls.Add(this.gv_main);
            this.Name = "SubForm";
            this.Text = "推荐股";
            ((System.ComponentModel.ISupportInitialize)(this.bs_main)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_main)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView gv_main;
        public System.Windows.Forms.BindingSource bs_main;
    }
}