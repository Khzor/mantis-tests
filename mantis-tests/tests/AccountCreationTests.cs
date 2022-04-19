using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace mantis_tests
{

    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [OneTimeSetUp]
        public void setUpConfig()
        {
            app.Ftp.BackupFile("/config_inc.php");
            using (Stream localFile = File.Open("config_inc.php", FileMode.Open))
            {
                app.Ftp.Upload("/config_inc.php", localFile);
            }
        }

        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData()
            {
                Username = "testuser8",
                RealName = "Robot",
                Password = "password",
                Email = "testuser8@localhost.localdomain"
                
            };

            List<AccountData> accounts = app.Admin.GetAllAccounts();
            AccountData existingAccount = accounts.Find(x => x.Username == account.Username);

            if (existingAccount != null)
            {
                app.Admin.DeleteAccount(existingAccount);
            }

            app.James.Delete(account);
            app.James.Add(account);

            app.Registration.Register(account);
        }

        [OneTimeTearDown]
        public void restoreConfig()
        {
            app.Ftp.RestoreBackupFile("/config_inc.php");
        }
    }
}
