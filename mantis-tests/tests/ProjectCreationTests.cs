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


            List<ProjectData> oldProjects = app.Project.GetAllProjects();

            ProjectData project = new ProjectData()
            {
                Name = "TestProject_00"
            };

            app.Project.CreateNewProject(project);

            List<ProjectData> newProjects = app.Project.GetAllProjects();

            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}