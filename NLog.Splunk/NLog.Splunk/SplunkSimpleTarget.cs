using NLog.Common;
using NLog.Config;
using NLog.Targets;
using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace NLog.Splunk
{
    /// <summary>
    /// Splunk simple target
    /// http://docs.splunk.com/Documentation/Splunk/6.1.1/RESTAPI/RESTinput#POST_receivers.2Fsimple
    /// POST receivers/simple
    /// </summary>
    [Target("SplunkSimple")]
    public class SplunkSimpleTarget : TargetWithLayout
    {
        #region Properties

        /// <summary>
        /// Splunk URL with 8089 port
        /// </summary>
        [RequiredParameter]
        public string Host { get; set; }

        /// <summary>
        /// Username for authorized requests
        /// </summary>
        [RequiredParameter]
        public string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [RequiredParameter]
        public string Password { get; set; }

        /// <summary>
        /// Source for pushing logs
        /// </summary>
        [RequiredParameter]
        public string Source { get; set; }

        /// <summary>
        /// Source type of logs
        /// </summary>
        [RequiredParameter]
        public string SourceType { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Pushing logs
        /// </summary>
        /// <param name="logEvent"></param>
        protected override void Write(LogEventInfo logEvent)
        {
            var logMessage = Layout.Render(logEvent);
            SendToSplunk(logMessage);
        }

        /// <summary>
        /// Async pushing logs
        /// </summary>
        /// <param name="logEvents"></param>
        protected override void Write(AsyncLogEventInfo[] logEvents)
        {
            var logContent = new StringBuilder();
            foreach (AsyncLogEventInfo item in logEvents)
            {
                var logMessage = Layout.Render(item.LogEvent);
                logContent.Append(logMessage);
                logContent.AppendLine();
            }
            SendToSplunk(logContent.ToString());
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Send data to splunk by POST request
        /// </summary>
        /// <param name="data">Log</param>
        private void SendToSplunk(string data)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            using (var client = new HttpClient())
            {
                var url = $"{Host?.TrimEnd('/')}/services/receivers/simple?source={Source}&sourcetype={SourceType}";
                var byteArray = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", Username, Password));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var response = client.PostAsync(url, new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded")).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                }
            }
        }

        #endregion
    }
}
