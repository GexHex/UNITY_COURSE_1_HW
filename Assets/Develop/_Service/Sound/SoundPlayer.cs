using UnityEngine;
using UnityEngine.Audio;

public static class SoundPlayer
{
    public static void Play(AudioClip clip, Vector3 position, AudioMixerGroup mixer)
    {
        GameObject obj = new GameObject("MixerController");
        obj.transform.position = position;

        AudioSource source = obj.AddComponent<AudioSource>();
        source.clip = clip;
        source.outputAudioMixerGroup = mixer;
        source.spatialBlend = 1f; // 3D звук
        source.Play();

        Object.Destroy(obj, clip.length);
    }
}