using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour
{
    public GameObject dyingUnityChan;

    private SpriteRenderer mySpriteRenderer;
    private Animator myAnimator;
    private Rigidbody2D myRigidbody2D;
    private CapsuleCollider2D myCapsuleCollider2D;

    private bool isGround = true;
    private int flightProcessingTime = 0;

    private float horizonAccelerationGround = 20.0f;
    private float horizonAccelerationAir = 10.0f;
    private float maxHorizonVelocityGround = 3.0f;
    private float maxHorizonVelocityAir = 1.5f;
    private float horizonAcceleration;
    private float maxHorizonVelocity;

    void Start()
    {
        this.mySpriteRenderer = GetComponent<SpriteRenderer>();
        this.myAnimator = GetComponent<Animator>();
        this.myRigidbody2D = GetComponent<Rigidbody2D>();
        this.myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        isGround = IsGroundCheck();
        AnimationAdaptor();
    }

    void AnimationAdaptor()
    {
        if ( isGround ){
            flightProcessingTime = 0;
            this.myAnimator.SetBool("Ground", isGround);
        }
        else
        {
            flightProcessingTime++;
            if ( flightProcessingTime > 60 )
            {
                this.myAnimator.SetBool("Ground", isGround);
            }
        }

        if ( Mathf.Abs(this.myRigidbody2D.velocity.x) < 0.6)
        {
            this.myAnimator.SetBool("Run", false);
        }
        else
        {
            this.myAnimator.SetBool("Run", true);
        }
    }

    bool IsGroundCheck()
    {
        List<ContactPoint2D> contacts = new List<ContactPoint2D>();
        this.myCapsuleCollider2D.GetContacts(contacts);

        foreach ( ContactPoint2D contact in contacts )
        {
            //条件文内のリテラル値は、自キャラよりy軸nの距離で接点を持ったら接地になる閾値
            //今は大体勾配45度でギリ接地
            if ( contact.point.y - this.transform.position.y < -0.45f )
            {
                return true;
            }
        }

        return false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log(other.rigidbody?.velocity.sqrMagnitude);
        //動作不安定
        //結局スクリプト追加してブール値もってこよ
        if ( other.gameObject.GetComponent<KillableCheck>() != null ){
            if ( other.gameObject.GetComponent<KillableCheck>().Killable() )
            {
                GameObject dying = Instantiate(dyingUnityChan);
                dying.transform.position = this.transform.position;
                dying.GetComponent<SpriteRenderer>().flipX = this.mySpriteRenderer.flipX;
                Destroy(this.gameObject);
            }
        }
    }

    ///<summary>
    /// clickLeft = true    :   自キャラ左側をクリック
    /// clickLeft = false   :   自キャラ右側をクリック
    ///</summary>
    public void HorizonMotionAdaptor(bool clickLeft)
    {
        //加速度与えた方向へキャラを向き直す
        this.mySpriteRenderer.flipX = clickLeft;
        if (isGround)
        {
            horizonAcceleration = horizonAccelerationGround;
            maxHorizonVelocity = maxHorizonVelocityGround;
        }
        else
        {
            horizonAcceleration = horizonAccelerationAir;
            maxHorizonVelocity = maxHorizonVelocityAir;
        }
        //現在の速度が自力で出していい最高速度以下なら加速度を与える
        if (clickLeft)
        {
            if ( maxHorizonVelocity*-1 < myRigidbody2D.velocity.x )
            {
                myRigidbody2D.velocity = new Vector2
                (
                    myRigidbody2D.velocity.x + horizonAcceleration * Time.deltaTime * -1
                    ,myRigidbody2D.velocity.y
                );
            }
        }else
        {
            if ( myRigidbody2D.velocity.x < maxHorizonVelocity )
            {
                myRigidbody2D.velocity = new Vector2
                (
                    myRigidbody2D.velocity.x + horizonAcceleration * Time.deltaTime
                    ,myRigidbody2D.velocity.y
                );
            }
        }
    }

}
