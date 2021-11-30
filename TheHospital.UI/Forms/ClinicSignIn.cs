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
    public partial class ClinicSignIn : Form
    {
        private readonly clinicMangers _ClinicManger;
        private readonly VisitManger _VisitManger;
        private int Id;
        private int RowIndex;
        public ClinicSignIn()
        {
            _ClinicManger = new clinicMangers();
            _VisitManger = new VisitManger();
            InitializeComponent();
            FillClinic();
            timer3.Stop();
        }

        private void ClinicSignIn_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        void FillClinic()
        {
            Clinic.DataSource = _ClinicManger.GetAll().ToList();
            Clinic.DisplayMember = "Name";
            Clinic.ValueMember = "Id";
            Clinic.SelectedValue = -1;

        }
        void FillClinic2()
        {
            Clinic2.DataSource = _ClinicManger.GetAll().ToList();
            Clinic2.DisplayMember = "Name";
            Clinic2.ValueMember = "Id";

        }
        private void FillDgVi(int Id)
        {
            if (RowIndex < 0)
            {

                dataGridView1.DataSource = _VisitManger.GetAll()
                  .Where(x => x.clinic_Id == Id &&
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
                      State = x.State,
                      XrayState = x.XraysState,
                      Xray = x.Xrays,
                      VisitId = x.VisitId,
                      analyzes = x.analyzes,
                      analyzesState = x.analyzesState
                  }).ToList();
            }
           
            else
            {                
                dataGridView1.DataSource = _VisitManger.GetAll()
                  .Where(x => x.clinic_Id == Id &&
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
                      State = x.State,
                      XrayState = x.XraysState,
                      Xray = x.Xrays,
                      VisitId = x.VisitId,
                      analyzes = x.analyzes,
                      analyzesState = x.analyzesState
                  }).ToList();
                dataGridView1.Rows[RowIndex].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.SelectedRows[0].Index;
            }
           
        }
        void RnmDgv()
        {
            dataGridView1.Columns["VisitId"].Visible = false;
            dataGridView1.Columns["XrayState"].HeaderText = "إتمام الاشعة";
            dataGridView1.Columns["Xray"].HeaderText = "الاشعة";
            dataGridView1.Columns["analyzesState"].HeaderText = "إتمام التحاليل";
            dataGridView1.Columns["analyzes"].HeaderText = "التحاليل";
            dataGridView1.Columns["VisitId"].Visible = false;
            dataGridView1.Columns["VisitId"].Visible = false;
            dataGridView1.Columns["PationtName"].HeaderText = "الاسم";
            dataGridView1.Columns["SoldierNum"].HeaderText = "الرقم العسكري";
            dataGridView1.Columns["Location"].HeaderText = "الوحدة";
            dataGridView1.Columns["CaseDescription"].HeaderText = "وصف الحالة";
            dataGridView1.Columns["DoctoreName"].HeaderText = "الدكتور المشخص";
            dataGridView1.Columns["EnterDate"].HeaderText = "تاريخ الدخول";
            dataGridView1.Columns["clinicName"].HeaderText = "العيادة";
            dataGridView1.Columns["State"].Visible = false;
            dataGridView1.Columns["PationtName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["SoldierNum"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Location"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["CaseDescription"].Visible = false;
            dataGridView1.Columns["DoctoreName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["EnterDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["clinicName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        bool CheackEmpty(string text)
        {
            bool Cond = string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);
            return Cond;
        } 
        private void ResetData()
        {
            Clinic2.SelectedValue = -1;
            PationtName.Text = null;
            SoldierNum.Text = null;
            Location.Text = null;
            CaseDescription.Text = null;
            DoctoreName.Text = null;
            PationtName.Enabled = true;
            SoldierNum.Enabled = true;
            Location.Enabled = true;
            analyzes.Checked = false;
            Xrays.Checked = false;
            Xrays.Enabled = false;
            analyzes.Enabled = false;
        }
        private void Search(string text)
        {
            if (!(string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text)))
            {
                Id = Convert.ToInt32(Clinic.SelectedValue);
                dataGridView1.DataSource = _VisitManger.GetAll()
                     .Where(x => x.clinic_Id == Id && (
                         x.DoctoreName.Contains(text) ||
                         x.CaseDescription.Contains(text) ||
                         x.Location.Contains(text) ||
                         x.PationtName.Contains(text) ||
                         x.SoldierNum.Contains(text)))
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
                        XrayState = x.XraysState,
                        Xray = x.Xrays,
                        VisitId = x.VisitId,
                        analyzes = x.analyzes,
                        analyzesState = x.analyzesState
                    }).ToList();
            }
            else
            {
                Id = Convert.ToInt32(Clinic.SelectedValue);
                FillDgVi(Id);
            }
            RnmDgv();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                clinic model = new clinic()
                {
                    Id = Convert.ToInt32(Clinic.SelectedValue),
                    Name = Clinic.Text,
                    Password = Password.Text
                };
                Id = model.Id;

                List<clinic> lsitt = _ClinicManger.GetAll()
                    .Where(x => x.Id == model.Id && x.Name == model.Name && x.Password == model.Password)
                    .ToList();

                if (!(lsitt.Count == 0))
                {
                    this.pictureBox3.Show();
                    timer3.Start();

                    tableLayoutPanel1.Hide();
                    tableLayoutPanel2.Show();
                    this.pictureBox3.BringToFront();

                    label12.Text = "  عيادة : " + model.Name;
                    var dd = _VisitManger.GetAll().Where(x => x.clinic_Id == Id && x.EnterDate.Day == DateTime.Now.Day).ToList();
                    if (dd.Count == 0)
                    {
                        dataGridView1.DataSource = null;
                    }
                    else
                    {
                        Search(string.Empty);
                        dataGridView1.ClearSelection();
                    }
                }
                else
                {
                    MessageBox
                            .Show("خطاء في كلمة المرور برجاء التاكد من الكلمة المكتوبة", "تنبيه",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridView1.CurrentRow.Index > 0)
            {
                if (e.KeyCode == Keys.Up)
                {
                    RowIndex = dataGridView1.CurrentRow.Index - 1;
                    dataGridView1.Rows[RowIndex].Selected = true;
                    if (dataGridView1.Rows[RowIndex] != null)
                    {
                        Xrays.Enabled = true;
                        analyzes.Enabled = true;
                        if (dataGridView1.Rows[RowIndex].Index >= 0)
                        {
                            if (dataGridView1.Rows[RowIndex].Selected)
                            {
                                Id = Convert.ToInt32(dataGridView1.Rows[RowIndex].Cells["VisitId"].Value);

                                Visit model = new Visit();
                                model = _VisitManger.GetBy(Id);
                                FillClinic2();

                                if (model != null)
                                {
                                    Clinic2.SelectedValue = model.clinic_Id;
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

                                }
                            }
                        }
                    }                    
                }
            }

            if (e.KeyCode == Keys.Down)
            {
                RowIndex = dataGridView1.CurrentRow.Index + 1;
                if (RowIndex< dataGridView1.Rows.Count)
                {
                    
                    dataGridView1.Rows[RowIndex].Selected = true;
                    if (dataGridView1.Rows[RowIndex] != null)
                    {
                        Xrays.Enabled = true;
                        analyzes.Enabled = true;
                        if (dataGridView1.Rows[RowIndex].Index >= 0)
                        {
                            if (dataGridView1.Rows[RowIndex].Selected)
                            {
                                Id = Convert.ToInt32(dataGridView1.Rows[RowIndex].Cells["VisitId"].Value);

                                Visit model = new Visit();
                                model = _VisitManger.GetBy(Id);
                                FillClinic2();

                                if (model != null)
                                {
                                    Clinic2.SelectedValue = model.clinic_Id;
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
                                    if (model.State)
                                    {
                                        Done.Enabled = false;
                                        button4.Enabled = false;
                                    }
                                    else
                                    {
                                        Done.Enabled = true;
                                        button4.Enabled = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }       
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            RowIndex = dataGridView1.CurrentRow.Index;
            if (dataGridView1.CurrentRow != null)
            {
                Xrays.Enabled = true;
                analyzes.Enabled = true;
                if (dataGridView1.CurrentRow.Index >= 0)
                {
                    if (dataGridView1.CurrentRow.Selected)
                    {
                        Id = Convert.ToInt32(dataGridView1.Rows[RowIndex].Cells["VisitId"].Value);

                        Visit model = new Visit();
                        model = _VisitManger.GetBy(Id);
                        FillClinic2();

                        if (model != null)
                        {
                            Clinic2.SelectedValue = model.clinic_Id;
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

                            if (model.State)
                            {
                                Done.Enabled = false;
                                button4.Enabled = false;
                            }
                            else
                            {
                                Done.Enabled = true;
                                button4.Enabled = true;
                            }
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Visit model = _VisitManger.GetBy(Id);
            model.clinic_Id = Convert.ToInt32(Clinic2.SelectedValue.ToString());
            model.CaseDescription = CaseDescription.Text;
            model.DoctoreName = DoctoreName.Text;
            _VisitManger.Update(model);
        }
        private void Xrays_MouseClick(object sender, MouseEventArgs e)
        {
            Visit model = _VisitManger.GetBy(Id);
            if (!CheackEmpty(DoctoreName.Text))
            {
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
            else
            {
                Xrays.Checked = false;
                MessageBox
                       .Show("ادخل اسم الطبيب من فضلك", "تنبيه",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void analyzes_MouseClick(object sender, MouseEventArgs e)
        {
            Visit model = _VisitManger.GetBy(Id);
            if (!CheackEmpty(DoctoreName.Text))
            {
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

            else
            {
                analyzes.Checked = false;

                MessageBox
                       .Show("ادخل اسم الطبيب من فضلك", "تنبيه",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            ViewVisit form = new ViewVisit(Convert.ToInt32(dataGridView1.CurrentRow.Cells["VisitId"].Value));
            form.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {           
            Search(string.Empty);
        }
        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            Search(SearchBox.Text);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Visit model = _VisitManger.GetBy(Id);
                List<string>Info = new List<string>();
                if (!CheackEmpty(DoctoreName.Text))
                {
                    model.CaseDescription = CaseDescription.Text;
                    model.DoctoreName = DoctoreName.Text;
                    model.clinic_Id = Convert.ToInt32(Clinic.SelectedValue);
                    bool done= _VisitManger.Update(model);
                    if (done)
                    {
                        Info.Add("تم الحفظ بنجاح");
                        //1
                        if (model.analyzes==true && model.Xrays == true)
                        {
                            if (model.Xrays == model.XraysState && model.analyzes == model.analyzesState)
                            {
                                Info.Add(" يرجي العلم بانه  يمكنك الان إنهاء الكشف");

                            }
                            if (model.analyzes != model.analyzesState)
                            {
                                Info.Add("يرجي العلم بانه يوجد نتائج للتحاليل لم تاتي بعد");
                            }
                            if (model.Xrays != model.XraysState)
                            {
                                Info.Add("يرجي العلم بانه يوجد نتائج للاشعة لم تاتي بعد");
                            }
                            
                        }
                        //2
                        if (model.analyzes == true && model.Xrays == false)
                        {
                            if (model.analyzes == model.analyzesState)
                            {
                                Info.Add(" يرجي العلم بانه  يمكنك الان إنهاء الكشف");

                            }
                            else
                            {
                                Info.Add("يرجي العلم بانه يوجد نتائج للتحاليل لم تاتي بعد");
                            }
                        }
                        //3
                        if (model.analyzes == false && model.Xrays == true)
                        {
                            if (model.Xrays == model.XraysState)
                            {
                                Info.Add(" يرجي العلم بانه  يمكنك الان إنهاء الكشف");

                            }
                            else
                            {
                                Info.Add("يرجي العلم بانه يوجد نتائج الاشعة لم تاتي بعد");
                            }
                        }

                        var message = string.Join(Environment.NewLine, Info.ToArray());
                        MessageBox
                                .Show(message, "تنبيه",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox
                           .Show("ادخل اسم الطبيب من فضلك", "تنبيه",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            catch (Exception)
            {

                throw;
            }

        }
        private void Done_Click(object sender, EventArgs e)
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
                    if (model.State)
                    {
                        Done.Enabled = false;
                        button4.Enabled = false;
                    }
                    else
                    {
                        Done.Enabled = true;
                        button4.Enabled = true;
                    }
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
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton1.Checked) {
                Id = Convert.ToInt32(Clinic.SelectedValue);
                dataGridView1.DataSource = _VisitManger.GetAll()
                   .Where(x => (x.clinic_Id == Id && x.State==false) &&
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
                      State = x.State,
                      XrayState = x.XraysState,
                      Xray = x.Xrays,
                      VisitId = x.VisitId,
                      analyzes = x.analyzes,
                      analyzesState = x.analyzesState
                  }).ToList();
                ResetData();
                dataGridView1.ClearSelection();
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) {
                Id = Convert.ToInt32(Clinic.SelectedValue);
                dataGridView1.DataSource = _VisitManger.GetAll()
                   .Where(x => (x.clinic_Id == Id && x.State == true) &&
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
                      State = x.State,
                      XrayState = x.XraysState,
                      Xray = x.Xrays,
                      VisitId = x.VisitId,
                      analyzes = x.analyzes,
                      analyzesState = x.analyzesState
                  }).ToList();
                ResetData();
                dataGridView1.ClearSelection();
            }
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked) {
                Id = Convert.ToInt32(Clinic.SelectedValue);
                dataGridView1.DataSource = _VisitManger.GetAll()
                   .Where(x => x.clinic_Id == Id &&
                   x.EnterDate.Day==DateTime.Now.Day)
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
                       XrayState = x.XraysState,
                       Xray = x.Xrays,
                       VisitId = x.VisitId,
                       analyzes = x.analyzes,
                       analyzesState = x.analyzesState
                   }).ToList();
                ResetData();
                dataGridView1.ClearSelection();
                
            }
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                Id = Convert.ToInt32(Clinic.SelectedValue);
                dataGridView1.DataSource = _VisitManger.GetAll()
                   .Where(x => (x.clinic_Id == Id))
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
                       XrayState = x.XraysState,
                       Xray = x.Xrays,
                       VisitId = x.VisitId,
                       analyzes = x.analyzes,
                       analyzesState = x.analyzesState
                   }).ToList();
                ResetData();
                dataGridView1.ClearSelection();
            }
        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            timer1.Stop();
            pictureBox2.Dispose();
        }
        private void timer2_Tick_1(object sender, EventArgs e)
        {
            timer2.Stop();
            tableLayoutPanel3.Show();
            this.BackgroundImage = Properties.Resources.Slide1;

        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Stop();
            pictureBox3.Dispose();
        }
        private void ClinicSignIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.BackgroundImage = null;

            tableLayoutPanel3.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {

                Visit model = _VisitManger.GetBy(Id);
                model.State = false;
                _VisitManger.Update(model);

                if (model.State)
                {
                    Done.Enabled = false;
                    button4.Enabled = false;
                }
                else
                {
                    Done.Enabled = true;
                    button4.Enabled = true;
                }

            }

            catch (Exception)
            {
                throw;
            }
        }


    }
}
