using Data.DTO;
using Data.Model;
using Data.Repository;
using Foundation.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mediator.Helpers
{
    public class CommonOperations
    {
        public static List<RankingCalculationViewDTO> RankingDetailView = new List<RankingCalculationViewDTO>();

        public static void CalculateAllProvidersRanking()
        {
            MiscRepository _misc = new MiscRepository();
            List<Ranking> ranking = new List<Ranking>();
            List<RankingHelper> rankinghelper = new List<RankingHelper>();
            if (RankingDetailView.Count() > 0)
            {
                foreach (var item in RankingDetailView)
                {
                    decimal TargetEnergy = item.SunHours * item.PlantCapacity * DateTime.Now.Day-1;
                    decimal TargetAchieved;
                    if (TargetEnergy > 0)
                    {
                        TargetAchieved = (item.Energy / TargetEnergy) * 100;
                    }
                    else
                    {
                        TargetAchieved = 0;
                    }
                    rankinghelper.Add(new RankingHelper { PlantId = item.PlantId, RankingPercentage = TargetAchieved, Category = item.PlantType });
                }


                rankinghelper.Select(c => c.Category).Distinct().ToList().ForEach(category =>
                {

                    var pool = rankinghelper.Where(x => x.Category.Equals(category)).Count() + 1;
                    int position = 1;
                    foreach (var item in rankinghelper.Where(x => x.Category.Equals(category)).OrderByDescending(x => x.RankingPercentage).ToList())
                    {
                        var date = DateTime.Now;

                        ranking.Add(new Ranking
                        {
                            PlantId = item.PlantId,
                            Rank = position,
                            RankingPercentage = 100 - (Convert.ToDecimal((position / Convert.ToDouble(pool)) * 100)),
                            CreatedOn = date,
                            UpdatedOn = date
                        });
                        position++;
                    }
                });
                _misc.FinalRanking(ranking);
                RankingDetailView.Clear();
            }

        }
    }
}
