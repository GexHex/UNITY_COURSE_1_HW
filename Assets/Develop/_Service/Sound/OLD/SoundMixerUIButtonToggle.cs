using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerUIButtonToggle : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private string _volumeParameter;

    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private string _onText = "On";
    [SerializeField] private string _offText = "Off";

    [SerializeField] private float _onVolume = 0f;
    [SerializeField] private float _offVolume = -80f;

    [SerializeField] private bool _startEnabled = true;
    private bool _isButtonEnabled;

    private void Awake()
    {
        _isButtonEnabled = _startEnabled;
        UpdateText();
    }

    public void OnButtonClick()
    {
        if (_isButtonEnabled)
            Disable();
        else
            Enable();

        _isButtonEnabled = !_isButtonEnabled;

        UpdateText();
    }

    private void Enable() => _audioMixer.SetFloat(_volumeParameter, _onVolume);
    private void Disable() => _audioMixer.SetFloat(_volumeParameter, _offVolume);

    private void UpdateText()
    {
        if (_buttonText == null)
            return;

        _buttonText.text = _isButtonEnabled ? _onText : _offText;
    }
}