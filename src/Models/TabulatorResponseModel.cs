namespace TabulatorDynamicQueryBuilder.Models
{
    public class TabulatorResponseModel<T>
    {
        public int Last_page { get; set; }
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
    }
}
