using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
 
namespace AzureMessageQueue
{
    public partial class AzureMessageQueue : ServiceBase
    {
        System.Diagnostics.EventLog eventLog1;
        private int eventId = 1;
        public AzureMessageQueue(string[] args)
        {
           // InitializeComponent();

             eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MessageQueue"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MessageQueue", "MessageLog");
            }
            eventLog1.Source = "MessageQueue";
            eventLog1.Log = "MessageLog";
             
        }

        public AzureMessageQueue()
        {
             InitializeComponent();

        }

        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                AzureMessageQueue service1 = new AzureMessageQueue(args);
                service1.TestStartupAndStop(args);
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new AzureMessageQueue()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }


        protected override void OnStart(string[] args)
        {
            Timer timer = new Timer();
            timer.Interval = 60000; // 60 seconds
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("In OnStop.");
        }
    }
}
