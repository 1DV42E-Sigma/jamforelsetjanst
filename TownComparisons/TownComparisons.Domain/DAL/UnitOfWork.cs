using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TownComparisons.Domain.Abstract;
using TownComparisons.Domain.Entities;

namespace TownComparisons.Domain.DAL
{
    internal class UnitOfWork : IUnitOfWork
    {
        //Fields
        private TownComparisonsContext _context = new TownComparisonsContext();
        
        private IRepository<OrganisationalUnitInfo> _organisationalUnitInfoRepository;
        private IRepository<PropertyQueryInfo> _propertyQueryInfoRepository;
        private IRepository<GroupCategory> _groupCategoriesRepository;
        private IRepository<Category> _categoriesRepository;
        private IRepository<Contact> _contactsRepository;

        //Properties
        public IRepository<OrganisationalUnitInfo> OrganisationalUnitInfoRepository
        {
            get
            {
                return _organisationalUnitInfoRepository ?? (_organisationalUnitInfoRepository = new Repository<OrganisationalUnitInfo>(_context));
            }
        }
        public IRepository<PropertyQueryInfo> PropertyQueryInfoRepository
        {
            get
            {
                return _propertyQueryInfoRepository ?? (_propertyQueryInfoRepository = new Repository<PropertyQueryInfo>(_context));
            }
        }
        public IRepository<GroupCategory> GroupCategoriesRepository
        {
            get
            {
                return _groupCategoriesRepository ?? (_groupCategoriesRepository = new Repository<GroupCategory>(_context));
            }
        }
        public IRepository<Category> CategoriesRepository
        {
            get
            {
                return _categoriesRepository ?? (_categoriesRepository = new Repository<Category>(_context));
            }
        }
        public IRepository<Contact> ContactsRepository
        {
            get
            {
                return _contactsRepository ?? (_contactsRepository = new Repository<Contact>(_context));
            }
        }



        /// <summary>
        /// Saves all changes made in the unit of work to the underlying database.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
        }


        #region IDisposable

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
