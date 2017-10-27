using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Models
{
    public class UserLogin
    {
        [DisplayName("用户名")]
        public string Name { get; set; }
        [DisplayName("密码")]
        public string PassWord { get; set; }
        [DisplayName("记住密码")]
        public bool IsPersistent { get; set; }
    }
}
