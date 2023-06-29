using Entities.Models;

namespace Entities.LinkModels
{
    public class LinkResponse
    {
        public bool HasLinks { get; set; }
        public List<Entity> ShapedEntities { get; set; }
        public LinkCollectionWraper<Entity> LinkedEntities { get; set; }
        public LinkResponse() 
        {
            LinkedEntities= new LinkCollectionWraper<Entity>();
            ShapedEntities = new List<Entity>();
        }
    }
}
