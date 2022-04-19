using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class RegistrationHelper : HelperBase
    {
        public RegistrationHelper(ApplicationManager manager) : base(manager) { }

        public void Register(AccountData account)
        {
            manager.loginHelper.Logout();
            OpenMainPage();
            OpenRegistrationForm();
            FillRegistrationForm(account);
            SubmitRegistration();
            String url = GetConfirmationUrl(account);
            FillPasswordForm(url, account);
            SubmitPasswordForm();
        }

        private string GetConfirmationUrl(AccountData account)
        {
            String message = manager.Mail.GetLastMail(account);
            Match match = Regex.Match(message, @"http://\S*");
            return match.Value;
        }

        private void FillPasswordForm(string url, AccountData account)
        {
            driver.Url = url;
            driver.FindElement(By.Name("realname")).SendKeys(account.RealName);
            driver.FindElement(By.Name("password")).SendKeys(account.Password);
            driver.FindElement(By.Name("password_confirm")).SendKeys(account.Password);
        }

        private void SubmitPasswordForm()
        {
            driver.FindElement(By.CssSelector("button.btn")).Click();
        }

        public void SubmitRegistration()
        {
            driver.FindElement(By.CssSelector("input.btn")).Click();
        }

        public void FillRegistrationForm(AccountData account)
        {
            driver.FindElement(By.Name("username")).SendKeys(account.Username);
            driver.FindElement(By.Name("email")).SendKeys(account.Email);
        }

        public void OpenRegistrationForm()
        {
            driver.FindElement(By.LinkText("Зарегистрировать новую учётную запись")).Click();
        }

        public void OpenMainPage()
        {
            manager.Driver.Url = "http://localhost/mantisbt-2.25.3/login_page.php";
        }
    }
}
