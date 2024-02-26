using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
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

        SubsystemManager.reloadSubsytemsCompleted  += test;

        yield return new WaitForEndOfFrame();

        CheckVR();


    }
    void test()
    {
        print("test");
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
