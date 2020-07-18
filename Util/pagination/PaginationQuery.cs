using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Intern.Util.pagination
{
    public class PaginationQuery<T>
    {
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public List<T> data { get; set; }
        public PaginationQuery()
        {

        }
        public PaginationQuery(List<T> items,int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;

            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            data = items;
        }

        public bool PreviousPage {
            get {
                return (PageNumber > 1);
            }
        }

        public bool NextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }

        public static PaginationQuery<T> CreateAsync(List<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var skip = (pageNumber - 1) * pageSize;
            
            var items = source.Skip(skip).Take(pageSize).ToList();

            return new PaginationQuery<T>(items, count, pageNumber, pageSize);

        }
    }
}
