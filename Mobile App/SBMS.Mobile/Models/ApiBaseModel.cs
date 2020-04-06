using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SBMS.Mobile.Models
{
    public class ApiBaseModel<T>
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
