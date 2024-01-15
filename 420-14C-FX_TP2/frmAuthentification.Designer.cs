namespace _420_14C_FX_TP2
{
    partial class FrmAuthentification
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
            this.btnConnecter = new System.Windows.Forms.Button();
            this.lblNom = new System.Windows.Forms.Label();
            this.lblMotPasse = new System.Windows.Forms.Label();
            this.txtNomUtilisateur = new System.Windows.Forms.TextBox();
            this.txtMotPasse = new System.Windows.Forms.TextBox();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.txtAdresseServeur = new System.Windows.Forms.TextBox();
            this.lblServeur = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnecter
            // 
            this.btnConnecter.Location = new System.Drawing.Point(102, 134);
            this.btnConnecter.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnecter.Name = "btnConnecter";
            this.btnConnecter.Size = new System.Drawing.Size(68, 25);
            this.btnConnecter.TabIndex = 0;
            this.btnConnecter.Text = "Connecter";
            this.btnConnecter.UseVisualStyleBackColor = true;
            this.btnConnecter.Click += new System.EventHandler(this.BtnConnecter_Click);
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(27, 63);
            this.lblNom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(84, 13);
            this.lblNom.TabIndex = 6;
            this.lblNom.Text = "Nom d\'utilisateur";
            // 
            // lblMotPasse
            // 
            this.lblMotPasse.AutoSize = true;
            this.lblMotPasse.Location = new System.Drawing.Point(39, 97);
            this.lblMotPasse.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMotPasse.Name = "lblMotPasse";
            this.lblMotPasse.Size = new System.Drawing.Size(74, 13);
            this.lblMotPasse.TabIndex = 8;
            this.lblMotPasse.Text = "Mot  de passe";
            // 
            // txtNomUtilisateur
            // 
            this.txtNomUtilisateur.Location = new System.Drawing.Point(116, 61);
            this.txtNomUtilisateur.Margin = new System.Windows.Forms.Padding(2);
            this.txtNomUtilisateur.Name = "txtNomUtilisateur";
            this.txtNomUtilisateur.Size = new System.Drawing.Size(156, 20);
            this.txtNomUtilisateur.TabIndex = 7;
            // 
            // txtMotPasse
            // 
            this.txtMotPasse.Location = new System.Drawing.Point(116, 97);
            this.txtMotPasse.Margin = new System.Windows.Forms.Padding(2);
            this.txtMotPasse.Name = "txtMotPasse";
            this.txtMotPasse.Size = new System.Drawing.Size(156, 20);
            this.txtMotPasse.TabIndex = 9;
            this.txtMotPasse.UseSystemPasswordChar = true;
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.Location = new System.Drawing.Point(182, 134);
            this.btnAnnuler.Margin = new System.Windows.Forms.Padding(2);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(68, 25);
            this.btnAnnuler.TabIndex = 1;
            this.btnAnnuler.Text = "Annuler";
            this.btnAnnuler.UseVisualStyleBackColor = true;
            this.btnAnnuler.Click += new System.EventHandler(this.BtnAnnuler_Click);
            // 
            // txtAdresseServeur
            // 
            this.txtAdresseServeur.Location = new System.Drawing.Point(116, 29);
            this.txtAdresseServeur.Margin = new System.Windows.Forms.Padding(2);
            this.txtAdresseServeur.Name = "txtAdresseServeur";
            this.txtAdresseServeur.Size = new System.Drawing.Size(84, 20);
            this.txtAdresseServeur.TabIndex = 3;
            // 
            // lblServeur
            // 
            this.lblServeur.AutoSize = true;
            this.lblServeur.Location = new System.Drawing.Point(68, 33);
            this.lblServeur.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblServeur.Name = "lblServeur";
            this.lblServeur.Size = new System.Drawing.Size(44, 13);
            this.lblServeur.TabIndex = 2;
            this.lblServeur.Text = "Serveur";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(204, 32);
            this.lblPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 4;
            this.lblPort.Text = "Port";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(234, 29);
            this.txtPort.Margin = new System.Windows.Forms.Padding(2);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(38, 20);
            this.txtPort.TabIndex = 5;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // FrmAuthentification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 174);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.txtAdresseServeur);
            this.Controls.Add(this.lblServeur);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.txtMotPasse);
            this.Controls.Add(this.txtNomUtilisateur);
            this.Controls.Add(this.lblMotPasse);
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.btnConnecter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmAuthentification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Authentification";
            this.Load += new System.EventHandler(this.FrmClient_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnecter;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.Label lblMotPasse;
        private System.Windows.Forms.TextBox txtNomUtilisateur;
        private System.Windows.Forms.TextBox txtMotPasse;
        private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.TextBox txtAdresseServeur;
        private System.Windows.Forms.Label lblServeur;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}