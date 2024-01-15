namespace _420_14C_FX_TP2
{
    partial class FrmUtilisateur
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
            this.gboUtilisateurs = new System.Windows.Forms.GroupBox();
            this.lstUtilisateurs = new System.Windows.Forms.ListBox();
            this.gboUtilisateur = new System.Windows.Forms.GroupBox();
            this.txtNomUtilisateur = new System.Windows.Forms.TextBox();
            this.btnNouveau = new System.Windows.Forms.Button();
            this.btnSupprimer = new System.Windows.Forms.Button();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.txtMotPasse = new System.Windows.Forms.TextBox();
            this.lblMotDePasse = new System.Windows.Forms.Label();
            this.lblAdresseIP = new System.Windows.Forms.Label();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.btnEnregistrer = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatut = new System.Windows.Forms.ToolStripStatusLabel();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.gboUtilisateurs.SuspendLayout();
            this.gboUtilisateur.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // gboUtilisateurs
            // 
            this.gboUtilisateurs.Controls.Add(this.lstUtilisateurs);
            this.gboUtilisateurs.Location = new System.Drawing.Point(281, 10);
            this.gboUtilisateurs.Margin = new System.Windows.Forms.Padding(2);
            this.gboUtilisateurs.Name = "gboUtilisateurs";
            this.gboUtilisateurs.Padding = new System.Windows.Forms.Padding(2);
            this.gboUtilisateurs.Size = new System.Drawing.Size(177, 199);
            this.gboUtilisateurs.TabIndex = 0;
            this.gboUtilisateurs.TabStop = false;
            this.gboUtilisateurs.Text = "Liste des utilisateurs";
            // 
            // lstUtilisateurs
            // 
            this.lstUtilisateurs.FormattingEnabled = true;
            this.lstUtilisateurs.Location = new System.Drawing.Point(13, 17);
            this.lstUtilisateurs.Margin = new System.Windows.Forms.Padding(2);
            this.lstUtilisateurs.Name = "lstUtilisateurs";
            this.lstUtilisateurs.Size = new System.Drawing.Size(152, 173);
            this.lstUtilisateurs.TabIndex = 5;
            this.lstUtilisateurs.SelectedIndexChanged += new System.EventHandler(this.LstUtilisateur_SelectedIndexChanged);
            // 
            // gboUtilisateur
            // 
            this.gboUtilisateur.Controls.Add(this.txtNomUtilisateur);
            this.gboUtilisateur.Controls.Add(this.btnNouveau);
            this.gboUtilisateur.Controls.Add(this.btnSupprimer);
            this.gboUtilisateur.Controls.Add(this.btnAjouter);
            this.gboUtilisateur.Controls.Add(this.txtMotPasse);
            this.gboUtilisateur.Controls.Add(this.lblMotDePasse);
            this.gboUtilisateur.Controls.Add(this.lblAdresseIP);
            this.gboUtilisateur.Location = new System.Drawing.Point(20, 10);
            this.gboUtilisateur.Margin = new System.Windows.Forms.Padding(2);
            this.gboUtilisateur.Name = "gboUtilisateur";
            this.gboUtilisateur.Padding = new System.Windows.Forms.Padding(2);
            this.gboUtilisateur.Size = new System.Drawing.Size(257, 199);
            this.gboUtilisateur.TabIndex = 1;
            this.gboUtilisateur.TabStop = false;
            this.gboUtilisateur.Text = "Utilisateur";
            // 
            // txtNomUtilisateur
            // 
            this.txtNomUtilisateur.Location = new System.Drawing.Point(96, 32);
            this.txtNomUtilisateur.Margin = new System.Windows.Forms.Padding(2);
            this.txtNomUtilisateur.Name = "txtNomUtilisateur";
            this.txtNomUtilisateur.Size = new System.Drawing.Size(134, 20);
            this.txtNomUtilisateur.TabIndex = 0;
            // 
            // btnNouveau
            // 
            this.btnNouveau.Location = new System.Drawing.Point(44, 161);
            this.btnNouveau.Margin = new System.Windows.Forms.Padding(2);
            this.btnNouveau.Name = "btnNouveau";
            this.btnNouveau.Size = new System.Drawing.Size(67, 28);
            this.btnNouveau.TabIndex = 2;
            this.btnNouveau.Text = "Nouveau";
            this.btnNouveau.UseVisualStyleBackColor = true;
            this.btnNouveau.Click += new System.EventHandler(this.BtnNouveau_Click);
            // 
            // btnSupprimer
            // 
            this.btnSupprimer.Location = new System.Drawing.Point(186, 161);
            this.btnSupprimer.Margin = new System.Windows.Forms.Padding(2);
            this.btnSupprimer.Name = "btnSupprimer";
            this.btnSupprimer.Size = new System.Drawing.Size(67, 28);
            this.btnSupprimer.TabIndex = 4;
            this.btnSupprimer.Text = "Supprimer";
            this.btnSupprimer.UseVisualStyleBackColor = true;
            this.btnSupprimer.Click += new System.EventHandler(this.BtnSupprimer_Click);
            // 
            // btnAjouter
            // 
            this.btnAjouter.Location = new System.Drawing.Point(115, 161);
            this.btnAjouter.Margin = new System.Windows.Forms.Padding(2);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(67, 28);
            this.btnAjouter.TabIndex = 3;
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.UseVisualStyleBackColor = true;
            this.btnAjouter.Click += new System.EventHandler(this.BtnAjouter_Click);
            // 
            // txtMotPasse
            // 
            this.txtMotPasse.Location = new System.Drawing.Point(96, 63);
            this.txtMotPasse.Margin = new System.Windows.Forms.Padding(2);
            this.txtMotPasse.Name = "txtMotPasse";
            this.txtMotPasse.Size = new System.Drawing.Size(134, 20);
            this.txtMotPasse.TabIndex = 1;
            // 
            // lblMotDePasse
            // 
            this.lblMotDePasse.AutoSize = true;
            this.lblMotDePasse.Location = new System.Drawing.Point(22, 66);
            this.lblMotDePasse.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMotDePasse.Name = "lblMotDePasse";
            this.lblMotDePasse.Size = new System.Drawing.Size(71, 13);
            this.lblMotDePasse.TabIndex = 4;
            this.lblMotDePasse.Text = "Mot de passe";
            // 
            // lblAdresseIP
            // 
            this.lblAdresseIP.AutoSize = true;
            this.lblAdresseIP.Location = new System.Drawing.Point(13, 36);
            this.lblAdresseIP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAdresseIP.Name = "lblAdresseIP";
            this.lblAdresseIP.Size = new System.Drawing.Size(82, 13);
            this.lblAdresseIP.TabIndex = 0;
            this.lblAdresseIP.Text = "Non d\'utilisateur";
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.Location = new System.Drawing.Point(382, 214);
            this.btnAnnuler.Margin = new System.Windows.Forms.Padding(2);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(76, 28);
            this.btnAnnuler.TabIndex = 7;
            this.btnAnnuler.Text = "Annuler";
            this.btnAnnuler.UseVisualStyleBackColor = true;
            this.btnAnnuler.Click += new System.EventHandler(this.BtnAnnuler_Click);
            // 
            // btnEnregistrer
            // 
            this.btnEnregistrer.Location = new System.Drawing.Point(294, 214);
            this.btnEnregistrer.Margin = new System.Windows.Forms.Padding(2);
            this.btnEnregistrer.Name = "btnEnregistrer";
            this.btnEnregistrer.Size = new System.Drawing.Size(76, 28);
            this.btnEnregistrer.TabIndex = 6;
            this.btnEnregistrer.Text = "Enregistrer";
            this.btnEnregistrer.UseVisualStyleBackColor = true;
            this.btnEnregistrer.Click += new System.EventHandler(this.BtnEnregistrer_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatut});
            this.statusStrip.Location = new System.Drawing.Point(0, 250);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip.Size = new System.Drawing.Size(467, 22);
            this.statusStrip.TabIndex = 8;
            this.statusStrip.Text = "statusStrip1";
            // 
            // lblStatut
            // 
            this.lblStatut.Name = "lblStatut";
            this.lblStatut.Size = new System.Drawing.Size(0, 17);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // FrmUtilisateur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 272);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btnEnregistrer);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.gboUtilisateur);
            this.Controls.Add(this.gboUtilisateurs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmUtilisateur";
            this.Text = "Gestion des utilisateurs";
            this.Load += new System.EventHandler(this.FrmUtilisateur_Load);
            this.gboUtilisateurs.ResumeLayout(false);
            this.gboUtilisateur.ResumeLayout(false);
            this.gboUtilisateur.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gboUtilisateurs;
        private System.Windows.Forms.ListBox lstUtilisateurs;
        private System.Windows.Forms.GroupBox gboUtilisateur;
        private System.Windows.Forms.Button btnNouveau;
        private System.Windows.Forms.Button btnSupprimer;
        private System.Windows.Forms.Button btnAjouter;
        private System.Windows.Forms.TextBox txtMotPasse;
        private System.Windows.Forms.Label lblMotDePasse;
        private System.Windows.Forms.Label lblAdresseIP;
        private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.Button btnEnregistrer;
        private System.Windows.Forms.TextBox txtNomUtilisateur;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatut;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}