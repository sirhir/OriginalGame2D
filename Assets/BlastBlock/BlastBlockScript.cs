using System.Collections.Generic;
using UnityEngine;

public class BlastBlockScript : MonoBehaviour
{
    public AudioClip destructSound;
    private bool SelfDestructFlag = false;

    private float destructTimer;

    private float destructColliderSpan = 0.2f;
    private float destructSpan = 0.8f;

    void start()
    {
        destructTimer = 0;
    }
 
    void Update()
    {
        if (SelfDestructFlag)
        {
            destructTimer += Time.deltaTime;

            if (destructTimer>destructColliderSpan)
            {
                Destroy(this.GetComponent<BoxCollider2D>());
            }

            if (destructTimer>destructSpan)
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
            this.GetComponent<AudioSource>().PlayOneShot(destructSound, 0.1f);
            Destroy(this.GetComponent<SpriteRenderer>());
            this.GetComponent<BoxCollider2D>().isTrigger = true;
            this.GetComponent<BoxCollider2D>().size = new Vector2
            (
                this.GetComponent<BoxCollider2D>().size.x * 1.2f,
                this.GetComponent<BoxCollider2D>().size.y * 1.2f
            );
            //パーティクル再生
            this.GetComponent<ParticleSystem>().Play();
            SelfDestructFlag = true;
        }
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (SelfDestructFlag && other.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            Vector2 temp = other.gameObject.transform.position - this.transform.position;
            other.gameObject.GetComponent<Rigidbody2D>().velocity += temp * this.transform.localScale.z * 16.0f;
        }
    }
}
