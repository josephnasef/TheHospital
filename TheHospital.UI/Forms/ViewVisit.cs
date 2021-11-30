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

namespace TheHospital.UI.Forms
{
    public partial class ViewVisit : Form
    {
        public int Id;
        private readonly clinicMangers _ClinicManger;
        private readonly VisitManger _VisitManger;
        public ViewVisit(int ID)
        {
            Id = ID;
            _ClinicManger = new clinicMangers();
            _VisitManger = new VisitManger();
            InitializeComponent();
            GetData();
        }

        private void GetData()
        {
            Visit model = _VisitManger.GetBy(Id);
            FillClinic();
            Clinic.SelectedValue = model.clinic_Id;
            PationtName.Text = model.PationtName;
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
            PationtName.Enabled = false;
            SoldierNum.Enabled = false;
            Location.Enabled = false;
            if (model.analyzes)
            {
                analyzes.Checked = true;
            }
            else
            {
                analyzes.Checked = false;
            }
            if (model.Xrays)
            {
                Xrays.Checked = true;
            }
            else
            {
                Xrays.Checked = false;
            }

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

            byte[] byte_image2 = (byte[])model.analyzesImage;

            if (byte_image2 != null)
            {
                MemoryStream ms2 = new MemoryStream(byte_image2);
                analyzesIamge.Image = Image.FromStream(ms2);
            }
            else
            {
                analyzesIamge.Image = null;
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> Err = new List<string>();
                Visit model = _VisitManger.GetBy(Id);
                if (model.analyzes != model.analyzesState)
                {
                    Err.Add("من فضلك انتظر انتهاء التحليل ");
                }
                if (model.Xrays != model.XraysState)
                {
                    Err.Add("من فضلك انتظر انتهاء الاشعة ");
                }
                if (Err.Count == 0)
                {
                    model.State = true;
                    model.CaseDescription = CaseDescription.Text;
                    model.DoctoreName = DoctoreName.Text;
                    model.clinic_Id = Convert.ToInt32(Clinic.SelectedValue);
                    _VisitManger.Update(model);
                    MessageBox
                            .Show("تم انهاء الكشف بنجاح ", "تنبيه",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var message = string.Join(Environment.NewLine, Err.ToArray());
                    MessageBox
                            .Show(message, "تنبيه",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception)
            {

                throw;
            }
           
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            //tableLayoutPanel1.Show();
            pictureBox1.Dispose();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            tableLayoutPanel1.Show();
            this.BackgroundImage = Properties.Resources.Slide1;

        }

        private void ViewVisit_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.BackgroundImage = null;

            tableLayoutPanel1.Hide();
        }

        private void Xrays_CheckedChanged(object sender, EventArgs e)
        {
            Visit model = _VisitManger.GetBy(Id);
            if (Xrays.Checked)
            {
                model.Xrays = true;
                _VisitManger.Update(model);
            }
            else
            {
                model.Xrays = false;
                _VisitManger.Update(model);
            }
        }

        private void analyzes_CheckedChanged(object sender, EventArgs e)
        {
            Visit model = _VisitManger.GetBy(Id);
            if (analyzes.Checked)
            {
                model.analyzes = true;
                _VisitManger.Update(model);
            }
            else
            {
                model.analyzes = false;
                _VisitManger.Update(model);
            }
        }
    }
}
