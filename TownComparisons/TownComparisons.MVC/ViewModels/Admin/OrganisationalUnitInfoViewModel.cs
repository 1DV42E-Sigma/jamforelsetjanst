using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownComparisons.Domain.Entities;
using TownComparisons.Domain.Models;

namespace TownComparisons.MVC.ViewModels.Admin
{
    public class OrganisationalUnitInfoViewModel : Shared.OrganisationalUnitInfoViewModel
    {
        public bool Use { get; set; }

        //.. more properties from Shared.OrganisationalUnitViewModel


        public OrganisationalUnitInfoViewModel()
        {
            //Empty
        }
        public OrganisationalUnitInfoViewModel(OrganisationalUnit model, bool use = false)
        {
            OrganisationalUnitId = model.OrganisationalUnitId;
            Name = model.Name;

            Use = use;
        }
        public OrganisationalUnitInfoViewModel(Shared.OrganisationalUnitInfoViewModel baseViewModel, bool use = false)
        {
            Id = baseViewModel.Id;
            OrganisationalUnitId = baseViewModel.OrganisationalUnitId;
            CategoryID = baseViewModel.CategoryID;
            CategoryName = baseViewModel.CategoryName;
            Name = baseViewModel.Name;
            OrganisationalUnitId = baseViewModel.OrganisationalUnitId;
            Name = baseViewModel.Name;
            ShortDescription = baseViewModel.ShortDescription;
            LongDescription = baseViewModel.LongDescription;
            ImagePath = baseViewModel.ImagePath;
            Address = baseViewModel.Address;
            Telephone = baseViewModel.Telephone;
            Contact = baseViewModel.Contact;
            Email = baseViewModel.Email;
            OrganisationalForm = baseViewModel.OrganisationalForm;
            Website = baseViewModel.Website;
            Latitude = baseViewModel.Latitude;
            Longitude = baseViewModel.Longitude;
            Other = baseViewModel.Other;

            Use = use;
        }
    }
}