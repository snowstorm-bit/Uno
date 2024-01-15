namespace _420_14C_FX_TP2
{
    /// <summary>
    /// Formulaire permettant de sélectionner une couleur.
    /// </summary>
    partial class FrmSelectionCouleur
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
            this.pboBleu = new System.Windows.Forms.PictureBox();
            this.pboVert = new System.Windows.Forms.PictureBox();
            this.pboRouge = new System.Windows.Forms.PictureBox();
            this.pboJaune = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pboBleu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboVert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboRouge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboJaune)).BeginInit();
            this.SuspendLayout();
            // 
            // pboBleu
            // 
            this.pboBleu.Image = global::_420_14C_FX_TP2.Properties.Resources.Bleu;
            this.pboBleu.Location = new System.Drawing.Point(12, 12);
            this.pboBleu.Name = "pboBleu";
            this.pboBleu.Size = new System.Drawing.Size(90, 90);
            this.pboBleu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pboBleu.TabIndex = 3;
            this.pboBleu.TabStop = false;
            this.pboBleu.Tag = "3";
            this.pboBleu.Click += new System.EventHandler(this.PictureBox_Click);
            // 
            // pboVert
            // 
            this.pboVert.Image = global::_420_14C_FX_TP2.Properties.Resources.Vert;
            this.pboVert.Location = new System.Drawing.Point(108, 12);
            this.pboVert.Name = "pboVert";
            this.pboVert.Size = new System.Drawing.Size(90, 90);
            this.pboVert.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pboVert.TabIndex = 4;
            this.pboVert.TabStop = false;
            this.pboVert.Tag = "1";
            this.pboVert.Click += new System.EventHandler(this.PictureBox_Click);
            // 
            // pboRouge
            // 
            this.pboRouge.Image = global::_420_14C_FX_TP2.Properties.Resources.Rouge;
            this.pboRouge.Location = new System.Drawing.Point(12, 108);
            this.pboRouge.Name = "pboRouge";
            this.pboRouge.Size = new System.Drawing.Size(90, 90);
            this.pboRouge.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pboRouge.TabIndex = 5;
            this.pboRouge.TabStop = false;
            this.pboRouge.Tag = "0";
            this.pboRouge.Click += new System.EventHandler(this.PictureBox_Click);
            // 
            // pboJaune
            // 
            this.pboJaune.Image = global::_420_14C_FX_TP2.Properties.Resources.Jaune;
            this.pboJaune.Location = new System.Drawing.Point(108, 108);
            this.pboJaune.Name = "pboJaune";
            this.pboJaune.Size = new System.Drawing.Size(90, 90);
            this.pboJaune.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pboJaune.TabIndex = 6;
            this.pboJaune.TabStop = false;
            this.pboJaune.Tag = "2";
            this.pboJaune.Click += new System.EventHandler(this.PictureBox_Click);
            // 
            // frmSelectionCouleur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 204);
            this.Controls.Add(this.pboJaune);
            this.Controls.Add(this.pboRouge);
            this.Controls.Add(this.pboVert);
            this.Controls.Add(this.pboBleu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmSelectionCouleur";
            this.Text = "Sélectionner une couleur";
            this.Load += new System.EventHandler(this.FrmSelectionCouleur_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pboBleu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboVert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboRouge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboJaune)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pboBleu;
        private System.Windows.Forms.PictureBox pboVert;
        private System.Windows.Forms.PictureBox pboRouge;
        private System.Windows.Forms.PictureBox pboJaune;
    }
}