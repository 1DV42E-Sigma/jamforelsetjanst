using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TownComparisons.Domain.Entities;

namespace TownComparisons.MVC.ViewModels.Shared
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Namnet måste fyllas i")]
        [MaxLength(100, ErrorMessage = "Namnet kan inte vara längre än {1} tecken.")]
        [MinLength(4, ErrorMessage = "Namnet måste vara minst {1} tecken långt.")]
        public string Name { get; set; }

        [MaxLength(300, ErrorMessage = "Beskrivningen kan inte vara längre än {1} tecken.")]
        public string Description { get; set; }

        [Required]
        public List<PropertyQueryInfoViewModel> Queries { get; set; }

        [Required]
        public List<OrganisationalUnitInfoViewModel> OrganisationalUnits { get; set; }


        public GroupCategoryViewModel GroupCategory { get; set; }


        public CategoryViewModel()
        {
            //Empty
        }
        public CategoryViewModel(Category entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            Queries = entity.Queries.Select(q => new PropertyQueryInfoViewModel(q)).ToList();
            OrganisationalUnits = entity.OrganisationalUnits.Select(o => new OrganisationalUnitInfoViewModel(o)).ToList();
            GroupCategory = new GroupCategoryViewModel(entity.GroupCategory, false);
        }

        public Category ToEntity(Category existing = null)
        {
            Category entity = (existing != null ? existing : new Category());

            entity.Id = this.Id;
            entity.Name = this.Name;
            entity.Description = this.Description;
            entity.Queries = this.Queries.Select(q => q.ToEntity(existing != null ? existing.Queries.FirstOrDefault(exQ => exQ.Id == q.Id) : null)).ToList();
            entity.OrganisationalUnits = this.OrganisationalUnits.Select(o => o.ToEntity(existing != null ? existing.OrganisationalUnits.FirstOrDefault(exO => exO.Id == o.Id) : null)).ToList();

            return entity;
        }
    }
}