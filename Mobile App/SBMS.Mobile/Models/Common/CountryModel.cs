using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SBMS.Mobile.Models.Common
{
    public class CountryRootModel
    {
        public CountryRootModel()
        {
            Countries = new ObservableCollection<CountryModel>();
        }

        [JsonProperty("countries")]
        public ObservableCollection<CountryModel> Countries { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "countries";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(CountryModel);
        }
    }

    public partial class CountryModel
    {
        //private ICollection<StateProvinceModel> _stateProvinces;
        //private ICollection<ShippingMethodCountryMapping> _shippingMethodCountryMappings;

        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether billing is allowed to this country
        /// </summary>
        public bool AllowsBilling { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether shipping is allowed to this country
        /// </summary>
        public bool AllowsShipping { get; set; }

        /// <summary>
        /// Gets or sets the two letter ISO code
        /// </summary>
        public string TwoLetterIsoCode { get; set; }

        /// <summary>
        /// Gets or sets the three letter ISO code
        /// </summary>
        public string ThreeLetterIsoCode { get; set; }

        /// <summary>
        /// Gets or sets the numeric ISO code
        /// </summary>
        public int NumericIsoCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether customers in this country must be charged EU VAT
        /// </summary>
        public bool SubjectToVat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is limited/restricted to certain stores
        /// </summary>
        public bool LimitedToStores { get; set; }

        /// <summary>
        /// Gets or sets the state/provinces
        /// </summary>
        //public virtual ICollection<StateProvinceModel> StateProvinces
        //{
        //    get => _stateProvinces ?? (_stateProvinces = new List<StateProvinceModel>());
        //    protected set => _stateProvinces = value;
        //}

        ///// <summary>
        ///// Gets or sets the shipping method-country mappings
        ///// </summary>
        //public virtual ICollection<ShippingMethodCountryMapping> ShippingMethodCountryMappings
        //{
        //    get => _shippingMethodCountryMappings ?? (_shippingMethodCountryMappings = new List<ShippingMethodCountryMapping>());
        //    protected set => _shippingMethodCountryMappings = value;
        //}
    }
}
