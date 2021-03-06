﻿namespace CRM
{
    partial class EditarPerfil
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
            this.panel = new System.Windows.Forms.Panel();
            this.btnFoto = new System.Windows.Forms.Button();
            this.picture = new System.Windows.Forms.PictureBox();
            this.btnEmpleo = new System.Windows.Forms.Button();
            this.btnCiudad = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.AutoScroll = true;
            this.panel.Controls.Add(this.button1);
            this.panel.Controls.Add(this.btnFoto);
            this.panel.Controls.Add(this.picture);
            this.panel.Controls.Add(this.btnEmpleo);
            this.panel.Controls.Add(this.btnCiudad);
            this.panel.Controls.Add(this.btnUpdate);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(737, 480);
            this.panel.TabIndex = 0;
            // 
            // btnFoto
            // 
            this.btnFoto.Location = new System.Drawing.Point(535, 217);
            this.btnFoto.Name = "btnFoto";
            this.btnFoto.Size = new System.Drawing.Size(75, 23);
            this.btnFoto.TabIndex = 4;
            this.btnFoto.Text = "Subir foto";
            this.btnFoto.UseVisualStyleBackColor = true;
            this.btnFoto.Click += new System.EventHandler(this.btnFoto_Click);
            // 
            // picture
            // 
            this.picture.Location = new System.Drawing.Point(455, 29);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(231, 182);
            this.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picture.TabIndex = 3;
            this.picture.TabStop = false;
            // 
            // btnEmpleo
            // 
            this.btnEmpleo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEmpleo.Location = new System.Drawing.Point(645, 454);
            this.btnEmpleo.Name = "btnEmpleo";
            this.btnEmpleo.Size = new System.Drawing.Size(89, 23);
            this.btnEmpleo.TabIndex = 2;
            this.btnEmpleo.Text = "Agregar empleo";
            this.btnEmpleo.UseVisualStyleBackColor = true;
            this.btnEmpleo.Click += new System.EventHandler(this.btnEmpleo_Click);
            // 
            // btnCiudad
            // 
            this.btnCiudad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCiudad.Location = new System.Drawing.Point(645, 425);
            this.btnCiudad.Name = "btnCiudad";
            this.btnCiudad.Size = new System.Drawing.Size(89, 23);
            this.btnCiudad.TabIndex = 1;
            this.btnCiudad.Text = "Agregar ciudad";
            this.btnCiudad.UseVisualStyleBackColor = true;
            this.btnCiudad.Click += new System.EventHandler(this.btnCiudad_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(233, 454);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(97, 23);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "Actualizar datos";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(336, 453);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 25);
            this.button1.TabIndex = 5;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // EditarPerfil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 480);
            this.Controls.Add(this.panel);
            this.Name = "EditarPerfil";
            this.Text = "Editar perfil de usuario";
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCiudad;
        private System.Windows.Forms.Button btnEmpleo;
        private System.Windows.Forms.PictureBox picture;
        private System.Windows.Forms.Button btnFoto;
        private System.Windows.Forms.Button button1;
    }
}