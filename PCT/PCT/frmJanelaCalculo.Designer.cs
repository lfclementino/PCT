namespace PCT
{
    partial class frmJanelaCalculo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmJanelaCalculo));
            this.gridTrechos = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btToolAdd = new System.Windows.Forms.ToolStripButton();
            this.btToolRem = new System.Windows.Forms.ToolStripButton();
            this.btToolCopy = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.ctxtAdd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.horizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridTrechos)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.ctxtAdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridTrechos
            // 
            this.gridTrechos.AllowUserToAddRows = false;
            this.gridTrechos.AllowUserToDeleteRows = false;
            this.gridTrechos.AllowUserToResizeColumns = false;
            this.gridTrechos.AllowUserToResizeRows = false;
            this.gridTrechos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridTrechos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTrechos.Location = new System.Drawing.Point(1, 25);
            this.gridTrechos.Margin = new System.Windows.Forms.Padding(0);
            this.gridTrechos.MultiSelect = false;
            this.gridTrechos.Name = "gridTrechos";
            this.gridTrechos.ReadOnly = true;
            this.gridTrechos.RowHeadersVisible = false;
            this.gridTrechos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridTrechos.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridTrechos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTrechos.ShowCellErrors = false;
            this.gridTrechos.ShowCellToolTips = false;
            this.gridTrechos.ShowEditingIcon = false;
            this.gridTrechos.ShowRowErrors = false;
            this.gridTrechos.Size = new System.Drawing.Size(620, 339);
            this.gridTrechos.TabIndex = 0;
            this.gridTrechos.DoubleClick += new System.EventHandler(this.gridTrechos_DoubleClick);
            this.gridTrechos.SelectionChanged += new System.EventHandler(this.gridTrechos_SelectionChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btToolAdd,
            this.btToolRem,
            this.btToolCopy});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(621, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btToolAdd
            // 
            this.btToolAdd.Image = global::PCT.resImagens._101;
            this.btToolAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btToolAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btToolAdd.Name = "btToolAdd";
            this.btToolAdd.Size = new System.Drawing.Size(105, 22);
            this.btToolAdd.Text = "Adicionar Trecho";
            this.btToolAdd.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // btToolRem
            // 
            this.btToolRem.Image = global::PCT.resImagens.minipix_omit_page_red;
            this.btToolRem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btToolRem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btToolRem.Name = "btToolRem";
            this.btToolRem.Size = new System.Drawing.Size(68, 22);
            this.btToolRem.Text = "Remover";
            this.btToolRem.Click += new System.EventHandler(this.btToolRem_Click);
            // 
            // btToolCopy
            // 
            this.btToolCopy.Image = global::PCT.resImagens.copy_icon;
            this.btToolCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btToolCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btToolCopy.Name = "btToolCopy";
            this.btToolCopy.Size = new System.Drawing.Size(95, 22);
            this.btToolCopy.Text = "Copiar Trecho";
            this.btToolCopy.Click += new System.EventHandler(this.btToolCopy_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusTotal});
            this.statusStrip1.Location = new System.Drawing.Point(0, 364);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(621, 22);
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 4;
            // 
            // StatusTotal
            // 
            this.StatusTotal.AutoSize = false;
            this.StatusTotal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StatusTotal.Name = "StatusTotal";
            this.StatusTotal.Size = new System.Drawing.Size(606, 17);
            this.StatusTotal.Spring = true;
            this.StatusTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctxtAdd
            // 
            this.ctxtAdd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.horizontalToolStripMenuItem,
            this.verticalToolStripMenuItem});
            this.ctxtAdd.Name = "ctxtAdd";
            this.ctxtAdd.Size = new System.Drawing.Size(134, 48);
            // 
            // horizontalToolStripMenuItem
            // 
            this.horizontalToolStripMenuItem.Name = "horizontalToolStripMenuItem";
            this.horizontalToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.horizontalToolStripMenuItem.Text = "Horizontal";
            this.horizontalToolStripMenuItem.Click += new System.EventHandler(this.horizontalToolStripMenuItem_Click);
            // 
            // verticalToolStripMenuItem
            // 
            this.verticalToolStripMenuItem.Name = "verticalToolStripMenuItem";
            this.verticalToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.verticalToolStripMenuItem.Text = "Vertical";
            this.verticalToolStripMenuItem.Click += new System.EventHandler(this.verticalToolStripMenuItem_Click);
            // 
            // frmJanelaCalculo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 386);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.gridTrechos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(627, 411);
            this.Name = "frmJanelaCalculo";
            this.ShowInTaskbar = false;
            this.Text = "<Novo>";
            this.Deactivate += new System.EventHandler(this.frmJanelaCalculo_Deactivate);
            this.Activated += new System.EventHandler(this.frmJanelaCalculo_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmJanelaCalculo_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gridTrechos)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ctxtAdd.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridTrechos;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusTotal;
        private System.Windows.Forms.ToolStripButton btToolAdd;
        private System.Windows.Forms.ToolStripButton btToolRem;
        private System.Windows.Forms.ContextMenuStrip ctxtAdd;
        private System.Windows.Forms.ToolStripMenuItem horizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btToolCopy;
    }
}