using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Add-Migration after any edit of dal
// Update-Database after any edit of dal

namespace TheHospital.DAL.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
