using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TownComparisons.Domain.Entities;

namespace TownComparisons.MVC.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Förnamnet måste fyllas i")]
        [MaxLength(100, ErrorMessage = "Förnamnet kan inte vara längre än {1} tecken.")]
        [MinLength(4, ErrorMessage = "Förnamnet måste vara minst {1} tecken långt.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Förnamnet måste fyllas i")]
        [MaxLength(100, ErrorMessage = "Förnamnet kan inte vara längre än {1} tecken.")]
        [MinLength(4, ErrorMessage = "Förnamnet måste vara minst {1} tecken långt.")]
        public string LastName { get; set; }

        [MaxLength(300, ErrorMessage = "Rollnamnet kan inte vara längre än {1} tecken.")]
        public string Role { get; set; }

        [MaxLength(300, ErrorMessage = "Rollnamnet kan inte vara längre än {1} tecken.")]
        public string Telephone { get; set; }

        [MaxLength(100, ErrorMessage = "E-post kan inte vara längre än {1} tecken.")]
        [EmailAddress(ErrorMessage = "E-post är inte i korrekt format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Måste innehåller ett ID till en aktör.")]
        public string OrganisationalUnitId { get; set; }

        [Required(ErrorMessage = "Måste innehåller ett namn till en aktör.")]
        public string OrganisationalUnitName { get; set; }


        public ContactViewModel()
        {
            // Empty.
        }

        public ContactViewModel(Contact entity)
        {
            Id = entity.Id;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            Role = entity.Role;
            Telephone = entity.Telephone;
            Email = entity.Email;
        }

        public Contact ToEntity(Contact existing = null)
        {
            Contact entity = (existing != null ? existing : new Contact());

            entity.Id = this.Id;
            entity.FirstName = this.FirstName;
            entity.LastName = this.LastName;
            entity.Role = this.Role;
            entity.Telephone = this.Telephone;
            entity.Email = this.Email;

            return entity;
        }
    }
}