using System;
using System.Collections.Generic;
using System.Text;

namespace Solucionesetech.Dtos.Common.Response
{
    public class SearchPaginatedResponseDto<TEntity> where TEntity : class
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }

        public List<TEntity> data { get; set; }
      
    }
}
