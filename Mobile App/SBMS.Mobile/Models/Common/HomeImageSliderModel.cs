using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SBMS.Mobile.Models.Common
{
    public class HomeImageSliderRootObjectDto
    {
        public HomeImageSliderRootObjectDto()
        {
            Items = new List<HomeImageSliderModel>();
        }

        [JsonProperty("items")]
        public IList<HomeImageSliderModel> Items { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "items";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(HomeImageSliderModel);
        }
    }

    public class HomeImageSliderModel
    {
        public int PictureId { get; set; }
        public string PictureUrl { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
        public string AltText { get; set; }
    }
}
