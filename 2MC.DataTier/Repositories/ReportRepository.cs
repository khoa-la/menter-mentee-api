using _2MC.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _2MC.DataTier.Repositories
{
    public interface IReportRepository : IBaseRepository<Report> { }
    public class ReportRepository : IReportRepository
    {
        private readonly MentorMenteeConnectContext _context;

        public ReportRepository(MentorMenteeConnectContext context)
        {
            _context = context;
        }
        public Report Create(Report entity)
        {
            var result = _context.Reports.Add(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public Report Delete(Report entity)
        {
            var result = _context.Reports.Remove(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public IQueryable<Report> Get()
        {
            return _context.Reports;
        }

        public IQueryable<Report> Get(Expression<Func<Report, bool>> predicate)
        {
            return _context.Reports.Where(predicate);
        }

        public Report Get<TKey>(TKey id)
        {
            return _context.Reports.Find(id);
        }

        public async Task<Report> GetAsync<TKey>(TKey id)
        {
            return await _context.Reports.FindAsync(id);
        }

        public Report Update(Report entity)
        {
            var result = _context.Reports.Update(entity);
            _context.SaveChanges();
            return result.Entity;
        }
    }
}
