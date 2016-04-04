using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TownComparisons.Domain.Entities;

namespace TownComparisons.MVC.ViewModels.Shared
{
    public class GroupCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Namnet måste fyllas i")]
        [MaxLength(100, ErrorMessage = "Namnet kan inte vara längre än {1} tecken.")]
        [MinLength(2, ErrorMessage = "Namnet måste vara minst {1} tecken långt.")]
        public string Name { get; set; }

        public List<CategoryViewModel> Categories { get; set; }

        public GroupCategoryViewModel()
        {
            //Empty
        }
        public GroupCategoryViewModel(GroupCategory entity, bool useCategories = true)
        {
            Id = entity.Id;
            Name = entity.Name;

            if (useCategories)
            {
                Categories = entity.Categories.Select(c => new CategoryViewModel(c)).ToList();
            }
        }

        public GroupCategory ToEntity(GroupCategory existing = null)
        {
            GroupCategory entity = (existing != null ? existing : new GroupCategory());

            entity.Id = this.Id;
            entity.Name = this.Name;

            return entity;
        }
    }
}