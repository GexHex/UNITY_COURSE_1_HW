using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Mines
{
    public class MineTriggeredEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }
}
