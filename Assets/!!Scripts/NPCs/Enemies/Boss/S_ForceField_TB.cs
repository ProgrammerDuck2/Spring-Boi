using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_ForceField_TB : MonoBehaviour
{
    [SerializeField] List <S_Lever_TB> levers = new List <S_Lever_TB> ();

    private void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer> ();

        foreach (var item in levers)
        {
            LineRenderer lr = item.AddComponent<LineRenderer>();
            lr.SetPosition(0, item.transform.position);
            lr.SetPosition(1, transform.position);

            lr.material = meshRenderer.material;
        }
    }

    void Update()
    {
        if(AllLeversActive())
        {
            Destroy (gameObject);
        }
    }

    bool AllLeversActive()
    {
        bool allActive = true;
        foreach (var item in levers)
        {
            if(!item.active)
            {
                allActive = false;
            } else
            {
                if(item.TryGetComponent<LineRenderer>(out LineRenderer lr))
                {
                    Destroy(lr);
                }
            }
        }

        return allActive;
    }
}
