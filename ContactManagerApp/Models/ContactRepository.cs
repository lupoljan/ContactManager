using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactManagerApp.Models
{
    public class ContactRepository : IContactRepository
    {
        private List<Contact> contacts = new List<Contact>();
        private int _nextId = 1;

        public ContactRepository()
        {
            Add(new Contact
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1995, 04, 03),
                Email = "mail@example.com",
                PhoneNumber = "123 345 678"
            });
            Add(new Contact
            {
                Id = 2,
                FirstName = "Name2",
                LastName = "LastName2",
                DateOfBirth = new DateTime(1995, 04, 03),
                Email = "mail2@example.com",
                PhoneNumber = "123 345 678"
            });
            Add(new Contact
            {
                Id = 3,
                FirstName = "Name3",
                LastName = "LastName3",
                DateOfBirth = new DateTime(1995, 04, 03),
                Email = "mail3@example.com",
                PhoneNumber = "123 345 678"
            });
            Add(new Contact
            {
                Id = 4,
                FirstName = "Name4",
                LastName = "LastName4",
                DateOfBirth = new DateTime(1995, 04, 03),
                Email = "mail4@example.com",
                PhoneNumber = "123 345 678"
            });
            Add(new Contact
            {
                Id = 5,
                FirstName = "Name5",
                LastName = "LastName5",
                DateOfBirth = new DateTime(1995, 04, 03),
                Email = "mail5@example.com",
                PhoneNumber = "123 345 678"
            });
        }

        public IEnumerable<Contact> GetAll()
        {
            return contacts;
        }

        public Contact Get(int id)
        {
            return contacts.Find(p => p.Id == id);
        }

        public Contact Add(Contact item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            item.Id = _nextId++;
            contacts.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            contacts.RemoveAll(p => p.Id == id);
        }

        public bool Update(Contact item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = contacts.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            contacts.RemoveAt(index);
            contacts.Add(item);
            return true;
        }
    }
}