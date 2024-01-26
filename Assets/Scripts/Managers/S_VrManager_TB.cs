using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class S_VrManager_TB : MonoBehaviour
{
    public bool IsVrConnected;

    //VR
    GameObject XrOrigin;
    GameObject XrInteractionManager;

    //PC
    GameObject PcCamera;

    void Start()
    {
        XrOrigin = FindFirstObjectByType<XROrigin>().gameObject;
        XrInteractionManager = FindFirstObjectByType<XRInteractionManager>().gameObject;
        PcCamera = GameObject.Find("PcCamera");

        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();

        CheckVR();
    }

    [Button]
    public void CheckVR()
    {
        IsVrConnected = IsVrHeadsetConnected();
        SwapSystem();
    }
    [Button]
    public void ToggleVR()
    {
        IsVrConnected = !IsVrConnected;
        SwapSystem();
    }

    void SwapSystem()
    {
        if (IsVrConnected)
        {
            PcCamera.SetActive(false);
            XrOrigin.SetActive(true);
            XrInteractionManager.SetActive(true);
        }
        else
        {
            XrOrigin.SetActive(false);
            XrInteractionManager.SetActive(false);
            PcCamera.SetActive(true);
        }
    }

    public static bool IsVrHeadsetConnected()
    {
        List<XRDisplaySubsystem> xDisplaySubsystems = new List<XRDisplaySubsystem>();
        SubsystemManager.GetInstances<XRDisplaySubsystem>(xDisplaySubsystems);
        foreach (var item in xDisplaySubsystems)
        {
            if(item.running)
            {
                Debug.Log(item + " is connected");
                return true;
            }
        }

        Debug.LogWarning("VR is not connected, using PC controlls");
        return false;
    }

}
