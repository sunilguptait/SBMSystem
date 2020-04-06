using BMS.Data.Entities;
using BMS.Services.Publisher;
using BMS.ViewModels;
using BMS.ViewModels.Common;
using BMS.ViewModels.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BMS.API.Controllers
{
    public class PublisherController : BaseApiController
    {
        IPublisherService _PublisherService;
        public PublisherController(IPublisherService PublisherService)
        {
            _PublisherService = PublisherService;
        }

        [HttpPost]
        public ResponseModel<string> Create(PublisherMasterVM model)
        {
            var response = new ResponseModel<string>();
            var PublisherDetails = _PublisherService.SavePublisher(model);
            if (PublisherDetails != null)
            {
                response.Data = model.Id > 0 ? "Publisher updated successfully" : "Publisher created successfully";
            }
            else
            {
                response.ErrorMessage = "Something went wrong. Please try again.";
            }
            return response;
        }

        [HttpPost]
        public ResponseModel<List<PublisherMasterVM>> List(PublisherMasterSearchVM model)
        {
            var response = new ResponseModel<List<PublisherMasterVM>>();
            var PublishersList = _PublisherService.GetPublishers(model);
            response.Data = PublishersList.List;
            response.TotalItems = PublishersList.ItemCount;
            return response;
        }

        [HttpGet]
        public ResponseModel<List<PublisherMaster>> GetListForDropdown()
        {
            var response = new ResponseModel<List<PublisherMaster>>();
            var PublishersList = _PublisherService.GetListForDropdown(SessionManager.UserId);
            response.Data = PublishersList;
            return response;
        }
    }
}
