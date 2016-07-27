using System;
using System.Collections.Generic;
////using System.ComponentModel;
using System.Configuration;
//using System.Data;
using System.Diagnostics;
//using System.Linq;
////using System.Net;
using System.Net.Http;
//using System.Net.Http.Headers;
using System.ServiceProcess;
//using System.Text;
//using System.Threading.Tasks;
using System.Timers;
//using System.Net.Http;
using System.Net.Http.Formatting;
//using System.Net.Http.Headers;
namespace BusinessCenter.BatchService
{
    partial class BblEntityDailyMailBatchService : ServiceBase
    {
      
        public BblEntityDailyMailBatchService()
        {
            InitializeComponent();
            InitializeTimer();
        }
        private Timer _timer;

        private void InitializeTimer()
        {
            if (_timer == null)
            {
                // ReSharper disable once UseObjectOrCollectionInitializer
                _timer = new Timer();
                _timer.AutoReset = true;
                _timer.Interval = 60000 * Convert.ToDouble(
                    ConfigurationSettings.AppSettings["IntervalMinutes"]);
                // ReSharper disable once RedundantDelegateCreation
                _timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            }
        }
        protected override void OnStart(string[] args)
        {
           
            //string message = "BusinessCenterBatchService Started the Service at :"
            //                   + DateTime.Now.ToShortDateString() + " "
            //                   + DateTime.Now.ToShortTimeString();
            //this.EventLog.WriteEntry(message, EventLogEntryType.Error);
            _timer.Start();
          
        }

        protected override void OnStop()
        {
            _timer.Stop();
        }

        protected override void OnContinue()
        {
            base.OnContinue();
            _timer.Start();
        }

        protected override void OnPause()
        {
            base.OnPause();
            _timer.Stop();
        }

        protected override void OnShutdown()
        {
           
        
            base.OnShutdown();
            _timer.Stop();
           
        }
        // ReSharper disable once RedundantNameQualifier
        private void timer_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            RunCommands();
        }

        private void RunCommands()
        {
            EventLog evt = new EventLog();
            string sSource = "BusinessCenterServiceLog";
            string sLog = "Application";
           
#pragma warning disable 618
            // ReSharper disable once RedundantToStringCall
            string siteLocation = ConfigurationSettings.AppSettings["siteAddress"].ToString();
#pragma warning restore 618
#pragma warning disable 618
            // ReSharper disable once RedundantToStringCall
            string controllerLocation = ConfigurationSettings.AppSettings["ControllerLocation"].ToString();
#pragma warning restore 618
            try
            {

                evt = new EventLog();
                if (!EventLog.SourceExists(sSource))
                    EventLog.CreateEventSource(sSource, sLog);

              


                var client = new HttpClient()
                {
                    BaseAddress = new Uri(siteLocation)
                };

                var form = new Dictionary<string, string>    
               {    
                   {"grant_type", "password"},    
#pragma warning disable 618
                   // ReSharper disable once RedundantToStringCall
                   {"username", ConfigurationSettings.AppSettings["userName"].ToString()},    
#pragma warning restore 618
#pragma warning disable 618
                   // ReSharper disable once RedundantToStringCall
                   {"password", ConfigurationSettings.AppSettings["password"].ToString()},    
#pragma warning restore 618
               };

                var tokenResponse = client.PostAsync(siteLocation + "/authtoken", new FormUrlEncodedContent(form)).Result;
                var token = tokenResponse.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() }).Result;

                client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", token.AccessToken));
                var response = client.GetAsync(controllerLocation).Result;
              
                if (response.IsSuccessStatusCode)
                {
                    // ReSharper disable once UnusedVariable
                    var dto = response.Content.ReadAsStringAsync().Result;
                }




                string message = "BusinessCenterBatchService invoked dailymaile webservice at :" + response.IsSuccessStatusCode
                                 + DateTime.Now.ToShortDateString() + " "
                                 + DateTime.Now.ToShortTimeString();
                evt.Source = sSource;
                evt.WriteEntry(message, EventLogEntryType.Information);

            }
            catch (Exception e)
            {
                string message = "Error while invoking event log :"
                                    + DateTime.Now.ToShortDateString() + " "
                                    + DateTime.Now.ToShortTimeString() + " "
                                    + e.Message;
                evt.Source = sSource;
                evt.WriteEntry(message, EventLogEntryType.Error);
            }
        }
    }
}
