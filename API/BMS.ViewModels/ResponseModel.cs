using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.ViewModels
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success
        {
            get { return string.IsNullOrEmpty(this.ErrorMessage); }
        }
        public int TotalItems { get; set; }
    }
}
