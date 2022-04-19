using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ProjectRemovalTests : AuthTestBase
    {
        [Test]
        public void ProjectRemovalTest()
        {
            AccountData admin = new AccountData()
            {
                Username = "administrator",
                Password = "root1"
            };

            List<ProjectData> oldProjects = app.API.GetAllProjects(admin);

            if (oldProjects.Count == 0)
            {
                ProjectData project = new ProjectData()
                {
                    Name = "TestProject_" + GenerateRandomString(5)
                };

                app.API.CreateNewProject(admin, project);
                oldProjects = app.API.GetAllProjects(admin);
            }

            ProjectData toBeRemoved = oldProjects[0];

            app.Project.Remove(toBeRemoved);

            List<ProjectData> newProjects = app.API.GetAllProjects(admin);

            oldProjects.RemoveAt(0);

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}