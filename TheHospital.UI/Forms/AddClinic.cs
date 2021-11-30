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
    public partial class AddClinic : Form
    {
        private readonly clinicMangers _ClinicManger;
        public AddClinic()
        {
            _ClinicManger = new clinicMangers();
            InitializeComponent();
            Search(string.Empty);
        }

        private void FillDgVi()
        {
            Allclinics.DataSource = _ClinicManger.GetAll()
                .Select(x => new ClinicViewModel
                {
                    Id=x.Id,
                    Name = x.Name,
                    Password = x.Password,
                }).ToList();
        }
        private void RnmDgv()
        {
            Allclinics.Columns["Id"].Visible = false;
            Allclinics.Columns["Name"].HeaderText = "الاسم";
            Allclinics.Columns["Password"].HeaderText = "الرقم السري";            
            Allclinics.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Allclinics.Columns["Password"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        bool CheackEmpty(string text)
        {
            bool Cond = string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);
            return Cond;
        }
        private void Submit_Click(object sender, EventArgs e)
        {
            try
            {
            List<string> Err = new List<string>();
                clinic model = new clinic()
                    {
                        Name = ClinicName.Text,
                        Password = Password.Text
                    };
                if (!CheackEmpty(ClinicName.Text))
                {
                    model.Name = ClinicName.Text;

                }
                else
                {
                    Err.Add("ادخل الاسم من فضلك");
                    ClinicName.BackColor = Color.Red;
                }
                if (!CheackEmpty(Password.Text))
                {
                    if (Password.Text!= ConfirmPass.Text)
                    {
                        Err.Add("كلمة المرور غير متطابقة");
                        ConfirmPass.BackColor = Color.Red;
                    }
                    else
                    {
                        model.Password = Password.Text;
                    }
                }
                else
                {
                    Err.Add("ادخل كلمة المرور من فضلك");
                    Password.BackColor = Color.Red;
                }
                if (Err.Count==0)
                {
                    List<clinic> MyList = _ClinicManger
                       .GetAll()
                       .Where(x => x.Name == model.Name)
                       .ToList();
                    if (MyList.Count == 0)
                    {
                       var retarnedValue = _ClinicManger.Add(model);
                        if (retarnedValue!=null)
                        {
                            MessageBox
                            .Show("تمت اضافة العيادة بنجاح", "تنبيه",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        Search(string.Empty);
                    }
                    else
                    {
                        MessageBox
                            .Show("هذه العيادة موجودة مسبقا", "تنبيه",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void AddClinic_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Search(string.Empty);
            }
            catch (Exception)
            {
                throw;
            }           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(SearchBox.Text);
        }

        private void Search(string text)
        {
            if (!(string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text)))
            {
                Allclinics.DataSource = _ClinicManger.GetAll()
                    .Where(x => x.Name.Contains(text))
                    .Select(x => new ClinicViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Password = x.Password,
                    }).ToList();
            }
            else
            {
                FillDgVi();
            }
            RnmDgv();
        }

        private void ClinicName_Enter(object sender, EventArgs e)
        {
            ClinicName.BackColor = Color.White;
        }

        private void ConfirmPass_TextChanged(object sender, EventArgs e)
        {
            if (Password.Text == ConfirmPass.Text)
            {
                ConfirmPass.BackColor = Color.White;
            }
            else
            {
                ConfirmPass.BackColor = Color.Red;
            }
        }

        private void Password_Enter(object sender, EventArgs e)
        {
            Password.BackColor = Color.White;
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

        private void AddClinic_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.BackgroundImage = null;

            tableLayoutPanel1.Hide();
        }
    }
}
