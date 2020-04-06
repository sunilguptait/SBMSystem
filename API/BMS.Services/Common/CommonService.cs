using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMS.Data;
using BMS.Data.Entities;

namespace BMS.Services.Common
{
    public class CommonService : ICommonService
    {
        BMSContext bMSContext = new BMSContext();
        public List<StateMaster> GetStates()
        {
            return bMSContext.StateMaster.ToList();
        }
        public List<CityMaster> GetCities(int stateId)
        {
            return bMSContext.CityMaster.Where(a => stateId == 0 || a.StateId == stateId).ToList();
        }
        public List<BookTypeMaster> GetBookTypes()
        {
            return bMSContext.BookTypeMaster.ToList();
        }
        public List<SessionMaster> GetSessions()
        {
            return bMSContext.SessionMaster.ToList();
        }
        public SessionMaster GetCurrentSession()
        {
            return bMSContext.SessionMaster.Where(a=>a.Session_IsCurrent==true).FirstOrDefault();
        }
        public EmailAccount GetEmailAccount()
        {
            return bMSContext.EmailAccount.Where(a => a.IsActive == true).FirstOrDefault();
        }
    }
}
