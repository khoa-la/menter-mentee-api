using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using _2MC.DataTier.Models;

namespace _2MC.DataTier.Repositories
{
    public interface IMajorRepository : IBaseRepository<Major>
    {
    }

    public class MajorRepository : IMajorRepository
    {
        private readonly MentorMenteeConnectContext _context;

        public MajorRepository(MentorMenteeConnectContext context)
        {
            _context = context;
        }

        public IQueryable<Major> Get()
        {
            return _context.Majors;
        }

        public IQueryable<Major> Get(Expression<Func<Major, bool>> predicate)
        {
            return _context.Majors.Where(predicate);
        }

        public Major Get<TKey>(TKey id)
        {
            return _context.Majors.Find(id);
        }

        public async Task<Major> GetAsync<TKey>(TKey id)
        {
            return await _context.Majors.FindAsync(id);
        }

        public Major Create(Major entity)
        {
            var result = _context.Majors.Add(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public Major Update(Major entity)
        {
            var result = _context.Majors.Update(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public Major Delete(Major entity)
        {
            var result = _context.Remove(entity);
            _context.SaveChanges();
            return result.Entity;
        }
    }
}