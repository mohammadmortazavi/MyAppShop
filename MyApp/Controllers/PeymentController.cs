using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Models;
using MyApp.Classes;
using System.Web.Security;
using System.Globalization;
using System.Data.Entity;


namespace MyApp.Controllers
{
    public class PeymentController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        PersianCalendar PC = new PersianCalendar();
        string strTodey = "";
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.FirstOrDefault(u => u.Mobile == User.Identity.Name);

                if (user.IsActive)
                {
                    var factor = db.Factors.FirstOrDefault(f => f.UserId == user.Id && f.IsPay == false);

                    Random RND = new Random();
                    int OrderRand = RND.Next(10000000, 99999990);
                    int SumPrice = 0;

                    strTodey = PC.GetYear(DateTime.Now).ToString("0000") + "/" +
                             PC.GetMonth(DateTime.Now).ToString("00") + "/" +
                             PC.GetDayOfMonth(DateTime.Now).ToString("00");


                    if (factor != null)
                    {
                        var detail = db.FactorDetails.Where(d => d.factorId == factor.Id).ToList();
                        var setting = db.Settings.FirstOrDefault();

                        int TotalPrice = 0;
                        int TaxPercent = 0;
                        int ServicePrice = 0;



                        foreach (var item in detail)
                        {

                            TotalPrice = TotalPrice + (item.Tedad * item.Products.SalePrice);
                           
                        }

                        if (setting.TaxPercent != 0)
                        {
                            TaxPercent = TotalPrice * (setting.TaxPercent / 100);
                        }
                        else
                        {
                            TaxPercent = 0;
                        }

                        if (TotalPrice < setting.ServiceBetween)
                        {
                            ServicePrice = setting.ServicePric;
                        }
                        else
                        {
                            ServicePrice = 0;
                        }

                        SumPrice = TaxPercent + ServicePrice + TotalPrice;

                        factor.NumberFactor = OrderRand.ToString();
                        factor.CutPrice = ServicePrice;
                        factor.Tax = TaxPercent;
                        factor.SumItem = TotalPrice;
                        factor.SumPrice = SumPrice;
                        db.SaveChanges();
                    }

                    long orderID = OrderRand;
                    long priceAmount = SumPrice;
                    string additionalText = "تراکنش جدید"; // توضیحات شما برای این تراکنش

                    BankMellat bankmellat = new BankMellat();

                    string resultRequest = bankmellat.bpPayRequest(orderID, priceAmount, additionalText);
                    string[] StatusSendRequest = resultRequest.Split(',');

                    if (int.Parse(StatusSendRequest[0]) == (int)BankMellat.MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_ﺑﺎ_ﻣﻮﻓﻘﻴﺖ_اﻧﺠﺎم_ﺷﺪ)
                    {
                        return RedirectToAction("RedirectVPOS", "Payment", new { id = StatusSendRequest[1] });
                    }

                    TempData["Message"] = bankmellat.DesribtionStatusCode(int.Parse(StatusSendRequest[0].Replace("_", " ")));
                    return RedirectToAction("ShowError", "Payment");

                }
                else
                {
                    return RedirectToAction("Activate", "Account");
                }

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        public ActionResult ShowError()
        {
            return View();
        }
        public ActionResult RedirectVPOS(string id)
        {
            var user = db.Users.FirstOrDefault(u => u.Mobile == User.Identity.Name);

            var factor = db.Factors.FirstOrDefault(f => f.UserId == user.Id && f.IsPay == false);

            try
            {
                if (id == null)
                {
                    TempData["Message"] = "هیچ شماره پیگیری برای پرداخت از سمت بانک ارسال نشده است!";

                    return RedirectToAction("ShowError", "Payment");
                }
                else
                {
                    ViewBag.id = id;
                    return View(factor);
                }
            }
            catch (Exception error)
            {
                TempData["Message"] = error + "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید";

                return RedirectToAction("ShowError", "Payment");
            }
        }

