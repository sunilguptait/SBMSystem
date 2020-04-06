using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMS.Common;
using BMS.Data.Entities;
using BMS.ViewModels.Publisher;

namespace BMS.Services.Publisher
{
    public interface IPublisherService
    {
        PublisherMaster GetPublisherById(int id);
        PublisherMaster SavePublisher(PublisherMasterVM model);
        PagedList<PublisherMasterVM> GetPublishers(PublisherMasterSearchVM searchModel);
        List<PublisherMaster> GetListForDropdown(int bookSellerId = 0);
    }
}
