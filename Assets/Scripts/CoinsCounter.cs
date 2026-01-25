using UnityEngine;

public class CoinsCounter : MonoBehaviour
{
    [SerializeField] private PrintInfo _printInfo;

    public int Coins { get; private set; }
    public void AddCoins(int value)
    {
        Coins += value;       
    }
}