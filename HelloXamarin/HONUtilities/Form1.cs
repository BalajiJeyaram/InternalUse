using System;
using System.ServiceProcess;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Collections.Generic;

namespace HONUtilities
{
    public partial class Form1 : Form
    {
        //EventLog logs = new EventLog();
        
        //private AutoCompleteStringCollection 
        public Form1()
        {
            //CreateNewEventLog();
            //logs.Source = "Honeywell Diagnostic Tool";
            ProgressBar pBar = new ProgressBar();
            InitializeComponent();
            LoadDataGrid();
        }

        private void LoadDataGrid() 
        {
            dGrid.DataSource = ReturnServiceNameWithStatus();
            dGrid.Columns[0].Width = 300;
            dGrid.Columns[1].Width = 100;
        }

        private void CreateNewEventLog()
        {
            if (!EventLog.SourceExists("Honeywell Diagnostic Tool")) {

                EventLog.CreateEventSource("Honeywell Diagnostic Tool", "Honeywell Diagnostic Tool");
            }
        }
        private void cmdStop_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;

            if (CheckAppIsRunning()) {
                MessageBox.Show("Close Smart System / Staging Hub Console to proceed further","Validation");
                return;
            }

            var AppsettingsCollection = ReturnServicesName();
            var applist = from o in AppsettingsCollection
                          where o == "Honeywell Staging Hub Client Service" || o == "Honeywell Staging Hub Console Service" || o == "Honeywell Staging Hub Core Messaging Service"
                          select o;

            StartStopService(false,applist.ToList());
            LoadDataGrid();


        }

        private bool StartStopService(bool blnstart, List<string> services )
        {
            bool returnvalue = false;
            TimeSpan timeout = TimeSpan.FromMilliseconds(10000);
            //var AppsettingsCollection = ReturnServicesName();
            //var applist = from o in AppsettingsCollection
            //              where o == "Honeywell Staging Hub Client Service" || o == "Honeywell Staging Hub Console Service" || o == "Honeywell Staging Hub Core Messaging Service"
            //              select o;

            foreach (string item in services)
            {
                //logs.WriteEntry(item, EventLogEntryType.Information);
                lblmessage.Text = (blnstart)? "Starting " : "Stopping" + item + " Service" ;
                ServiceController service = new ServiceController(item);
                try
                {
                    //   1000          00:00:01 - 1 second
                    //   60000          00:01:00 - 1 minute
                    // 3600000          01:00:00 - 1 hour
                    //86400000        1.00:00:00 - 24 hours

                    timeout = TimeSpan.FromMilliseconds(10000);
                    if (!blnstart)
                    {
                        service.Stop();
                        service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                    }
                    else
                    {
                        service.Start();

                        while (service.Status != ServiceControllerStatus.Running)
                        {
                            timeout = TimeSpan.FromMilliseconds(30000);
                            service.WaitForStatus(ServiceControllerStatus.Running,timeout);
                            //service.Status = ServiceControllerStatus.Running;
                        }

                    }

                    if (service.Status == ServiceControllerStatus.Stopped)
                        lblmessage.Text = "Stopped " + item + " Service";
                    else
                        lblmessage.Text = "Started " + item + " Service";

                }
                catch (InvalidOperationException opEx)
                {
                    //var exceptionType = ex.GetType().Name;
                    MessageBox.Show(opEx.InnerException.ToString(), "Invalid Operation Exception");
                    returnvalue = false;
                }
                catch (System.TimeoutException te)
                {
                    timeout.Add(TimeSpan.FromMilliseconds(30000));
                    MessageBox.Show(te.InnerException.ToString(), "Timed Out Exception");
                    returnvalue = false;
                }
                catch (Exception ex)
                {
                    var exceptionType = ex.GetType().Name;
                    MessageBox.Show(ex.Message + "\r" + ex.StackTrace, "Runtime Exception");
                    returnvalue = false;
                    //logs.WriteEntry(ex.StackTrace,EventLogEntryType.Error);
                }
            }
            return returnvalue;
        }


        private bool CheckAppIsRunning(string filelocation = @"c:\Program Files (x86)\Intermec\SmartSystem\Server")
        {
            bool returnvalue = false;

            try {

                var exists = System.Diagnostics.Process.GetProcessesByName("SmartSystemsConsoleGui");
                returnvalue = exists.Count() >= 1;
                return returnvalue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r" + ex.StackTrace, "RunTime Exception");
                returnvalue = false;
                return returnvalue;
            }

        }

        private List<string> ReturnServicesName()
        {
            List<string> collection = new List<string>();
            collection = ConfigurationManager.AppSettings.AllKeys
                            .Where(key => key.StartsWith("Service"))
                            .Select(key => ConfigurationManager.AppSettings[key])
                            .ToList();



            return collection;
            
        
        }


        private DataTable ReturnServiceNameWithStatus()
        {
            DataTable dt = null;
            dt = new DataTable("ServiceNameWithStatus");
            dt.Columns.Add("Name");
            dt.Columns.Add("Status");


            DataRow dr;

            var AppsettingsCollection = ReturnServicesName();
            foreach (string item in AppsettingsCollection)
            {
                ServiceController service = new ServiceController(item);
                dr = dt.NewRow();// new DataRow();
                dr[0] = item;
                dr[1] = service.Status;
                dt.Rows.Add(dr);
            }


            return dt;
        
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            lblmessage.Text = string.Empty;

            if (CheckAppIsRunning())
            {
                MessageBox.Show("Close Smart System / Staging Hub Console to proceed further", "Validation");
                return;
            }

            var AppsettingsCollection = ReturnServicesName();
            var applist = from o in AppsettingsCollection
                          where o != "Honeywell Staging Hub Core Messaging Service"
                          select o;

            StartStopService(true, applist.ToList());
            LoadDataGrid();

        }

        private void cmdcreatelogfile_Click(object sender, EventArgs e)
        {
            CreateLogEntryInRegEdit();
        }

        private bool CreateLogEntryInRegEdit()
        {
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Intermec\SmartSystem\Logging\");
            key.SetValue("AndroidQService", 1);
            key.Close();


            //use following key to delete the elemant
            //key.DeleteValue("AndroidQService",true);
            //key.Close();
            //Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Intermec\SmartSystem\Logging\").SetValue("AndroidQService", (decimal)1, Microsoft.Win32.RegistryValueKind.DWord); 
            return true;
        }
    }
}
