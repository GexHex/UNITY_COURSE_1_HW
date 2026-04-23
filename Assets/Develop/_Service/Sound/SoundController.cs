using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    [SerializeField] private bool _isMusicStartPlay;
    [SerializeField] private AudioMixer _audioMixer;

    [Header("Mixer Groups")]
    [SerializeField] private AudioMixerGroup _groupMaster;
    [SerializeField] private AudioMixerGroup _groupMusic;
    [SerializeField] private AudioMixerGroup _groupSFX;

    [Header("Parameters")]
    [SerializeField] public string MasterVolumeParametr;
    [SerializeField] public string SFXVolumeParametr;
    [SerializeField] public string MusicVolumeParametr;

    [Header("Volume Values")]
    [SerializeField] private float _onVolume = 10f;
    [SerializeField] private float _offVolume = -80f;

    [Header("MusicAudioSource")]
    [SerializeField] private AudioSource _musicAudioSource;

    [Header("AudioClips")]
    [SerializeField] private AudioClip _mineExplosionSFXClip;
    [SerializeField] private AudioClip _HealClip;
    [SerializeField] private AudioClip _misicClip;

    private void Awake()
    {
        if (_isMusicStartPlay)
        {
            PlayMusic(true);
            Enable(MixerParameterTypes.Music);
        }
        else
        {
            PlayMusic(false);
            Disable(MixerParameterTypes.Music);
        }
    }

    public bool IsEnabled(MixerParameterTypes type)
    {
        _audioMixer.GetFloat(GetParameter(type), out float value);
        return value > _offVolume;
    }

    public void Enable(MixerParameterTypes type)
    {
        _audioMixer.SetFloat(GetParameter(type), _onVolume);

        if (type == MixerParameterTypes.Music)
            PlayMusic(true);
    }

    public void Disable(MixerParameterTypes type)
    {
        _audioMixer.SetFloat(GetParameter(type), _offVolume);

        if (type == MixerParameterTypes.Music)
            PlayMusic(false);
    }

    public void PlayMineExplosionSFX(Transform transform)
    {
        SoundPlayer.Play(_mineExplosionSFXClip, transform.position, _groupSFX);
    }

    public void PlayHealSFX(Transform transform)
    {
        SoundPlayer.Play(_HealClip, transform.position, _groupSFX);
    }

    public void PlayMusic(bool isPlay)
    {
        SoundPlayer.PlayMusic(_misicClip, _groupMusic, _musicAudioSource, isPlay);
    }

    public void EnableSFX() => _audioMixer.SetFloat(SFXVolumeParametr, _onVolume);
    public void DisableSFX() => _audioMixer.SetFloat(SFXVolumeParametr, _offVolume);

    public void EnableMusic() => _audioMixer.SetFloat(MusicVolumeParametr, _onVolume);
    public void DisableMisic() => _audioMixer.SetFloat(MusicVolumeParametr, _offVolume);

    public void EnableMaster() => _audioMixer.SetFloat(MasterVolumeParametr, _onVolume);
    public void DisableMaster() => _audioMixer.SetFloat(MasterVolumeParametr, _offVolume);

    private string GetParameter(MixerParameterTypes type)
    {
        switch (type)
        {
            case MixerParameterTypes.Master:
                return MasterVolumeParametr;

            case MixerParameterTypes.SFX:
                return SFXVolumeParametr;

            case MixerParameterTypes.Music:
                return MusicVolumeParametr;

            default:
                return "";
        }
    }
}
