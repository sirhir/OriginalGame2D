using UnityEngine;

public class InputModuleController : MonoBehaviour
{
    private GameObject myCamera;
    private GameObject myUnityChan;
    private Collider2D myCollider2D;

    private bool unityChanControl;

    void Start()
    {
        this.myCamera = GameObject.FindGameObjectWithTag("MainCamera");
        this.myUnityChan = GameObject.FindGameObjectWithTag("PlayChara");
    }

    void Update()
    {
        this.transform.position = myCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

        if ( Input.GetMouseButtonDown(0) )
        {
            myCollider2D = Physics2D.OverlapPoint(this.transform.position);
            if ( myCollider2D == null )
            {
                unityChanControl = true;
            }
            else
            {
                if ( myCollider2D.tag == "ClickEventObject" )
                {
                    myCollider2D.GetComponent<ObjectController>().ClickEvent();
                }
                else
                {
                    unityChanControl = true;
                }
            }
        }

        if ( Input.GetMouseButtonUp(0) )
        {
            unityChanControl = false;
        }

        if ( unityChanControl )
        {
            if (myUnityChan != null)
            {
                myUnityChan.GetComponent<UnityChanController>().HorizonMotionAdaptor(this.transform.position.x < myUnityChan.transform.position.x);
            }
        }
    }

}
