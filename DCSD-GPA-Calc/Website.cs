using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DCSD_GPA_Calc
{
    class Website
    {
        public static string GetWebsite(string url)
        {
            WebClient webClient = new WebClient();
            string page = webClient.DownloadString(url);           

            return page;
        }
    }
}
