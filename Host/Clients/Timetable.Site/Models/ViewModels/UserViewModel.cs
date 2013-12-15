using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timetable.Logic.Models;

namespace Timetable.Site.Models.ViewModels
{
    public class UserViewModel
    {
        public string Login { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }

        public UserViewModel(){ }

        public UserViewModel(UserDataTransfer x)
        {
            Login = x.Login;
            Firstname = x.Firstname;
            Middlename = x.Middlename;
            Lastname = x.Lastname;
        }
    }
}