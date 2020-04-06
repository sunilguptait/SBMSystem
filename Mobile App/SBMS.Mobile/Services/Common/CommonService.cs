using SBMS.Mobile.Models;
using SBMS.Mobile.Models.Common;
using SBMS.Mobile.Services.Caching;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace SBMS.Mobile.Services.Common
{
    public class CommonService : BaseService, ICommonService
    {
        private const string ImageSliderKey = "slider.images";
        private const string StatesKey = "states-";
        private const string CityKey = "cities-";
        private const string CountryKey = "countries";
        private ICacheService _cacheService;
        public CommonService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public async Task<ApiBaseModel<CountryRootModel>> GetCountries()
        {
            //var responce = await _ApiClient.Get<ObservableCollection<CountryModel>>("api/countries");
            //return responce;
            //var response = new ApiBaseModel<ObservableCollection<CountryModel>>();
            //response.Data = new ObservableCollection<CountryModel>();
            //response.Data.Add(new CountryModel()
            //{
            //    Id = 1,
            //    Name = "United States",
            //    //StateProvinces = new List<StateProvinceModel>()
            //});
            //return response;

            var resp = _cacheService.Get(CountryKey, async () =>
            {
                var responce = await _ApiClient.Get<CountryRootModel>("api/countries");
                return responce;
            });
            return await resp;

        }
        public async Task<ApiBaseModel<ObservableCollection<StateModel>>> GetStates(int countryId=0)
        {
            //var resp = _cacheService.Get(StatesKey + countryId.ToString(), async () =>
            //  {
            //      var responce = await _ApiClient.Get<ObservableCollection<StateModel>>("api/common/GetStates");
            //      return responce;
            //  });
            //return await resp;
            var responce = await _ApiClient.Get<ObservableCollection<StateModel>>("api/common/GetStates");
            return responce;

            //var responce = await _ApiClient.Get<ObservableCollection<StateProvinceModel>>("api/countries");
            //return responce;
        }
        public async Task<ApiBaseModel<ObservableCollection<CityModel>>> GetCities(int stateId = 0)
        {
            var resp = _cacheService.Get(CityKey + stateId.ToString(), async () =>
            {
                var responce = await _ApiClient.Get<ObservableCollection<CityModel>>("api/common/GetCities?stateId=" + stateId);
                return responce;
            });
            return await resp;

            //var responce = await _ApiClient.Get<ObservableCollection<StateProvinceModel>>("api/countries");
            //return responce;
        }

        //NM
        public async Task<ApiBaseModel<ObservableCollection<DropdownModel>>> GetSchoolDropdown()
        {
            var responce = await _ApiClient.Get<ObservableCollection<DropdownModel>>("api/school/getschooldropdown");
            return responce;
        }
        public async Task<ApiBaseModel<ObservableCollection<DropdownModel>>> GetClassDropdown()
        {
            var responce = await _ApiClient.Get<ObservableCollection<DropdownModel>>("api/class/getclassdropdown");
            return responce;
        }
    }
}
