using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace BoardingHouse.Common.Logs
{
    public class LogCommon
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(LogCommon));

        public LogCommon()
        {
            //BasicConfigurator.Configure();
            XmlConfigurator.Configure();
        }

        public void WriteLogInfo(string msg)
        {
            _logger.Info(msg);
        }

        public void WriteLogError(string msg)
        {
            _logger.Error(msg);
        }

        public void WriteLogFatal(string msg)
        {
            _logger.Fatal(msg);
        }

        public void WriteLogDebug(string msg)
        {
            _logger.Debug(msg);
        }

        public void WriteLogWarning(string msg)
        {
            _logger.Warn(msg);
        }
        //public static string PhysicalPath;
        //public static void WriteError(string errorMessage)
        //{
        //    try
        //    {
        //        string path = "~/Error/" + DateTime.Today.ToString("dd-MM-yy") + ".txt";

        //        if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
        //        {
        //            File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
        //        }
        //        using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
        //        {
        //            w.WriteLine("\r\nLog Entry : ");
        //            w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
        //            string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() +
        //                       ". Error Message:" + errorMessage;
        //            w.WriteLine(err);
        //            w.WriteLine("__________________________");
        //            w.Flush();
        //            w.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //WriteError(ex.Message);
        //    }
        //}

        //public static void WriteError(string errorMessage, string FunctionName)
        //{
        //    try
        //    {
        //        string path = "~/Error/" + DateTime.Today.ToString("dd-MM-yy") + ".txt";
        //        if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
        //        {
        //            File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
        //        }
        //        using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
        //        {
        //            w.WriteLine("\r\nLog Entry : ");
        //            w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
        //            string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() + " Function: " + FunctionName +
        //                       ". Error Message:" + errorMessage;
        //            w.WriteLine(err);
        //            w.WriteLine("__________________________");
        //            w.Flush();
        //            w.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // WriteError(ex.Message);
        //    }
        //}
    }
}
