using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TownComparisons.Domain.Abstract;
using TownComparisons.Domain.DAL;
using TownComparisons.Domain.Entities;
using TownComparisons.Domain.Helpers;
using TownComparisons.Domain.Models;
using TownComparisons.Domain.WebServices;
using TownComparisons.Domain.WebServices.Models;

namespace TownComparisons.Domain
{
    public class Service : IService
    {
        private readonly ISettings _settings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITownWebService _townWebService;
        private readonly ICache _cache;

        //Constructors
        public Service()
            : this (new Settings(true), new UnitOfWork(), new KoladaTownWebService(), new CacheManager())
        {
            // Empty
        }
        
        public Service(Settings settings, IUnitOfWork unitOfWork, ITownWebService townWebService, ICache cacheManager)
        {
            _settings = settings;
            _unitOfWork = unitOfWork;
            _townWebService = townWebService;
            _cache = cacheManager;

            _settings.Save();
        }


        //Methods

        #region WebService functionality

        public OrganisationalUnit GetWebServiceOrganisationalUnit(string id)
        {
            string cacheKey = $"{"getOrganisationalUnitByID"}{id}";

            if (_cache.HasValue(cacheKey))
            {
                return (OrganisationalUnit)_cache.GetCache(cacheKey);
            }

            var ou = _townWebService.GetOrganisationalUnit(id);
            _cache.SetCache(cacheKey, ou, _settings.CacheSeconds_OrganisationalUnits);

            return ou;
        }

        public List<OrganisationalUnit> GetWebServiceOrganisationalUnits()
        {
            string id = _settings.MunicipalityId;
            string cacheKey = $"{"getAllOrganisationalUnitsFromWebService"}{id}";

            if (_cache.HasValue(cacheKey))
            {
                return (List<OrganisationalUnit>)_cache.GetCache(cacheKey);
            }

            var allOU = _townWebService.GetAllOrganisationalUnits(_settings.MunicipalityId);
            _cache.SetCache(cacheKey, allOU, _settings.CacheSeconds_OrganisationalUnits);

            return allOU;
        }

        public List<PropertyQueryGroup> GetWebServicePropertyQueries()
        {
            var list = new List<PropertyQueryGroup>();
            string cacheKey = "getAllPropertyQueries";

            //returns from cache if value is present, else returns from Webservice
            if (_cache.HasValue(cacheKey))
            {
                return (List<PropertyQueryGroup>)_cache.GetCache(cacheKey);
            }

            //Get from webService
            list = _townWebService.GetAllPropertyQueries();
            //Save to cache
            _cache.SetCache(cacheKey, list, _settings.CacheSeconds_PropertyQueries);

            return list;
        }

        public List<PropertyQueryWithResults> GetWebServicePropertyResults(Category category, List<OrganisationalUnitInfo> organisationalUnits) //List<string> queryIds, List<string> organisationalUnitIds) //List<PropertyQuery> queries, List<OrganisationalUnit> organisationalUnits)
        {
            //Get id's from All KpiQuestions and OrganisationalUnits in parameter
            //and compund to an unique cacheKey
            
            var queryIds = from q in category.Queries
                           select q.QueryId;
            var organisationalUnitIds = from ou in organisationalUnits
                                        select ou.OrganisationalUnitId;
            

            //adding all KPIQuestionId + ouIds 
            var cacheKey = "PropertyResults" + queryIds.Aggregate("", (current, kpi) => current + kpi);
            cacheKey = organisationalUnitIds.Aggregate(cacheKey, (current, ouId) => current + ouId);

            if (_cache.HasValue(cacheKey))
            {
                return (List<PropertyQueryWithResults>)_cache.GetCache(cacheKey);
            }

            var returnValue = _townWebService.GetPropertyResults(category.Queries.ToList(), organisationalUnits); //queryIds.ToList(), organisationalUnitIds.ToList());
            _cache.SetCache(cacheKey, returnValue, _settings.CacheSeconds_PropertyResult);

            return returnValue;
        }

        #endregion


        #region Database functionality

        public List<GroupCategory> GetAllCategories()
        {
            string cacheKey = "getAllCategories";

            if (_cache.HasValue(cacheKey))
            {
                return (List<GroupCategory>)_cache.GetCache(cacheKey);
            }

            var listOfAllGroupCategories = _unitOfWork.GroupCategoriesRepository.Get(null, null, "Categories").ToList();

            var listofAllCategories = GetAllCategoriesBasedOnAlphabet();

            foreach (Category cat in listofAllCategories)
            {
                listOfAllGroupCategories.Find(gc => gc.Id == cat.GroupCategory.Id).Categories.ToList().Find(ca => ca.Id == cat.Id).OrganisationalUnits = cat.OrganisationalUnits;
            }

            return listOfAllGroupCategories;

        }

        public List<Category> GetAllCategoriesBasedOnAlphabet()
        {
            string cacheKey = "getAllCategoriesBasedOnAlphabet";

            if (_cache.HasValue(cacheKey))
            {
                return (List<Category>)_cache.GetCache(cacheKey);
            }
            var listOfAllCategories = _unitOfWork.CategoriesRepository.Get(null, null, "GroupCategory, OrganisationalUnits").OrderBy(n => n.Name).ToList();

            return listOfAllCategories;

        }

