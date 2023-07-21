using System;//N
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2MC.DataTier.Models;

namespace _2MC.DataTier.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
    }
    public class UserRepository : IUserRepository
    {
        private readonly MentorMenteeConnectContext _context;

        public UserRepository(MentorMenteeConnectContext context)
        {
            _context = context;
        }
        public User Create(User entity)
        {
            var result = _context.Users.Add(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public User Delete(User entity)
        {
            var result = _context.Remove(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public IQueryable<User> Get()
        {
            return _context.Users;
        }

        public IQueryable<User> Get(System.Linq.Expressions.Expression<Func<User, bool>> predicate)
        {
            return _context.Users.Where(predicate);
        }

        public User Get<TKey>(TKey id)
        {
            return _context.Users.Find(id);
        }

        public async Task<User> GetAsync<TKey>(TKey id)
        {
            return await _context.Users.FindAsync(id);
        }

        public User Update(User entity)
        {
            var result = _context.Users.Update(entity);
            _context.SaveChanges();
            return result.Entity;
        }
    }
}
