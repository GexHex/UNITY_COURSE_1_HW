using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _minValue;
    [SerializeField] private int _maxValue;  
    [SerializeField] private CoinsCounter _coinsCounter;

    private void OnTriggerEnter(Collider other)
    {
        Player _player = other.GetComponent<Player>();

        if (_player != null)
        {
            _coinsCounter.AddCoins(Random.Range(_minValue, _maxValue + 1));
            gameObject.SetActive(false);
        }
    }      
}