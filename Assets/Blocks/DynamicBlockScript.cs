using System.Collections.Generic;
using UnityEngine;

public class DynamicBlockScript : MonoBehaviour
{
    public AudioClip killableCollisionSound;

    private struct coords
    {
        private Vector3 pos;
        public Vector3 Pos
        {
            get { return pos;}
            set { pos = value; }
        }
        private float tm;
        public float Tm
        {
            get { return tm; }
            set { tm = value; }
        }
    };
    private coords current;

    private float freq = 0.2f;
    //freqタイム内にthreshold以上の位置変化量なら致命的物体
    private float threshold = 0.8f; 
    
    Queue<coords> q = new Queue<coords>();
 

    void Update()
    {
        current.Pos = this.transform.position;
        current.Tm = Time.time;
        q.Enqueue(current);

        if ( Time.time - q.Peek().Tm > freq )
        {
            q.Dequeue();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if ( this.Killable() )
        {
            GetComponent<AudioSource>().PlayOneShot(killableCollisionSound, 0.1f);
            if ( other.gameObject.tag == "PlayChara" )
            {
                other.gameObject.GetComponent<UnityChanController>().KillUnityChan();
            }
            else if ( other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyScript>().KillEnemy();
            }
        }
    }

    bool Killable()
    {
        if (q.Count > 0){
            //Debug.Log((this.transform.position - q.Peek().Pos ).sqrMagnitude);
            if ( ( this.transform.position - q.Peek().Pos ).sqrMagnitude > threshold )
            {
                return true;
            }
        }
        return false;
    }
}
