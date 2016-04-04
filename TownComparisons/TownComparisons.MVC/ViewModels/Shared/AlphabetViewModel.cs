using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownComparisons.Domain.Entities;

namespace TownComparisons.MVC.ViewModels.Shared
{
    public class AlphabetViewModel
    {
        public List<CategoryViewModel> Categories { get; set; }
        public List<OrganisationalUnitsViewModel> OrganisationalUnits { get; set; }
        public List<GroupCategoryViewModel> GroupCategory { get; set; }

        public AlphabetViewModel()
        {
            //Empty
        }
        public AlphabetViewModel(List<Category> groupCategories)
        {
            GroupCategory = groupCategories.Select(g => new GroupCategoryViewModel(g.GroupCategory)).ToList();
            Categories = groupCategories.Select(c => new CategoryViewModel(c)).ToList();
            OrganisationalUnits = groupCategories.Select(c => new OrganisationalUnitsViewModel(c.OrganisationalUnits.ToList())).ToList();
        }
    }
}