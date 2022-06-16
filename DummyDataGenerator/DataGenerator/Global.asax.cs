using Bogus.Premium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace DataGenerator
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            License.LicenseTo = "modern requirements";
            License.LicenseKey = "ZOrx1ktlBne61VxQ29JSHMCC8ZZwSl15rIpQJP1QkdPZ9T9cQbWw05Hkj6748HPRnbyXsYkWBtLIJF9tio2BjcjMUFgj6YgMIv3IxZ6SulPVAO1OFtO+lIzmLLe20NvBGnP+SYzyCImkbMXoz9f7/qjfpOmWm/NVV8bvy3gJ04k=";
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
