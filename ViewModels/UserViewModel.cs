using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.ViewModels
{
    public class UserViewModel
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string FirebaseToken { get; set; }
    }
}
