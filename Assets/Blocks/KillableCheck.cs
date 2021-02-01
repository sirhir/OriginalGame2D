using UnityEngine;

public class KillableCheck : MonoBehaviour
{
    private float previousTime;
    private Vector3 previousPosition;
    private float passedTime;

    private float freq = 0.5f;

    void Start()
    {
        previousTime = 0.0f;
        previousPosition = this.transform.position;
    }

    void Update()
    {
        passedTime = Time.time - previousTime;
        if( passedTime > freq) 
        {
            previousTime = Time.time;
            previousPosition = this.transform.position;
        }
    }

    public bool Killable()
    {
        if ( ( this.transform.position - previousPosition ).sqrMagnitude / passedTime > 3.0f )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
