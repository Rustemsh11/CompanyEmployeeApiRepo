namespace Entities.LinkModels
{
    public class LinkCollectionWraper<T>:LinkResourceBase
    {
        List<T> Value { get; set; } = new List<T>();
        public LinkCollectionWraper()
        {

        }
        public LinkCollectionWraper(List<T> value)
        {
            Value = value;
        }
    }
}
