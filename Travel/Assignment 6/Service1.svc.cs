using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace Assignment_6
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {


        /*
         * -----------------------------------------------------------------------
         * return names of restaurants around a specific longtitude and latitude 
         * -----------------------------------------------------------------------
         */

        public string[] aroundYou(double lat, double lon)
        {

            string Key = "f4500873cfbb1db93f2741592f4ed5e1";
            double Long = lon;
            double Lati = lat;

            Web2String.ServiceClient client = new Web2String.ServiceClient();
            string text = client.GetWebContent("https://developers.zomato.com/api/v2.1/geocode?lat=" + Lati + "&lon=" + Long + "&apikey=" + Key);
            int ind = size(text);

            string[] names = info(text);
            return names;
        }
        public string[] info(string text)
        {
            string[] names = new String[size(text)];
            int[] nums = end(text);
            int i = 0;
            foreach (Match extract in Regex.Matches(text, "\"name\":\"?"))
            {
                int z = (extract.Index + 7);
                int x = nums[i] - z;
                names[i] = text.Substring(extract.Index + 8, x);
                i++;
            }
            return names;
        }
        public int size(string text)
        {
            int n = 0;
            foreach (Match extract in Regex.Matches(text, "\",\"url\"?"))
            {
                n++;
            }
            return n;
        }

        public int[] end(string text)
        {
            int n = size(text);

            int[] nums = new int[n];
            int i = 0;

            foreach (Match extract in Regex.Matches(text, "\",\"url\"?"))
            {
                nums[i] = extract.Index - 1;
                i++;
            }
            return nums;
        }

        /*
         * ----------------------------------------------------
         * distance between 2 zip codes 
         * ----------------------------------------------------
         */

        public double distance(string zip1, string zip2)
        {
            Web2String.ServiceClient client = new Web2String.ServiceClient();
            string code1 = client.GetWebContent("https://api.zip-codes.com/ZipCodesAPI.svc/1.0/QuickGetZipCodeDetails/" + zip1 + "?key=7BCM9XC85J68W7HIFO79");
            string code2 = client.GetWebContent("https://api.zip-codes.com/ZipCodesAPI.svc/1.0/QuickGetZipCodeDetails/" + zip2 + "?key=7BCM9XC85J68W7HIFO79");

            double lat1 = infoLat(code1);
            double lon1 = infoLon(code1);
            double lat2 = infoLat(code2);
            double lon2 = infoLon(code2
                );

            double dis = calculation(lat1, lon1, lat2, lon2);
            return dis;
        }

        public double toRad(double degree)
        {
            return degree * (Math.PI / 180);
        }

        public  double calculation(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371; // radius of earth in KM
            double dLati = toRad(lat2 - lat1);
            double dLong = toRad(lon2 - lon1);
            double a = Math.Sin(dLati / 2) * Math.Sin(dLati / 2) + Math.Cos(toRad(lat1)) * Math.Cos(toRad(lat2)) * Math.Sin(dLong / 2) * Math.Sin(dLong / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c;
            return d * (0.6213712);

        }

        public double infoLat(string text)
        {
            string names = "";
            int nums = endLat(text);
            foreach (Match extract in Regex.Matches(text, "titude\": ?"))
            {
                int z = (extract.Index + 8);
                int x = nums - z - 4;
                names = text.Substring(extract.Index + 8, x);
            }
            double lat = Convert.ToDouble(names);
            return lat;
        }

        public int endLat(string text)
        {
            int num = 0;

            foreach (Match extract in Regex.Matches(text, "\"Longitude\"?"))
            {
                num = extract.Index - 1;
            }
            return num;
        }

        public double infoLon(string text)
        {
            string names = "";
            int nums = endLon(text);
            foreach (Match extract in Regex.Matches(text, "gitude\":?"))
            {
                int z = (extract.Index + 8);
                int x = nums - z - 4;
                names = text.Substring(extract.Index + 8, x);
            }
            double lon = Convert.ToDouble(names);
            return lon;
        }

        public int endLon(string text)
        {
            int num = 0;

            foreach (Match extract in Regex.Matches(text, "\"ZipCode\"?"))
            {
                num = extract.Index - 1;
            }
            return num;
        }

    }
}
