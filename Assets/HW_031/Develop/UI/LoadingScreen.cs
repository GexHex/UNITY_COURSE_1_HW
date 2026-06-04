using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image _loadingCircle;
    [SerializeField] private TMP_Text _messageText;

    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);

    public void ShowMessage(string text) => _messageText.text = text;

    private void Update()
    {
        float scale = 1f + Mathf.Sin(Time.time * 5f) * 0.2f;

        _loadingCircle.transform.localScale = Vector3.one * scale;
        _loadingCircle.transform.Rotate(Vector3.forward * Time.deltaTime * 1000, Space.World);
    }
}