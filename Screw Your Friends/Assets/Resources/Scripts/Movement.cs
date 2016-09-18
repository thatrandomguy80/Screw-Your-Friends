using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    //#######################
    //PUBLIC VARIABLES
    //#######################

    public float speed = 5f;
    public float maxSpeed = 5f;

    //#######################
    //PRIVATE VARIABLES
    //#######################
    private float jumpSpeed = 0.05f;
    private float maxJumpSpeed = 1f;
    private int jumpTicker = 0;
    private bool jumping = false;
    private bool crouching = false;
    private int colliders = 0;
    private float distToGround;


    private Rigidbody2D rBody;
    private Vector2 mVector;

    //#######################
    //PUBLIC FUNCTIONS
    //#######################

    void Start() {
        rBody = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        distToGround = this.GetComponent<SpriteRenderer>().bounds.extents.y;
        checkGround();
        checkInputs();
    }
    //#######################
    //PRIVATE FUNCTIONS
    //#######################

    //all input based movement is here.
    private void checkInputs() {
        //check jumps
        if (Input.GetKey(KeyCode.W) && isGrounded() && !jumping) {
            jump();//first jump
        } else if (Input.GetKey(KeyCode.W) && jumping) {
            jump();//while in air and allowing speed to be gained.
        } else if (!Input.GetKey(KeyCode.W)) {//when player lets go stop calling jumping until grounded again.
            jumping = false;
            jumpSpeed = 0.05f;
            jumpTicker = 0;
        }

        //left/right movement and restrictions when not pressing.
        if (Input.GetAxis("Horizontal") != 0) {
            mVector.x = Input.GetAxis("Horizontal");
            if (mVector.x > 0)
                this.GetComponent<SpriteRenderer>().flipX = false;
            else if (mVector.x < 0)
                this.GetComponent<SpriteRenderer>().flipX = true;
            movement();
        } else if (rBody.velocity.magnitude > maxSpeed) {//bring player to a halt. If too fast then half speed until slow enough to freeze.
            rBody.AddForce(-(rBody.velocity / 2));
        } else {
            rBody.AddForce(-rBody.velocity);
        }


        //crouch and ground smash
        if (Input.GetKey(KeyCode.S)) {
            //crouch when grounded
            if (isGrounded() && !crouching) {
                crouch();
            }

        } else if (crouching) {
            //uncrouch
            crouch();
        }


        //about to hit ground with q held
        if (rBody.velocity.y < -0.5f && Input.GetKey(KeyCode.Q) && Physics2D.Raycast(transform.position, -Vector3.up, distToGround + 0.2f, LayerMask.GetMask("ground"))) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, distToGround + 0.2f, LayerMask.GetMask("ground"));
            if (hit.transform.GetComponent<DescPlat>() != null) {
                hit.transform.GetComponent<DescPlat>().breakPlat();
            }
        }
    }

    private void crouch() {
        if (crouching) {
            transform.localScale *= 2;
            transform.Translate(new Vector3(0, this.GetComponent<SpriteRenderer>().bounds.extents.y / 2, 0));
        } else {
            transform.localScale /= 2;
            transform.Translate(new Vector3(0, -this.GetComponent<SpriteRenderer>().bounds.extents.y / 2, 0));
        }
        crouching = !crouching;
    }

    private void jump() {
        if (!(jumpSpeed > maxJumpSpeed)) {
            if (!jumping) {
                colliders = 0;
                checkGround();
                rBody.AddForce(Vector2.up * (jumpSpeed + 1f) * 100);//on first tick allow a original boost
                jumping = true;
            }

            //add jump
            rBody.AddForce(Vector2.up * jumpSpeed * 100);

            //add to jump amount every x ticks
            jumpSpeed += 0.1f;
            //Debug.Log(jumpSpeed);
        } else {
            //Debug.Log("Speed reached");
        }
    }

    private bool isGrounded() {
        float off =0f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, distToGround + off, LayerMask.GetMask("ground"));
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, new Vector3(-0.5f, -0.5f, 0), distToGround + off, LayerMask.GetMask("ground"));
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector3(0.5f, -0.5f, 0), distToGround + off, LayerMask.GetMask("ground"));
        Color col = Color.red;
        if (hit || hit1 || hit2) {
            col = Color.cyan;
        }
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - distToGround + 0.1f, transform.position.z), col, 2);
        return hit || hit1 || hit2;
    }

    private void movement() {
        //don't go past a certain speed..
        mVector.x = mVector.x * speed;
        rBody.AddForce(mVector);
        Vector2 vel = rBody.velocity, result = new Vector2(0, 0);
        if ((!(rBody.velocity.x < maxSpeed / 2 && rBody.velocity.x > -(maxSpeed / 2))) && isGrounded())// if grounded and too fast then basic slow down.
        {
            result = vel * -1;
            result.Normalize();
            rBody.AddForce(result * speed);
        } else if ((!(rBody.velocity.magnitude < maxSpeed * 1.5f && rBody.velocity.magnitude > -maxSpeed * 1.5f)) && !isGrounded())// if in air restrict vertical movment
          {
            if (vel.x > maxSpeed) {
                result.x = -(vel.x - maxSpeed);//slow down but don't stop player
            }
            if (vel.x < -maxSpeed) {
                result.x = -(vel.x + maxSpeed);//slow down but don't stop player
            }
            rBody.AddForce(result * speed);//add slowing force
        }

    }

    private void checkGround() {
        if (colliders < 0) colliders = 0;//shouldn't be below zero

        if (colliders == 0) {//if in the air stay unfrozen
            rBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        } else if (colliders > 0) {//freeze the y when grounded
            rBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        } else {//if collider count is greater then zero but your notgrounded then unfreeze.
            rBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }
    //#######################
    //GETTERS
    //#######################
    public int getColliders() { return colliders; }

    public Vector3 getVel() { return rBody.velocity; }
    //#######################
    //TRIGGERS
    //#######################
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("ground"))
            colliders++;
    }

    public void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("ground"))
            colliders--;
    }

}
