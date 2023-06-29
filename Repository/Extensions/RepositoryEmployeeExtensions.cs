using Entities;
using Repository.Extensions.Utility;
using System.ComponentModel.DataAnnotations;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace Repository.Extensions
{
    public static class RepositoryEmployeeExtensions
    {
        public static IQueryable<Employee> FilterEmployee(this IQueryable<Employee> employees, uint minAge, uint maxAge)
        {
            return employees.Where(e => (e.Age >= minAge && e.Age <= maxAge));
        }
        public static IQueryable<Employee> Search(this IQueryable<Employee> employees, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return employees;
            }
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return employees.Where(c => c.Name.ToLower().Contains(lowerCaseTerm));
        }
        public static IQueryable<Employee> Sort(this IQueryable<Employee> employees, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return employees.OrderBy(e => e.Name);
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Employee>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return employees.OrderBy(e => e.Name);
            }
            return employees.OrderBy(orderQuery);
        }
    }
}
