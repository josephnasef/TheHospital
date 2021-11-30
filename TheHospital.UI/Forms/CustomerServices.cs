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
using TheHospital.UI.Printing;

namespace TheHospital.UI.Forms
{
    public partial class CustomerServices : Form
    {
        private readonly VisitManger _VisitManger;
        private readonly clinicMangers _clinicMangers;
        private int Id;
        public CustomerServices()
        {
            _clinicMangers = new clinicMangers();
            _VisitManger = new VisitManger();
            InitializeComponent();
            FillDgVi();
            RnmDgv();
        }
        private void CustomerServices_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
        private void FillDgVi()
        {
            AllDoneVisits.DataSource = _VisitManger.GetAll()
                .Where(x => x.State == true)
                .Select(x => new VisitsViewModel
                {
                    PationtName = x.PationtName,
                    SoldierNum = x.SoldierNum,
                    CaseDescription = x.CaseDescription,
                    clinicName = x.Clinic.Name,
                    DoctoreName = x.DoctoreName,
                    EnterDate = x.EnterDate,
                    Location = x.Location,
                    State = x.State,
                    VisitId = x.VisitId,
                    analyzes = x.analyzes,
                    analyzesState = x.analyzesState,
                    Xray = x.Xrays,
                    XrayState = x.XraysState
                }).ToList();
        }
        private void RnmDgv()
        {
            AllDoneVisits.Columns["VisitId"].Visible = false;
            AllDoneVisits.Columns["PationtName"].HeaderText = "الاسم";
            AllDoneVisits.Columns["SoldierNum"].HeaderText = "الرقم العسكري";
            AllDoneVisits.Columns["Location"].HeaderText = "الوحدة";
            AllDoneVisits.Columns["CaseDescription"].HeaderText = "وصف الحالة";
            AllDoneVisits.Columns["DoctoreName"].HeaderText = "الدكتور المشخص";
            AllDoneVisits.Columns["EnterDate"].HeaderText = "تاريخ الدخول";
            AllDoneVisits.Columns["clinicName"].HeaderText = "العيادة";
            AllDoneVisits.Columns["XrayState"].HeaderText = "تمت الاشعة";
            AllDoneVisits.Columns["analyzesState"].HeaderText = "تمت تحاليل";
            AllDoneVisits.Columns["Xray"].HeaderText = "مطلوب الاشعة";
            AllDoneVisits.Columns["analyzes"].HeaderText = "مطلوب تحاليل";
            AllDoneVisits.Columns["State"].Visible = false;
            AllDoneVisits.Columns["PationtName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllDoneVisits.Columns["SoldierNum"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllDoneVisits.Columns["Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllDoneVisits.Columns["CaseDescription"].Visible = false;
            AllDoneVisits.Columns["DoctoreName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllDoneVisits.Columns["EnterDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllDoneVisits.Columns["clinicName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void FillClinic2()
        {
            Clinic2.DataSource = _clinicMangers.GetAll().ToList();
            Clinic2.DisplayMember = "Name";
            Clinic2.ValueMember = "Id";
            //Clinic2.SelectedValue = -1;

        }
        private void Search(string text)
        {
            if (!(string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text)))
            {
                AllDoneVisits.DataSource = _VisitManger
                    .GetAll()
                    .Where(x => x.State == true && (
                        x.PationtName.Contains(text) ||
                        x.SoldierNum.Contains(text) ||
                        x.Location.Contains(text) ||
                        x.CaseDescription.Contains(text) ||
                        x.DoctoreName.Contains(text)
                    )).Select(x => new VisitsViewModel
                    {
                        PationtName = x.PationtName,
                        SoldierNum = x.SoldierNum,
                        CaseDescription = x.CaseDescription,
                        clinicName = x.Clinic.Name,
                        DoctoreName = x.DoctoreName,
                        EnterDate = x.EnterDate,
                        Location = x.Location,
                        State = x.State,
                        VisitId = x.VisitId,
                        analyzes = x.analyzes,
                        analyzesState = x.analyzesState,
                        Xray = x.Xrays,
                        XrayState = x.XraysState
                    }).ToList();
            }
            else
            {
                AllDoneVisits.DataSource = _VisitManger.GetAll().Select(x => new VisitsViewModel
                {
                    PationtName = x.PationtName,
                    SoldierNum = x.SoldierNum,
                    CaseDescription = x.CaseDescription,
                    clinicName = x.Clinic.Name,
                    DoctoreName = x.DoctoreName,
                    EnterDate = x.EnterDate,
                    Location = x.Location,
                    State = x.State,
                    VisitId = x.VisitId,
                    analyzes = x.analyzes,
                    analyzesState = x.analyzesState,
                    Xray = x.Xrays,
                    XrayState = x.XraysState
                }).ToList();
            }
            RnmDgv();
        }
        private void AllDoneVisits_SelectionChanged(object sender, EventArgs e)
        {
            if (AllDoneVisits.CurrentRow != null) {
                if (AllDoneVisits.CurrentRow.Index >= 0) {
                    if (AllDoneVisits.CurrentRow.Selected) {
                        Id = Convert.ToInt32(AllDoneVisits.CurrentRow.Cells["VisitId"].Value);
                        Visit model = new Visit();
                        model = _VisitManger.GetBy(Id);
                        FillClinic2();
                        Clinic2.SelectedValue = model.clinic_Id;
                        Clinic2.Enabled = false;
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
                        DoctoreName.Enabled = false;
                        PationtName.Enabled = false;
                        SoldierNum.Enabled = false;
                        Location.Enabled = false;
                        CaseDescription.Enabled = false;
                    }
                }
            }
        }

        private bool CheackEmpty(string text)
        {
            bool Cond = string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);
            return Cond;
        }

        private void Update_Click(object sender, EventArgs e)
        {
            FillDgVi();
            RnmDgv();
        }
        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            Search(SearchBox.Text);
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

        private void CustomerServices_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.BackgroundImage = null;

            tableLayoutPanel1.Hide();
        }

        private void Print_Click(object sender, EventArgs e)
        {
            Id = Convert.ToInt32(AllDoneVisits.CurrentRow.Cells["VisitId"].Value);
            Form1 form = new Form1(Id);
            
            form.Show();
        }
    }
}
