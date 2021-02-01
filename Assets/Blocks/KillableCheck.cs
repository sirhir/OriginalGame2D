using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillableCheck : MonoBehaviour
{
    private Vector3 previousPosition;
    private float moveAmount;
    public bool killable = false;

    private int freq = 16;
    private int f;

    void Start()
    {
        f=0;
        moveAmount = 0.0f;
        previousPosition = this.transform.position;
    }

    void Update()
    {
        if(f<freq)
        {
            f++;
        }
        else
        {
            moveAmount = ( this.transform.position - previousPosition ).sqrMagnitude / Time.deltaTime;
            if ( moveAmount > 16.0f )
            {
                killable = true;
            }
            else
            {
                killable = false;
            }
            previousPosition = this.transform.position;
            f=0;
        }
    }
}
