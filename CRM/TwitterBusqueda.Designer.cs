namespace CRM
{
    partial class TwitterBusqueda
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
            this.textBusqueda = new System.Windows.Forms.TextBox();
            this.btnBusqueda = new System.Windows.Forms.Button();
            this.split = new System.Windows.Forms.SplitContainer();
            this.labelBusqueda = new System.Windows.Forms.Label();
            this.comboOpciones = new System.Windows.Forms.ComboBox();
            this.textTweets = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBusqueda
            // 
            this.textBusqueda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBusqueda.Location = new System.Drawing.Point(422, 6);
            this.textBusqueda.Name = "textBusqueda";
            this.textBusqueda.Size = new System.Drawing.Size(141, 20);
            this.textBusqueda.TabIndex = 0;
            // 
            // btnBusqueda
            // 
            this.btnBusqueda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBusqueda.Location = new System.Drawing.Point(569, 3);
            this.btnBusqueda.Name = "btnBusqueda";
            this.btnBusqueda.Size = new System.Drawing.Size(75, 23);
            this.btnBusqueda.TabIndex = 1;
            this.btnBusqueda.Text = "Busqueda";
            this.btnBusqueda.UseVisualStyleBackColor = true;
            this.btnBusqueda.Click += new System.EventHandler(this.btnBusqueda_Click);
            // 
            // split
            // 
            this.split.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.split.Location = new System.Drawing.Point(12, 12);
            this.split.Name = "split";
            this.split.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // split.Panel1
            // 
            this.split.Panel1.Controls.Add(this.labelBusqueda);
            this.split.Panel1.Controls.Add(this.comboOpciones);
            this.split.Panel1.Controls.Add(this.textBusqueda);
            this.split.Panel1.Controls.Add(this.btnBusqueda);
            // 
            // split.Panel2
            // 
            this.split.Panel2.AutoScroll = true;
            this.split.Panel2.Controls.Add(this.textTweets);
            this.split.Size = new System.Drawing.Size(647, 425);
            this.split.SplitterDistance = 32;
            this.split.TabIndex = 2;
            // 
            // labelBusqueda
            // 
            this.labelBusqueda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelBusqueda.AutoSize = true;
            this.labelBusqueda.Location = new System.Drawing.Point(200, 9);
            this.labelBusqueda.Name = "labelBusqueda";
            this.labelBusqueda.Size = new System.Drawing.Size(99, 13);
            this.labelBusqueda.TabIndex = 3;
            this.labelBusqueda.Text = "Tipo de busqueda: ";
            // 
            // comboOpciones
            // 
            this.comboOpciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboOpciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboOpciones.FormattingEnabled = true;
            this.comboOpciones.Location = new System.Drawing.Point(305, 6);
            this.comboOpciones.Name = "comboOpciones";
            this.comboOpciones.Size = new System.Drawing.Size(111, 21);
            this.comboOpciones.TabIndex = 2;
            // 
            // textTweets
            // 
            this.textTweets.BackColor = System.Drawing.SystemColors.Control;
            this.textTweets.Location = new System.Drawing.Point(1, 4);
            this.textTweets.Multiline = true;
            this.textTweets.Name = "textTweets";
            this.textTweets.ReadOnly = true;
            this.textTweets.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textTweets.Size = new System.Drawing.Size(643, 385);
            this.textTweets.TabIndex = 0;
            // 
            // TwitterBusqueda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 449);
            this.Controls.Add(this.split);
            this.Name = "TwitterBusqueda";
            this.Text = "Busqueda en Twitter";
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel1.PerformLayout();
            this.split.Panel2.ResumeLayout(false);
            this.split.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBusqueda;
        private System.Windows.Forms.Button btnBusqueda;
        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.ComboBox comboOpciones;
        private System.Windows.Forms.Label labelBusqueda;
        private System.Windows.Forms.TextBox textTweets;
    }
}