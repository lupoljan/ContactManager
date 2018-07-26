using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
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

        private ContactRepository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            _repository = new ContactRepository();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _repository = null;
        }

        [TestMethod]
        public void Get_ShouldReturnAllContacts()
        {
            var testContacts = GetTestContacts();
            var result = _repository.GetAll();
            Assert.AreEqual(testContacts.Count, result.Count());
        }

        [TestMethod]
        public void Get_ShouldReturnCorrectContact()
        {
            var testContacts = GetTestContacts();
            var result = _repository.Get(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(testContacts[0].FirstName, result.FirstName);
        }

        [TestMethod]
        public void Get_ShouldNotFindContact()
        {
            var result = _repository.Get(999);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Post_ShouldAddNewContact()
        {
            var contact = new Contact
            {
                Id = 6,
                FirstName = "Name6",
                LastName = "LastName6"
            };

            var addedcontact = _repository.Add(contact);
            Assert.AreEqual(contact, addedcontact);
        }

        [TestMethod]
        public void Post_FirstNameRequired()
        {
            var contact = new Contact
            {
                Id = 6,
                FirstName = "Name6",
                LastName = "LastName6"
            };

            var addedcontact = _repository.Add(contact);
            Assert.AreEqual(contact, addedcontact);
            Assert.IsNotNull(addedcontact.FirstName);
        }

        [TestMethod]
        public void Post_LastNameRequired()
        {
            var contact = new Contact
            {
                Id = 6,
                FirstName = "Name6",
                LastName = "LastName6"
            };

            var addedcontact = _repository.Add(contact);
            Assert.AreEqual(contact, addedcontact);
            Assert.IsNotNull(addedcontact.LastName);
        }

        [TestMethod]
        public void Remove_ShouldDeleteContact()
        {
            int id = 5;
            _repository.Remove(id);
            Assert.IsNull(_repository.Get(id));
        }

        [TestMethod]
        public void Update_ShouldEditContact()
        {
            var contact = new Contact
            {
                Id = 6,
                FirstName = "Name6",
                LastName = "LastName6"
            };

            var addedcontact = _repository.Add(contact);

            addedcontact.Id = 10;
            addedcontact.FirstName = "Name10";
            addedcontact.LastName = "LastName10";

            var updatedcontact = _repository.Update(addedcontact);
            Assert.IsTrue(updatedcontact);
        }

        private List<Contact> GetTestContacts()
        {
            var testContacts = new List<Contact>();
            testContacts.Add(new Contact
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1995, 04, 03),
                Email = "mail@example.com",
                PhoneNumber = "123 345 678"
            });
            testContacts.Add(new Contact
            {
                Id = 2,
                FirstName = "Name2",
                LastName = "LastName2",
                DateOfBirth = new DateTime(1995, 04, 03),
                Email = "mail2@example.com",
                PhoneNumber = "123 345 678"
            });
            testContacts.Add(new Contact
            {
                Id = 3,
                FirstName = "Name3",
                LastName = "LastName3",
                DateOfBirth = new DateTime(1995, 04, 03),
                Email = "mail3@example.com",
                PhoneNumber = "123 345 678"
            });
            testContacts.Add(new Contact
            {
                Id = 4,
                FirstName = "Name4",
                LastName = "LastName4",
                DateOfBirth = new DateTime(1995, 04, 03),
                Email = "mail4@example.com",
                PhoneNumber = "123 345 678"
            });
            testContacts.Add(new Contact
            {
                Id = 5,
                FirstName = "Name5",
                LastName = "LastName5",
                DateOfBirth = new DateTime(1995, 04, 03),
                Email = "mail5@example.com",
                PhoneNumber = "123 345 678"
            });

            return testContacts;
        }

    }
}
