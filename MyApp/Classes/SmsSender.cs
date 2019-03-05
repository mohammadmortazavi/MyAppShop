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
            DatabaseContext db = new DatabaseContext();
            var setting = db.Settings.FirstOrDefault();
            MyAppSmsService.Send send = new MyAppSmsService.Send();
            long[] RecIdArray = null;
            byte[] RecStatusArray = null;

            string[] StrArray = new string[] { To.ToString() };
            int RectId = send.SendSms(setting.UserSms, setting.PassSms, setting.SenderSms, StrArray, Body, false, ref RecStatusArray, ref RecIdArray);
        }
    }
}