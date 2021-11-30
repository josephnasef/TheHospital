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

namespace TheHospital.UI.Forms
{
    public partial class loging : Form
    {
        private readonly  DepartmentManger _DepartmentManger;
        private readonly  KindManger _KindManger;
        private readonly  UserManger _UserManger;
        public  loging()
        {
            _KindManger = new KindManger();
            _UserManger = new UserManger();
            _DepartmentManger = new DepartmentManger();
            setKind();
            setAdmin();
            setClinic();
            InitializeComponent();
            FillCompobox();
            
        }
        void setAdmin()
        {
            List<Department> s = _DepartmentManger.GetAll().Where(x => x.Name == "Admin" && x.Password == "1234").ToList();
            if (s.Count == 0)
            {
                Department model = new Department()
                {
                    Id = 1,
                    Name = "Admin",
                    Password = "1234"
                };
                _DepartmentManger.Add(model);
            }
            List<User> s2 = _UserManger.GetAll().Where(x => x.Name == "مشرف" && x.Password == "1234").ToList();
            if (s2.Count == 0)
            {
                Kind mykin = _KindManger.GetAll().FirstOrDefault(x=>x.KindName=="مشرف");
                User model = new User()
                {
                    UserId = 1,
                    Name = "مشرف",
                    Password = "1234",                    
                    Kind_Id = mykin.KindId
                };
                _UserManger.Add(model);
            }
        }

        void setClinic()
        {
            List<Department> s = _DepartmentManger.GetAll().Where(x => x.Name == "عيادة" && x.Password == "1234").ToList();
            if (s.Count == 0)
            {
                Department model = new Department()
                {
                    Id = 2,
                    Name = "عيادة",
                    Password = "1234"
                };
                _DepartmentManger.Add(model);
            }
        }
        void setKind()
        {
            List<Kind> s = _KindManger.GetAll().Where(x => x.KindName == "إشعة").ToList();
            if (s.Count == 0)
            {
                Kind model = new Kind()
                {
                    KindId = 1,
                    KindName = "إشعة"                    
                };
                _KindManger.Add(model);
            }
            List<Kind> s2 = _KindManger.GetAll().Where(x => x.KindName == "تحاليل").ToList();
            if (s2.Count == 0)
            {
                Kind model = new Kind()
                {
                    KindId = 2,
                    KindName = "تحاليل"
                };
                _KindManger.Add(model);
            }
            List<Kind> s3 = _KindManger.GetAll().Where(x => x.KindName == "عيادة").ToList();
            if (s3.Count == 0)
            {
                Kind model = new Kind()
                {
                    KindId = 3,
                    KindName = "عيادة"
                };
                _KindManger.Add(model);
            }
            List<Kind> s4 = _KindManger.GetAll().Where(x => x.KindName == "مشرف").ToList();
            if (s4.Count == 0)
            {
                Kind model = new Kind()
                {
                    KindId = 4,
                    KindName = "مشرف"
                };
                _KindManger.Add(model);
            }
            List<Kind> s5 = _KindManger.GetAll().Where(x => x.KindName == "مدخل").ToList();
            if (s5.Count == 0)
            {
                Kind model = new Kind()
                {
                    KindId = 5,
                    KindName = "مدخل"
                };
                _KindManger.Add(model);
            }
            List<Kind> s6 = _KindManger.GetAll().Where(x => x.KindName == "شئون المرضي").ToList();
            if (s6.Count == 0)
            {
                Kind model = new Kind()
                {
                    KindId = 6,
                    KindName = "شئون المرضي"
                };
                _KindManger.Add(model);
            }
        }

        void FillCompobox()
        {
            Departments.DataSource = _UserManger.GetAll().ToList();
            Departments.DisplayMember = "Name";
            Departments.ValueMember = "UserId";
            Departments.SelectedValue = -1;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!((string.IsNullOrEmpty(Departments.Text)||string.IsNullOrWhiteSpace(Departments.Text))||(string.IsNullOrEmpty(Password.Text) || string.IsNullOrWhiteSpace(Password.Text))))
            {
                User model = new User()
                {
                    UserId = Convert.ToInt32(Departments.SelectedValue.ToString()),
                    Name = Departments.Text,
                    Password = Password.Text
                };

                List<User> dd = _UserManger.GetAll().Where(x => x.UserId == model.UserId && x.Password == model.Password).ToList();
                if (dd.Count != 0)
                {
                    MainForm form = new MainForm(dd.First().UserId);
                    form.model.UserId = dd.First().UserId;
                    form.model.Name = dd.First().Name;
                    form.model.Password = dd.First().Password;
                    this.Hide();
                    form.Show();
                }
                else
                {
                    MessageBox
                       .Show("تاكد من اسم المستخدم و كلمة المرور", "تنبيه",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox
                       .Show("تاكد من اسم المستخدم و كلمة المرور", "تنبيه",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!((string.IsNullOrEmpty(Departments.Text) || string.IsNullOrWhiteSpace(Departments.Text)) || (string.IsNullOrEmpty(Password.Text) || string.IsNullOrWhiteSpace(Password.Text))))
                {
                    User model = new User()
                    {
                        UserId = Convert.ToInt32(Departments.SelectedValue.ToString()),
                        Name = Departments.Text,
                        Password = Password.Text
                    };

                    List<User> dd = _UserManger.GetAll().Where(x => x.UserId == model.UserId && x.Password == model.Password).ToList();
                    if (dd.Count != 0)
                    {
                        MainForm form = new MainForm(dd.First().UserId);
                        form.model.UserId = dd.First().UserId;
                        form.model.Name = dd.First().Name;
                        form.model.Password = dd.First().Password;
                        this.Hide();
                        form.Show();
                    }
                    else
                    {
                        MessageBox
                           .Show("تاكد من اسم المستخدم و كلمة المرور", "تنبيه",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox
                           .Show("تاكد من اسم المستخدم و كلمة المرور", "تنبيه",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
