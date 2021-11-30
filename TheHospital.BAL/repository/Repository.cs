using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHospital.DAL.Context;

namespace TheHospital.BAL.repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private Hospital Context;
        private DbSet<TEntity> set;
        public Repository()
        {
            Context = new Hospital();
            set = Context.Set<TEntity>();

        }

        public TEntity Add(TEntity entity)
        {
            set.Add(entity);
            return Context.SaveChanges() > 0 ? entity : null;
        }

        public IQueryable<TEntity> GetAll()
        {
            return set;
        }

        public List<TEntity> GetAllBind()
        {
            return GetAll().ToList();
        }

        public TEntity GetBy(params object[] Id)
        {
            return set.Find(Id);
        }

        public bool Remove(TEntity entity)
        {
            set.Remove(entity);
            return Context.SaveChanges() > 0;
        }

        public bool Update(TEntity entity)
        {
            set.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChanges() > 0;


            //Context.Entry(entity).State = EntityState.Modified;
            //return Context.SaveChanges() > 0;
        }
        public bool Save()
        {
            bool cond;
            if (Context.SaveChanges() == 0)
            {
                cond = false;
            }
            else
            {
                cond = true;
            }
            return cond;
        }

    }
}
