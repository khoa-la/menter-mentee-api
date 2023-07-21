using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using _2MC.DataTier.Models;

namespace _2MC.DataTier.Repositories
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
    }

    public class CourseRepository : ICourseRepository
    {
        private readonly MentorMenteeConnectContext _context;
        
        public CourseRepository(MentorMenteeConnectContext context)
        {
            _context = context;
        }

        public IQueryable<Course> Get()
        {
            return _context.Courses;
        }

        public IQueryable<Course> Get(Expression<Func<Course, bool>> predicate)
        {
            return _context.Courses.Where(predicate);
        }

        public Course Get<TKey>(TKey id)
        {
            return _context.Courses.Find(id);
        }

        public async Task<Course> GetAsync<TKey>(TKey id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public Course Create(Course entity)
        {
            var result = _context.Courses.Add(entity);  
            _context.SaveChanges();
            return result.Entity;
        }

        public Course Update(Course entity)
        {
            var result = _context.Courses.Update(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public Course Delete(Course entity)
        {
            var result = _context.Remove(entity);
            _context.SaveChanges();
            return result.Entity;
        }
    }
}