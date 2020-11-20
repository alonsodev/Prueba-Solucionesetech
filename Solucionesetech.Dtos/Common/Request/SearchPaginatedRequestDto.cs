using System;
using System.Collections.Generic;
using System.Text;

namespace Solucionesetech.Dtos.Common.Request
{
    public class SearchPaginatedRequestDto
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<ColumnRequestDto> columns { get; set; }
        public SearchRequestDto search { get; set; }
        public List<OrderRequestDto> order { get; set; }
        public int consultation_id { get; set; }
    }

    public class ColumnRequestDto
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public SearchRequestDto search { get; set; }
    }

    public class SearchRequestDto
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class OrderRequestDto
    {
        public int column { get; set; }
        public string dir { get; set; }
    }
}

