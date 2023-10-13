namespace TabulatorDynamicQueryBuilder.Models
{
    public class TabulatorOptionsModel
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 25;
        public TabulatorFilterModel[] Filter { get; set; } = Array.Empty<TabulatorFilterModel>();
        public TabulatorSortModel[] Sort { get; set; } = Array.Empty<TabulatorSortModel>();
    }

    public class TabulatorFilterModel
    {
        public string Field { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Value { get; set; } = null!;
    }

    public class TabulatorSortModel
    {
        public string Field { get; set; } = null!;
        public string Dir { get; set; } = null!;
    }
}
