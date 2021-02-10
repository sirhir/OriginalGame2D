using UnityEngine;
using UnityEngine.UI;

public class BubbleFloatScript : MonoBehaviour
{
    private Vector3 defaultPostion;

    void Start()
    {
        this.defaultPostion = this.transform.position;
    }

    void Update()
    {
        this.transform.position = new Vector3
        (
            this.defaultPostion.x,
            this.defaultPostion.y +  ( Mathf.Sin(Time.time*3) ) * 0.05f,
            this.defaultPostion.z
        );
    }

}
