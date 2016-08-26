using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float speed = 8f;
    public float maxSpeed = 5f;
    public float jumpSpeed = 2f;

    private float distToGround;
    private Rigidbody2D rBody;
    private Vector2 mVector;

    void Start() {
        distToGround = this.GetComponent<SpriteRenderer>().bounds.extents.y;
        rBody = this.GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded()) {
            jump();
        }
        if (Input.GetAxis("Horizontal") != 0) {
            mVector.x = Input.GetAxis("Horizontal");
            if (mVector.x > 0)
                this.GetComponent<SpriteRenderer>().flipX = false;
            else if (mVector.x < 0)
                this.GetComponent<SpriteRenderer>().flipX = true;
                
            movement();
        }
        //about to hit ground with s held
        if (rBody.velocity.y <-0.5f && Input.GetKey(KeyCode.S) && Physics2D.Raycast(transform.position, -Vector3.up, distToGround + 0.2f, LayerMask.GetMask("ground"))) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, distToGround + 0.2f, LayerMask.GetMask("ground"));
            if (hit.transform.GetComponent<DescPlat>() != null) {
                hit.transform.GetComponent<DescPlat>().breakPlat();
            }
        }

    }

    public void jump() {
        rBody.AddForce(Vector2.up * jumpSpeed * 200);
        Vector2 vel = rBody.velocity;
        if (vel.y > maxSpeed) {
            rBody.AddForce(new Vector2(0, -jumpSpeed / 2));
        }
    }

    bool isGrounded() {
        Debug.DrawRay(transform.position, -Vector3.up, Color.red);
        return Physics2D.Raycast(transform.position, -Vector3.up, distToGround + 0.1f, LayerMask.GetMask("ground"));
    }

    public void movement() {
        //don't go past a certain speed..
        mVector.x = mVector.x * speed;
        rBody.AddForce(mVector);
        Vector2 vel = rBody.velocity, result = new Vector2(0, 0);
        if ((!(rBody.velocity.magnitude < maxSpeed && rBody.velocity.magnitude > -maxSpeed)) && isGrounded())// if grounded and too fast then basic slow down.
        {
            result = vel * -1;
            result.Normalize();
            //result = result * (vel.magnitude - maxSpeed);
            rBody.AddForce(result * speed);
        } else if ((!(rBody.velocity.magnitude < maxSpeed*1.5f && rBody.velocity.magnitude > -maxSpeed * 1.5f)) && !isGrounded())// if in air restrict vertical movment
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
}
