using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class S_VrManager_TB : MonoBehaviour
{
    //VR
    GameObject XrOrigin;
    GameObject XrInteractionManager;

    public static List<XRDisplaySubsystem> xDisplaySubsystems = new List<XRDisplaySubsystem>();
    static bool VR_Found;
    static List<InputDevice> RightDevices = new List<InputDevice>();
    static bool RightControllerFound;
    static List<InputDevice> LeftDevices = new List<InputDevice>();
    static bool LeftControllerFound;

    public bool VRReady = false;

    //PC
    GameObject PcCamera;

    IEnumerator Start()
    {
        GameObject player = FindFirstObjectByType<S_Movement_TB>().gameObject;
        XrOrigin = player.transform.GetChild(0).gameObject;
        XrInteractionManager = FindFirstObjectByType<XRInteractionManager>().gameObject;
        PcCamera = GameObject.Find("PcCamera");

        while (!VRReady)
        {
            //print("hi");
            yield return new WaitForEndOfFrame();

            if(VR_Found)
                CheckVR();
            if(RightControllerFound)
                CheckInputDevice(InputDeviceCharacteristics.Right, 0);
            if(LeftControllerFound)
                CheckInputDevice(InputDeviceCharacteristics.Left, 1);

            if(VR_Found && RightControllerFound && LeftControllerFound)
            {
                VRReady = true;
            } else
            {
                VRReady = false;
            }
        }
    }
    [Button]
    public void CheckVR()
    {
        S_Settings_TB.IsVRConnected = IsVrHeadsetConnected();
        SwapSystem();
    }
    [Button]
    public void ToggleVR()
    {
        S_Settings_TB.IsVRConnected = !S_Settings_TB.IsVRConnected;
        SwapSystem();
    }

    void SwapSystem()
    {
        if (S_Settings_TB.IsVRConnected)
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
        SubsystemManager.GetSubsystems<XRDisplaySubsystem>(xDisplaySubsystems);

        foreach (var item in xDisplaySubsystems)
        {
            if (item.running)
            {
                Debug.Log(item + " is connected");
                VR_Found = true;
                return true;
            }
        }
        Debug.LogError("VR is not connected, using PC controlls");
        return false;
    }

    public static void CheckInputDevice(InputDeviceCharacteristics characteristics, int RightLeft)
    {
        switch (RightLeft)
        {
            case 0:
                InputDevices.GetDevicesWithCharacteristics(characteristics, RightDevices);
                if(RightDevices.Count > 0)
                {
                    RightControllerFound = true;
                    Debug.Log("Right controller found");
                } else
                {
                    Debug.LogWarning("Right controller is not connected");
                }
                break;
            case 1:
                InputDevices.GetDevicesWithCharacteristics(characteristics, LeftDevices);
                if (LeftDevices.Count > 0)
                {
                    LeftControllerFound = true;
                    Debug.Log("Left controller found");
                } else
                {
                    Debug.LogWarning("Left controller is not connected");
                }
                break;
            default:
                Debug.LogError(RightLeft + " is not valid");
                break;
        }
    }
}
