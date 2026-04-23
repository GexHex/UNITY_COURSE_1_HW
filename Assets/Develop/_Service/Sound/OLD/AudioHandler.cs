using UnityEngine.Audio;

public class AudioHandler 
{
    private const float MusicOffVolumeValue = -80;
    private const float MusicOnVolumeValue = -5;
    private const float SFXOffVolumeValue = -80;
    private const float SFXOnVolumeValue = 10;

    private const string MusicKey = "Music";
    private const string SFXKey = "SFX";

    private AudioMixer _audioMixer;

    public AudioHandler(AudioMixer audioMixer)
    {
        _audioMixer = audioMixer;
    }

    public void OnMusic() => _audioMixer.SetFloat(MusicKey, MusicOnVolumeValue);
    public void OnSounds() => _audioMixer.SetFloat(SFXKey, SFXOnVolumeValue);
    public void OffMusic() => _audioMixer.SetFloat(MusicKey, MusicOffVolumeValue);
    public void OffSounds() => _audioMixer.SetFloat(SFXKey, SFXOffVolumeValue);
}