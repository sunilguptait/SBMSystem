using SBMS.Mobile.Models;
using SBMS.Mobile.Models.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace SBMS.Mobile.Services.Common
{
    public interface ICommonService
    {
        Task<ApiBaseModel<CountryRootModel>> GetCountries();
        Task<ApiBaseModel<ObservableCollection<StateModel>>> GetStates(int countryId);
        Task<ApiBaseModel<ObservableCollection<CityModel>>> GetCities(int stateId = 0);
        Task<ApiBaseModel<ObservableCollection<DropdownModel>>> GetSchoolDropdown();
        Task<ApiBaseModel<ObservableCollection<DropdownModel>>> GetClassDropdown();
    }
}
