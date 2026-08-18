using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.ApplyDamage;
using Assets._Project.Develop.Runtime.Gameplay.Features.LifeCycle;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class LowestHealthDamageableTargetSelector : ITargetSelector
    {
        private readonly Entity _source;

        public LowestHealthDamageableTargetSelector(Entity source)
        {
            _source = source;
        }

        public Entity SelectTargetFrom(IEnumerable<Entity> targets)
        {
            Entity selectedTarget = null;

            float lowestHealth = float.MaxValue;

            foreach (Entity target in targets)
            {
                if (IsAvailable(target) == false)
                    continue;

                float health = target.CurrentHealth.Value;

                if (health < lowestHealth)
                {
                    lowestHealth = health;
                    selectedTarget = target;
                }
            }

            return selectedTarget;
        }

        private bool IsAvailable(Entity target)
        {
            if (target == null || target == _source || target.IsInit == false)
                return false;

            if (target.HasComponent<TakeDamageRequest>() == false || target.HasComponent<CurrentHealth>() == false)
                return false;

            if (target.TryGetCanApplyDamage(out ICompositeCondition canApplyDamage) && canApplyDamage.Evaluate() == false)
                return false;

            return target.CurrentHealth.Value > 0f;
        }
    }
}
