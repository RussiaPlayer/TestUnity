using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField]
    LayerMask groundLayer;
    public Collider2D col;
    public float fGroundedRemember = 0;
    [SerializeField]
    public float fGroundedRememberTime = 0.25f;

    private Rigidbody2D rb;
    private float directionH;
    private float directionV;

    [Header("Movement")]
    [Range(0.0f,1.0f)]
    public float fHorizontalDamping = 0.125f;
    public float fVerticalClimb = 0.002f;

    [Header("Jumping")]
    public float jumpHight = 5.0f;
    public float jumpPressedTimer = 0.15f;
    public float jumpTimerReset = 0.15f;
    public float fallMultiplier = 2.0f;

    [Header("Gun")]
    public GameObject gun;
    public GameObject projectile;
    public DistanceJoint2D joint;


    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        directionH = Input.GetAxisRaw("Horizontal");
        directionV = Input.GetAxisRaw("Vertical");

        Jump();
        Flip();

        if(gun.GetComponent<GraplingGun>().inShoot && !gun.GetComponent<GraplingGun>().midAir)
        {
            if(directionV > 0)
            {
                joint.distance -= fVerticalClimb * Time.deltaTime;
            }
            else if (directionV < 0)
            {
                joint.distance += fVerticalClimb * Time.deltaTime;
            }
        }
    }

    void FixedUpdate()
    {
        float fHorizontalVelocity = rb.velocity.x;
        fHorizontalVelocity += directionH;
        fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDamping, Time.deltaTime * 10f);
        if (!isOverRope())
        {
            rb.velocity = new Vector2(fHorizontalVelocity, rb.velocity.y);
        }

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    private bool isOverRope()
    {
        if (gun.GetComponent<GraplingGun>().inShoot && !gun.GetComponent<GraplingGun>().midAir && this.transform.position.y > projectile.transform.position.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        return raycastHit2D.collider != null;
    }

    void Jump()
    {
        fGroundedRemember -= Time.deltaTime;
        if (isGrounded())
        {
            fGroundedRemember = fGroundedRememberTime;
        }

        jumpPressedTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressedTimer = jumpTimerReset;
        }

        if ((jumpPressedTimer > 0) && (fGroundedRemember > 0))
        {
            jumpPressedTimer = 0;
            rb.velocity = new Vector2(rb.velocity.x, jumpHight);
        }
    }

    void Flip()
    {
        if (directionH > 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (directionH < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
