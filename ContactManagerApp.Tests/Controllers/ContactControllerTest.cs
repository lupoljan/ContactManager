using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Web.Http.Results;
using ContactManagerApp.Controllers;
using ContactManagerApp.Models;


namespace ContactManagerApp.Tests.Controllers
{
    /// <summary>
    /// Summary description for ContactControllerTest
    /// </summary>
    [TestClass]
    public class ContactControllerTest
    {
        public ContactControllerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion


        [TestMethod]
        public void Get_ShouldReturnAllContacts()
        {
            var testContacts = GetTestContacts();
            var controller = new ContactController(testContacts);

            var result = controller.Get() as List<Contact>;
            Assert.AreEqual(testContacts.Count, result.Count);
        }

        [TestMethod]
        public void Get_ShouldReturnCorrectContact()
        {
            var testContacts = GetTestContacts();
            var controller = new ContactController(testContacts);

            var result = controller.Get(1) as OkNegotiatedContentResult<Contact>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testContacts[0].FirstName, result.Content.FirstName);
        }

        [TestMethod]
        public void Get_ShouldNotFindContact()
        {
            var controller = new ContactController(GetTestContacts());

            var result = controller.Get(999);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Post_ShouldAddNewContact()
        {
            var testContacts = GetTestContacts();
            var controller = new ContactController(testContacts);

            controller.Post(new Contact { Id = 6, FirstName = "Name6", LastName = "LastName6" });
            Assert.IsNotNull(testContacts[5].Id);
            Assert.AreEqual(testContacts[5].Id, 6);
        }

        [TestMethod]
        public void Post_FirstNameRequired()
        {
            var testContacts = GetTestContacts();
            var controller = new ContactController(testContacts);

            controller.Post(new Contact { Id = 6, FirstName = "Name6", LastName = "LastName6" });
            Assert.IsNotNull(testContacts[5].FirstName);
            Assert.AreEqual(testContacts[5].FirstName, "Name6");
        }

        [TestMethod]
        public void Post_LastNameRequired()
        {
            var testContacts = GetTestContacts();
            var controller = new ContactController(testContacts);

            controller.Post(new Contact { Id = 6, FirstName = "Name6", LastName = "LastName6" });
            Assert.IsNotNull(testContacts[5].LastName);
            Assert.AreEqual(testContacts[5].LastName, "LastName6");
        }
        [TestMethod]
        public void Delete_ShouldDeleteContact()
        {
            var testContacts = GetTestContacts();
            var controller = new ContactController(testContacts);
            int id = 5;
            int count = testContacts.Count;

            controller.Delete(id);
            Assert.AreEqual(testContacts.Count, count-1);
        }

        private List<Contact> GetTestContacts()
        {
            var testContacts = new List<Contact>();
            testContacts.Add(new Contact { Id = 1, FirstName = "Name1", LastName = "LastName1" });
            testContacts.Add(new Contact { Id = 2, FirstName = "Name2", LastName = "LastName2" });
            testContacts.Add(new Contact { Id = 3, FirstName = "Name3", LastName = "LastName3" });
            testContacts.Add(new Contact { Id = 4, FirstName = "Name4", LastName = "LastName4" });
            testContacts.Add(new Contact { Id = 5, FirstName = "Name5", LastName = "LastName5" });

            return testContacts;
        }

    }
}
