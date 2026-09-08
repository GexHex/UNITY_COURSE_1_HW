using Assets._Project.Develop.Runtime.Utilities.DataManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Meta.Features.Statistics
{
    public class GameStatsService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly ReactiveVariable<int> _wins = new();
        private readonly ReactiveVariable<int> _losses = new();

        public GameStatsService(PlayerDataProvider playerDataProvider)
        {
            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public IReadOnlyVariable<int> Wins => _wins;
        public IReadOnlyVariable<int> Losses => _losses;

        public void RegisterWin() => _wins.Value++;

        public void RegisterLoss() => _losses.Value++;

        public void ReadFrom(PlayerData data)
        {
            _wins.Value = data.Wins;
            _losses.Value = data.Losses;
        }

        public void WriteTo(PlayerData data)
        {
            data.Wins = _wins.Value;
            data.Losses = _losses.Value;
        }
    }
}
