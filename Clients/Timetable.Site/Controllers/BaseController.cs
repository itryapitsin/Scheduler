using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Configuration;
using System.ServiceModel;
using Timetable.Site.DataService;
using Timetable.Site.UserService;

namespace Timetable.Site.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUserService UserService;
        protected readonly IDataService DataService;
        
        public BaseController()
        {
            UserService = GetService<IUserService>("UserServiceAddress");
            DataService = new DataServiceClient();
        }

        protected List<int> GetListFromString(string str)
        {
            var result = new List<int>();
            if (!String.IsNullOrEmpty(str))
                result = str.Split(',').Select(int.Parse).ToList();
            return result;
        }

        protected T GetService<T>(string serviceName)
        {
            
            var url = ConfigurationManager.AppSettings[serviceName];

            if (String.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");

            var myBinding = new BasicHttpBinding() { CloseTimeout = new TimeSpan(0, 0, 10, 0) };

            myBinding.MaxReceivedMessageSize = 2147483647;
            myBinding.MaxBufferSize = 2147483647;

            var myEndpoint = new EndpointAddress(url);
            var myChannelFactory = new ChannelFactory<T>(myBinding, myEndpoint);
            var host = myChannelFactory.CreateChannel();

            return host;
        }
    }
}
