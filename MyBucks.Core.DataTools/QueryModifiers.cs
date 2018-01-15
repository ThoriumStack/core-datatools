using MyBucks.Core.DataTools.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBucks.Core.DataTools
{
    public class QueryModifiers
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
        public static PaginatedReportReply<TViewModel> Paginate<TQuery, TViewModel>(IQueryable<TQuery> queryData, int pageNumber, int pageSize) where TViewModel : new()
        {
            var reply = new PaginatedReportReply<TViewModel>();
            reply.TotalRecordCount = queryData.Count();
            if (pageSize == 0)
            {
                pageSize = reply.TotalRecordCount;
            }
            if (reply.TotalRecordCount > 0)
            {
                reply.TotalPageCount = reply.TotalRecordCount / pageSize + (reply.TotalRecordCount % pageSize > 0 ? 1 : 0);
            }
            else
            {
                reply.TotalPageCount = 0;
                reply.ResultList = new List<TViewModel>();
                return reply;
            }
            reply.ResultList = queryData.Skip(pageNumber * pageSize).Take(pageSize).ToList().Select(TypeMapper.GetNewPopulatedObject<TQuery, TViewModel>).ToList();
            return reply;
        }

    }
}
