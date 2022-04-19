using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace mantis_tests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }
                Logout();
            }
            
            Type(By.Name("username"), account.Username);
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
            Type(By.Name("password"), account.Password);
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.XPath("//div[2]/ul/li[3]")).Click();
                driver.FindElement(By.ClassName("user-menu"))
                .FindElement(By.XPath("//li[4]/a/i")).Click();
            }
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.ClassName("user-info"));
        }

        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
                && GetLoggetUserName() == account.Username;
        }

        public string GetLoggetUserName()
        {
            return driver.FindElement(By.ClassName("user-info")).Text;
        }
    }
}
