using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IconicFund.Web.Controllers
{
    public class DateConvertionController : Controller
    {
        //public JsonResult ConvertFromHigriToGregorian(string Date)
        //{
        //    //string Result = ConvertDateCalendar(DateTime.Parse(Date), "Hijri", "en-US");
        //  //  return Json(JsonConvert.SerializeObject(Result));
        //}

        public JsonResult ConvertFromGregorianToHijri(string Date)
        {
            string Result = string.Empty;
            return Json(JsonConvert.SerializeObject(Result));
        }

        //public string ConvertDateCalendar(DateTime DateConv, string Calendar, string DateLangCulture)
        //{
        //    System.Globalization.DateTimeFormatInfo DTFormat;
        //    DateLangCulture = DateLangCulture.ToLower();
        //    /// We can't have the hijri date writen in English. We will get a runtime error - LAITH - 11/13/2005 1:01:45 PM -

        //    if (Calendar == "Hijri" && DateLangCulture.StartsWith("en-"))
        //    {
        //        DateLangCulture = "ar-sa";
        //    }

        //    /// Set the date time format to the given culture - LAITH - 11/13/2005 1:04:22 PM -
        //    DTFormat = new System.Globalization.CultureInfo(DateLangCulture, false).DateTimeFormat;

        //    /// Set the calendar property of the date time format to the given calendar - LAITH - 11/13/2005 1:04:52 PM -
        //    switch (Calendar)
        //    {
        //        case "Hijri":
        //            DTFormat.Calendar = new System.Globalization.HijriCalendar();
        //            break;

        //        case "Gregorian":
        //            DTFormat.Calendar = new System.Globalization.GregorianCalendar();
        //            break;

        //        default:
        //            return "";
        //    }

        //    /// We format the date structure to whatever we want - LAITH - 11/13/2005 1:05:39 PM -
        //    DTFormat.ShortDatePattern = "dd/MM/yyyy";
        //    return (DateConv.Date.ToString("f", DTFormat));
        //}
    }
}
