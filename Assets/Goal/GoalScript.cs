using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.tag == "PlayChara" )
        {
            GameObject.Find("GameManager").GetComponent<StageScript>().Cleared();
        }
    }
}
