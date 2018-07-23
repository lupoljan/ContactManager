using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ContactManagerApp.Models;

namespace ContactManagerApp.Controllers
{
    public class ContactController : ApiController
    {

        //Example data - it should come from database    
        public List<Contact> contacts = new List<Contact>
        {
            new Contact { Id = 1, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1995,04,03) ,Email = "mail@example.com", PhoneNumber ="123 345 678" },
            new Contact { Id = 2, FirstName = "Name2", LastName = "LastName2", DateOfBirth = new DateTime(1995,04,03), Email = "mail2@example.com", PhoneNumber ="123 345 678" },
            new Contact { Id = 3, FirstName = "Name3", LastName = "LastName3", DateOfBirth = new DateTime(1995,04,03), Email ="mail3@example.com", PhoneNumber ="123 345 678" },
            new Contact { Id = 4, FirstName = "Name4", LastName = "LastName4", DateOfBirth = new DateTime(1995,04,03), Email ="mail4@example.com", PhoneNumber ="123 345 678" },
            new Contact { Id = 5, FirstName = "Name5", LastName = "LastName5", DateOfBirth = new DateTime(1995,04,03), Email = "mail5@example.com", PhoneNumber ="123 345 678" }
        };

        //Constructors
        public ContactController() { }

        public ContactController(List<Contact> contacts)
        {
            this.contacts = contacts;
        }

        [HttpGet]
        public List<Contact> Get()
        {
            return contacts;
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var contact = contacts.FirstOrDefault((p) => p.Id == id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        public bool Post(Contact contact)
        {
            try
            {
                contacts.Add(contact);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            try
            {
                var item = contacts.Find((r) => r.Id == id);
                contacts.Remove(item);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
