using Microsoft.Win32;
using System;

namespace MyApplication
{
    internal class AppSettings
    {
        private const string RegPath = "software/ApplicationBase/demo";
        private const string DefaultConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MyApplicationDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        #region Singleton
        private static AppSettings defaultInstance = new AppSettings();
        public static AppSettings Actual
        {
            get { return defaultInstance; }
        }
        #endregion

        public string ConnectionString { get; set; }

        public void Load()
        {
            using (RegistryKey regKey = Registry.CurrentUser.CreateSubKey(RegPath))
            {
                ConnectionString = Convert.ToString(regKey.GetValue("ConnectionString", DefaultConnectionString));
            }
        }

        public void Save()
        {
            using (RegistryKey regKey = Registry.CurrentUser.CreateSubKey(RegPath))
            {
                regKey.SetValue("ConnectionString", ConnectionString, RegistryValueKind.String);
            }
        }
    }
}
