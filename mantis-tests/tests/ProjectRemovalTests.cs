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
            List<ProjectData> oldProjects = app.Project.GetAllProjects();

            if (oldProjects.Count == 0)
            {
                ProjectData project = new ProjectData()
                {
                    Name = "NewProjectToDelete",
                };

                app.Project.CreateNewProject(project);
            }

            ProjectData toBeRemoved = oldProjects[0];

            app.Project.Remove(toBeRemoved);

            List<ProjectData> newProjects = app.Project.GetAllProjects();

            oldProjects.RemoveAt(0);

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}