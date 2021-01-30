using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    private Animator myAnimator;
    private Rigidbody2D myRigidbody2D;
    private CapsuleCollider2D myCapsuleCollider2D;
    private Camera myCamera;

    private bool isGround = true;


    private float horizonAcceleration = 0.04f;
    private float maxHorizonVelocityGround = 4.0f;
    private float maxHorizonVelocityAir = 2.0f;

    void Start()
    {
        this.mySpriteRenderer = GetComponent<SpriteRenderer>();
        this.myAnimator = GetComponent<Animator>();
        this.myRigidbody2D = GetComponent<Rigidbody2D>();
        this.myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        this.myCamera = Camera.main;
    }

    void Update()
    {
        isGround = IsGroundCheck();

        if ( Input.GetMouseButton(0) )
        {
            HorizonMotionAdaptor( myCamera.ScreenToWorldPoint(Input.mousePosition).x < this.transform.position.x );
        }

        AnimationAdaptor();
    }

    void AnimationAdaptor(){ //W.I.P
        if (isGround)
        {
            if ( Mathf.Abs(myRigidbody2D.velocity.x) < 0.5)
            {
                this.myAnimator.SetBool("Run", false);
            }
            else
            {
                this.myAnimator.SetBool("Run", true);
            }
        }
        else
        {
            //空中でのアニメーションを制御 W.I.P
        }
    }


    ///<summary>
    /// clickLeft = true    :   自キャラ左側をクリック
    /// clickLeft = false   :   自キャラ右側をクリック
    ///</summary>
    void HorizonMotionAdaptor(bool clickLeft)
    {
        //加速度与えた方向へキャラを向き直す
        this.mySpriteRenderer.flipX = clickLeft;

        //現在の速度が自力で出していい最高速度以下なら加速度を与える
        float maxHorizonVelocity;
        if (isGround)
        {
            maxHorizonVelocity = maxHorizonVelocityGround;
        }
        else
        {
            maxHorizonVelocity = maxHorizonVelocityAir;
        }

        if (clickLeft)
        {
            if ( maxHorizonVelocity*-1 < myRigidbody2D.velocity.x )
            {
                myRigidbody2D.velocity = new Vector2
                (
                    myRigidbody2D.velocity.x + horizonAcceleration*-1
                    ,myRigidbody2D.velocity.y
                );
            }
        }else
        {
            if ( myRigidbody2D.velocity.x < maxHorizonVelocity )
            {
                myRigidbody2D.velocity = new Vector2
                (
                    myRigidbody2D.velocity.x + horizonAcceleration
                    ,myRigidbody2D.velocity.y
                );
            }
        }
    }

    bool IsGroundCheck()
    {
       List<ContactPoint2D> contacts = new List<ContactPoint2D>();;
        this.myCapsuleCollider2D.GetContacts(contacts);

        foreach ( ContactPoint2D contact in contacts )
        {
            //条件文内のリテラル値は、自キャラよりy軸nの距離で接点を持ったら接地になる閾値
            if ( contact.point.y - this.transform.position.y < -0.55f )
            {
                return true;
            }
        }

        return false;
    }

}
