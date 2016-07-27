namespace BusinessCenter.BatchService
{
    partial class BusinessCenterInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BusinessCenterBatchInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // BusinessCenterBatchInstaller
            // 
            this.BusinessCenterBatchInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.BusinessCenterBatchInstaller.Password = null;
            this.BusinessCenterBatchInstaller.Username = null;
            // 
            // serviceInstaller
            // 
            this.serviceInstaller.Description = "BusinessCenterBatchService";
            this.serviceInstaller.DisplayName = "BusinessCenterBatchServiceDailyMails";
            this.serviceInstaller.ServiceName = "BblEntityDailyMailBatchService";
            this.serviceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // BusinessCenterInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.BusinessCenterBatchInstaller,
            this.serviceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller BusinessCenterBatchInstaller;
        private System.ServiceProcess.ServiceInstaller serviceInstaller;
    }
}