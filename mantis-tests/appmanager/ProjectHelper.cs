using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace mantis_tests
{
    public class ProjectHelper : HelperBase
    {
        public ProjectHelper(ApplicationManager manager) : base(manager) { }

        public void CreateNewProject(ProjectData project)
        {
            manager.Nav.GoToProjectsPage();
            InitProjectCreation();
            FillProjectForm(project);
            SubmitProjectCreation();
        }

        private void FillProjectForm(ProjectData project)
        {
            driver.FindElement(By.Id("project-name")).SendKeys(project.Name);
        }

        public void Remove(ProjectData toBeRemoved)
        {
            manager.Nav.GoToProjectsPage();
            SelectProject(toBeRemoved);
            InitProjectRemoval();
            SubmitProjectRemoval();
        }

        public void SelectProject(ProjectData toBeRemoved)
        {
            driver.FindElement(By.XPath("(//a[text()='" + toBeRemoved.Name + "'])[2]")).Click();
        }

        public void InitProjectRemoval()
        {
            driver.FindElement(By.Id("project-delete-form"))
                .FindElement(By.ClassName("btn")).Click();
        }

        public void SubmitProjectRemoval()
        {
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        }

        public void SubmitProjectCreation()
        {
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        }

        public void InitProjectCreation()
        {
            driver.FindElement(By.XPath("//div/div/div[2]/div[2]//button")).Click();
        }

        public List<ProjectData> GetAllProjects()
        {
            manager.Nav.GoToProjectsPage();

            List<ProjectData> projectsList = new List<ProjectData>();

            ICollection<IWebElement> projects = driver.FindElements(By.XPath("//div[2]/table/tbody/tr"));

            foreach (IWebElement project in projects)
            {
                string name = project.FindElement(By.XPath("./td[1]")).Text;

                projectsList.Add(new ProjectData
                {
                    Name = name
                });
            }

            return projectsList;
        }
    }
}