        public GroupCategory GetGroupCategory(int id)
        {
            GroupCategory groupCategory = _unitOfWork.GroupCategoriesRepository.Get(c => c.Id == id, null, "Categories").FirstOrDefault();
            return groupCategory;
        }

        public Category GetCategory(int id)
        {
            Category category = _unitOfWork.CategoriesRepository.Get(c => c.Id == id, null, "GroupCategory, Queries, OrganisationalUnits").FirstOrDefault();
            return category;
        }

        public List<OrganisationalUnitInfo> GetOrganisationalUnitInfos()
        {
            string cacheKey = "getOrganisationalUnitInfos";

            if (_cache.HasValue(cacheKey))
            {
                return (List<OrganisationalUnitInfo>)_cache.GetCache(cacheKey);
            }

            var returnValue = _unitOfWork.OrganisationalUnitInfoRepository.Get().ToList();
            //Saves in cache for 10 seconds
            _cache.SetCache(cacheKey, returnValue, 10);

            return returnValue;
        }

        public OrganisationalUnitInfo GetOrganisationalUnitInfo(int categoryId, string organisationalUnitId)
        {
            var returnValue = _unitOfWork.OrganisationalUnitInfoRepository.Get(o => o.OrganisationalUnitId == organisationalUnitId && o.Category.Id == categoryId).FirstOrDefault();
            return returnValue;
        }

        public PropertyQueryInfo GetPropertyQueryInfo(int categoryId, string queryId)
        {
            var returnValue = _unitOfWork.PropertyQueryInfoRepository.Get(o => o.QueryId == queryId && o.Category.Id == categoryId).FirstOrDefault();
            
            return returnValue;
        }

        public List<OrganisationalUnitInfo> GetOrganisationalUnitsInfo(string operatorsList)
        {
            List<OrganisationalUnitInfo> oui = new List<OrganisationalUnitInfo>();
            string[] operators = operatorsList.Split(',');

            foreach (string ou in operators)
            {
                oui.Add(_unitOfWork.OrganisationalUnitInfoRepository.Get(o => o.OrganisationalUnitId == ou).FirstOrDefault());
            }

            return oui;
        }

        public List<Contact> GetContactsByOU(string organisationalUnitId)
        {
            return _unitOfWork.ContactsRepository.Get(c => c.OrganisationalUnitInfo.OrganisationalUnitId == organisationalUnitId).ToList() ?? new List<Contact>();
        }

        public bool UpdateOrganisationalUnitInfo(OrganisationalUnitInfo ou)
        {
            try
            {
                _unitOfWork.OrganisationalUnitInfoRepository.Update(ou);
                _unitOfWork.Save();
                return true;
            }
            catch { }

            return false;
        }


        public bool UpdatePropertyQueryInfo(PropertyQueryInfo propertyQuery)
        {
            try
            {
                _unitOfWork.PropertyQueryInfoRepository.Update(propertyQuery);
                _unitOfWork.Save();
                return true;
            }
            catch { }

            return false;
        }

        public bool DeleteGroupCategory(GroupCategory groupCategory)
        {
            try
            {
                _unitOfWork.GroupCategoriesRepository.Delete(groupCategory);
                _unitOfWork.Save();
                return true;
            }
            catch { }

            return false;
        }
        public bool DeleteCategory(Category category)
        {
            try
            {
                _unitOfWork.CategoriesRepository.Delete(category);
                _unitOfWork.Save();
                return true;
            }
            catch { }

            return false;
        }

        public bool UpdateGroupCategory(GroupCategory groupCategory)
        {
            try
            {
                _unitOfWork.GroupCategoriesRepository.Update(groupCategory);
                _unitOfWork.Save();
                return true;
            }
            catch { }

            return false;
        }
        public bool UpdateCategory(Category category, List<OrganisationalUnitInfo> earlierOrganisationalUnits = null, List<PropertyQueryInfo> earlierPropertyQueries = null)
        {
            try
            {
                if (earlierOrganisationalUnits != null)
                {
                    //do some checks that related associations are not removed in category (otherwise delete them)
                    List<OrganisationalUnitInfo> ouToRemove = earlierOrganisationalUnits.Where(eou => !category.OrganisationalUnits.Any(ou => ou.Id == eou.Id)).ToList();
                    foreach (OrganisationalUnitInfo ou in ouToRemove)
                    {
                        _unitOfWork.OrganisationalUnitInfoRepository.Delete(ou);
                    }
                }
                if (earlierPropertyQueries != null)
                { 
                    List<PropertyQueryInfo> pqToRemove = earlierPropertyQueries.Where(eq => !category.Queries.Any(q => q.Id == eq.Id)).ToList();
                    foreach (PropertyQueryInfo pq in pqToRemove)
                    {
                        _unitOfWork.PropertyQueryInfoRepository.Delete(pq);
                    }
                }

                _unitOfWork.CategoriesRepository.Update(category);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
                e = e;
            }

            return false;
        }

        public bool InsertGroupCategory(GroupCategory groupCategory)
        {
            try
            {
                _unitOfWork.GroupCategoriesRepository.Insert(groupCategory);
                _unitOfWork.Save();
                return true;
            }
            catch { }

            return false;
        }
        public bool InsertCategory(Category category)
        {
            try
            {
                _unitOfWork.CategoriesRepository.Insert(category);
                _unitOfWork.Save();
                return true;
            }
            catch { }

            return false;
        }



        #endregion
    }
}