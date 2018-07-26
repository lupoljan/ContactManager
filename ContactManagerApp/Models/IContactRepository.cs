using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerApp.Models
{
    interface IContactRepository
    {
        IEnumerable<Contact> GetAll();
        Contact Get(int id);
        Contact Add(Contact item);
        void Remove(int id);
        bool Update(Contact item);
    }
}
