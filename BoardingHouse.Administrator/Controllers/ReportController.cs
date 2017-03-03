using BoardingHouse.Service.IService;
using Stimulsoft.Report;
using Stimulsoft.Report.Export;
using Stimulsoft.Report.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BoardingHouse.Administrator.Controllers
{
    public class ReportController : Controller
    {
        private readonly IRoomService _roomService;
        public ReportController(IRoomService roomService)
        {
            this._roomService = roomService;
        }
        public string ReportTempFolder = "~/ReportTemplates/";
        public PartialViewResult _Report()
        {
            return PartialView();
        }
        public ActionResult GetReportSnapshot()
        {
            try
            {
                var report = new StiReport();
                TempData["dataExportFileTres"] = report;
                report.Load(Server.MapPath(ReportTempFolder + "Report.mrt"));
                //report.RegData("dtRoom", _roomService.GetAll());
                return StiMvcViewer.GetReportSnapshotResult(report);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult();
        }
        public ActionResult ExportFilePrint()
        {
            try
            {
                StiReport report = (StiReport)TempData["dataExportFileTres"];
                var stream = new MemoryStream();

                var settings = new StiPdfExportSettings();
                settings.AutoPrintMode = StiPdfAutoPrintMode.Dialog;

                var service = new StiPdfExportService();

                service.ExportPdf(report, stream, settings);

                this.Response.Buffer = true;
                this.Response.ClearContent();
                this.Response.ClearHeaders();
                this.Response.ContentType = "application/pdf";
                this.Response.ContentEncoding = Encoding.UTF8;

                this.Response.AddHeader("Content-Length", stream.Length.ToString());
                this.Response.BinaryWrite(stream.ToArray());
                this.Response.End();

                TempData["dataExportFileTres"] = report;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }

        public ActionResult ExportFilePdf()
        {
            try
            {
                StiReport report = (StiReport)TempData["dataExportFileTres"];
                var stream = new MemoryStream();
                var settings = new StiPdfExportSettings();

                var service = new StiPdfExportService();
                service.ExportPdf(report, stream);

                this.Response.ClearContent();
                this.Response.ClearHeaders();
                this.Response.ContentType = "application/json";
                this.Response.ContentEncoding = Encoding.UTF8;
                this.Response.AddHeader("content-disposition", "attachment; filename=Report.PDF");
                this.Response.BinaryWrite(stream.ToArray());
                this.Response.End();

                TempData["dataExportFileTres"] = report;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }

        public ActionResult ExportFileExcel()
        {
            try
            {
                StiReport report = (StiReport)TempData["dataExportFileTres"];

                var stream = new MemoryStream();

                var service = new StiExcelExportService();
                service.ExportExcel(report, stream);

                this.Response.Clear();
                this.Response.ContentType = "text/csv";
                this.Response.AddHeader("content-disposition", "attachment;filename=Report.xls");
                this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                this.Response.BinaryWrite(stream.ToArray());
                this.Response.End();

                TempData["dataExportFileTres"] = report;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }
    }
}