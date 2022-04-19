using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace mantis_tests
{
    [TestFixture]
    public class AddNewIssue : TestBase
    {
        [Test]
        public void AddNewIssueTest()
        {
            AccountData account = new AccountData()
            {
                Username = "administrator",
                Password = "root1"
            };

            ProjectData project = new ProjectData()
            {
                Id = "4"
            };

            IssueData issue = new IssueData()
            {
                Summary = "some text summary",
                Description = "some text description",
                Category = "General"
        };

            app.API.CreateNewIssue(account, project, issue);
        }
    }
}
