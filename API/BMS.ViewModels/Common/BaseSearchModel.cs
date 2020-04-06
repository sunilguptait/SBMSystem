using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.ViewModels.Common
{
    public class BaseSearchModel:BaseViewModel
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
    }
}
