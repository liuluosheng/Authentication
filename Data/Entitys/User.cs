using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entitys
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string PassWord { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public virtual ICollection<UserRole> Roles { get; set; }
    }
}
