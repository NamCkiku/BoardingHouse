using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BoardingHouse.Administrator.Controllers
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
        public static string ResolveServerUrl(string serverUrl, bool forceHttps)
        {
            if (serverUrl.IndexOf("://") > -1)
                return serverUrl;

            string newUrl = serverUrl;
            Uri originalUri = System.Web.HttpContext.Current.Request.Url;
            newUrl = (forceHttps ? "https" : originalUri.Scheme) +
                "://" + originalUri.Authority + newUrl;
            return newUrl;
        }
        //public ClientInformation GetClientInformation()
        //{
        //    ClientInformation user = new ClientInformation();
        //    HttpBrowserCapabilitiesBase browser = Request.Browser;
        //    user.IPAddress = GetVisitorIPAddress(true);
        //    user.Device = "Browser Capabilities" + System.Environment.NewLine +
        //       "Name = " + browser.Browser + System.Environment.NewLine +
        //       "Version = " + browser.Version + System.Environment.NewLine +
        //       "Platform = " + browser.Platform;
        //    return user;
        //}
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
                    visitorIPAddress = arrIpAddress[arrIpAddress.Length - 2].ToString();
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
    }
}