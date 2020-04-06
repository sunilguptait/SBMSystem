using BMS.Data.Entities;
using BMS.Services.Common;
using BMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BMS.API.Controllers
{
    public class CommonController : BaseApiController
    {
        private readonly ICommonService _commonService;

        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;

        }
        [AllowAnonymous]
        [HttpGet]
        public ResponseModel<List<StateMaster>> GetStates()
        {
            var response = new ResponseModel<List<StateMaster>>();
            var states = _commonService.GetStates();
            response.Data = states;
            return response;
        }
        [AllowAnonymous]
        [HttpGet]
        public ResponseModel<List<CityMaster>> GetCities(int stateId)
        {
            var response = new ResponseModel<List<CityMaster>>();
            var cities = _commonService.GetCities(stateId);
            response.Data = cities;
            return response;
        }
        [HttpGet]
        public ResponseModel<List<BookTypeMaster>> GetBookTypes()
        {
            var response = new ResponseModel<List<BookTypeMaster>>();
            var bookTypes = _commonService.GetBookTypes();
            response.Data = bookTypes;
            return response;
        }

    }
}