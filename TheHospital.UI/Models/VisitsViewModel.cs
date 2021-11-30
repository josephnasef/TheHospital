using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHospital.UI.Models
{
    public class VisitsViewModel
    {
        public int VisitId { get; set; }
        public string PationtName { get; set; }
        public string SoldierNum { get; set; }
        public string Location { get; set; }
        public string CaseDescription { get; set; }
        public string DoctoreName { get; set; }
        public DateTime EnterDate { get; set; }
        public string clinicName { get; set; }
        public bool State { get; set; }
        public bool XrayState { get; set; }
        public bool Xray { get; set; }
        public bool analyzesState { get; set; }
        public bool analyzes { get; set; }
    }
}
