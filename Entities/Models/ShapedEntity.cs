namespace Entities.Models
{
    public class ShapedEntity
    {
        public Entity Entity { get; set; }
        public Guid Id { get; set; }
        public ShapedEntity()
        {
            Entity = new Entity();              
        }
    }
}
