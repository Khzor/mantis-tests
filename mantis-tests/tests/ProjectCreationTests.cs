using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {
        [Test]
        public void ProjectCreationTest()
        {
            AccountData admin = new AccountData()
            {
                Username = "administrator",
                Password = "root1"
            };

            List<ProjectData> oldProjects = app.API.GetAllProjects(admin);

            ProjectData project = new ProjectData()
            {
                Name = "TestProject_"
            };

            foreach (ProjectData p in oldProjects)
            {
                if (p.Name == project.Name)
                {
                    project.Name = project.Name + GenerateRandomString(5);
                }
            }

            app.Project.CreateNewProject(project);

            List<ProjectData> newProjects = app.API.GetAllProjects(admin);

            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}