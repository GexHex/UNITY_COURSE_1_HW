using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    private AudioHandler _audioHandler;

    private void Awake()
    {
        _audioHandler = new AudioHandler(_audioMixer);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _audioHandler.OffMusic();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _audioHandler.OnMusic();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _audioHandler.OffSounds();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _audioHandler.OnSounds();
        }
    }
}