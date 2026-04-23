using TMPro;
using UnityEngine;

public class SoundMixerUIToggle : MonoBehaviour
{
    [SerializeField] SoundController _soundController;
    [SerializeField] private MixerParameterTypes _parameterType;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private string _buttonTextON = "On";
    [SerializeField] private string _buttonTextOFF = "Off";
    private bool _isButtonEnabled;

    private void Awake()
    {
        _isButtonEnabled = _soundController.IsEnabled(_parameterType);
        UpdateText();
    }

    public void OnButtonClick()
    {
        _isButtonEnabled = !_isButtonEnabled;

        if (_isButtonEnabled)
            Enable();
        else
            Disable();

        UpdateText();
    }

    private void Enable() => _soundController.Enable(_parameterType);
    private void Disable() => _soundController.Disable(_parameterType);

    private void UpdateText()
    {
        _buttonText.text = _isButtonEnabled ? _buttonTextON : _buttonTextOFF;
    }
}