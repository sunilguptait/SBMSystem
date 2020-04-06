using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMS.Common;
using BMS.Data;
using BMS.Data.Entities;
using BMS.ViewModels.Publisher;

namespace BMS.Services.Publisher
{
    public class PublisherService : IPublisherService
    {
        BMSContext bMSContext = new BMSContext();

        public PublisherMaster GetPublisherById(int id)
        {
            return bMSContext.PublisherMaster.Where(m => m.Publisher_id == id).FirstOrDefault();
        }
        public PublisherMaster SavePublisher(PublisherMasterVM model)
        {
            var entity = GetPublisherById(Convert.ToInt32(model.Id));
            if (entity == null)
            {
                entity = new PublisherMaster();
            }
            entity.Publisher_id = model.Id;
            entity.Publisher_Name = model.Name;
            entity.Publisher_Address = model.Address;
            entity.Publisher_MobileNo = model.MobileNo;
            entity.Publisher_BSMId = model.UserId;

            if (entity.Publisher_id == 0)
            {
                bMSContext.PublisherMaster.Add(entity);
            }
            bMSContext.SaveChanges();
            return entity;
        }
        public PagedList<PublisherMasterVM> GetPublishers(PublisherMasterSearchVM searchModel)
        {
            var publishers = (from BSM in bMSContext.PublisherMaster
                              orderby BSM.Publisher_id
                              where BSM.Publisher_BSMId==searchModel.UserId
                              select new PublisherMasterVM()
                              {
                                  Id = BSM.Publisher_id,
                                  Name = BSM.Publisher_Name,
                                  Address = BSM.Publisher_Address,
                                  MobileNo = BSM.Publisher_MobileNo
                              });
            return new PagedList<PublisherMasterVM>(publishers, searchModel.PageIndex, searchModel.PageSize, new string[] { searchModel.SortDirection }, new string[] { searchModel.SortBy });
        }

        public List<PublisherMaster> GetListForDropdown(int bookSellerId=0)
        {
            var publishers = bMSContext.PublisherMaster.Where(a => a.Publisher_BSMId == bookSellerId).ToList();
            return publishers;
        }
    }
}
