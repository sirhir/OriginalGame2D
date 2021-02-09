using System.Collections.Generic;
using UnityEngine;

public class BlastBlockScript : MonoBehaviour
{
    public AudioClip destructSound;
    private bool SelfDestructFlag = false;

    private float destructTimer;
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
            other.gameObject.GetComponent<Rigidbody2D>().velocity += temp * 30.0f * this.transform.localScale.z;
            //vector3 temp = other.gameObject.transform.position - this.transform.position;

            //other.gameObject.GetComponent<Rigidbody2D>();
            //otherにリジッドボディがあるなら放射方向に加速させる
        }
    }
}
