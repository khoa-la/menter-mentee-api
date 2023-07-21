using _2MC.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _2MC.DataTier.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order> { }
    public class OrderRepository : IOrderRepository
    {
        private readonly MentorMenteeConnectContext _context;
        public OrderRepository(MentorMenteeConnectContext context)
        {
            _context = context;
        }

        public Order Create(Order entity)
        {
            var result = _context.Orders.Add(entity);
            return result.Entity;
        }

        public Order Delete(Order entity)
        {
            var result = _context.Remove(entity);
            return result.Entity;
        }

        public IQueryable<Order> Get()
        {
            return _context.Orders;
        }

        public IQueryable<Order> Get(Expression<Func<Order, bool>> predicate)
        {
            return _context.Orders.Where(predicate);
        }

        public Order Get<TKey>(TKey id)
        {
            return _context.Orders.Find(id);
        }

        public async Task<Order> GetAsync<TKey>(TKey id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public Order Update(Order entity)
        {
            var result = _context.Orders.Update(entity);
            return result.Entity;
        }
    }
}
