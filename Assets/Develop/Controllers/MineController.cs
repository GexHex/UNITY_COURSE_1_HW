using System.Collections.Generic;
using UnityEngine;

public class MineController : Controller
{
    [SerializeField] private Mine _mine;
    private Character _character;
    private List<Mine> _mines;

    public MineController(List<Mine> mines, Character character)
    {
        _mines = mines;
        _character = character;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        foreach (var _mine in _mines)
        {           
            if (_mine != null)
            {
                _mine.ExplodeMine(_character, _mine, Time.deltaTime);
            }
        }
    }
}