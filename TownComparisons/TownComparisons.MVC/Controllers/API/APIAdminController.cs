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
using TownComparisons.MVC.ViewModels.Shared;
using TownComparisons.MVC.ViewModels.Admin;
using TownComparisons.MVC.Filters;
using System.IO;

namespace TownComparisons.MVC.Controllers.API
{
    [RoutePrefix("api")]
    public class APIAdminController : ApiController
    {
        private IService _service;
        private readonly string _operatorImagesFolder = HttpContext.Current.Server.MapPath("~/uploads/operator_images");

        public APIAdminController()
            : this (new Service())
        { }
        public APIAdminController(IService service)
        {
            _service = service;
        }
        

        /* A special get category for admin pages, where all OU and Property queries also are included (not only the selected ones) */
        [HttpGet]
        [Route("admin/category/{categoryId}")]
        public HttpResponseMessage GetCategory(HttpRequestMessage request, int categoryId)
        {
            Category category = _service.GetCategory(categoryId);
            if (category != null)
            {
                List<OrganisationalUnit> allOrganisationalUnits = _service.GetWebServiceOrganisationalUnits();
                List<PropertyQueryGroup> allPropertyQueryGroups = _service.GetWebServicePropertyQueries();
                CategoryWithUnusedViewModel model = new CategoryWithUnusedViewModel(category, allOrganisationalUnits, allPropertyQueryGroups);
                return request.CreateResponse<CategoryWithUnusedViewModel>(HttpStatusCode.OK, model);
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [HttpGet]
        [Route("admin/groupcategory/{groupCategoryId}")]
        public HttpResponseMessage GetGroupCategory(HttpRequestMessage request, int groupCategoryId)
        {
            GroupCategory groupCategory = _service.GetGroupCategory(groupCategoryId);
            if (groupCategory != null)
            {
                GroupCategoryViewModel model = new GroupCategoryViewModel(groupCategory);
                return request.CreateResponse<GroupCategoryViewModel>(HttpStatusCode.OK, model);
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [HttpGet]
        [Route("admin/category/{categoryId}/query/{queryId}")]
        public HttpResponseMessage GetCategoryQuery(HttpRequestMessage request, int categoryId, string queryId)
        {
            PropertyQueryInfo query = _service.GetPropertyQueryInfo(categoryId, queryId);
            if (query != null)
            {
                ViewModels.Admin.PropertyQueryInfoViewModel model = new ViewModels.Admin.PropertyQueryInfoViewModel(new ViewModels.Shared.PropertyQueryInfoViewModel(query));
                return request.CreateResponse<ViewModels.Admin.PropertyQueryInfoViewModel>(HttpStatusCode.OK, model);
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }



        [HttpPost]
        [Route("admin/groupcategory/{groupCategoryId}")]
        [ValidateModel] //this will handle validation (and return with errors) before method is run
        public HttpResponseMessage UpdateGroupCategory(HttpRequestMessage request, int groupCategoryId, [FromBody]GroupCategoryViewModel groupCategory)
        {
            GroupCategory existing = _service.GetGroupCategory(groupCategoryId);
            if (existing != null)
            {
                //update entity model
                existing = groupCategory.ToEntity(existing);

                //save it
                if (_service.UpdateGroupCategory(existing))
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("admin/category/{categoryId}")]
        [ValidateModel] //this will handle validation (and return with errors) before method is run
        public HttpResponseMessage UpdateCategory(HttpRequestMessage request, int categoryId, [FromBody]CategoryViewModel category)
        {
            Category existing = _service.GetCategory(categoryId);
            if (existing != null)
            {
                //save list of earlier ous and pqs
                List<OrganisationalUnitInfo> earlierOUs = new List<OrganisationalUnitInfo>(existing.OrganisationalUnits);
                List<PropertyQueryInfo> earlierPQs = new List<PropertyQueryInfo>(existing.Queries);

                //update entity model
                existing = category.ToEntity(existing);

                //save it
                if (_service.UpdateCategory(existing, earlierOUs, earlierPQs))
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }


        [HttpPost]
        [Route("admin/groupcategory/{groupCategoryId}/delete")]
        public HttpResponseMessage DeleteGroupCategory(HttpRequestMessage request, int groupCategoryId)
        {
            GroupCategory groupCategory = _service.GetGroupCategory(groupCategoryId);
            if (groupCategory != null)
            {
                if (_service.DeleteGroupCategory(groupCategory))
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [HttpPost]
        [Route("admin/category/{categoryId}/delete")]
        public HttpResponseMessage DeleteCategory(HttpRequestMessage request, int categoryId)
        {
            Category category = _service.GetCategory(categoryId);
            if (category != null || true == false)
            {
                if (_service.DeleteCategory(category))
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [HttpGet]
        [Route("admin/newgroupcategory")]
        public HttpResponseMessage NewGroupCategory(HttpRequestMessage request)
        {
            GroupCategory groupCategory = new GroupCategory();
            GroupCategoryViewModel model = new GroupCategoryViewModel(groupCategory);
            return request.CreateResponse<GroupCategoryViewModel>(HttpStatusCode.OK, model);
        }

        [HttpGet]
        [Route("admin/groupcategory/{groupCategoryId}/newcategory")]
        public HttpResponseMessage NewCategory(HttpRequestMessage request, int groupCategoryId)
        {
            GroupCategory groupCategory = _service.GetGroupCategory(groupCategoryId);
            if (groupCategory != null)
            {
                Category category = new Category()
                {
                    GroupCategory = groupCategory
                };
                List<OrganisationalUnit> allOrganisationalUnits = _service.GetWebServiceOrganisationalUnits();
                List<PropertyQueryGroup> allPropertyQueryGroups = _service.GetWebServicePropertyQueries();
                CategoryWithUnusedViewModel model = new CategoryWithUnusedViewModel(category, allOrganisationalUnits, allPropertyQueryGroups);
                return request.CreateResponse<CategoryWithUnusedViewModel>(HttpStatusCode.OK, model);
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [HttpPost]
        [Route("admin/insertgroupcategory")]
        [ValidateModel] //this will handle validation (and return with errors) before method is run
        public HttpResponseMessage InsertGroupCategory(HttpRequestMessage request, [FromBody]GroupCategoryViewModel groupCategory)
        {
            GroupCategory entity = groupCategory.ToEntity();

            //save it
            if (_service.InsertGroupCategory(entity))
            {
                GroupCategoryViewModel model = new GroupCategoryViewModel(entity);
                return request.CreateResponse<GroupCategoryViewModel>(HttpStatusCode.OK, model);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("admin/groupcategory/{groupCategoryId}/insertcategory")]
        [ValidateModel] //this will handle validation (and return with errors) before method is run
        public HttpResponseMessage InsertCategory(HttpRequestMessage request, int groupCategoryId, [FromBody]CategoryViewModel category)
        {
            if (groupCategoryId == category.GroupCategory.Id)
            {
                GroupCategory groupCategory = _service.GetGroupCategory(groupCategoryId);
                if (groupCategory != null)
                {
                    Category entity = category.ToEntity();

                    //add the new category to the group category
                    groupCategory.Categories.Add(entity);

                    //save it
                    if (_service.UpdateGroupCategory(groupCategory))
                    {
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }


        [HttpPost]
        [Route("admin/category/{categoryId}/operator/{organisationalUnitId}/image")]
        public HttpResponseMessage SaveOrganisationalUnitImage(HttpRequestMessage request, int categoryId, string organisationalUnitId) //, HttpPostedFileBase imageFile)
        {
            OrganisationalUnitInfo ou = _service.GetOrganisationalUnitInfo(categoryId, organisationalUnitId);
            if (ou != null)
            {
                var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
                if (file != null && file.ContentLength > 0)
                {
                    var path = "";

                    //delete any old image file
                    if (!String.IsNullOrWhiteSpace(ou.ImagePath))
                    {
                        path = Path.Combine(_operatorImagesFolder, ou.ImagePath);
                        if (File.Exists(path))
                        {
                            try
                            {
                                File.Delete(path);
                            }
                            catch { }
                        }
                    }

                    //generate new filename
                    string fileExtension = Path.GetExtension(file.FileName);
                    string filename = "";
                    while (path == "" || System.IO.File.Exists(path))
                    {
                        filename = Guid.NewGuid() + fileExtension;
                        path = Path.Combine(_operatorImagesFolder, filename);

                    }
                    
                    //save the new image file
                    try
                    {
                        file.SaveAs(path);

                        //update entity
                        ou.ImagePath = filename;
                        _service.UpdateOrganisationalUnitInfo(ou);

                        ViewModels.Shared.OrganisationalUnitInfoViewModel model = new ViewModels.Shared.OrganisationalUnitInfoViewModel(ou);
                        return request.CreateResponse<ViewModels.Shared.OrganisationalUnitInfoViewModel>(HttpStatusCode.OK, model);
                    }
                    catch { }

                    ModelState.AddModelError(null, "Kunde inte spara bilden.");
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("admin/category/{categoryId}/operator/{organisationalUnitId}")]
        [ValidateModel] //this will handle validation (and return with any errors) before method is run
        public HttpResponseMessage SaveOrganisationalUnit(HttpRequestMessage request, int categoryId, string organisationalUnitId, [FromBody]ViewModels.Shared.OrganisationalUnitInfoViewModel organisationalUnit)
        {
            OrganisationalUnitInfo ou = _service.GetOrganisationalUnitInfo(categoryId, organisationalUnitId);
            if (ou != null)
            {
                //transfer info from view model to existing entity
                ou = organisationalUnit.ToEntity(ou);
                _service.UpdateOrganisationalUnitInfo(ou);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }


        [HttpPost]
        [Route("admin/category/{categoryId}/query/{queryId}")]
        [ValidateModel] //this will handle validation (and return with any errors) before method is run
        public HttpResponseMessage SavePropertyQuery(HttpRequestMessage request, int categoryId, string queryId, [FromBody]ViewModels.Shared.PropertyQueryInfoViewModel propertyQuery)
        {
            PropertyQueryInfo query = _service.GetPropertyQueryInfo(categoryId, queryId);
            if (query != null)
            {
                //transfer info from view model to existing entity
                query = propertyQuery.ToEntity(query);
                _service.UpdatePropertyQueryInfo(query);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }



    }
}
