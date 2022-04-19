using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;
        public LoginHelper loginHelper;
        protected NavigationHelper navigationHelper;
        protected ProjectHelper projectHelper;

        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            baseURL = "http://localhost/mantisbt-2.25.3";
            Registration = new RegistrationHelper(this);
            Ftp = new FtpHelper(this);
            James = new JamesHelper(this);
            Mail = new MailHelper(this);
            loginHelper = new LoginHelper(this);
            navigationHelper = new NavigationHelper(this, baseURL);
            projectHelper = new ProjectHelper(this);
            Admin = new AdminHelper(this, baseURL);
            API = new APIHelper(this);
        }

        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
            }
        }

        public static ApplicationManager GetInstance()
        {
            if (! app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url = "http://localhost/mantisbt-2.25.3/login_page.php";
                app.Value = newInstance;
            }
            return app.Value;
        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

        protected AccountData admin;

        public AdminHelper Admin { get; set; }
        public APIHelper API { get; set; }
        public RegistrationHelper Registration { get; set; }
        public FtpHelper Ftp { get; set; }
        public JamesHelper James { get; set; }
        public MailHelper Mail { get; set; }
        public LoginHelper Auth
        {
            get
            {
                return loginHelper;
            }
        }
        public NavigationHelper Nav
        {
            get
            {
                return navigationHelper;
            }
        }
        public ProjectHelper Project
        {
            get
            {
                return projectHelper;
            }
        }
    }
}
