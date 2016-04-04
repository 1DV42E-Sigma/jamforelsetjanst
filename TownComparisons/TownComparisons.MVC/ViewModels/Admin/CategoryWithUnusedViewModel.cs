using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownComparisons.Domain.Entities;
using TownComparisons.Domain.Models;
using TownComparisons.MVC.ViewModels.Shared;

namespace TownComparisons.MVC.ViewModels.Admin
{
    public class CategoryWithUnusedViewModel
    {
        public CategoryViewModel Category { get; set; }

        public List<OrganisationalUnitInfoViewModel> AllOrganisationalUnits { get; set; }
        public List<PropertyQueryGroupViewModel> AllPropertyQueryGroups { get; set; }


        public CategoryWithUnusedViewModel()
        {
            AllOrganisationalUnits = new List<OrganisationalUnitInfoViewModel>();
            AllPropertyQueryGroups = new List<PropertyQueryGroupViewModel>();
        }
        public CategoryWithUnusedViewModel(Category category, List<OrganisationalUnit> allOrganisationalUnits, List<PropertyQueryGroup> allPropertyQueryGroups)
        {
            Category = new CategoryViewModel(category);
            AllOrganisationalUnits = allOrganisationalUnits.Select(o => new OrganisationalUnitInfoViewModel(o)).ToList();
            AllPropertyQueryGroups = allPropertyQueryGroups.Select(p => new PropertyQueryGroupViewModel(p)).ToList();

            for (int i = 0; i < AllOrganisationalUnits.Count; i++)
            {
                Shared.OrganisationalUnitInfoViewModel existing = Category.OrganisationalUnits.Find(cou => cou.OrganisationalUnitId == AllOrganisationalUnits[i].OrganisationalUnitId);
                if (existing != null)
                {
                    AllOrganisationalUnits[i] = new OrganisationalUnitInfoViewModel(existing);
                    AllOrganisationalUnits[i].Use = true;
                }
                //ou.Use = (Category.OrganisationalUnits.Find(cou => cou.OrganisationalUnitId == ou.OrganisationalUnitId) != null);
            }

            for (int i = 0; i < AllPropertyQueryGroups.Count; i++)
            {
                for (int j = 0; j < AllPropertyQueryGroups[i].Queries.Count; j++)
                {
                    Shared.PropertyQueryInfoViewModel existing = Category.Queries.Find(q => q.QueryId == AllPropertyQueryGroups[i].Queries[j].QueryId);
                    if (existing != null)
                    {
                        AllPropertyQueryGroups[i].Queries[j] = new PropertyQueryInfoViewModel(existing, true);
                        AllPropertyQueryGroups[i].AnyQueriesToUse = true;
                    }
                }
            }
            /*
            foreach (PropertyQueryGroupViewModel qg in AllPropertyQueryGroups)
            {
                foreach (PropertyQueryViewModel q in qg.Queries)
                {
                    q.Use = (Category.Queries.Find(cq => cq.WebServiceName == q.WebServiceName && cq.QueryId == q.QueryId) != null);
                    if (q.Use) { qg.AnyQueriesToUse = true; }
                }
            }
            */
        }
    }
}