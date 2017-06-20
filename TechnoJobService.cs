using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace TechnoJobService
{
    public partial class TechnoJobService : ServiceBase
    {
        public TechnoJobService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("Service TechnoJobsService Starting ");
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("Service TechnoJobsService Stopping ");
        }
    }
}
