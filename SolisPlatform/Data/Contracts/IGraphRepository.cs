using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IGraphRepository
    {
        void InsertGraphStats(List<APISuccessResponses> apiresponse);
    }
}
