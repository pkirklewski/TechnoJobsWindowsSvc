﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace TechnoJobService
{
    public partial class TechnoJobService : ServiceBase
    {
        public TechnoJobService(string[] args)
        {
            InitializeComponent();
            string eventSourceName = "MySource";
            string logName = "MyNewLog";
            
            if (args.Count() > 0) { 
                eventSourceName = args[0]; 
            }

            if (args.Count() > 1) { 
                logName = args[1]; 
            }

            eventLog1 = new System.Diagnostics.EventLog();

            if (!System.Diagnostics.EventLog.SourceExists(eventSourceName)) { 
                System.Diagnostics.EventLog.CreateEventSource(eventSourceName, logName); 
            }

            eventLog1.Source = eventSourceName;

            eventLog1.Log = logName; 

        }

        protected override void OnStart(string[] args)
        {

            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);  

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();


            eventLog1.WriteEntry("Service TechnoJobsService Starting ");
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus); 

        }// End of OnStart

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args) 
        {
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        
        }
     

        protected override void OnStop()
        {
            eventLog1.WriteEntry("Service TechnoJobsService Stopping ");
        }

        protected override void OnContinue()
        {
            eventLog1.WriteEntry("Service TechnoJobsService in OnContinue ");
        }

        
        protected override void OnPause()
        {
            eventLog1.WriteEntry("Service TechnoJobsService - OnPause ");
        }

        protected override void OnShutdown()
        {
            eventLog1.WriteEntry("Service TechnoJobsService Shutting Down ");
        }


        public int eventId { get; set; }



        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]

        public struct ServiceStatus
        {
            public long dwServiceType;
            public ServiceState dwCurrentState;
            public long dwControlsAccepted;
            public long dwWin32ExitCode;
            public long dwServiceSpecificExitCode;
            public long dwCheckPoint;
            public long dwWaitHint;
        };

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus); // Build succeeded

    } // End of public partial class TechoJonsService
}



