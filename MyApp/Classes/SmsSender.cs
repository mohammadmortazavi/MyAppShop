using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyApp.Classes;
using MyApp.Models;

namespace MyApp.Classes
{
    public class SmsSender
    {
        public void Send(string To, string Body)
        {
            MyAppSmsService.Send send = new MyAppSmsService.Send();
            long[] RecIdArray = null;
            byte[] RecStatusArray = null;

            string[] StrArray = new string[] { To.ToString() };
            int RectId = send.SendSms("Meeting2", "123456", "30007477", StrArray, Body, false, ref RecStatusArray, ref RecIdArray);
        }
    }
}