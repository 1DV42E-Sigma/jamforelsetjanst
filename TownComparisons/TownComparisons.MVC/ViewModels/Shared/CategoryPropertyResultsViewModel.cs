using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownComparisons.Domain.Models;

namespace TownComparisons.MVC.ViewModels.Shared
{
    public class CategoryPropertyResultsViewModel
    {
        public List<PropertyQueryWithResultsViewModel> QueryResults { get; set; }
        

        public CategoryPropertyResultsViewModel()
        {
            //Empty
        }
        public CategoryPropertyResultsViewModel(List<PropertyQueryWithResults> models)
        {
            QueryResults = models.Select(o => new PropertyQueryWithResultsViewModel(o)).ToList();
        }
    }
}