using System;
using System.Net;


namespace ReportPassThrough {
	public class GetReportData {
		/// <summary>
		/// Username for the authenticated user
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// Password for the authenticated user
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Domain the user will  be authenticated
		/// </summary>
		public string Domain { get; set; }


		/// <summary>
		/// Get the Excel report from the reporting server and returns 
		/// an byte array with the content.
		/// </summary>
		/// <param name="Url">URL of the report incl. all parameter.</param>
		/// <returns>Excel report </returns>
		public byte[] GetExcelReport(string Url) {
			if (string.IsNullOrWhiteSpace(Url)) {
				throw new ArgumentNullException("Url", "URL to report server can not be null!");
			}
			return GetFromServer(Url);
		}

		/// <summary>
		/// This function makes the actual work. Get the data from the server and returns it
		/// </summary>
		/// <param name="Url">URL with the report parameter</param>
		/// <returns>byte array with the Excel sehh</returns>
		private byte[] GetFromServer(string Url) {
			byte[] _content = null;

			
			using (var _client = new WebClient()) {
				// set the credentials
				// I use this normally with a Domain but the NetworkCredentials can be also something different.
				if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Domain)) {
					NetworkCredential _cred = null;
					try {
						_cred = new NetworkCredential(Username, Password, Domain);
					} catch (Exception _exception) {
						throw new ApplicationException("Unable to authenticate.", _exception);
					}
					_client.Credentials = _cred;
				} else {
					_client.UseDefaultCredentials = true;
				}

				// Format the URL string to return Excel as the requested format.
				string _excelDownload = string.Format("{0}&rs:Format=EXCEL", Url);
				try {
					_content = _client.DownloadData(_excelDownload);
				} catch (Exception _exception) {
					throw new ApplicationException("Error while downloading report data.", _exception);
				}
			}

			return _content;
		}



	}
}