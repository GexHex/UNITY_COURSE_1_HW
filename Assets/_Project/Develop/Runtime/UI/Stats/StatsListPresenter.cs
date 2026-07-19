using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using System.Collections.Generic;

using Assets._Project.Develop.Runtime.Meta.Features.ScoreCounter;

namespace Assets._Project.Develop.Runtime.UI.Stats
{
    public class StatsListPresenter : IPresenter
    {
        private readonly StatsService _statsService;
        private readonly ProjectPresentersFactory _presentersFactory;
        private readonly ViewsFactory _viewsFactory;
        private readonly IconTextListView _view;
        private readonly List<StatPresenter> _presenters = new();

        public StatsListPresenter(
            StatsService statsService,
            ProjectPresentersFactory presentersFactory,
            ViewsFactory viewsFactory,
            IconTextListView view)
        {
            _statsService = statsService;
            _presentersFactory = presentersFactory;
            _viewsFactory = viewsFactory;
            _view = view;
        }

        public void Initialize()
        {
            CreateStat(StatsType.Wins, _statsService.Wins);
            CreateStat(StatsType.Losses, _statsService.Losses);
        }

        public void Dispose()
        {
            foreach (var presenter in _presenters)
            {
                _view.Remove(presenter.View);
                _viewsFactory.Release(presenter.View);
                presenter.Dispose();
            }
            _presenters.Clear();
        }

        private void CreateStat(StatsType type, Utilities.Reactive.IReadOnlyVariable<int> statVariable)
        {
            IconTextView view = _viewsFactory.Create<IconTextView>(ViewIDs.CurrencyView);
            _view.Add(view);

            var presenter = _presentersFactory.CreateStatsPresenter(view, statVariable, type);
            presenter.Initialize();
            _presenters.Add(presenter);
        }
    }
}