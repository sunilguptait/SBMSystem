using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMS.Data.Entities;

namespace BMS.Services.Common
{
    public interface ICommonService
    {
        List<StateMaster> GetStates();
        List<CityMaster> GetCities(int stateId);
        List<BookTypeMaster> GetBookTypes();
        List<SessionMaster> GetSessions();
        SessionMaster GetCurrentSession();
        EmailAccount GetEmailAccount();
    }
}
