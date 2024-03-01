using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class S_VrManager_TB : MonoBehaviour
{
    //VR
    GameObject XrOrigin;
    GameObject XrInteractionManager;

    public static List<XRDisplaySubsystem> xDisplaySubsystems = new List<XRDisplaySubsystem>();

    //PC
    GameObject PcCamera;

    IEnumerator Start()
    {
        GameObject player = FindFirstObjectByType<S_Movement_TB>().gameObject;
        XrOrigin = player.transform.GetChild(0).gameObject;
        XrInteractionManager = FindFirstObjectByType<XRInteractionManager>().gameObject;
        PcCamera = GameObject.Find("PcCamera");

        yield return new WaitForEndOfFrame();

        CheckVR();
    }

    private void Update()
    {
        if(!S_Settings_TB.IsVRConnected)
            CheckVR();
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

        //S_VRDependant_TB[] vrDependants = (S_VRDependant_TB[])FindObjectsByType(typeof(S_VRDependant_TB), FindObjectsSortMode.None);

        //for (int i = 0; i < vrDependants.Length; i++)
        //{
        //    vrDependants[i].IfElseVR();
        //}
    }

    public static bool IsVrHeadsetConnected()
    {
        SubsystemManager.GetSubsystems<XRDisplaySubsystem>(xDisplaySubsystems);

        foreach (var item in xDisplaySubsystems)
        {
            if (item.running)
            {
                Debug.Log(item + " is connected");

                List<InputDevice> RightDevices = new List<InputDevice>();
                List<InputDevice> LeftDevices = new List<InputDevice>();
                InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, RightDevices);
                InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left, LeftDevices);

                print(RightDevices);
                if (RightDevices.Count > 0)
                {
                    Debug.Log("Right controller is connected");
                }
                else
                {
                    Debug.LogWarning("Right controller is not connected");
                }

                if (LeftDevices.Count > 0)
                {
                    Debug.Log("Left controller is connected");
                }
                else
                {
                    Debug.LogWarning("Left controller is not connected");
                }

                return true;
            }
        }
        Debug.LogError("VR is not connected, using PC controlls");
        return false;
    }
}
