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

namespace TheHospital.UI.Printing
{
    public partial class Form1 : Form
    {
        private readonly VisitManger _VisitManger;
        private readonly Visit Visitor;
        public Form1(int ID)
        {
            _VisitManger = new VisitManger();
            Visitor = _VisitManger.GetBy(ID);
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label3.Text = Visitor.PationtName;
            label4.Text = Visitor.SoldierNum;
            label6.Text = Visitor.Location;
            textBox1.Text = Visitor.CaseDescription;
            label10.Text = Visitor.EnterDate.ToShortDateString()+" "+ Visitor.EnterDate.ToShortTimeString();
            label12.Text = Visitor.Xrays?"تمت الاشعة":"لا يوجد اشعة مطلوبة";
            label14.Text = Visitor.analyzes?"تمت التحاليل": "لا يوجد التحاليل مطلوبة";
            label16.Text = Visitor.DoctoreName;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(label1.Text, new Font("Tahoma", 20, FontStyle.Bold), Brushes.Black, 100, 10);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }
    }
}
