using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests { 

    public class NavigationHelper : HelperBase
    {
        private string baseURL;

        public NavigationHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }
        public void GoToManagePage()
        {
            if (driver.Url == baseURL + "/manage_overview_page.php")
                if (driver.Url.Contains(baseURL + "/manage_"))
                {
                    return;
                }
            driver.Navigate().GoToUrl(baseURL + "/manage_overview_page.php");
        }
        
        public void GoToProjectsPage()
        {
            if (driver.Url == baseURL + "/manage_proj_page.php")
            {
                return;
            }
            GoToManagePage();
            driver.FindElement(By.LinkText("Управление проектами")).Click() ;
        }
        
    }
}