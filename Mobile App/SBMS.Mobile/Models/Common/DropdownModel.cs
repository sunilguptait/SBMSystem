using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SBMS.Mobile.Models.Common
{
    public class DropdownModel
    {
        public string Type { get; set; }
        public int Value { get; set; }
        public string Text { get; set; }
    }
}
