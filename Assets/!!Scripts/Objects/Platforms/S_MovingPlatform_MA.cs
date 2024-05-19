using UnityEngine;

public class S_MovingPlatform_MA : MonoBehaviour
{
    S_Lever_TB lever;

    [SerializeField] float speed;

    [Range(0, 1)]
    [SerializeField] float value;

    [SerializeField] float startPos;

    Transform Location1;
    Transform Location2;

    [SerializeField] bool reverse;
    bool hasRun;

    private void Start()
    {
        lever = FindFirstObjectByType<S_Lever_TB>();
        print   (lever);
        Location1 = transform.parent.GetChild(1); //gets start
        Location2 = transform.parent.GetChild(2); //gets end

        startPos = transform.localPosition.y;

    }

    private void Update()
    {
        if (lever == null) return;
        if (lever.active == false) return;

       //slider, starts at objects position first time
        if (!hasRun)
        {
            transform.localPosition = Vector3.Lerp(Location1.localPosition, Location2.localPosition, startPos);
            hasRun = true;
        }
        if (hasRun)
        {
            transform.localPosition = Vector3.Lerp(Location1.localPosition, Location2.localPosition, value);
        }

        //reverse
        value += reverse ? -Time.deltaTime * speed : Time.deltaTime * speed;
        if (value > 1) reverse = true;
        if (value < 0) reverse = false;
    }

    private void OnValidate()
    {
        Location1 = transform.parent.GetChild(1); //gets start
        Location2 = transform.parent.GetChild(2); //gets end
        transform.localPosition = Vector3.Lerp(Location1.localPosition, Location2.localPosition, value);
    }
}
