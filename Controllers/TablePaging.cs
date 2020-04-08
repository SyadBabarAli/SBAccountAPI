using System;
using System.Collections.Generic;

namespace SBAccountAPI.Controllers
{
    public class PagedResults<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalNumberOfPages { get; set; }
        public int TotalNumberOfRecords { get; set; }
        public string NextPageUrl { get; set; }
        public IEnumerable<T> Results { get; set; }
    }

    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }

        public string Searchable { get; set; }
        public string Search { get; set; }

        public string SortColumn { get; set; }
        public bool IsAsc { get; set; }
        public int FirstRowOnPage
        {

            get { return (CurrentPage - 1) * PageSize + 1; }
        }
        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }
    }

    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}