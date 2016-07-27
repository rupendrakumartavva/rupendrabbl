using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace BusinessCenter.BatchService
{
    [RunInstaller(true)]
    public partial class BusinessCenterInstaller : Installer
    {
        public BusinessCenterInstaller()
        {
            InitializeComponent();
        }
        protected override void OnAfterInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);

            //The following code starts the services after it is installed.
            using (System.ServiceProcess.ServiceController serviceController = new System.ServiceProcess.ServiceController(serviceInstaller.ServiceName))
            {
                serviceController.Start();
            }
        }
    }
}
