using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace BoardingHouse.Web.Controllers
{
    public class BaseController : Controller
    {
        public bool ValidateRequestHeader(HttpRequestBase request)
        {
            bool result = false;
            string cookieToken = "";
            string formToken = "";
            try
            {
                //IEnumerable<string> tokenHeaders;
                string[] token = request.Headers.GetValues("RequestVerificationToken");

                if (token.Length > 0)
                {
                    string[] tokens = token[0].Split(':');
                    if (tokens.Length == 2)
                    {
                        cookieToken = tokens[0].Trim();
                        formToken = tokens[1].Trim();
                    }
                }
                AntiForgery.Validate(cookieToken, formToken);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;

            }
            return result;


        }

        public string GetVisitorIPAddress(bool GetLan = false)
        {
            string visitorIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (String.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = Request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = Request.UserHostAddress;

            if (string.IsNullOrEmpty(visitorIPAddress) || visitorIPAddress.Trim() == "::1")
            {
                GetLan = true;
                visitorIPAddress = string.Empty;
            }

            if (GetLan && string.IsNullOrEmpty(visitorIPAddress))
            {
                //This is for Local(LAN) Connected ID Address
                string stringHostName = Dns.GetHostName();
                //Get Ip Host Entry
                IPHostEntry ipHostEntries = Dns.GetHostEntry(stringHostName);
                //Get Ip Address From The Ip Host Entry Address List
                IPAddress[] arrIpAddress = ipHostEntries.AddressList;

                try
                {
                    visitorIPAddress = arrIpAddress[arrIpAddress.Length - 1].ToString();
                }
                catch
                {
                    try
                    {
                        visitorIPAddress = arrIpAddress[0].ToString();
                    }
                    catch
                    {
                        try
                        {
                            arrIpAddress = Dns.GetHostAddresses(stringHostName);
                            visitorIPAddress = arrIpAddress[0].ToString();
                        }
                        catch
                        {
                            visitorIPAddress = "127.0.0.1";
                        }
                    }
                }

            }


            return visitorIPAddress;
        }
        public static List<string> GetLatLng(string address)
        {
            var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=true", Uri.EscapeDataString(address));

            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
            var xdoc = XDocument.Load(response.GetResponseStream());
            var latLngArray = new List<string>();
            var xElement = xdoc.Element("GeocodeResponse");
            if (xElement != null)
            {
                var result = xElement.Element("result");
                if (result != null)
                {
                    var element = result.Element("geometry");
                    if (element != null)
                    {
                        var locationElement = element.Element("location");
                        if (locationElement != null)
                        {
                            var xElement1 = locationElement.Element("lat");
                            if (xElement1 != null)
                            {
                                var lat = xElement1.Value;
                                latLngArray.Add(lat);

                            }
                            var element1 = locationElement.Element("lng");
                            if (element1 != null)
                            {
                                var lng = element1.Value;
                                latLngArray.Add(lng);
                            }
                        }
                    }
                }
            }
            return latLngArray;
        }
    }
}