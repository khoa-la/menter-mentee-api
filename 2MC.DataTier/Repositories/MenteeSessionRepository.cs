using _2MC.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _2MC.DataTier.Repositories
{
    public interface IMenteeSessionRepository : IBaseRepository<MenteeSession> { }

    public class MenteeSessionRepository : IMenteeSessionRepository
    {
        private readonly MentorMenteeConnectContext _context;
        public MenteeSessionRepository(MentorMenteeConnectContext context)
        {
            _context = context;
        } 
        public MenteeSession Create(MenteeSession entity)
        {
            var result = _context.MenteeSessions.Add(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public MenteeSession Delete(MenteeSession entity)
        {
            var result = _context.MenteeSessions.Remove(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public IQueryable<MenteeSession> Get()
        {
            return _context.MenteeSessions;
        }

        public IQueryable<MenteeSession> Get(Expression<Func<MenteeSession, bool>> predicate)
        {
            return _context.MenteeSessions.Where(predicate);
        }

        public MenteeSession Get<TKey>(TKey id)
        {
            return _context.MenteeSessions.Find(id);
        }

        public async Task<MenteeSession> GetAsync<TKey>(TKey id)
        {
            return await _context.MenteeSessions.FindAsync(id);
        }

        public MenteeSession Update(MenteeSession entity)
        {
            var result = _context.MenteeSessions.Update(entity);
            _context.SaveChanges();
            return result.Entity;
        }
    }
}
