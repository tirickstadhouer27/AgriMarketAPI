using System.Collections.Generic;
using System.Linq;
using AgriMarketAPI.Models;
using AgriMarketAPI.Interfaces;

namespace AgriMarketAPI.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private static List<Order> _orders = new List<Order>();

        public IEnumerable<Order> GetAll() => _orders;

        public Order? GetById(int id) => _orders.FirstOrDefault(o => o.OrderId == id);

        public void Add(Order order)
        {
            order.OrderId = _orders.Count > 0 ? _orders.Max(o => o.OrderId) + 1 : 1;
            _orders.Add(order);
        }

        public void Delete(int id)
        {
            var order = GetById(id);
            if (order != null) _orders.Remove(order);
        }
    }
}