using Data.DTO;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IMiscRepository
    {
        IEnumerable<RankingCalculationViewDTO> CalculateRanking(IEnumerable<string> PlantIds, string Provider);
        IEnumerable<RankingCalculationViewDTO> GetPlantsCapacity(string TableName, string Field, string Filter, IEnumerable<string> PlantIds);
        void FinalRanking(IEnumerable<Ranking> ranking);
    }
}
