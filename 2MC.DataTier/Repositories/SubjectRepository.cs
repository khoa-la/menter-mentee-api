using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using _2MC.DataTier.Models;

namespace _2MC.DataTier.Repositories
{
    public interface ISubjectRepository : IBaseRepository<Subject>
    {
    }

    public class SubjectRepository : ISubjectRepository
    {
        private readonly MentorMenteeConnectContext _context;

        public SubjectRepository(MentorMenteeConnectContext context)
        {
            _context = context;
        }

        public IQueryable<Subject> Get()
        {
            return _context.Subjects;
        }

        public IQueryable<Subject> Get(Expression<Func<Subject, bool>> predicate)
        {
            return _context.Subjects.Where(predicate);
        }

        public Subject Get<TKey>(TKey id)
        {
            return _context.Subjects.Find(id);
        }

        public async Task<Subject> GetAsync<TKey>(TKey id)
        {
            return await _context.Subjects.FindAsync(id);
        }

        public Subject Create(Subject entity)
        {
            var result = _context.Subjects.Add(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public Subject Update(Subject entity)
        {
            var result = _context.Subjects.Update(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public Subject Delete(Subject entity)
        {
            var result = _context.Subjects.Remove(entity);
            _context.SaveChanges();
            return result.Entity;
        }
    }
}