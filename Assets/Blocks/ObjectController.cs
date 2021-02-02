using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public AudioClip destructSound;

    private bool SelfDestructFlag = false;
    private Vector3 shrinkScale = new Vector3(-0.5f, -0.5f, -0.5f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( SelfDestructFlag )
        {
            this.transform.localScale = this.transform.localScale + (Time.deltaTime * shrinkScale);
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
        if (SelfDestructFlag != true){
            GetComponent<AudioSource>().PlayOneShot(destructSound, 0.1f);
        }
        SelfDestructFlag = true;
    }
}
