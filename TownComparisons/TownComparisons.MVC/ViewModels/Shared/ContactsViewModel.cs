using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownComparisons.Domain.Entities;
using TownComparisons.Domain.Models;

namespace TownComparisons.MVC.ViewModels.Shared
{
    public class ContactsViewModel
    {
        public List<ContactViewModel> Contacts { get; set; }

        public ContactsViewModel()
        {
            //Empty
        }

        public ContactsViewModel(List<Contact> entities)
        {
            Contacts = entities.Select(c => new ContactViewModel(c)).ToList();
        }
    }
}