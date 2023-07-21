using _2MC.DataTier.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace _2MC.DataTier.Repositories
{
    public interface ICertificateRepository : IBaseRepository<Certificate> { }
    public class CertificateRepository : ICertificateRepository
    {
        private MentorMenteeConnectContext _context;

        public CertificateRepository(MentorMenteeConnectContext context)
        {
            _context = context;
        }
        public Certificate Create(Certificate entity)
        {
            var result = _context.Certificates.Add(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public Certificate Delete(Certificate entity)
        {
            var result = _context.Certificates.Remove(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public IQueryable<Certificate> Get()
        {
            return _context.Certificates;
        }

        public IQueryable<Certificate> Get(Expression<Func<Certificate, bool>> predicate)
        {
            return _context.Certificates.Where(predicate);
        }

        public Certificate Get<TKey>(TKey id)
        {
            return _context.Certificates.Find(id);
        }

        public async Task<Certificate> GetAsync<TKey>(TKey id)
        {
            return await _context.Certificates.FindAsync(id);
        }

        public Certificate Update(Certificate entity)
        {
            var result = _context.Certificates.Update(entity);
            _context.SaveChanges();
            return result.Entity;
        }
    }
}