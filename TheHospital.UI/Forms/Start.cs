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
    public partial class Start : Form
    {

        public Department model ;
        private readonly clinicMangers _ClinicManger;
        private readonly VisitManger _VisitManger;
        private readonly DepartmentManger _DepartmentManger;

        public Start(int Id)
        {
            _DepartmentManger = new DepartmentManger();
            var s= _DepartmentManger.GetBy(Id);
            model = new Department()
            {
                Id =Id,
                Password=s.Password,
                Name=s.Name
            };
            InitializeComponent();
            _ClinicManger = new clinicMangers();
            _VisitManger = new VisitManger();
            FillClinic();
            FillDgVi();
            RnmDgv();
            //clinic model = new clinic()
            //{
            //    Name = "Hello"
            //};
            //_ClinicManger.Add(model);
        }
        public Start()
        {
            InitializeComponent();
            _ClinicManger = new clinicMangers();
            _VisitManger = new VisitManger();
            FillClinic();
            FillDgVi();
            RnmDgv();           
        }

        bool CheackEmpty(string text)
        {
            bool Cond = string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);
            return Cond;
        }

        private void submit_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> Err = new List<string>();
                Visit visit = new Visit()
                {
                    EnterDate = DateTime.Now,
                };
                if (!CheackEmpty(SoldierName.Text))
                {
                    visit.PationtName = SoldierName.Text;
                }
                else
                {
                    Err.Add("ادخل الاسم من فضلك");
                }
                if (!CheackEmpty(SoldierLocation.Text))
                {
                    visit.Location = SoldierLocation.Text;
                }
                else
                {
                    Err.Add("ادخل اسم الوحدة من فضلك");
                }
                if (!CheackEmpty(SoldierNum.Text))
                {
                    visit.SoldierNum = SoldierNum.Text;
                }
                else
                {
                    Err.Add("ادخل الرقم العسكري من فضلك");
                }
                if (!(!(Convert.ToInt32(Clinic.SelectedValue) > 0) || (Clinic.SelectedValue == null)))
                {
                    visit.clinic_Id = Convert.ToInt32(Clinic.SelectedValue);
                }
                else
                {
                    Err.Add("اختار العيادة من فضللك");
                }
                if (Err.Count == 0)
                {
                    _VisitManger.Add(visit);
                    MessageBox
                        .Show("نمت اضافة زيارة جديدة بنجاح", "تنبيه",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var message = string.Join(Environment.NewLine, Err.ToArray());
                    MessageBox
                            .Show(message, "تنبيه",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                FillDgVi();
                RnmDgv();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void SoldierNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        void FillClinic()
        {
                Clinic.DataSource = _ClinicManger.GetAll().ToList();
                Clinic.DisplayMember = "Name";
                Clinic.ValueMember = "Id";
        }

        void FillDgVi()
        {
            AllVisits.DataSource = _VisitManger.GetAll()                
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
        void RnmDgv()
        {
            AllVisits.Columns["VisitId"].Visible = false;
            
            AllVisits.Columns["PationtName"].HeaderText = "الاسم";
            AllVisits.Columns["SoldierNum"].HeaderText = "الرقم العسكري";
            AllVisits.Columns["Location"].HeaderText = "الوحدة";
            AllVisits.Columns["CaseDescription"].HeaderText = "وصف الحالة";
            AllVisits.Columns["DoctoreName"].HeaderText = "الدكتور المشخص";
            AllVisits.Columns["EnterDate"].HeaderText = "تاريخ الدخول";
            AllVisits.Columns["clinicName"].HeaderText = "العيادة";
            AllVisits.Columns["XrayState"].HeaderText = "تمت الاشعة";
            AllVisits.Columns["analyzesState"].HeaderText = "تمت تحاليل";
            AllVisits.Columns["Xray"].HeaderText = "مطلوب الاشعة";
            AllVisits.Columns["analyzes"].HeaderText = "مطلوب تحاليل";
            AllVisits.Columns["State"].Visible = false;
            AllVisits.Columns["PationtName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllVisits.Columns["SoldierNum"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllVisits.Columns["Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllVisits.Columns["CaseDescription"].Visible = false;
            AllVisits.Columns["DoctoreName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllVisits.Columns["EnterDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllVisits.Columns["clinicName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void Start_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillDgVi();
            RnmDgv();
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            Search(SearchBox.Text);
        }

        private void Search(string text)
        {
            if (!(string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text)))
            {
                AllVisits.DataSource = _VisitManger.GetAll()
               .Where(x =>
                
                   x.DoctoreName.Contains(text) ||
                   x.CaseDescription.Contains(text) ||
                   x.Location.Contains(text) ||
                   x.PationtName.Contains(text) ||
                   x.SoldierNum.Contains(text)
               )
               .Select(x => new VisitsViewModel
               {
                   PationtName = x.PationtName,
                   SoldierNum = x.SoldierNum,
                   CaseDescription = x.CaseDescription,
                   clinicName = x.Clinic.Name,
                   DoctoreName = x.DoctoreName,
                   EnterDate = x.EnterDate,
                   Location = x.Location,
                   State = x.analyzes,
                   VisitId = x.VisitId,
                   analyzes=x.analyzes,
                   analyzesState=x.analyzesState,
                   Xray=x.Xrays,
                   XrayState =x.XraysState

               }).ToList();
            }
            else
            {
                AllVisits.DataSource = _VisitManger
                    .GetAll()                     
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
                        analyzesState = x.analyzesState,
                        Xray = x.Xrays,
                        XrayState = x.XraysState
                    })
                    .ToList();
            }
            RnmDgv();
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

        private void Start_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.BackgroundImage = null;

            tableLayoutPanel1.Hide();
        }
    }
}
