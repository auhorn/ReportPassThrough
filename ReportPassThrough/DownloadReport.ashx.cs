
using System;
using System.Net;
using System.Web;

namespace ReportPassThrough {
	/// <summary>
	/// Summary description for Handler1
	/// </summary>
	public class DownloadReport : IHttpHandler {

		public void ProcessRequest(HttpContext context) {

			var _downloader = new GetReportData();
			// add the string to your report and do not forget to adjust the parameter
			string _url = @"http://reportServer.example.com/ReportServer10?/SuperSecretExcelReport&EmployeeNumber=42";
			byte[] _xlsContent;
			try {
				_xlsContent = _downloader.GetExcelReport(_url);
			}
			catch (Exception _exception) {
				_xlsContent = null;
				// write to the log file
			}
			if (_xlsContent != null) {
				// set the file name
				context.Response.AppendHeader("Content-Disposition",
																			"attachment; filename=" + context.Server.UrlPathEncode("ExcelReport.xls").Replace("%20", " "));
				// set the content
				context.Response.BinaryWrite(_xlsContent);

				// set the ContentType to Excel so Excel is opening
				context.Response.ContentType = "application/excel";
			}
			else {
				context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
			}

		}

		public bool IsReusable {
			get {
				return false;
			}
		}
	}
}