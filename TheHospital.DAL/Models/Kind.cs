using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHospital.DAL.Models
{
    public class Kind
    {
        public Kind()
        {
            Users = new HashSet<User>();
        }
        [Key]
        public int KindId { get; set; }
        public string KindName { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
