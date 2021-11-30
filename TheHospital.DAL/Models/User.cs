using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHospital.DAL.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string KindName { get; set; }
        public int Kind_Id { get; set; }
        [ForeignKey("Kind_Id")]
        public Kind kind{ get; set; }
    }
}
