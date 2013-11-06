using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Timetable.Site.DataService;
using Timetable.Site.Controllers.Extends;

namespace Timetable.Site.Models.Branches
{
    [DataContract(IsReference = true)]
    public class SendModel
    {
        [DataMember(Name = "Id")]
        int _id;

        [DataMember(Name = "Name")]
        string _name;

        public SendModel() { }
        public SendModel(int Id, string Name)
        {
            this._id = Id;
            this._name = Name;
        }

        public SendModel(Branch t)
        {
            var cns = new CutNameService();
            this._id = t.Id;
            this._name = cns.Cut(t.Name);
        }

    }
}