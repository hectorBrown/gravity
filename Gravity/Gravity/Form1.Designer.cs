namespace Gravity
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.TS_main = new System.Windows.Forms.ToolStrip();
            this.TS_save = new System.Windows.Forms.ToolStripButton();
            this.TS_open = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TS_addGrav = new System.Windows.Forms.ToolStripButton();
            this.TS_addObj = new System.Windows.Forms.ToolStripButton();
            this.TS_clear = new System.Windows.Forms.ToolStripButton();
            this.TS_random = new System.Windows.Forms.ToolStripButton();
            this.TS_trail = new System.Windows.Forms.ToolStripButton();
            this.TS_objGrav = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.TSTXT_mass = new System.Windows.Forms.ToolStripTextBox();
            this.TSTXT_vX = new System.Windows.Forms.ToolStripTextBox();
            this.TSTXT_vY = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.TS_help = new System.Windows.Forms.ToolStripButton();
            this.TIM_main = new System.Windows.Forms.Timer(this.components);
            this.PB_main = new System.Windows.Forms.PictureBox();
            this.TIM_trail = new System.Windows.Forms.Timer(this.components);
            this.SFD_main = new System.Windows.Forms.SaveFileDialog();
            this.OFD_main = new System.Windows.Forms.OpenFileDialog();
            this.PN_sidebar = new System.Windows.Forms.Panel();
            this.RTXT_focused = new System.Windows.Forms.RichTextBox();
            this.TS_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_main)).BeginInit();
            this.PN_sidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // TS_main
            // 
            this.TS_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TS_save,
            this.TS_open,
            this.toolStripSeparator1,
            this.TS_addGrav,
            this.TS_addObj,
            this.TS_clear,
            this.TS_random,
            this.TS_trail,
            this.TS_objGrav,
            this.toolStripSeparator,
            this.TSTXT_mass,
            this.TSTXT_vX,
            this.TSTXT_vY,
            this.toolStripSeparator2,
            this.toolStripSeparator3,
            this.TS_help});
            this.TS_main.Location = new System.Drawing.Point(0, 0);
            this.TS_main.Name = "TS_main";
            this.TS_main.Size = new System.Drawing.Size(938, 25);
            this.TS_main.TabIndex = 0;
            this.TS_main.Text = "toolStrip1";
            // 
            // TS_save
            // 
            this.TS_save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TS_save.Image = ((System.Drawing.Image)(resources.GetObject("TS_save.Image")));
            this.TS_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_save.Name = "TS_save";
            this.TS_save.Size = new System.Drawing.Size(23, 22);
            this.TS_save.Text = "&Save";
            this.TS_save.Click += new System.EventHandler(this.TS_save_Click);
            // 
            // TS_open
            // 
            this.TS_open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TS_open.Image = ((System.Drawing.Image)(resources.GetObject("TS_open.Image")));
            this.TS_open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_open.Name = "TS_open";
            this.TS_open.Size = new System.Drawing.Size(23, 22);
            this.TS_open.Text = "&Open";
            this.TS_open.Click += new System.EventHandler(this.TS_open_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // TS_addGrav
            // 
            this.TS_addGrav.Checked = true;
            this.TS_addGrav.CheckOnClick = true;
            this.TS_addGrav.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TS_addGrav.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TS_addGrav.Image = ((System.Drawing.Image)(resources.GetObject("TS_addGrav.Image")));
            this.TS_addGrav.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_addGrav.Name = "TS_addGrav";
            this.TS_addGrav.Size = new System.Drawing.Size(112, 22);
            this.TS_addGrav.Text = "Add Gravity Source";
            this.TS_addGrav.CheckedChanged += new System.EventHandler(this.TS_addGrav_CheckedChanged);
            // 
            // TS_addObj
            // 
            this.TS_addObj.CheckOnClick = true;
            this.TS_addObj.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TS_addObj.Image = ((System.Drawing.Image)(resources.GetObject("TS_addObj.Image")));
            this.TS_addObj.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_addObj.Name = "TS_addObj";
            this.TS_addObj.Size = new System.Drawing.Size(71, 22);
            this.TS_addObj.Text = "Add Object";
            this.TS_addObj.CheckedChanged += new System.EventHandler(this.TS_addObj_CheckedChanged);
            // 
            // TS_clear
            // 
            this.TS_clear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TS_clear.Image = ((System.Drawing.Image)(resources.GetObject("TS_clear.Image")));
            this.TS_clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_clear.Name = "TS_clear";
            this.TS_clear.Size = new System.Drawing.Size(55, 22);
            this.TS_clear.Text = "Clear All";
            this.TS_clear.Click += new System.EventHandler(this.TS_clear_Click);
            // 
            // TS_random
            // 
            this.TS_random.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TS_random.Image = ((System.Drawing.Image)(resources.GetObject("TS_random.Image")));
            this.TS_random.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_random.Name = "TS_random";
            this.TS_random.Size = new System.Drawing.Size(149, 22);
            this.TS_random.Text = "Generate Random Objects";
            this.TS_random.Click += new System.EventHandler(this.TS_random_Click);
            // 
            // TS_trail
            // 
            this.TS_trail.CheckOnClick = true;
            this.TS_trail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TS_trail.Image = ((System.Drawing.Image)(resources.GetObject("TS_trail.Image")));
            this.TS_trail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_trail.Name = "TS_trail";
            this.TS_trail.Size = new System.Drawing.Size(70, 22);
            this.TS_trail.Text = "Show Trails";
            this.TS_trail.CheckedChanged += new System.EventHandler(this.TS_trail_CheckedChanged);
            // 
            // TS_objGrav
            // 
            this.TS_objGrav.CheckOnClick = true;
            this.TS_objGrav.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TS_objGrav.Image = ((System.Drawing.Image)(resources.GetObject("TS_objGrav.Image")));
            this.TS_objGrav.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_objGrav.Name = "TS_objGrav";
            this.TS_objGrav.Size = new System.Drawing.Size(86, 22);
            this.TS_objGrav.Text = "Object Gravity";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // TSTXT_mass
            // 
            this.TSTXT_mass.Name = "TSTXT_mass";
            this.TSTXT_mass.Size = new System.Drawing.Size(100, 25);
            // 
            // TSTXT_vX
            // 
            this.TSTXT_vX.Name = "TSTXT_vX";
            this.TSTXT_vX.Size = new System.Drawing.Size(100, 25);
            // 
            // TSTXT_vY
            // 
            this.TSTXT_vY.Name = "TSTXT_vY";
            this.TSTXT_vY.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 6);
            // 
            // TS_help
            // 
            this.TS_help.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TS_help.Image = ((System.Drawing.Image)(resources.GetObject("TS_help.Image")));
            this.TS_help.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TS_help.Name = "TS_help";
            this.TS_help.Size = new System.Drawing.Size(23, 20);
            this.TS_help.Text = "He&lp";
            this.TS_help.Click += new System.EventHandler(this.TS_help_Click);
            // 
            // TIM_main
            // 
            this.TIM_main.Enabled = true;
            this.TIM_main.Interval = 10;
            this.TIM_main.Tick += new System.EventHandler(this.TIM_main_Tick);
            // 
            // PB_main
            // 
            this.PB_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PB_main.Location = new System.Drawing.Point(0, 25);
            this.PB_main.Name = "PB_main";
            this.PB_main.Size = new System.Drawing.Size(938, 425);
            this.PB_main.TabIndex = 1;
            this.PB_main.TabStop = false;
            this.PB_main.Click += new System.EventHandler(this.PB_main_Click);
            this.PB_main.Paint += new System.Windows.Forms.PaintEventHandler(this.PB_main_Paint);
            this.PB_main.MouseEnter += new System.EventHandler(this.PB_main_MouseEnter);
            this.PB_main.MouseLeave += new System.EventHandler(this.PB_main_MouseLeave);
            // 
            // TIM_trail
            // 
            this.TIM_trail.Interval = 10;
            this.TIM_trail.Tick += new System.EventHandler(this.TIM_trail_Tick);
            // 
            // SFD_main
            // 
            this.SFD_main.FileName = "config1";
            this.SFD_main.Filter = "Grav Files|*.GRAVPROJ";
            this.SFD_main.Tag = "";
            this.SFD_main.FileOk += new System.ComponentModel.CancelEventHandler(this.SFD_main_FileOk);
            // 
            // OFD_main
            // 
            this.OFD_main.FileName = "config1";
            this.OFD_main.Filter = "Grav Files|*.GRAVPROJ";
            this.OFD_main.FileOk += new System.ComponentModel.CancelEventHandler(this.OFD_main_FileOk);
            // 
            // PN_sidebar
            // 
            this.PN_sidebar.Controls.Add(this.RTXT_focused);
            this.PN_sidebar.Dock = System.Windows.Forms.DockStyle.Right;
            this.PN_sidebar.Enabled = false;
            this.PN_sidebar.Location = new System.Drawing.Point(745, 25);
            this.PN_sidebar.Name = "PN_sidebar";
            this.PN_sidebar.Size = new System.Drawing.Size(193, 425);
            this.PN_sidebar.TabIndex = 2;
            // 
            // RTXT_focused
            // 
            this.RTXT_focused.Location = new System.Drawing.Point(3, 3);
            this.RTXT_focused.Name = "RTXT_focused";
            this.RTXT_focused.Size = new System.Drawing.Size(187, 210);
            this.RTXT_focused.TabIndex = 0;
            this.RTXT_focused.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 450);
            this.Controls.Add(this.PN_sidebar);
            this.Controls.Add(this.PB_main);
            this.Controls.Add(this.TS_main);
            this.Name = "Form1";
            this.Text = "Gravity";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.TS_main.ResumeLayout(false);
            this.TS_main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_main)).EndInit();
            this.PN_sidebar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip TS_main;
        private System.Windows.Forms.ToolStripButton TS_addGrav;
        private System.Windows.Forms.ToolStripButton TS_addObj;
        private System.Windows.Forms.Timer TIM_main;
        private System.Windows.Forms.PictureBox PB_main;
        private System.Windows.Forms.ToolStripTextBox TSTXT_mass;
        private System.Windows.Forms.ToolStripTextBox TSTXT_vX;
        private System.Windows.Forms.ToolStripTextBox TSTXT_vY;
        private System.Windows.Forms.ToolStripButton TS_clear;
        private System.Windows.Forms.ToolStripButton TS_random;
        private System.Windows.Forms.ToolStripButton TS_trail;
        private System.Windows.Forms.Timer TIM_trail;
        private System.Windows.Forms.ToolStripButton TS_objGrav;
        private System.Windows.Forms.ToolStripButton TS_save;
        private System.Windows.Forms.ToolStripButton TS_open;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.SaveFileDialog SFD_main;
        private System.Windows.Forms.OpenFileDialog OFD_main;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton TS_help;
        private System.Windows.Forms.Panel PN_sidebar;
        private System.Windows.Forms.RichTextBox RTXT_focused;
    }
}

