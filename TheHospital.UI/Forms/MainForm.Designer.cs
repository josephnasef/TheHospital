
namespace TheHospital.UI.Forms
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.file = new System.Windows.Forms.ToolStripMenuItem();
            this.اضافةعيادةToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.اضافةعيادةToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.مستخدمجديدToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clinicSignin = new System.Windows.Forms.ToolStripMenuItem();
            this.entry = new System.Windows.Forms.ToolStripMenuItem();
            this.PationtPolice = new System.Windows.Forms.ToolStripMenuItem();
            this.Xrays = new System.Windows.Forms.ToolStripMenuItem();
            this.analzes = new System.Windows.Forms.ToolStripMenuItem();
            this.تسجيلالخروجToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("PT Bold Heading", 10F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.file,
            this.clinicSignin,
            this.entry,
            this.PationtPolice,
            this.Xrays,
            this.analzes,
            this.تسجيلالخروجToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStrip1.Size = new System.Drawing.Size(800, 33);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // file
            // 
            this.file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.اضافةعيادةToolStripMenuItem});
            this.file.Name = "file";
            this.file.Size = new System.Drawing.Size(48, 29);
            this.file.Text = "ملف";
            this.file.Visible = false;
            // 
            // اضافةعيادةToolStripMenuItem
            // 
            this.اضافةعيادةToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.اضافةعيادةToolStripMenuItem1,
            this.مستخدمجديدToolStripMenuItem});
            this.اضافةعيادةToolStripMenuItem.Name = "اضافةعيادةToolStripMenuItem";
            this.اضافةعيادةToolStripMenuItem.Size = new System.Drawing.Size(122, 30);
            this.اضافةعيادةToolStripMenuItem.Text = "اضافات";
            // 
            // اضافةعيادةToolStripMenuItem1
            // 
            this.اضافةعيادةToolStripMenuItem1.Name = "اضافةعيادةToolStripMenuItem1";
            this.اضافةعيادةToolStripMenuItem1.Size = new System.Drawing.Size(185, 30);
            this.اضافةعيادةToolStripMenuItem1.Text = "اضافة عيادة جديدة";
            this.اضافةعيادةToolStripMenuItem1.Click += new System.EventHandler(this.اضافةعيادةToolStripMenuItem1_Click);
            // 
            // مستخدمجديدToolStripMenuItem
            // 
            this.مستخدمجديدToolStripMenuItem.Name = "مستخدمجديدToolStripMenuItem";
            this.مستخدمجديدToolStripMenuItem.Size = new System.Drawing.Size(185, 30);
            this.مستخدمجديدToolStripMenuItem.Text = "مستخدم جديد";
            this.مستخدمجديدToolStripMenuItem.Click += new System.EventHandler(this.مستخدمجديدToolStripMenuItem_Click);
            // 
            // clinicSignin
            // 
            this.clinicSignin.Name = "clinicSignin";
            this.clinicSignin.Size = new System.Drawing.Size(69, 29);
            this.clinicSignin.Text = "العيادات";
            this.clinicSignin.Visible = false;
            this.clinicSignin.Click += new System.EventHandler(this.تسجيلالدخولللعيادةToolStripMenuItem_Click);
            // 
            // entry
            // 
            this.entry.Name = "entry";
            this.entry.Size = new System.Drawing.Size(73, 29);
            this.entry.Text = "الاستقبال";
            this.entry.Visible = false;
            this.entry.Click += new System.EventHandler(this.اضافةالزياراتToolStripMenuItem_Click);
            // 
            // PationtPolice
            // 
            this.PationtPolice.Name = "PationtPolice";
            this.PationtPolice.Size = new System.Drawing.Size(95, 29);
            this.PationtPolice.Text = "شئون المرضي";
            this.PationtPolice.Visible = false;
            this.PationtPolice.Click += new System.EventHandler(this.شئونالمرضيToolStripMenuItem_Click);
            // 
            // Xrays
            // 
            this.Xrays.Name = "Xrays";
            this.Xrays.Size = new System.Drawing.Size(50, 29);
            this.Xrays.Text = "إشعة";
            this.Xrays.Visible = false;
            this.Xrays.Click += new System.EventHandler(this.إشعةToolStripMenuItem_Click);
            // 
            // analzes
            // 
            this.analzes.Name = "analzes";
            this.analzes.Size = new System.Drawing.Size(57, 29);
            this.analzes.Text = "تحاليل";
            this.analzes.Visible = false;
            this.analzes.Click += new System.EventHandler(this.تحاليلToolStripMenuItem_Click);
            // 
            // تسجيلالخروجToolStripMenuItem
            // 
            this.تسجيلالخروجToolStripMenuItem.Name = "تسجيلالخروجToolStripMenuItem";
            this.تسجيلالخروجToolStripMenuItem.Size = new System.Drawing.Size(96, 29);
            this.تسجيلالخروجToolStripMenuItem.Text = "تسجيل الخروج";
            this.تسجيلالخروجToolStripMenuItem.Click += new System.EventHandler(this.تسجيلالخروجToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem file;
        private System.Windows.Forms.ToolStripMenuItem اضافةعيادةToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem اضافةعيادةToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem clinicSignin;
        private System.Windows.Forms.ToolStripMenuItem entry;
        private System.Windows.Forms.ToolStripMenuItem PationtPolice;
        private System.Windows.Forms.ToolStripMenuItem Xrays;
        private System.Windows.Forms.ToolStripMenuItem analzes;
        private System.Windows.Forms.ToolStripMenuItem مستخدمجديدToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem تسجيلالخروجToolStripMenuItem;
    }
}