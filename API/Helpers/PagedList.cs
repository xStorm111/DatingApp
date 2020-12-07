using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers

{
    //T = Type, means we will have a Type in PagedList
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync(); //Unavoidable, we need this method to work out with the number of rows from db

            //Quite confusing if you're seeing this for the first time
            // ^ = lets suppose pageSize is 5

            //Here's 2 examples:
            // 1)
            //You're in page number 1, so you do ((1-1) * 5^), this will be 0 * 5 = 0, so we will skip no records, which means we will take 5 records starting on first record
            // 2)
            //You're in page number 2, so you do ((2-1) * 5^), this will be 1 * 5 = 5, so we will skip 5 records, which means we will take 5 records starting on record 5
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}