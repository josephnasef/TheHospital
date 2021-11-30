using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHospital.DAL.Models
{
     public  class Visit
    {
        [Key]
        public int VisitId { get; set; }
        public string PationtName { get; set; }
        public string SoldierNum { get; set; }
        public string Location { get; set; }
        public string CaseDescription { get; set; }
        public string DoctoreName { get; set; }
        public DateTime EnterDate { get; set; }

        [ForeignKey("clinic_Id")]
        public clinic Clinic { get; set; }
        public int clinic_Id { get; set; }

        public bool State { get; set; }
        public bool Xrays { get; set; }
        public bool XraysState { get; set; }
        public bool analyzes { get; set; }
        public bool analyzesState { get; set; }
        public byte[] XraysImage { get; set; }
        public byte[] analyzesImage { get; set; }

    }
}
