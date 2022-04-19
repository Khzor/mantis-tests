using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SimpleBrowser.WebDriver;
using System.Text.RegularExpressions;

namespace mantis_tests
{
    public class AdminHelper : HelperBase
    {
        private string baseURL;

        public AdminHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }

        public List<AccountData> GetAllAccounts()
        {
            List<AccountData> accounts = new List<AccountData>();
            OpenAppAndLogin();
            driver.Url = baseURL + "/manage_user_page.php";
            IList<IWebElement> rows = driver.FindElements(By.XPath("//table/tbody/tr"));

            foreach (IWebElement row in rows)
            {
                IWebElement link = row.FindElement(By.TagName("a"));
                string username = link.Text;
                string href = link.GetAttribute("href");
                Match m = Regex.Match(href, @"\d+$");
                string id = m.Value;
                accounts.Add(new AccountData()
                {
                    Username = username,
                    Id = id
                });
            }
            return accounts;
        }

        public void DeleteAccount(AccountData account)
        {
            OpenAppAndLogin();
            driver.Url = baseURL + "/manage_user_edit_page.php?user_id=" + account.Id;
            driver.FindElement(By.Id("manage-user-delete-form")).Click();
            driver.FindElement(By.ClassName("alert")).FindElement(By.ClassName("btn")).Click();
        }

        public IWebDriver OpenAppAndLogin()
        {
            driver.Url = baseURL + "/login_page.php";
            manager.Auth.Login(new AccountData()
            {
                Username = "administrator",
                Password = "root1"
            });
            return driver;
        }
    }
}
