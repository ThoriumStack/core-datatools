using System.Collections.Generic;
using System.Linq;
using Thorium.Core.Model;

namespace Thorium.Core.DataTools
{
    public static class QueryModifiers
    {
        /// <summary>
        /// Turn any query data into paged data. Your query must be ordered first.
        /// </summary>
        /// <typeparam name="TQuery">Type of query data</typeparam>
        /// <typeparam name="TViewModel">optional different type for ViewModel. Will use TransferData</typeparam>
        /// <param name="queryData"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PaginatedListReply<TViewModel> Paginate<TQuery, TViewModel>(this IQueryable<TQuery> queryData, int pageNumber, int pageSize) where TViewModel : new()
        {
            var reply = new PaginatedListReply<TViewModel>();
            reply.TotalItems = queryData.Count();
            if (pageSize == 0)
            {
                pageSize = reply.TotalItems;
            }
            if (reply.TotalItems > 0)
            {
                reply.TotalPages = reply.TotalItems / pageSize + (reply.TotalItems % pageSize > 0 ? 1 : 0);
            }
            else
            {
                reply.TotalPages = 0;
                reply.ResultList = new List<TViewModel>();
                return reply;
            }
            reply.ResultList = queryData.Skip(pageNumber * pageSize).Take(pageSize).ToList().Select(TypeMapper.GetNewPopulatedObject<TQuery, TViewModel>).ToList();
            return reply;
        }

    }
}
