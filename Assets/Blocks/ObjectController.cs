﻿using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private bool SelfDestructFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( SelfDestructFlag )
        {
            this.transform.localScale = this.transform.localScale + new Vector3(-0.002f, -0.002f, -0.002f);
            if (this.transform.localScale.x < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    ///<summary>
    ///自己破壊
    ///</summary>
    public void ClickEvent()
    {
        SelfDestructFlag = true;
    }
}