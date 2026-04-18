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
    private bool _isEnabled;

    private void Awake()
    {
        _isEnabled = _startEnabled;
        UpdateText();
    }

    public void OnButtonClick()
    {
        if (_isEnabled)
            Disable();
        else
            Enable();

        _isEnabled = !_isEnabled;

        UpdateText();
    }

    private void Enable() => _audioMixer.SetFloat(_volumeParameter, _onVolume);
    private void Disable() => _audioMixer.SetFloat(_volumeParameter, _offVolume);

    private void UpdateText()
    {
        if (_buttonText == null)
            return;

        _buttonText.text = _isEnabled ? _onText : _offText;
    }
}