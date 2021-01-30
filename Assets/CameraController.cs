using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private GameObject playChara;
    // Start is called before the first frame update
    void Start()
    {
        this.playChara = GameObject.FindGameObjectWithTag("PlayChara");
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 cameraPos = new Vector3(this.playChara.transform.position.x
        //                                 ,this.playChara.transform.position.y
        //                                 ,this.transform.position.z);
        // this.transform.position = cameraPos;
    }
}
