using Entities.Models;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;

namespace Repository.Extensions
{
    public static class RepositoryUserExtensions
    {

        public static IQueryable<User> FilterUser(this IQueryable<User> cars, string firstUserBrend, string lastUserBrend) =>
                cars.Where(e => (e.Brend[0] >= firstUserBrend[0] && e.Brend[0] <= lastUserBrend[0]));
        public static IQueryable<User> Search(this IQueryable<User> cars, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return cars;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return cars.Where(e => e.Brend.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<User> Sort(this IQueryable<User> cars, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return cars.OrderBy(e => e.Brend);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<User>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return cars.OrderBy(e => e.Brend);
            return cars.OrderBy(orderQuery);
        }
    }
}