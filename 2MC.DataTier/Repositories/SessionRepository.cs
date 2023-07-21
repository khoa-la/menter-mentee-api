using _2MC.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _2MC.DataTier.Repositories
{
    public interface ISessionRepository : IBaseRepository<Session> { }
    public class SessionRepository : ISessionRepository
    {
        private readonly MentorMenteeConnectContext _context;
        public SessionRepository(MentorMenteeConnectContext context)
        {
            _context = context;
        }
        public Session Create(Session entity)
        {
            var result = _context.Sessions.Add(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public Session Delete(Session entity)
        {
            var result = _context.Remove(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public IQueryable<Session> Get()
        {
            return _context.Sessions;
        }

        public IQueryable<Session> Get(Expression<Func<Session, bool>> predicate)
        {
            return _context.Sessions.Where(predicate);
        }

        public Session Get<TKey>(TKey id)
        {
            return _context.Sessions.Find(id);
        }

        public async Task<Session> GetAsync<TKey>(TKey id)
        {
            return await _context.Sessions.FindAsync(id);
        }

        public Session Update(Session entity)
        {
            var result = _context.Sessions.Update(entity);
            _context.SaveChanges();
            return result.Entity;
        }
    }
}
