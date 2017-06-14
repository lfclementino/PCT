namespace PCT
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.toolbar = new System.Windows.Forms.MenuStrip();
            this.novoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salvarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salvarComotoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolbar
            // 
            this.toolbar.AutoSize = false;
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.novoToolStripMenuItem,
            this.abrirToolStripMenuItem,
            this.salvarToolStripMenuItem,
            this.salvarComotoolStripMenuItem,
            this.sobreToolStripMenuItem,
            this.sairToolStripMenuItem});
            this.toolbar.Location = new System.Drawing.Point(0, 0);
            this.toolbar.Name = "toolbar";
            this.toolbar.ShowItemToolTips = true;
            this.toolbar.Size = new System.Drawing.Size(793, 50);
            this.toolbar.TabIndex = 4;
            // 
            // novoToolStripMenuItem
            // 
            this.novoToolStripMenuItem.AutoSize = false;
            this.novoToolStripMenuItem.Image = global::PCT.resImagens.New_document;
            this.novoToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.novoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.novoToolStripMenuItem.Name = "novoToolStripMenuItem";
            this.novoToolStripMenuItem.Size = new System.Drawing.Size(94, 50);
            this.novoToolStripMenuItem.Text = "Novo";
            this.novoToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.novoToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.novoToolStripMenuItem.ToolTipText = "Novo";
            this.novoToolStripMenuItem.Click += new System.EventHandler(this.novoToolStripMenuItem_Click);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.AutoSize = false;
            this.abrirToolStripMenuItem.Image = global::PCT.resImagens.Folder;
            this.abrirToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.abrirToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(94, 50);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.abrirToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.abrirToolStripMenuItem.ToolTipText = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // salvarToolStripMenuItem
            // 
            this.salvarToolStripMenuItem.AutoSize = false;
            this.salvarToolStripMenuItem.Image = global::PCT.resImagens.Save;
            this.salvarToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.salvarToolStripMenuItem.Name = "salvarToolStripMenuItem";
            this.salvarToolStripMenuItem.Size = new System.Drawing.Size(80, 50);
            this.salvarToolStripMenuItem.Text = "Salvar";
            this.salvarToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.salvarToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.salvarToolStripMenuItem.ToolTipText = "Salvar";
            this.salvarToolStripMenuItem.Click += new System.EventHandler(this.salvatrToolStripMenuItem_Click);
            // 
            // salvarComotoolStripMenuItem
            // 
            this.salvarComotoolStripMenuItem.AutoSize = false;
            this.salvarComotoolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("salvarComotoolStripMenuItem.Image")));
            this.salvarComotoolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.salvarComotoolStripMenuItem.Name = "salvarComotoolStripMenuItem";
            this.salvarComotoolStripMenuItem.Size = new System.Drawing.Size(80, 50);
            this.salvarComotoolStripMenuItem.Text = "Salvar Como";
            this.salvarComotoolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.salvarComotoolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.salvarComotoolStripMenuItem.ToolTipText = "Salvar";
            this.salvarComotoolStripMenuItem.Click += new System.EventHandler(this.salvarComotoolStripMenuItem_Click);
            // 
            // sobreToolStripMenuItem
            // 
            this.sobreToolStripMenuItem.AutoSize = false;
            this.sobreToolStripMenuItem.Image = global::PCT.resImagens.Info;
            this.sobreToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.sobreToolStripMenuItem.Name = "sobreToolStripMenuItem";
            this.sobreToolStripMenuItem.Size = new System.Drawing.Size(80, 50);
            this.sobreToolStripMenuItem.Text = "Sobre";
            this.sobreToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.sobreToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.sobreToolStripMenuItem.ToolTipText = "Sobre";
            this.sobreToolStripMenuItem.Click += new System.EventHandler(this.sobreToolStripMenuItem_Click);
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.AutoSize = false;
            this.sairToolStripMenuItem.Image = global::PCT.resImagens.Exit;
            this.sairToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(94, 50);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.sairToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.sairToolStripMenuItem.ToolTipText = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(793, 573);
            this.Controls.Add(this.toolbar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmMain";
            this.Text = "PCT";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip toolbar;
        private System.Windows.Forms.ToolStripMenuItem novoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salvarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salvarComotoolStripMenuItem;
    }
}

