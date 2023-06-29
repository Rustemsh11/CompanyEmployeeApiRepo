using Entities.Models;
using System.Dynamic;

namespace Contracts
{
    public interface IDataShaper<T>
    {
        IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string fieldString);
        ShapedEntity ShapeData(T entity, string fieldString);
    }
}
