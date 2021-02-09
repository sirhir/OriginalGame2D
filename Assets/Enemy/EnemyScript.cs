using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject dyingEnemy;

    private Animator myAnimator;
    private Rigidbody2D myRigidBody;
    private SpriteRenderer mySpriteRenderer;

    private float waittime; //一定時間止まってたら動き反転

    void Start()
    {
        this.myAnimator = GetComponent<Animator>();
        this.myRigidBody = GetComponent<Rigidbody2D>();
        this.mySpriteRenderer = GetComponent<SpriteRenderer>();
    }




    void Update()
    {
        //一定時間横移動速度が0だったら一定時間速度0に、その後反転
        if ( -0.5f <= this.myRigidBody.velocity.x || this.myRigidBody.velocity.x <= 0.5f )
        {
            waittime += Time.deltaTime;
        }

        if ( waittime > 4.0f )
        {
            waittime = 0.0f;
            if (this.mySpriteRenderer.flipX)
            {
                this.mySpriteRenderer.flipX = false;
            }
            else
            {
                this.mySpriteRenderer.flipX = true;
            }
        }
        if (this.myAnimator.GetBool("Attack") != true)
        {
            MotionAdaptor(this.mySpriteRenderer.flipX);
        }
        AnimationAdaptor();
    }

    void MotionAdaptor(bool rightmove)
    {
        if (rightmove)
        {
            if (this.myRigidBody.velocity.x < 2.0f)
            {
                this.myRigidBody.velocity = new Vector2
                (
                    this.myRigidBody.velocity.x + Time.deltaTime * 10.0f
                    ,this.myRigidBody.velocity.y
                );
            }
        }
        else
        {
            if (this.myRigidBody.velocity.x > -2.0f)
            {
                this.myRigidBody.velocity = new Vector2
                (
                    this.myRigidBody.velocity.x + Time.deltaTime * -10.0f
                    ,this.myRigidBody.velocity.y
                );
            }
        }
    }

    void AnimationAdaptor()
    {
        if (this.myRigidBody.velocity.x < -0.5f)
        {
            this.mySpriteRenderer.flipX = false;
            this.myAnimator.SetBool("Walk", true);
        }
        else if (this.myRigidBody.velocity.x > 0.5f)
        {
            this.mySpriteRenderer.flipX = true;
            this.myAnimator.SetBool("Walk", true);
        }
        else
        {
            this.myAnimator.SetBool("Walk", false);
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if ( other.gameObject.tag == "PlayChara" )
        {
            this.myRigidBody.velocity = new Vector2(0,this.myRigidBody.velocity.y);

            if ( other.gameObject.transform.position.x < this.transform.position.x )
            {
                this.mySpriteRenderer.flipX = false;
            }
            else
            {
                this.mySpriteRenderer.flipX = true;
            }
            this.myAnimator.SetBool("Attack", true);
            other.gameObject.GetComponent<UnityChanController>().KillUnityChan();
        }
    }

    public void KillEnemy()
    {
        GameObject dying = Instantiate(dyingEnemy);
        dying.transform.position = this.transform.position;
        dying.GetComponent<SpriteRenderer>().flipX = this.mySpriteRenderer.flipX;
        Destroy(this.gameObject);
    }
}
