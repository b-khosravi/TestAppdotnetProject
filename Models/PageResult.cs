using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public class PageResult<T>
    {
        public class PagingInfo
        {
            public int PageNumber{ get;set;}
            public int PageSize{get;set;}
            public int  TotalRecords{get;set;}
            public int PageCount{get;set;}
        }
        public List<T> Data {get;  private set;}
        public PagingInfo Paging {get; private set;}
        public PageResult(IEnumerable<T> items,int pageNumber, int pageSize, int totalRecords)
        {
            Data= new List<T>(items);
            Paging= new PagingInfo
            {
                PageNumber=pageNumber,
                PageSize=pageSize,
                TotalRecords=totalRecords,
                PageCount=totalRecords>0
                ? (int) Math.Ceiling(totalRecords/(double)pageSize)
                :0
            };


        }
        

    }
}