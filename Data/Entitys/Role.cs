using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Entitys
{
    public class Role : EntityBase
    {
        public string Name { get; set; }
        public string Permissions { get; set; }

        public virtual ICollection<UserRole> Users { get; set; }
    }
}
