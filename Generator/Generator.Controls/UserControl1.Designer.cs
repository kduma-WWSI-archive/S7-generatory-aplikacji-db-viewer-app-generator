namespace Generator.Controls
{
    partial class Join
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.leftSide = new System.Windows.Forms.ComboBox();
            this.rightSide = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // leftSide
            // 
            this.leftSide.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftSide.FormattingEnabled = true;
            this.leftSide.Location = new System.Drawing.Point(0, 0);
            this.leftSide.Name = "leftSide";
            this.leftSide.Size = new System.Drawing.Size(121, 21);
            this.leftSide.TabIndex = 0;
            // 
            // rightSide
            // 
            this.rightSide.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightSide.FormattingEnabled = true;
            this.rightSide.Location = new System.Drawing.Point(145, 0);
            this.rightSide.Name = "rightSide";
            this.rightSide.Size = new System.Drawing.Size(129, 21);
            this.rightSide.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "=";
            // 
            // Join
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rightSide);
            this.Controls.Add(this.leftSide);
            this.MaximumSize = new System.Drawing.Size(274, 23);
            this.MinimumSize = new System.Drawing.Size(274, 23);
            this.Name = "Join";
            this.Size = new System.Drawing.Size(274, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox leftSide;
        private System.Windows.Forms.ComboBox rightSide;
        private System.Windows.Forms.Label label1;
    }
}
