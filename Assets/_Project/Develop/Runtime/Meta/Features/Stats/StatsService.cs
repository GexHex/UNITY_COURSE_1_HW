using Assets._Project.Develop.Runtime.Utilities.DataManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Meta.Features.ScoreCounter
{
    public class StatsService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly ReactiveVariable<int> _wins = new(0);
        private readonly ReactiveVariable<int> _losses = new(0);

        public IReadOnlyVariable<int> Wins => _wins;
        public IReadOnlyVariable<int> Losses => _losses;

        public StatsService(PlayerDataProvider playerDataProvider)
        {
            playerDataProvider.RegisterReader(this);
            playerDataProvider.RegisterWriter(this);
        }

        public void AddWin()
        {
            _wins.Value += 1;
        }

        public void AddLoss()
        {
            _losses.Value += 1;
        }

        public void ResetStats()
        {
            _wins.Value = 0;
            _losses.Value = 0;
        }

        public void ReadFrom(PlayerData data)
        {
            _wins.Value = data.Wins;
            _losses.Value = data.Loses;
        }

        public void WriteTo(PlayerData data)
        {
            data.Wins = _wins.Value;
            data.Loses = _losses.Value;
        }
    }
}