using MyBucks.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBucks.Core.DataTools.Models
{
    public class PaginatedReportReply<T> : ListReply<T>
    {
        public int TotalRecordCount { get; set; }
        public int TotalPageCount { get; set; }
    }
}
