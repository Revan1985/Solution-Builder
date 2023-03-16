namespace SolutionBuilder.WinForms.Panels
{
    partial class ctlEmpty
    {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.lnkOpen = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lnkOpen
            // 
            this.lnkOpen.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lnkOpen.AutoSize = true;
            this.lnkOpen.LinkArea = new System.Windows.Forms.LinkArea(15, 27);
            this.lnkOpen.Location = new System.Drawing.Point(379, 226);
            this.lnkOpen.Name = "lnkOpen";
            this.lnkOpen.Size = new System.Drawing.Size(91, 37);
            this.lnkOpen.TabIndex = 0;
            this.lnkOpen.TabStop = true;
            this.lnkOpen.Text = "No file opened\r\nOpen file...";
            this.lnkOpen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lnkOpen.UseCompatibleTextRendering = true;
            this.lnkOpen.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkOpen_LinkClicked);
            // 
            // ctlEmpty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lnkOpen);
            this.Name = "ctlEmpty";
            this.Size = new System.Drawing.Size(848, 488);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkLabel lnkOpen;
    }
}
