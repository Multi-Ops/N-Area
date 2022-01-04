using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Helper
{
    public class CommonFunctions
    {
        public static DateTime ConvertDateTime(DateTime date, TimeSpan time)
        {

            String dateString = date.ToShortDateString() + " " + time;
            DateTime dt = Convert.ToDateTime(dateString);
            return dt;
        }

        public static DateTime ConvertDate(DateTime date)
        {
            TimeSpan time = new TimeSpan(12, 00, 00);
            String dateString = date.ToShortDateString() + " " + time;
            DateTime dt = Convert.ToDateTime(dateString);
            return dt;
        }

        public static DateTime DDMMYYYYToMMDDYYYYWithDash(String date, TimeSpan time)
        {

            string[] dateSplit = date.Split('-');
            string finalDate = dateSplit[2] + "-" + dateSplit[1] + "-" + dateSplit[0];
            string finalDateTime = finalDate + " " + time;
            DateTime dt = Convert.ToDateTime(finalDateTime);
            return dt;
        }

        public static DateTime DDMMYYYYToMMDDYYYY(String date, TimeSpan time)
        {
            string[] dateSplit = date.Split('/');
            string finalDate = dateSplit[1] + "/" + dateSplit[0] + "/" + dateSplit[2];
            string finalDateTime = finalDate + " " + time;
            DateTime dt = Convert.ToDateTime(finalDateTime);
            //DateTime dt = DateTime.ParseExact(finalDateTime, "MM/DD/YYYY HH:mm:ss", null);
            return dt;
        }


        public static DateTime AccordingToDataBase(String date)
        {
            string[] dateSplit = date.Split('-');
            string finalDate = dateSplit[2] + "-" + dateSplit[1] + "-" + dateSplit[0];
            string finalDateTime = finalDate;
            DateTime dt = Convert.ToDateTime(finalDateTime);
            return dt;
        }

        public static string MMDDYYYYToDDMMYYYY(String date)
        {
            string[] dateSplit = date.Split('/');
            string finalDate = dateSplit[1] + "/" + dateSplit[0] + "/" + dateSplit[2];
            return finalDate;
        }

        public static string ConvertToCreatedOnDate(String date)
        {
            if (date.Contains("/"))
            {
                string[] dateSplit = date.Split('/');
                string finalDate = dateSplit[0] + "-" + dateSplit[1] + "-" + dateSplit[2];
                return finalDate;
            }
            else
            {
                string[] dateSplit = date.Split('-');
                string finalDate = dateSplit[0] + "-" + dateSplit[1] + "-" + dateSplit[2];
                return finalDate;
            }
        }
    }
}
