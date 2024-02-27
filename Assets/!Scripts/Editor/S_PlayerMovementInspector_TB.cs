using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(S_Movement_TB))]
public class S_PlayerMovementInspector_TB : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //GUI.color = Color.white;
        //GUIStyle Header = new GUIStyle() { fontSize = 18, };

        //GUILayout.Label("Physics", Header);

        //GUILayout.Toggle(target.GetComponent<S_Movement_TB>().VisualizeGroundCheck, "GroundCheck");

        //GUILayout.Space(100);

        //if (GUILayout.Button("UselessButton"))
        //{
        //    Debug.Log("hi");
        //}
    }
}
