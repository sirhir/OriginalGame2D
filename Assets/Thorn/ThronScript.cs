using UnityEngine;

public class ThronScript : MonoBehaviour
{
    public AudioClip sting;

    void OnCollisionEnter2D(Collision2D other)
    {
        if ( other.gameObject.tag == "PlayChara" )
        {
            this.GetComponent<AudioSource>().PlayOneShot(sting, 0.05f);
            other.gameObject.GetComponent<UnityChanController>().KillUnityChan();
        }
        else if ( other.gameObject.tag == "Enemy")
        {
            this.GetComponent<AudioSource>().PlayOneShot(sting, 0.05f);
            other.gameObject.GetComponent<EnemyScript>().KillEnemy();
        }
    }
}
