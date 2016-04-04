using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TownComparisons.Domain.Entities;
using TownComparisons.Domain.Models;
using TownComparisons.Domain.WebServices.Models;

namespace TownComparisons.Domain.Abstract
{
    public interface ITownWebService : IDisposable
    {
        string GetName();

        List<OrganisationalUnit> GetAllOrganisationalUnits(string municipalityId);
        OrganisationalUnit GetOrganisationalUnit(string id);
        List<PropertyQueryWithResults> GetPropertyResults(List<PropertyQueryInfo> queries, List<OrganisationalUnitInfo> organisationalUnits);
        List<PropertyQueryGroup> GetAllPropertyQueries();
    }
}
