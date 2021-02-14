using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace IconicFund.Helpers
{
    public static class ConvertDatetime
    {
        
         
        public static DateTime ConvertToGregorianDate(DateTime date)
        {

            DateTime _newDate = new DateTime(date.Year, date.Month, date.Day , new HijriCalendar());
            //var  DTFormat = new System.Globalization.CultureInfo("en-US", false);
            
            //DTFormat.DateTimeFormat.Calendar = new GregorianCalendar();


            //DTFormat.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy";
            //DTFormat.DateTimeFormat.LongDatePattern = "MM/dd/yyyy hh:mm:ss tt";

            //string x = _newDate.ToString(DTFormat.DateTimeFormat);
            //DateTime _result = DateTime.ParseExact(x, "MM/dd/yyyy", DTFormat);  
            return _newDate ;

        }
        public static DateTime ConvertToGre(DateTime date)
        {
            try
            {
                DateTime _newDate = new DateTime(date.Year, date.Month, date.Day, new HijriCalendar());

                //DateTime _newDate = new DateTime(date.Year, date.Month, date.Day, new GregorianCalendar());
                //string stdate = date.ToString("dd/MM/yyyy");
                //CultureInfo arSA = new CultureInfo("ar-SA");
                //arSA.DateTimeFormat.Calendar = new HijriCalendar();
                //var dateValue = DateTime.ParseExact(stdate, "dd/MM/yyyy", arSA);

                //  DateTime _newDate = new DateTime(date.Year, date.Month, date.Day, new HijriCalendar());
                //  var DTFormat = new System.Globalization.CultureInfo("en-US", false);

                //  DTFormat.DateTimeFormat.Calendar = new GregorianCalendar();


                //  DTFormat.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy";
                //  DTFormat.DateTimeFormat.LongDatePattern = "MM/dd/yyyy hh:mm:ss tt";

                ////  string x = _newDate.ToString(DTFormat.DateTimeFormat);
                //  DateTime _result = DateTime.ParseExact(date.ToString(), "MM/dd/yyyy", DTFormat);
                return _newDate;
            }
            catch(Exception e)
            {
                return new DateTime();
            }

        }


        //Added by Fahmy.
        public static string ConvertDateCalendar(DateTime DateConv, string Calendar, string DateLangCulture)
        {
            System.Globalization.DateTimeFormatInfo DTFormat;
            DateLangCulture = DateLangCulture.ToLower();
            /// We can't have the hijri date writen in English. We will get a runtime error - LAITH - 11/13/2005 1:01:45 PM -

            if (Calendar == "Hijri" && DateLangCulture.StartsWith("en-"))
            {
                DateLangCulture = "ar-sa";
            }

            /// Set the date time format to the given culture - LAITH - 11/13/2005 1:04:22 PM -
            DTFormat = new System.Globalization.CultureInfo(DateLangCulture, false).DateTimeFormat;

            /// Set the calendar property of the date time format to the given calendar - LAITH - 11/13/2005 1:04:52 PM -
            switch (Calendar)
            {
                case "Hijri":
                    DTFormat.Calendar = new System.Globalization.HijriCalendar();
                    break;

                case "Gregorian":
                    DTFormat.Calendar = new System.Globalization.GregorianCalendar();
                    break;

                default:
                    return "";
            }

            /// We format the date structure to whatever we want - LAITH - 11/13/2005 1:05:39 PM -
            DTFormat.ShortDatePattern = "yyyy/MM/dd";
            return (DateConv.Date.ToString(DTFormat));
        }

    }
}
