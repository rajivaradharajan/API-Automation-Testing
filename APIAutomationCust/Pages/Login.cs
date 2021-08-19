using System;
using System.Collections.Generic;
using System.Text;

namespace APIAutomationCust
{
    class Login
    {

        public Login(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public string email { get; set; }
            public string password { get; set; }
        }


    }



