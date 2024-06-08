using ALR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Data.Dto.PagingDto
{
    public class PagingListDto<T>
    {
        public PagingListDto()
        {
            Data = Enumerable.Empty<T>();
        }
        public IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; }

    }
}
