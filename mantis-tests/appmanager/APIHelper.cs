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
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager manager) : base(manager) { }

        public void CreateNewIssue(AccountData account, ProjectData project, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = project.Id;
            client.mc_issue_add(account.Username, account.Password, issue);
        }

        public void CreateNewProject(AccountData account, ProjectData project)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData mantisProject = new Mantis.ProjectData()
            {
                name = project.Name
            };
            client.mc_project_add(account.Username, account.Password, mantisProject);
        }

        public List<ProjectData> GetAllProjects(AccountData account)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            List<ProjectData> projectsData = new List<ProjectData>();
            Mantis.ProjectData[] projects = client.mc_projects_get_user_accessible(account.Username, account.Password);
            foreach (var project in projects)
            {
                projectsData.Add(new ProjectData()
                {
                    Name = project.name
                });
            }
            return projectsData;
        }
    }
}
