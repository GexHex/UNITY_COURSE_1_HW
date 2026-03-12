using UnityEngine;

public class MobBehaviorAnnihilation : IMobBehavior
{
    private Mob _mob;
    private GameObject _vfxDead;
    private Renderer _renderer;

    public MobBehaviorAnnihilation(Mob mob, GameObject VFXDead)
    {
        _mob = mob;
        _vfxDead = VFXDead;
        _renderer = mob.GetComponentInChildren<MeshRenderer>();
    }

    public void StartStrategy()
    {
        Debug.Log($"Start strategy {this}");
        _renderer.material.color = Color.red;
        _mob.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void UpdateStrategy()
    {
        if (_mob != null)
        {
            Object.Destroy(_mob.gameObject);
            OnDestroy();
        }
    }

    public void StopStrategy()
    {
        Debug.Log($"Stop strategy {this}");
        _renderer.material.color = Color.white;
        _mob.transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnDestroy()
    {
        GameObject.Instantiate(_vfxDead, _mob.transform.position, Quaternion.identity);
    }
}