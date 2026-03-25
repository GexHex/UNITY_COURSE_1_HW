using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private MineView _mineView;
    private float _time;
    private float _mineDistanceDamage = 2f;
    private float _mineDistanceTrigger = 1f;
    private float _timeToExplosion = 1;
    private bool _initializeExplosion;

    public void ExplodeMine(Character character, Mine _mine, float deltaTime)
    {
        Vector3 _distanceToCharacter = character.transform.position - _mine.transform.position;

        if (_distanceToCharacter.magnitude <= _mineDistanceTrigger)
        {
            _initializeExplosion = true;
        }

        if (_initializeExplosion == true)
        {
            _mine._time += deltaTime;

            if (_mine._time > _timeToExplosion)
            {
                _mine._time = 0;
                _mine.PlayExplosionEffect();
                _mine.ShowDamageRadius();

                Destroy(_mine.gameObject, 0.5f);

                if (_distanceToCharacter.magnitude <= _mineDistanceDamage && character.Health > 0)
                {
                    character.TakeDamage(10);
                }
            }
        }
    }

    public void PlayExplosionEffect()
    {
        _mineView.InstantiateEffect(transform.position);
    }

    public void ShowDamageRadius()
    {
        _mineView.SetDamageScaleView(_mineDistanceDamage / 5);
    }
}