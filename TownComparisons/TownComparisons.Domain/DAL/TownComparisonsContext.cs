using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TownComparisons.Domain.Abstract;
using TownComparisons.Domain.Entities;

namespace TownComparisons.Domain.DAL
{
    public class TownComparisonsContext : DbContext, ITownComparisonsContext
    {
        public IDbSet<OrganisationalUnitInfo> OrganisationalUnitInfos { get; set; }
        public IDbSet<PropertyQueryInfo> PropertyQueryInfos { get; set; }
        public IDbSet<GroupCategory> GroupCategories { get; set; }
        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Contact> Contacts { get; set; }


        public TownComparisonsContext()
            : base("TownComparisonsContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //removes plural to table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();


            //
            // This is for cascade delete to work when deleting a group category or category
            //
            modelBuilder.Entity<PropertyQueryInfo>().HasKey(q => q.Id)
                                                .HasRequired(q => q.Category)
                                                .WithMany(c => c.Queries)
                                                .WillCascadeOnDelete(true);
            
            modelBuilder.Entity<OrganisationalUnitInfo>().HasKey(o => o.Id)
                                                .HasRequired(o => o.Category)
                                                .WithMany(c => c.OrganisationalUnits)
                                                .WillCascadeOnDelete(true);
            

            modelBuilder.Entity<Category>().HasKey(c => c.Id)
                                                .HasRequired(c => c.GroupCategory)
                                                .WithMany(g => g.Categories)
                                                .WillCascadeOnDelete(true); 
            
            //not sure if this needs to be run?
            //base.OnModelCreating(modelBuilder);
        }
    }
}
