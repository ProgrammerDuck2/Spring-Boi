using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class S_AudioManager_HA : MonoBehaviour
{

    [Header("Volume")]
    [Range(0, 1)]

    public float masterVolume = 1;
    [Range(0, 1)]

    public float musicVolume = 1;
    [Range(0, 1)]

    public float ambienceVolume = 1;
    [Range(0, 1)]

    public float SFXVolume = 1;
    [Range(0, 1)]

    private Bus masterBus;

    private Bus musicBus;

    private Bus ambienceBus;

    private Bus sfxBus;



    private List<EventInstance> eventInstances;

    private List<StudioEventEmitter> eventEmitters;

    public static S_AudioManager_HA instance { get; private set; }

    private EventInstance ambienceEventInstance;

    private EventInstance musicEventInstance;

    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of AudioManager found!");           
        }
        instance = this;

        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
    }

    private void Update()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        ambienceBus.setVolume(ambienceVolume);
        sfxBus.setVolume(SFXVolume);
    }

    private void Start()
    {
        InitializeAmbience(S_FMODEvents_HA.instance.ambience);
        InitializeMusic(S_FMODEvents_HA.instance.Music);
    }

    private void InitializeAmbience(EventReference ambienceEventReference)
    {
        ambienceEventInstance = CreateInstance(ambienceEventReference);
        ambienceEventInstance.start();
    }

    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateInstance(musicEventReference);
        musicEventInstance.start();
    }

    public void setAmbianceParameter(string parameterName, float parameterValue)
    {
        ambienceEventInstance.setParameterByName(parameterName, parameterValue);
    }


    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }


    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
}
