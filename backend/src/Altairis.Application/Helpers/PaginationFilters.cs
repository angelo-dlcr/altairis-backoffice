using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Application.Helpers
{
    public class PaginationFilters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        internal int Skip => (PageNumber - 1) * PageSize;
        internal int Take => PageSize;
    }
}
