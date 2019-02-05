namespace MyBucks.Core.DataTools.Model
{
    public class SearchParameters
    {
        public string SortFieldName { get; set; } = "Id";

        public int PageSize { get; set; } = 0;

        public int PageIndex { get; set; } = 0;

        public SortOrder SortOrder { get; set; } = SortOrder.Descending;

        public string FilterExpression { get; set; }

        public object[] FilterParameters { get; set; }
    }
    public enum SortOrder
    {
        Ascending = 1,
        Descending = 2,
    }
}