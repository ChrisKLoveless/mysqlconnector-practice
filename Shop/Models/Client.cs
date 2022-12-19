using System.Collections.Generic;

namespace Shop.Models
{
    public class Client
    {
        private static List<Client> _instances = new List<Client> { };
        public string Name { get; set; }
        public int Id { get; }
        public List<Order> Orders { get; set; }

        public Client(string clientName)
        {
            Name = clientName;
            _instances.Add(this);
            Id = _instances.Count;
            Orders = new List<Order> { };
        }

        public static void ClearAll()
        {
            _instances.Clear();
        }

        public static List<Client> GetAll()
        {
            return _instances;
        }

        public static Client Find(int searchId)
        {
            return _instances[searchId - 1];
        }

        public void AddOrder(Order order)
        {
            Orders.Add(order);
        }
    }
}