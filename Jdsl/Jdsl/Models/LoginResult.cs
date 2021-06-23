using System;
using System.Collections.Generic;
using System.Text;

namespace Jdsl.Models
{
    class LoginResult
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public bool IsSuccesfull { get; set; }
    }
}
