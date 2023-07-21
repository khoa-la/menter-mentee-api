using _2MC.DataTier.Models;//N
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _2MC.DataTier.Repositories
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
    }
    public class RoleRepository : IRoleRepository
    {
        private readonly MentorMenteeConnectContext _context;

        public RoleRepository(MentorMenteeConnectContext context)
        {
            _context = context;
        }

        public Role Create(Role entity)
        {
            var result = _context.Roles.Add(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public Role Delete(Role entity)
        {
            var result = _context.Remove(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public IQueryable<Role> Get()
        {
            return _context.Roles;
        }

        public IQueryable<Role> Get(Expression<Func<Role, bool>> predicate)
        {
            return _context.Roles.Where(predicate);
        }

        public Role Get<TKey>(TKey id)
        {
            return _context.Roles.Find(id);
        }

        public async Task<Role> GetAsync<TKey>(TKey id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public Role Update(Role entity)
        {
            var result = _context.Roles.Update(entity);
            _context.SaveChanges();
            return result.Entity;
        }
    }
}
