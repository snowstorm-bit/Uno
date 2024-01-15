namespace _420_14C_FX_TP2
{
    partial class FrmServeur
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
            this.lstClients = new System.Windows.Forms.ListBox();
            this.btnDemarrerServeur = new System.Windows.Forms.Button();
            this.btnDemarrerPartie = new System.Windows.Forms.Button();
            this.txtStatut = new System.Windows.Forms.RichTextBox();
            this.Serveur = new System.Windows.Forms.GroupBox();
            this.txtNoPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAdresseIP = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCacher = new System.Windows.Forms.Button();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.Serveur.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstClients
            // 
            this.lstClients.FormattingEnabled = true;
            this.lstClients.Location = new System.Drawing.Point(4, 22);
            this.lstClients.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lstClients.Name = "lstClients";
            this.lstClients.Size = new System.Drawing.Size(142, 251);
            this.lstClients.TabIndex = 0;
            // 
            // btnDemarrerServeur
            // 
            this.btnDemarrerServeur.Location = new System.Drawing.Point(178, 46);
            this.btnDemarrerServeur.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDemarrerServeur.Name = "btnDemarrerServeur";
            this.btnDemarrerServeur.Size = new System.Drawing.Size(64, 22);
            this.btnDemarrerServeur.TabIndex = 2;
            this.btnDemarrerServeur.Text = "Démarrer";
            this.btnDemarrerServeur.UseVisualStyleBackColor = true;
            this.btnDemarrerServeur.Click += new System.EventHandler(this.BtnDemarrerServeur_Click);
            // 
            // btnDemarrerPartie
            // 
            this.btnDemarrerPartie.Enabled = false;
            this.btnDemarrerPartie.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDemarrerPartie.Location = new System.Drawing.Point(4, 281);
            this.btnDemarrerPartie.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDemarrerPartie.Name = "btnDemarrerPartie";
            this.btnDemarrerPartie.Size = new System.Drawing.Size(141, 19);
            this.btnDemarrerPartie.TabIndex = 3;
            this.btnDemarrerPartie.Text = "Démarrer la partie";
            this.btnDemarrerPartie.UseVisualStyleBackColor = true;
            this.btnDemarrerPartie.Click += new System.EventHandler(this.BtnDemarrerPartie_Click);
            // 
            // txtStatut
            // 
            this.txtStatut.Location = new System.Drawing.Point(14, 73);
            this.txtStatut.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtStatut.Name = "txtStatut";
            this.txtStatut.Size = new System.Drawing.Size(229, 227);
            this.txtStatut.TabIndex = 4;
            this.txtStatut.Text = "";
            // 
            // Serveur
            // 
            this.Serveur.Controls.Add(this.txtNoPort);
            this.Serveur.Controls.Add(this.label1);
            this.Serveur.Controls.Add(this.lblAdresseIP);
            this.Serveur.Controls.Add(this.txtStatut);
            this.Serveur.Controls.Add(this.btnDemarrerServeur);
            this.Serveur.Location = new System.Drawing.Point(20, 27);
            this.Serveur.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Serveur.Name = "Serveur";
            this.Serveur.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Serveur.Size = new System.Drawing.Size(253, 310);
            this.Serveur.TabIndex = 5;
            this.Serveur.TabStop = false;
            this.Serveur.Text = "Serveur";
            // 
            // txtNoPort
            // 
            this.txtNoPort.Location = new System.Drawing.Point(42, 50);
            this.txtNoPort.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNoPort.Name = "txtNoPort";
            this.txtNoPort.Size = new System.Drawing.Size(76, 20);
            this.txtNoPort.TabIndex = 7;
            this.txtNoPort.Text = "100";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Port";
            // 
            // lblAdresseIP
            // 
            this.lblAdresseIP.AutoSize = true;
            this.lblAdresseIP.Location = new System.Drawing.Point(12, 28);
            this.lblAdresseIP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAdresseIP.Name = "lblAdresseIP";
            this.lblAdresseIP.Size = new System.Drawing.Size(67, 13);
            this.lblAdresseIP.TabIndex = 5;
            this.lblAdresseIP.Text = "Adresse IP : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstClients);
            this.groupBox1.Controls.Add(this.btnDemarrerPartie);
            this.groupBox1.Location = new System.Drawing.Point(284, 27);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(150, 310);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Client(s) connecté(s)";
            // 
            // btnCacher
            // 
            this.btnCacher.Location = new System.Drawing.Point(300, 341);
            this.btnCacher.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCacher.Name = "btnCacher";
            this.btnCacher.Size = new System.Drawing.Size(64, 22);
            this.btnCacher.TabIndex = 7;
            this.btnCacher.Text = "Cacher";
            this.btnCacher.UseVisualStyleBackColor = true;
            this.btnCacher.Click += new System.EventHandler(this.BtnCacher_Click);
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(370, 341);
            this.btnQuitter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(64, 22);
            this.btnQuitter.TabIndex = 8;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.BtnQuitter_Click);
            // 
            // FrmServeur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 372);
            this.Controls.Add(this.btnQuitter);
            this.Controls.Add(this.btnCacher);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Serveur);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmServeur";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmServeur";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmServeur_FormClosing);
            this.Load += new System.EventHandler(this.FrmServeur_Load);
            this.Serveur.ResumeLayout(false);
            this.Serveur.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstClients;
        private System.Windows.Forms.Button btnDemarrerServeur;
        private System.Windows.Forms.Button btnDemarrerPartie;
        private System.Windows.Forms.RichTextBox txtStatut;
        private System.Windows.Forms.GroupBox Serveur;
        private System.Windows.Forms.TextBox txtNoPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAdresseIP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCacher;
        private System.Windows.Forms.Button btnQuitter;
    }
}