using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

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
    }
}