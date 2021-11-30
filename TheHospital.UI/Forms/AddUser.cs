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
    public partial class AddUser : Form
    {
        private readonly UserManger _UserManger;
        private readonly KindManger _KindManger;
        public AddUser()
        {
            _UserManger = new UserManger();
            _KindManger = new KindManger();
            InitializeComponent();
            fillCombo();
            Search(string.Empty);
        }

        private void RnmDgv()
        {
            AllUsers.Columns["UserId"].Visible = false;
            AllUsers.Columns["Name"].HeaderText = "الاسم";
            AllUsers.Columns["Password"].HeaderText = "الرقم السري";
            AllUsers.Columns["TypeName"].HeaderText = " النوع المسئولية";
            AllUsers.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllUsers.Columns["Password"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            AllUsers.Columns["TypeName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void FillDgVi()
        {
            AllUsers.DataSource = _UserManger.GetAll()
                .Select(x => new UserViewModel
                {
                    Name = x.Name,
                    Password = x.Password,
                    TypeName = x.kind.KindName,
                    UserId = x.UserId

                }).ToList();
        }

        private void fillCombo()
        {
            type.DataSource = _KindManger.GetAll().ToList();
            type.DisplayMember = "KindName";
            type.ValueMember = "KindId";
            type.SelectedValue = -1;
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
        
        bool CheackEmpty(string text)
        {
            bool Cond = string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);
            return Cond;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> Err = new List<string>();
                User model = new User();
                if (!CheackEmpty(UserName.Text))
                {
                    model.Name = UserName.Text;
                }
                else
                {
                    Err.Add("ادخل الاسم من فضلك");
                    UserName.BackColor = Color.Red;
                }
                if (!CheackEmpty(Password.Text))
                {
                    if (Password.Text != ConfirmPass.Text)
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
                }

                if (!(!(Convert.ToInt32(type.SelectedValue) > 0) || (type.SelectedValue == null)))
                {
                    model.Kind_Id = Convert.ToInt32(type.SelectedValue);

                }
                else
                {
                    Err.Add("اختار نوع المسئولية من فضللك");
                    type.BackColor = Color.Red;
                }
                if (Err.Count == 0)
                {
                   var returnedValue =  _UserManger.Add(model);
                    string KindName = _KindManger.GetBy(returnedValue.Kind_Id).KindName;
                    if (returnedValue!=null)
                    {
                        MessageBox
                           .Show(" تمت "+ " اضافة مستخدم جديد من نوع " +"'"+ KindName +"'" +" بنجاح ", "تنبيه",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    Search(string.Empty);
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

        private void button2_Click(object sender, EventArgs e)
        {
            Search(string.Empty);
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            Search(SearchBox.Text);
        }
        private void Search(string text)
        {
            if (!(string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text)))
            {
                AllUsers.DataSource = _UserManger.GetAll().Where(x => x.Name.Contains(text)).Select(x => new UserViewModel
                {
                    Name = x.Name,
                    Password = x.Password,
                    TypeName = x.kind.KindName,
                    UserId = x.UserId

                }).ToList();
            }
            else
            {
                FillDgVi();
            }
            RnmDgv();
        }

        private void UserName_Enter(object sender, EventArgs e)
        {
            UserName.BackColor = Color.White;
            Password.BackColor = Color.White;
            type.BackColor = Color.White;
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

        private void AddUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.BackgroundImage = null;

            tableLayoutPanel1.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
