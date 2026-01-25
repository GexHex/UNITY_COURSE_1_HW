using UnityEngine;
using UnityEngine.UI;

public class PrintInfo : MonoBehaviour
{
    [SerializeField] private Text _text1;
    [SerializeField] private Text _text2;
    [SerializeField] private Text _text3;

    public void Time(float time)        => _text1.text = $"Время: {time}";
    public void Score(float count)      => _text2.text = $"Ваши очки: {count}";    
    public void Win(double count)       => _text3.text = $"Вы выиграли. Вы собрали: {count} монет";
    public void GameOver(double count)  => _text3.text = $"Вы проиграли. Вы собрали: {count} монет";
}
