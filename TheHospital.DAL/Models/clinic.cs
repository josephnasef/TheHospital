using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHospital.DAL.Models
{
    public class clinic
    {
        public clinic()
        {
            Visits = new HashSet<Visit>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public ICollection<Visit> Visits { get; set; }
    }
}