        [HttpPost]
        public ActionResult BankCallback()
        {
            bool Run_bpReversalRequest = false;
            long saleReferenceId = -999;
            long saleOrderId = -999;
            string resultCode_bpPayRequest;

            BankMellat bankmellat = new BankMellat();

            try
            {
                saleReferenceId = long.Parse(Request.Params["SaleReferenceId"].ToString());
                saleOrderId = long.Parse(Request.Params["SaleOrderId"].ToString());
                resultCode_bpPayRequest = Request.Params["ResCode"].ToString();

                //Result Code
                string resultCode_bpinquiryRequest = "-9999";
                string resultCode_bpSettleRequest = "-9999";
                string resultCode_bpVerifyRequest = "-9999";

                if (int.Parse(resultCode_bpPayRequest) == (int)BankMellat.MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_ﺑﺎ_ﻣﻮﻓﻘﻴﺖ_اﻧﺠﺎم_ﺷﺪ)
                {
                    resultCode_bpVerifyRequest = bankmellat.VerifyRequest(saleOrderId, saleOrderId, saleReferenceId);

                    if (string.IsNullOrEmpty(resultCode_bpVerifyRequest))
                    {
                        resultCode_bpinquiryRequest = bankmellat.InquiryRequest(saleOrderId, saleOrderId, saleReferenceId);
                        if (int.Parse(resultCode_bpinquiryRequest) != (int)BankMellat.MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_ﺑﺎ_ﻣﻮﻓﻘﻴﺖ_اﻧﺠﺎم_ﺷﺪ)
                        {
                            //the transactrion faild
                            TempData["Message"] = bankmellat.DesribtionStatusCode(int.Parse(resultCode_bpinquiryRequest.Replace("_", " ")));
                            Run_bpReversalRequest = true;
                        }
                    }

                    if ((int.Parse(resultCode_bpVerifyRequest) == (int)BankMellat.MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_ﺑﺎ_ﻣﻮﻓﻘﻴﺖ_اﻧﺠﺎم_ﺷﺪ)
                        ||
                        (int.Parse(resultCode_bpinquiryRequest) == (int)BankMellat.MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_ﺑﺎ_ﻣﻮﻓﻘﻴﺖ_اﻧﺠﺎم_ﺷﺪ))
                    {
                        resultCode_bpSettleRequest = bankmellat.SettleRequest(saleOrderId, saleOrderId, saleReferenceId);
                        if ((int.Parse(resultCode_bpSettleRequest) == (int)BankMellat.MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_ﺑﺎ_ﻣﻮﻓﻘﻴﺖ_اﻧﺠﺎم_ﺷﺪ)
                            || (int.Parse(resultCode_bpSettleRequest) == (int)BankMellat.MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_Settle_ﺷﺪه_اﺳﺖ))
                        {
                            TempData["Message"] = "تراکنش شما با موفقیت انجام شد ";
                            TempData["Message"] += Environment.NewLine + " لطفا شماره پیگیری را یادداشت نمایید" + Environment.NewLine + saleReferenceId;
                        }
                        else
                        {
                            TempData["Message"] = bankmellat.DesribtionStatusCode(int.Parse(resultCode_bpSettleRequest.Replace("_", " ")));
                            Run_bpReversalRequest = true;
                        }

                        // Save information to Database...

                        var factor = db.Factors.FirstOrDefault(f => f.NumberFactor == saleOrderId.ToString());

                        if (factor != null)
                        {
                            var user = db.Users.Find(factor.UserId);

                            factor.IsPay = true;
                            factor.DatePay = strTodey;
                            factor.TimePay = System.DateTime.Now.ToShortTimeString();
                            factor.NumberControl = saleReferenceId.ToString();

                            db.SaveChanges();

                            SmsSender sms = new SmsSender();

                            sms.Send(user.Mobile, "صورتحساب جدید شما پرداخت شد. شماره پیگیری " + factor.NumberControl + "می باشد");

                            if (user != null)
                            {
                                System.Web.Security.FormsAuthentication.SetAuthCookie(user.Mobile, false);
                            }
                        }

                    }
                    else
                    {
                        TempData["Message"] = bankmellat.DesribtionStatusCode(int.Parse(resultCode_bpVerifyRequest.Replace("_", " ")));
                        Run_bpReversalRequest = true;
                    }
                }
                else
                {
                    TempData["Message"] = bankmellat.DesribtionStatusCode(int.Parse(resultCode_bpPayRequest)).Replace("_", " ");
                    Run_bpReversalRequest = true;
                }

                return RedirectToAction("ShowError", "Payment");
            }
            catch (Exception Error)
            {
                TempData["Message"] = "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید";
                // Save and send Error for admin user
                Run_bpReversalRequest = true;
                return RedirectToAction("ShowError", "Payment");
            }
            finally
            {
                if (Run_bpReversalRequest) //ReversalRequest
                {
                    if (saleOrderId != -999 && saleReferenceId != -999)
                        bankmellat.bpReversalRequest(saleOrderId, saleOrderId, saleReferenceId);
                    // Save information to Database...
                }
            }

        }
    }
}

