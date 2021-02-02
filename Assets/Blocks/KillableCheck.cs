using System.Collections.Generic;
using UnityEngine;

public class KillableCheck : MonoBehaviour
{
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

    private float freq = 0.5f;
    private float threshold = 3.0f;
    

    Queue<coords> q = new Queue<coords>();
    

    void start()
    {
 
    }

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

    public bool Killable()
    {
        if ( ( this.transform.position - q.Peek().Pos ).sqrMagnitude > threshold )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
