using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TownComparisons.Domain.Entities;

namespace TownComparisons.MVC.ViewModels.Shared
{
    public class OrganisationalUnitInfoViewModel
    {
        //private static string PhysicalUploadedImagesPath= System.Web.HttpContext.Current.Server.MapPath(".") + @"\Content\pictures\";

        private static string path = System.AppDomain.CurrentDomain.BaseDirectory + @"\Content\pictures\";

        public int Id { get; set; }

        [Required]
        public string OrganisationalUnitId { get; set; }

        [Required]
        public int CategoryID { get; set; }
        
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Namnet måste fyllas i.")]
        [MaxLength(100, ErrorMessage = "Namnet kan inte vara längre än {1} tecken.")]
        [MinLength(3, ErrorMessage = "Namnet måste vara minst {1} tecken.")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Korta beskrivningen måste fyllas i.")] //doesn't work when adding new ou to a category
        [MaxLength(500, ErrorMessage = "Korta beskrivningen kan inte vara längre än {1} tecken.")]
        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string ImagePath { get; set; }

        [MaxLength(255, ErrorMessage = "Adressen kan inte vara längre än {1} tecken.")]
        public string Address { get; set; }

        [MaxLength(50, ErrorMessage = "Telefon kan inte vara längre än {1} tecken.")]
        public string Telephone { get; set; }

        [MaxLength(100, ErrorMessage = "Kontakt kan inte vara längre än {1} tecken.")]
        public string Contact { get; set; }

        [MaxLength(100, ErrorMessage = "E-post kan inte vara längre än {1} tecken.")]
        [EmailAddress(ErrorMessage = "E-post är inte i korrekt format")]
        public string Email { get; set; }

        [MaxLength(50, ErrorMessage = "Organisations-form kan inte vara längre än {1} tecken.")]
        public string OrganisationalForm { get; set; } // Private, Public or some other form

        [MaxLength(100, ErrorMessage = "Webbsida kan inte vara längre än {1} tecken.")]
        public string Website { get; set; }
        
        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        [MaxLength(50, ErrorMessage = "Övrigt kan inte vara längre än {1} tecken.")]
        public string Other { get; set; }
        

        public OrganisationalUnitInfoViewModel()
        {
            //Empty
        }
        public OrganisationalUnitInfoViewModel(OrganisationalUnitInfo entity)
        {
            Id = entity.Id;

            if (entity.Category != null)
            {
                CategoryID = entity.Category.Id;
                CategoryName = entity.Category.Name;
            }
            
            OrganisationalUnitId = entity.OrganisationalUnitId;
            Name = entity.Name;
            ShortDescription = entity.ShortDescription;
            LongDescription = entity.LongDescription;

            if (entity.Category != null && (entity.ImagePath == ""))
            {
                if (entity.Category.Name == "Grundskola")
                {
                    ImagePath = "grund.jpg";
                }
                else if (entity.Category.Name == "Förskola")
                {
                    ImagePath = "dagis.jpg";
                }
                else if (entity.Category.Name == "Gymnasieskola")
                {
                    ImagePath = "gymnasieskola.jpg";
                }
                else if (entity.Category.Name == "Sjukhus")
                {
                    ImagePath = "sjukhus.jpg";
                }
                else if (entity.Category.Name == "Äldreboende")
                {
                    ImagePath = "elderly.jpg";
                }
                else if (entity.Category.Name == "Vårdcentral")
                {
                    ImagePath = "vardcentral.jpg";
                }
            }
            else if (entity.ImagePath != "")
            {
                ImagePath = entity.ImagePath;
            }
            else
            {
                ImagePath = "default.jpg";
            }
            Address = entity.Address;
            Telephone = entity.Telephone;
            Contact = entity.Contact;
            Email = entity.Email;
            OrganisationalForm = entity.OrganisationalForm;
            Website = entity.Website;
            Latitude = entity.Latitude;
            Longitude = entity.Longitude;
            Other = entity.Other;
        }

        public OrganisationalUnitInfo ToEntity(OrganisationalUnitInfo existing = null)
        {
            OrganisationalUnitInfo entity = (existing != null ? existing : new OrganisationalUnitInfo());

            entity.Id = this.Id;
            entity.OrganisationalUnitId = this.OrganisationalUnitId;
            entity.Name = this.Name;
            entity.ShortDescription = this.ShortDescription;
            entity.LongDescription = this.LongDescription;
            entity.ImagePath = this.ImagePath;
            entity.Address = this.Address;
            entity.Telephone = this.Telephone;
            entity.Contact = this.Contact;
            entity.Email = this.Email;
            entity.OrganisationalForm = this.OrganisationalForm;
            entity.Website = this.Website;
            entity.Latitude = this.Latitude;
            entity.Longitude = this.Longitude;
            entity.Other = this.Other;

            return entity;
        }
    }
}