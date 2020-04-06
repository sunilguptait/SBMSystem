using System;
using System.Collections.Generic;
using System.Text;

namespace SBMS.Mobile.Services
{
   public class BaseService
    {
        protected readonly ApiClient _ApiClient;
        public BaseService()
        {
            _ApiClient = new ApiClient();
        }

    }
}
