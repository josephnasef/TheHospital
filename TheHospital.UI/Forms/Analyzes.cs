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
    public partial class Analyzes : Form
    {
        private readonly clinicMangers _ClinicManger;
        private readonly VisitManger _VisitManger;
        private int Id;
        public Analyzes()
        {
            _ClinicManger = new clinicMangers();
            _VisitManger = new VisitManger();
            InitializeComponent();
            Search(string.Empty);
        }

        private void Analyzes_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

        }
        private void RnmDgv()
        {
            AllAnalyzesVisits.Columns["VisitId"].Visible = false;
            AllAnalyzesVisits.Columns["XrayState"].Visible = false;
            AllAnalyzesVisits.Columns["State"].Visible = false;
            AllAnalyzesVisits.Columns["Xray"].Visible = false;
            AllAnalyzesVisits.Columns["PationtName"].HeaderText = "الاسم";
            AllAnalyzesVisits.Columns["SoldierNum"].HeaderText = "الرقم العسكري";
            AllAnalyzesVisits.Columns["Location"].HeaderText = "الوحدة";
            AllAnalyzesVisits.Columns["CaseDescription"].HeaderText = "وصف الحالة";
            AllAnalyzesVisits.Columns["DoctoreName"].HeaderText = "الدكتور المشخص";
            AllAnalyzesVisits.Columns["EnterDate"].HeaderText = "تاريخ الدخول";
            AllAnalyzesVisits.Columns["clinicName"].HeaderText = "العيادة";
            AllAnalyzesVisits.Columns["analyzesState"].HeaderText = "تمت تحاليل";
            AllAnalyzesVisits.Columns["analyzesState"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllAnalyzesVisits.Columns["analyzes"].Visible = false;
            AllAnalyzesVisits.Columns["PationtName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllAnalyzesVisits.Columns["SoldierNum"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllAnalyzesVisits.Columns["Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllAnalyzesVisits.Columns["CaseDescription"].Visible = false;
            AllAnalyzesVisits.Columns["DoctoreName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllAnalyzesVisits.Columns["EnterDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllAnalyzesVisits.Columns["clinicName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void FillDgVi()
        {
            AllAnalyzesVisits.DataSource = _VisitManger.GetAll()
               .Where(x => x.analyzes == true &&
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
                   analyzes = x.analyzes,
                   VisitId = x.VisitId,
                   analyzesState = x.analyzesState
               }).ToList();
        }

        private void AllAnalyzesVisits_SelectionChanged(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(AllAnalyzesVisits.CurrentRow.Cells["VisitId"].Value);
            Visit model = new Visit();
            model = _VisitManger.GetBy(Id);
            FillClinic();
            Clinic.SelectedValue = model.clinic_Id;
            PationtName.Text = model.PationtName;
            SoldierNum.Text = model.SoldierNum.ToString();
            Location.Text = model.Location;
            CaseDescription.Text = model.CaseDescription;
            DoctoreName.Text = model.DoctoreName;
            DoctoreName.Enabled = false;
            PationtName.Enabled = false;
            SoldierNum.Enabled = false;
            Clinic.Enabled = false;
            Location.Enabled = false;
            CaseDescription.Enabled = false;

            byte[] byte_image = (byte[])model.analyzesImage;

            if (byte_image != null)
            {
                MemoryStream ms = new MemoryStream(byte_image);
                analyzesIamge.Image = Image.FromStream(ms);
            }
            else
            {
                analyzesIamge.Image = null;
            }
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
                analyzesIamge.Image = Image.FromFile(open.FileName);
                if (!(analyzesIamge.Image == null))
                {
                    button3.Visible = true;
                    button3.Enabled = true;
                }
                analyzesIamge.BackgroundImage = null;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            analyzesIamge.Image = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Visit model = _VisitManger.GetBy(Id);
                List<string> Info = new List<string>();
                byte[] byte_image;
                MemoryStream ms;
                Image img;
                ms = new MemoryStream();
                if (analyzesIamge.Image != null)
                {
                    img = analyzesIamge.Image;
                    analyzesIamge.Image.Save(ms, img.RawFormat);
                    byte_image = ms.ToArray();
                }
                else
                {
                    byte_image = null;
                    Info.Add("يرجي العلم بانه لم يتم ادراج صورة للتحليل هل انت متاكد من الاستمرار ؟");
                }
                model.analyzesImage = byte_image;
                model.analyzesState = true;
                if (Info.Count != 0)
                {
                    var message = string.Join(Environment.NewLine, Info.ToArray());
                    var Response = MessageBox
                          .Show(message, "تنبيه",
                          MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                    if (Response == System.Windows.Forms.DialogResult.OK)
                    {
                        MessageBox
                              .Show("نمت اضافة التحليل بنجاح", "تنبيه",
                              MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        _VisitManger.Update(model);
                        Search(string.Empty);
                    }
                }
                else
                {
                    MessageBox
                              .Show("نمت اضافة التحليل بنجاح", "تنبيه",
                              MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    _VisitManger.Update(model);
                    Search(string.Empty);
                }

                //Info.Add("نمت اضافة التحليل بنجاح");
                //var message = string.Join(Environment.NewLine, Info.ToArray());
                //var Response = MessageBox
                //          .Show(message, "تنبيه",
                //          MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                //if (Response == System.Windows.Forms.DialogResult.OK)
                //{
                //    _VisitManger.Update(model);
                //    Search(string.Empty);
                //}
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Search(string.Empty);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(SearchBox.Text);
        }
        private void Search(string text)
        {
            if (!(string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text)))
            {
                AllAnalyzesVisits.DataSource = _VisitManger.GetAll()
               .Where(x =>
               x.analyzes == true && (
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
                   analyzes = x.analyzes,
                   VisitId = x.VisitId,
                   analyzesState = x.analyzesState
               }).ToList();
            }
            else
            {
                FillDgVi();
            }
            RnmDgv();
        }


        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                AllAnalyzesVisits.DataSource = _VisitManger.GetAll()
               .Where(x => (x.analyzes == true) &&
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
                   analyzes = x.Xrays,
                   VisitId = x.VisitId,
                   analyzesState = x.analyzesState
               }).ToList();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                AllAnalyzesVisits.DataSource = _VisitManger.GetAll()
               .Where(x => (x.analyzes == true && x.analyzesState == true) &&
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
                   analyzes = x.Xrays,
                   VisitId = x.VisitId,
                   analyzesState = x.analyzesState
               }).ToList();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                AllAnalyzesVisits.DataSource = _VisitManger.GetAll()
               .Where(x => (x.analyzes == true && x.analyzesState == false) &&
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
                   analyzes = x.Xrays,
                   VisitId = x.VisitId,
                   analyzesState = x.analyzesState

               }).ToList();
            }
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                AllAnalyzesVisits.DataSource = _VisitManger.GetAll()
               .Where(x => x.analyzes == true)
               .Select(x => new VisitsViewModel
               {
                   PationtName = x.PationtName,
                   SoldierNum = x.SoldierNum,
                   CaseDescription = x.CaseDescription,
                   clinicName = x.Clinic.Name,
                   DoctoreName = x.DoctoreName,
                   EnterDate = x.EnterDate,
                   Location = x.Location,
                   analyzes = x.Xrays,
                   VisitId = x.VisitId,
                   analyzesState = x.analyzesState

               }).ToList();
            }
        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            timer1.Stop();
            //tableLayoutPanel1.Show();
            pictureBox1.Dispose();
        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            timer2.Stop();
            tableLayoutPanel1.Show();
            this.BackgroundImage = Properties.Resources.Slide1;

        }

        private void Analyzes_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.BackgroundImage = null;

            tableLayoutPanel1.Hide();
        }


    }
}
