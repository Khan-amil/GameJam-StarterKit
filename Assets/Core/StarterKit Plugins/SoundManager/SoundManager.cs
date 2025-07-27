using System;
using UnityEngine;
using System.Collections.Generic;
using Core.Scripts.Utils;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Audio Sources")]
    public AudioSource _musicSource;

    [Header("Volumes")]
    [Range(0f, 1f)]
    [SerializeField]
    private float _musicVolume = 1f;
    
    [Range(0f, 1f)] 
    [SerializeField]
    private float _sfxVolume = 1f;

    [Header("SFX Settings")]
    public Vector2 _pitchVariation = new Vector2(0.95f, 1.05f);

    public float MusicVolume
    {
        get => _musicVolume;
        set
        {
            _musicVolume = value;
            if (_musicSource != null)
            {
                _musicSource.volume = _musicVolume;
            }
        }
    }

    public float SfxVolume
    {
        get => _sfxVolume;
        set => _sfxVolume = value;
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (_musicSource.clip == clip && _musicSource.isPlaying) 
            return;
        
        _musicSource.clip = clip;
        _musicSource.loop = loop;
        _musicSource.volume = MusicVolume;
        _musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f, bool randomPitch = true)
    {
        var src = GetSFXSource();
        src.pitch = randomPitch ? Random.Range(_pitchVariation.x, _pitchVariation.y) : 1f;
        src.volume = SfxVolume * volume;
        src.spatialBlend = 0f; // fully 2D
        src.PlayOneShot(clip);
    }

    public void PlaySFX3D(AudioClip clip, Vector3 position, float volume = 1f, bool randomPitch = true)
    {
        var src = GetSFXSource();
        src.transform.position = position;
        src.spatialBlend = 1f; // fully 3D
        src.pitch = randomPitch ? Random.Range(_pitchVariation.x, _pitchVariation.y) : 1f;
        src.volume = SfxVolume * volume;
        src.PlayOneShot(clip);
    }

    private AudioSource GetSFXSource()
    {
        return PoolManager.Instance.Spawn("Sfx", transform.position, Quaternion.identity)
            .GetComponent<AudioSource>();
    }
}
