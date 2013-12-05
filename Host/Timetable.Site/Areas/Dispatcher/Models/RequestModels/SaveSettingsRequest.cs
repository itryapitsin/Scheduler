using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Timetable.Site.Areas.Dispatcher.Models.RequestModels
{
    public class SaveSettingsRequest
    {
        [Required]
        [MinLength(3)]
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        [Required]
        [MinLength(3)]
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public bool IsNeedPasswordUpdate()
        {
            return (Password == ConfirmPassword) && (!String.IsNullOrEmpty(Password)); 
        }
    }
}