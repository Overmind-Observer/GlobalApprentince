using System;
using System.Collections.Generic;
using System.Linq;
using Global_Intern.Models;

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
        public PaginationQuery(List<T> items, int count, int pageNumber, int pageSize)
        {
            // embedding new properties to the response
            PageNumber = pageNumber;
            PageSize = pageSize;

            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            data = items;
        }

        public bool PreviousPage
        {
            get
            {
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
            // Paging calculation
            var count = source.Count();
            var skip = (pageNumber - 1) * pageSize;

            //var ite = source.OrderBy(source)

            pageSize = count-1;

            var items = source.Skip(skip).Take(pageSize).ToList();

            return new PaginationQuery<T>(items, count, pageNumber, pageSize);

        }

        //public static PaginationQuery<T> CreateAsync1(List<T> source, int pageNumber, int pageSize)
        //{
        //    // Paging calculation
        //    var count = source.Count();
        //    var skip = (pageNumber - 1) * pageSize;

        //    //var ite = source.OrderBy(source)

        //    //var item = source.RemoveAt(0);

        //    var items = source.Skip(skip).Take(2).ToList();

        //    //List<Course> items1 = items.RemoveAt(0);

        //    return new PaginationQuery<T>(items, count, pageNumber, pageSize);

        //}
    }
}
