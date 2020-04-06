using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace MediII.Adapter.ListenerRouting
{
    public  class AppSettingString
    {
        public static string SendingApp=ConfigurationManager.AppSettings["recvApp"].ToString();

        public static string RecvApp = ConfigurationManager.AppSettings["sendingApp"].ToString();


    }
}
