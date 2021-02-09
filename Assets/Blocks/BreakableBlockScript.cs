using UnityEngine;

public class BreakableBlockScript : MonoBehaviour
{
    public AudioClip destructSound;
    private bool SelfDestructFlag = false;

    private float destructTimer;
    private float destructSpan = 2.0f;

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
            GetComponent<AudioSource>().PlayOneShot(destructSound, 0.1f);
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<BoxCollider2D>());
            //パーティクル再生
            GetComponent<ParticleSystem>().Play();
            SelfDestructFlag = true;
        }
    }
}
