using System.Linq;
using System.Linq.Dynamic.Core;
using Thorium.Core.DataTools.Model;
using Thorium.Core.Model;

namespace Thorium.Core.DataTools
{
    public static class SearchParameterExtensions
    {
        public static PaginatedListReply<TDto > Apply< TDto, TModel>(this SearchParameters parms, IQueryable<TModel> query) where TDto : new()
        {
            var finalQuery = query;
            if (!string.IsNullOrWhiteSpace(parms.FilterExpression))
            {
                finalQuery = query.Where(parms.FilterExpression, parms.FilterParameters);
            }

            if (parms.SortOrder == SortOrder.Ascending)
            {
                finalQuery = finalQuery
                    .OrderBy(parms.SortFieldName);
            }
            else
            {
                finalQuery = finalQuery.OrderBy($"{parms.SortFieldName} DESCENDING");
            }

            var result = finalQuery.Paginate<TModel, TDto>(parms.PageIndex, parms.PageSize);
            
            return result;

        }
    }
}