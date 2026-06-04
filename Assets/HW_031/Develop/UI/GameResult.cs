using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameResult : MonoBehaviour
{
    [SerializeField] private TMP_Text _textResult;
    [SerializeField] private TMP_Text _textMessage;
    [SerializeField] private Image _backbround;

    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);

    public void ShowResult(string text) => _textResult.text = text;

    public void ShowMessage(string text) => _textMessage.text = text;

    public void ChangeBackgroundColor(Color color) => _backbround.color = color;
}