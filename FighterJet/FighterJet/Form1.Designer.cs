namespace FighterJet
{
    partial class txtlvl1
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
            this.playerMovement = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // playerMovement
            // 
            this.playerMovement.Enabled = true;
            this.playerMovement.Interval = 20;
            this.playerMovement.Tick += new System.EventHandler(this.PlayerMovement_Tick);
            // 
            // txtlvl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(989, 544);
            this.Name = "txtlvl1";
            this.Text = "lvl1";
            this.Load += new System.EventHandler(this.Txtlvl1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer playerMovement;
    }
}

