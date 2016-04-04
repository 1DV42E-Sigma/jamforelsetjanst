using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownComparisons.Domain.Entities;

namespace TownComparisons.MVC.ViewModels.Shared
{
    public class CategoriesViewModel
    {
        public List<GroupCategoryViewModel> GroupCategories { get; set; }

        public CategoriesViewModel()
        {
            //Empty
        }
        public CategoriesViewModel(List<GroupCategory> groupCategories)
        {
            GroupCategories = groupCategories.Select(g => new GroupCategoryViewModel(g)).ToList();
        }
    }
}