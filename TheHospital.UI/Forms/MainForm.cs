using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheHospital.BAL.Mangers;
using TheHospital.DAL.Models;
using TheHospital.UI.Models;

namespace TheHospital.UI.Forms
{
    public partial class MainForm : Form
    {
        public UserViewModel model;
        private readonly UserManger _UserManger;
        private readonly KindManger _KindManger;
        public MainForm(int Id)
        {
            _KindManger = new KindManger();
            _UserManger = new UserManger();
            
            User s = _UserManger.GetBy(Id);
            Kind sK = _KindManger.GetBy(s.Kind_Id);
            model = new UserViewModel()
            {
                Name=s.Name,
                Password=s.Password,
                TypeName=sK.KindName,
                UserId=s.UserId
            };
            InitializeComponent();
        }
        public MainForm()
        {
            InitializeComponent();
        }

        private void اضافةعيادةToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            AddClinic form = new AddClinic()
            {
                TopLevel = false,
                TopMost = true,
                MdiParent = this,
                Opacity = 0
            }; ;
            form.MdiParent = this;
            form.Show();
        }

      

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (model.TypeName == "مشرف")
            {
                file.Visible = true;
                clinicSignin.Visible = true;
                entry.Visible = true;
                PationtPolice.Visible = true;
                Xrays.Visible = true;
                analzes.Visible = true;
            }
            if (model.TypeName == "عيادة")
            {
                clinicSignin.Visible = true;
            }

            if (model.TypeName == "مدخل")
            {
                entry.Visible = true;
            }
            if (model.TypeName == "شئون المرضي")
            {
                PationtPolice.Visible = true;
            }

            if (model.TypeName == "إشعة")
            {
                Xrays.Visible = true;
            }
            if (model.TypeName == "تحاليل")
            {
                analzes.Visible = true;
            }


        }

        private void الزياراتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            Start form = new Start()
            {
                TopLevel = false,
                TopMost = true,
                MdiParent = this,
                Opacity = 0
            }; 
            form.MdiParent = this;
            form.Show();

        }

        private void تسجيلالدخولللعيادةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            ClinicSignIn form = new ClinicSignIn()
            {
                TopLevel = false,
                TopMost = true,
                MdiParent = this,
                Opacity = 0
            };
            //ClinicLogIn form = new ClinicLogIn();
           
            form.Show();
        }

        private void اضافةالزياراتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            Start form = new Start()
            {
                TopLevel = false,
                TopMost = true,
                MdiParent = this,
                Opacity = 0
            };
            form.MdiParent = this;
            form.Show();
        }

        private void شئونالمرضيToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            CustomerServices form = new CustomerServices()
            {
                TopLevel = false,
                TopMost = true,
                MdiParent = this,
                Opacity = 0
            };
            form.MdiParent = this;
            form.Show();
        }

        private void إشعةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            Xrays form = new Xrays()
            {
                TopLevel = false,
                TopMost = true,
                MdiParent = this,
                Opacity = 0
            };
            form.MdiParent = this;
            form.Show();
        }

        private void تحاليلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            Analyzes form = new Analyzes()
            {
                TopLevel = false,
                TopMost = true,
                MdiParent = this,
                Opacity = 0
            };
            form.MdiParent = this;
            form.Show();
        }

        private void مستخدمجديدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            AddUser form = new AddUser()
            {
                TopLevel = false,
                TopMost = true,
                MdiParent = this,
                Opacity = 0
            };
            form.MdiParent = this;
            form.Show();
        }

        private void تسجيلالخروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            loging form = new loging();
            this.Close();
            form.Show();

        }
    }
}
