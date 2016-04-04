using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownComparisons.Domain.Entities;
using TownComparisons.Domain.Models;

namespace TownComparisons.MVC.ViewModels.Shared
{
    public class OrganisationalUnitsViewModel
    {
        public List<OrganisationalUnitInfoViewModel> OrganisationalUnits { get; set; }

        public OrganisationalUnitsViewModel()
        {
            //Empty
        }

        public OrganisationalUnitsViewModel(List<Domain.Entities.OrganisationalUnitInfo> entities)
        {
            OrganisationalUnits = entities.Select(g => new OrganisationalUnitInfoViewModel(g)).ToList();
        }
    }
}