using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TownComparisons.Domain;
using TownComparisons.Domain.Abstract;
using TownComparisons.Domain.Entities;
using TownComparisons.Domain.Models;
using TownComparisons.MVC.ModelBinders;
using TownComparisons.MVC.ViewModels;
using TownComparisons.MVC.ViewModels.Shared;

namespace TownComparisons.MVC.Controllers.API
{
    [RoutePrefix("api")]
    public class APICategoriesController : ApiController
    {
        private IService _service;

        public APICategoriesController()
            : this (new Service())
        { }
        public APICategoriesController(IService service)
        {
            _service = service;
        }


        [HttpGet]
        [Route("categories")]
        public HttpResponseMessage GetCategories(HttpRequestMessage request)
        {
            var categories = _service.GetAllCategories();
            CategoriesViewModel model = new CategoriesViewModel(categories);
            return request.CreateResponse<GroupCategoryViewModel[]>(HttpStatusCode.OK, model.GroupCategories.ToArray());
        }


        [HttpGet]
        [Route("categories/alphabet")]
        public HttpResponseMessage GetCategoriesBasedOnAlphabet(HttpRequestMessage request)
        {
            var categories = _service.GetAllCategoriesBasedOnAlphabet();
            AlphabetViewModel model = new AlphabetViewModel(categories);
            return request.CreateResponse<CategoryViewModel[]>(HttpStatusCode.OK, model.Categories.ToArray());
           
        }

        [HttpGet]
        [Route("category/{categoryId}/properties")]
        public HttpResponseMessage GetCategoryProperyResults(HttpRequestMessage request, int categoryId, [CommaDelimitedArrayModelBinder]string[] operators)
        {
            Category category = _service.GetCategory(categoryId);
            if (category != null)
            {
                List<OrganisationalUnitInfo> organisationalUnitsToCompare = category.OrganisationalUnits.Where(o => operators.Contains(o.OrganisationalUnitId)).ToList();
                if (organisationalUnitsToCompare.Count > 0)
                {
                    List<PropertyQueryWithResults> results = _service.GetWebServicePropertyResults(category, organisationalUnitsToCompare);
                    CategoryPropertyResultsViewModel model = new CategoryPropertyResultsViewModel(results);
                    return request.CreateResponse<PropertyQueryWithResultsViewModel[]>(HttpStatusCode.OK, model.QueryResults.ToArray());
                }
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [Route("category/{categoryId}")]
        public HttpResponseMessage GetCategory(HttpRequestMessage request, int categoryId)
        {
            Category category = _service.GetCategory(categoryId);
            if (category != null)
            {
                CategoryViewModel model = new CategoryViewModel(category);
                return request.CreateResponse<CategoryViewModel>(HttpStatusCode.OK, model);
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
        
        [HttpGet]
        [Route("category/{categoryId}/operator/{organisationalUnitId}")]
        public HttpResponseMessage GetCategoryOperator(HttpRequestMessage request, int categoryId, string organisationalUnitId)
        {
            OrganisationalUnitInfo ou = _service.GetOrganisationalUnitInfo(categoryId, organisationalUnitId);
            if (ou != null)
            {
                OrganisationalUnitInfoViewModel model = new OrganisationalUnitInfoViewModel(ou);
                return request.CreateResponse<OrganisationalUnitInfoViewModel>(HttpStatusCode.OK, model);
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [HttpGet]
        [Route("operators/{operatorsList}")]
        public HttpResponseMessage GetOperatorsByList(HttpRequestMessage request, string operatorsList)
        {
            List<OrganisationalUnitInfo> ou = _service.GetOrganisationalUnitsInfo(operatorsList);
            if (ou.Count > 0)
            {
                OrganisationalUnitsViewModel model = new OrganisationalUnitsViewModel(ou);
                return request.CreateResponse<OrganisationalUnitsViewModel>(HttpStatusCode.OK, model);
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [HttpGet]
        [Route("operators/{ouId}/contacts")]
        public HttpResponseMessage GetContactsByOu(HttpRequestMessage request, string ouId)
        {
            List<Contact> contacts = _service.GetContactsByOU(ouId);
            if (contacts != null)
            {
                ContactsViewModel model = new ContactsViewModel(contacts);
                return request.CreateResponse<ContactsViewModel>(HttpStatusCode.OK, model);
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}
