using System;
using System.Collections.Generic;
using System.Linq;
using MyBucks.Core.DataTools;
using Xunit;

namespace Tests
{
    public class QueryTests
    {
        [Fact]
        public void TestPaginate()
        {
            var lst = new List<InputModel>();

            for (var i = 0; i < 122; i++)
            {
                lst.Add(new InputModel { MyIntVal = i, MyVal = $"Num {i}" });
            }

            var qry = QueryModifiers.Paginate<InputModel, OutputModel>(lst.AsQueryable(), 0, 10);
            Assert.NotEmpty(qry.ResultList);
            Assert.Equal("Num 9", qry.ResultList[9].MyVal); 
            Assert.Equal(10, qry.ResultList.Count);
            Assert.Equal(13, qry.TotalPageCount);
            Assert.Equal(122,qry.TotalRecordCount);

        }

        public class InputModel
        {
            public string MyVal { get; set; }
            public int MyIntVal { get; set; }
        }

        public class OutputModel
        {
            public string MyVal { get; set; }
            public int MyIntVal { get; set; }
        }
    }
}
