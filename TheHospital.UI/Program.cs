using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheHospital.BAL.Mangers;
using TheHospital.DAL.Models;
using TheHospital.UI.Forms;

namespace TheHospital.UI
{
    static class Program
    {
        private readonly static DepartmentManger _departmentManger = new DepartmentManger();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new loging());
        }
      

    }

}
