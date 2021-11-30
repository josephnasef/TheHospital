using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheHospital.BAL.Mangers;
using TheHospital.DAL.Models;
using TheHospital.UI.Models;

namespace TheHospital.UI.Forms
{
    public partial class Xrays : Form
    {
        private readonly clinicMangers _ClinicManger;
        private readonly VisitManger _VisitManger;
        private int Id;

        public Xrays()
        {
            _ClinicManger = new clinicMangers();
            _VisitManger = new VisitManger();
            InitializeComponent();
            Search(string.Empty);
        }

        private void RnmDgv()
        {
            AllXraysVisits.Columns["VisitId"].Visible = false;
            AllXraysVisits.Columns["analyzesState"].Visible = false;
            AllXraysVisits.Columns["analyzes"].Visible = false;
            AllXraysVisits.Columns["State"].Visible = false;
            AllXraysVisits.Columns["PationtName"].HeaderText = "الاسم";
            AllXraysVisits.Columns["SoldierNum"].HeaderText = "الرقم العسكري";
            AllXraysVisits.Columns["Location"].HeaderText = "الوحدة";
            AllXraysVisits.Columns["CaseDescription"].HeaderText = "وصف الحالة";
            AllXraysVisits.Columns["DoctoreName"].HeaderText = "الدكتور المشخص";
            AllXraysVisits.Columns["EnterDate"].HeaderText = "تاريخ الدخول";
            AllXraysVisits.Columns["clinicName"].HeaderText = "العيادة";
            AllXraysVisits.Columns["XrayState"].HeaderText = "تمت الاشعة";
            AllXraysVisits.Columns["XrayState"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllXraysVisits.Columns["Xray"].Visible = false;
            AllXraysVisits.Columns["PationtName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllXraysVisits.Columns["SoldierNum"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllXraysVisits.Columns["Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllXraysVisits.Columns["CaseDescription"].Visible = false;
            AllXraysVisits.Columns["DoctoreName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllXraysVisits.Columns["EnterDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllXraysVisits.Columns["clinicName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void FillDgVi()
        {
            AllXraysVisits.DataSource = _VisitManger.GetAll()
                .Where(x => x.Xrays == true &&
                   x.EnterDate.Day == DateTime.Now.Day)
                .Select(x => new VisitsViewModel
                {
                    PationtName = x.PationtName,
                    SoldierNum = x.SoldierNum,
                    CaseDescription = x.CaseDescription,
                    clinicName = x.Clinic.Name,
                    DoctoreName = x.DoctoreName,
                    EnterDate = x.EnterDate,
                    Location = x.Location,
                    Xray = x.Xrays,
                    VisitId = x.VisitId,
                    XrayState = x.XraysState
                }).ToList();
        }

        private void Xrays_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void AllXraysVisits_SelectionChanged(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(AllXraysVisits.CurrentRow.Cells["VisitId"].Value);
            Visit model = new Visit();
            model = _VisitManger.GetBy(Id);
            FillClinic();
            Clinic.SelectedValue = model.clinic_Id;
            PationtName.Text = model.PationtName;

            SoldierNum.Text = model.SoldierNum.ToString();
            if (!CheackEmpty(model.SoldierNum))
            {
                SoldierNum.Text = model.SoldierNum.ToString();
            }
            else
            {
                SoldierNum.Text = string.Empty;
            }
            Location.Text = model.Location;
            CaseDescription.Text = model.CaseDescription;
            DoctoreName.Text = model.DoctoreName;
            DoctoreName.Enabled = false;
            PationtName.Enabled = false;
            SoldierNum.Enabled = false;
            Clinic.Enabled = false;
            Location.Enabled = false;
            CaseDescription.Enabled = false;

            byte[] byte_image = (byte[])model.XraysImage;

            if (byte_image != null)
            {
                MemoryStream ms = new MemoryStream(byte_image);
                XraysIamge.Image = Image.FromStream(ms);
            }
            else
            {
                XraysIamge.Image = null;
            }
        }

        private bool CheackEmpty(string text)
        {
            bool Cond = string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);
            return Cond;
        }

        private void FillClinic()
        {
            Clinic.DataSource = _ClinicManger.GetAll().ToList();
            Clinic.DisplayMember = "Name";
            Clinic.ValueMember = "Id";
            Clinic.SelectedValue = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenAndChoseImage();
        }

        private void OpenAndChoseImage()
        {
            OpenFileDialog open = new OpenFileDialog
            {
                Filter = "ملفات صور |*.JPG; *.PNG; *.GIF; *.BMP"
            };
            if (open.ShowDialog() == DialogResult.OK)
            {
                XraysIamge.Image = Image.FromFile(open.FileName);
                if (!(XraysIamge.Image == null))
                {
                    button3.Visible = true;
                    button3.Enabled = true;
                }
                XraysIamge.BackgroundImage = null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Visit model = _VisitManger.GetBy(Id);
            List<string> Info = new List<string>();
            byte[] byte_image;
            MemoryStream ms;
            Image img;
            ms = new MemoryStream();
            if (XraysIamge.Image != null)
            {
                img = XraysIamge.Image;
                XraysIamge.Image.Save(ms, img.RawFormat);
                byte_image = ms.ToArray();
            }
            else
            {
                byte_image = null;
                Info.Add("يرجي العلم بانه لم يتم ادراج صورة للاشعة هل انت متاكد من الاستمرار ؟");
            }

            model.XraysImage = byte_image;
            model.XraysState = true;


            if (Info.Count != 0)
            {
                var message = string.Join(Environment.NewLine, Info.ToArray());
                var Response = MessageBox
                      .Show(message, "تنبيه",
                      MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (Response == System.Windows.Forms.DialogResult.OK)
                {
                    MessageBox
                          .Show("نمت اضافة الاشعة بنجاح", "تنبيه",
                          MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    _VisitManger.Update(model);
                    Search(string.Empty);
                }
            }
            else
            {
                MessageBox
                          .Show("نمت اضافة الاشعة بنجاح", "تنبيه",
                          MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                _VisitManger.Update(model);
                Search(string.Empty);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            XraysIamge.Image = null;
        }


        private void Search(string text)
        {
            if (!(string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text)))
            {
                AllXraysVisits.DataSource = _VisitManger.GetAll()
               .Where(x =>
               x.Xrays == true && (
                   x.DoctoreName.Contains(text) ||
                   x.CaseDescription.Contains(text) ||
                   x.Location.Contains(text) ||
                   x.PationtName.Contains(text) ||
                   x.SoldierNum.Contains(text)
               ))
               .Select(x => new VisitsViewModel
               {
                   PationtName = x.PationtName,
                   SoldierNum = x.SoldierNum,
                   CaseDescription = x.CaseDescription,
                   clinicName = x.Clinic.Name,
                   DoctoreName = x.DoctoreName,
                   EnterDate = x.EnterDate,
                   Location = x.Location,
                   VisitId = x.VisitId,
                   XrayState = x.XraysState
               }).ToList();
            }
            else
            {
                FillDgVi();
            }
            RnmDgv();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Search(string.Empty);
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            Search(SearchBox.Text);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                AllXraysVisits.DataSource = _VisitManger.GetAll()
               .Where(x => (x.Xrays == true && x.XraysState == false) &&
                   x.EnterDate.Day == DateTime.Now.Day)
               .Select(x => new VisitsViewModel
               {
                   PationtName = x.PationtName,
                   SoldierNum = x.SoldierNum,
                   CaseDescription = x.CaseDescription,
                   clinicName = x.Clinic.Name,
                   DoctoreName = x.DoctoreName,
                   EnterDate = x.EnterDate,
                   Location = x.Location,
                   Xray = x.Xrays,
                   VisitId = x.VisitId,
                   XrayState = x.XraysState

               }).ToList();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                AllXraysVisits.DataSource = _VisitManger.GetAll()
               .Where(x => (x.Xrays == true && x.XraysState == true) &&
                   x.EnterDate.Day == DateTime.Now.Day)
               .Select(x => new VisitsViewModel
               {
                   PationtName = x.PationtName,
                   SoldierNum = x.SoldierNum,
                   CaseDescription = x.CaseDescription,
                   clinicName = x.Clinic.Name,
                   DoctoreName = x.DoctoreName,
                   EnterDate = x.EnterDate,
                   Location = x.Location,
                   Xray = x.Xrays,
                   VisitId = x.VisitId,
                   XrayState = x.XraysState
               }).ToList();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                AllXraysVisits.DataSource = _VisitManger.GetAll()
               .Where(x => (x.Xrays == true) &&
                   x.EnterDate.Day == DateTime.Now.Day)
               .Select(x => new VisitsViewModel
               {
                   PationtName = x.PationtName,
                   SoldierNum = x.SoldierNum,
                   CaseDescription = x.CaseDescription,
                   clinicName = x.Clinic.Name,
                   DoctoreName = x.DoctoreName,
                   EnterDate = x.EnterDate,
                   Location = x.Location,
                   Xray = x.Xrays,
                   VisitId = x.VisitId,
                   XrayState = x.XraysState
               }).ToList();
            }
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                AllXraysVisits.DataSource = _VisitManger.GetAll()
               .Where(x => (x.Xrays == true))
               .Select(x => new VisitsViewModel
               {
                   PationtName = x.PationtName,
                   SoldierNum = x.SoldierNum,
                   CaseDescription = x.CaseDescription,
                   clinicName = x.Clinic.Name,
                   DoctoreName = x.DoctoreName,
                   EnterDate = x.EnterDate,
                   Location = x.Location,
                   Xray = x.Xrays,
                   VisitId = x.VisitId,
                   XrayState = x.XraysState
               }).ToList();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            pictureBox1.Dispose();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            tableLayoutPanel1.Show();
            this.BackgroundImage = Properties.Resources.Slide1;

        }

        private void Xrays_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.BackgroundImage = null;

            tableLayoutPanel1.Hide();
        }


    }
}
