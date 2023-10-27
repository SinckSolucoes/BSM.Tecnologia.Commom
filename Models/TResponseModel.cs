using Benner.OpenApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSM.Tecnologia.Commom.Models
{
    public class TResponseModel : IPagingResponseModel<object>
    {
        public string Filter { get; set; }
        public string OrderBy { get; set; }
        public bool MorePages { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        int? IPagingResponseModel<object>.PageIndex { get; set; }
        int? IPagingResponseModel<object>.PageSize { get; set; }
        public object[] Items { get; set; }
        public string BtnProximo { get; set; }
        public string BtnAnterior { get; set; }
    }
}
