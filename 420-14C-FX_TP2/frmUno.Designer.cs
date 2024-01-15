
namespace _420_14C_FX_TP2
{
    partial class FrmUno
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statutStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatut = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrDragDrop = new System.Windows.Forms.Timer(this.components);
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuConnexions = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConnexionServeur = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConnexionClient = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUtilisateurs = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlTable = new System.Windows.Forms.Panel();
            this.statutStrip.SuspendLayout();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // statutStrip
            // 
            this.statutStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statutStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatut});
            this.statutStrip.Location = new System.Drawing.Point(0, 532);
            this.statutStrip.Name = "statutStrip";
            this.statutStrip.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statutStrip.Size = new System.Drawing.Size(870, 22);
            this.statutStrip.TabIndex = 0;
            this.statutStrip.Text = "statusStrip1";
            // 
            // lblStatut
            // 
            this.lblStatut.Name = "lblStatut";
            this.lblStatut.Size = new System.Drawing.Size(0, 17);
            // 
            // tmrDragDrop
            // 
            this.tmrDragDrop.Interval = 1;
            this.tmrDragDrop.Tick += new System.EventHandler(this.TmrDragDrop_Tick);
            // 
            // menu
            // 
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuConnexions,
            this.menuUtilisateurs});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menu.Size = new System.Drawing.Size(870, 24);
            this.menu.TabIndex = 17;
            this.menu.Text = "menuStrip1";
            // 
            // menuConnexions
            // 
            this.menuConnexions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuConnexionServeur,
            this.menuConnexionClient});
            this.menuConnexions.Name = "menuConnexions";
            this.menuConnexions.Size = new System.Drawing.Size(82, 20);
            this.menuConnexions.Text = "Connexions";
            // 
            // menuConnexionServeur
            // 
            this.menuConnexionServeur.Name = "menuConnexionServeur";
            this.menuConnexionServeur.Size = new System.Drawing.Size(180, 22);
            this.menuConnexionServeur.Text = "Serveur";
            this.menuConnexionServeur.Click += new System.EventHandler(this.MenuConnexionServeur_Click);
            // 
            // menuConnexionClient
            // 
            this.menuConnexionClient.Name = "menuConnexionClient";
            this.menuConnexionClient.Size = new System.Drawing.Size(180, 22);
            this.menuConnexionClient.Text = "Client";
            this.menuConnexionClient.Click += new System.EventHandler(this.MenuConnexionClient_Click);
            // 
            // menuUtilisateurs
            // 
            this.menuUtilisateurs.Name = "menuUtilisateurs";
            this.menuUtilisateurs.Size = new System.Drawing.Size(77, 20);
            this.menuUtilisateurs.Text = "Utilisateurs";
            this.menuUtilisateurs.Click += new System.EventHandler(this.MenuUtilisateurs_Click);
            // 
            // pnlTable
            // 
            this.pnlTable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTable.Location = new System.Drawing.Point(4, 25);
            this.pnlTable.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlTable.Name = "pnlTable";
            this.pnlTable.Size = new System.Drawing.Size(870, 506);
            this.pnlTable.TabIndex = 18;
            // 
            // FrmUno
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 554);
            this.Controls.Add(this.pnlTable);
            this.Controls.Add(this.statutStrip);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmUno";
            this.Text = "Uno !";
            this.Load += new System.EventHandler(this.FrmUno_Load);
            this.Resize += new System.EventHandler(this.FrmUno_Resize);
            this.statutStrip.ResumeLayout(false);
            this.statutStrip.PerformLayout();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statutStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatut;
        private System.Windows.Forms.Timer tmrDragDrop;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem menuConnexions;
        private System.Windows.Forms.ToolStripMenuItem menuConnexionServeur;
        private System.Windows.Forms.ToolStripMenuItem menuConnexionClient;
        private System.Windows.Forms.Panel pnlTable;
        private System.Windows.Forms.ToolStripMenuItem menuUtilisateurs;
    }
}
