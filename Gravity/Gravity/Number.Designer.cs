namespace Gravity
{
    partial class Number
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
            this.NUD_main = new System.Windows.Forms.NumericUpDown();
            this.BUT_submit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_main)).BeginInit();
            this.SuspendLayout();
            // 
            // NUD_main
            // 
            this.NUD_main.Location = new System.Drawing.Point(12, 12);
            this.NUD_main.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.NUD_main.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD_main.Name = "NUD_main";
            this.NUD_main.Size = new System.Drawing.Size(120, 20);
            this.NUD_main.TabIndex = 0;
            this.NUD_main.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // BUT_submit
            // 
            this.BUT_submit.Location = new System.Drawing.Point(12, 38);
            this.BUT_submit.Name = "BUT_submit";
            this.BUT_submit.Size = new System.Drawing.Size(120, 23);
            this.BUT_submit.TabIndex = 1;
            this.BUT_submit.Text = "OK";
            this.BUT_submit.UseVisualStyleBackColor = true;
            this.BUT_submit.Click += new System.EventHandler(this.BUT_submit_Click);
            // 
            // Number
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(148, 73);
            this.Controls.Add(this.BUT_submit);
            this.Controls.Add(this.NUD_main);
            this.Name = "Number";
            this.Text = "Number";
            ((System.ComponentModel.ISupportInitialize)(this.NUD_main)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown NUD_main;
        private System.Windows.Forms.Button BUT_submit;
    }
}