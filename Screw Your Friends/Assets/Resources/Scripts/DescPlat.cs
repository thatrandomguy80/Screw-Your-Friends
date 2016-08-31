using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DescPlat : MonoBehaviour {
    public float timerTime = 1000;

    private List<DescPlat> neighbours;
    private bool activeTimer = false;
    private bool gateway = false;
    private float time = 0;
    void Start() {
        neighbours = new List<DescPlat>();
    }

    // Update is called once per frame
    void Update() {
        if (activeTimer) {
            if (time >= timerTime) {
                Destroy(this.gameObject);
            } else time += Time.deltaTime;
        }
    }

    public void fallin(int delay) {
        Invoke("fall", delay);//invoke the delay
    }

    public void fall() {
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;//will make it fall
        activeTimer = !activeTimer;//kill timer activate
    }

    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<DescPlat>() != null && other.gameObject.GetComponent<Rigidbody2D>().velocity.y < -0.2f) {
            breakPlat();
        }
    }

    public void breakPlat() {
        //get neighbours if any
        if (!gateway) {//only allow one call [CHANGE]
            gateway = !gateway;
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x - 0.6f, transform.position.y), Vector2.right * -1, .5f);
            if (Physics2D.Raycast(new Vector2(transform.position.x - 0.6f, transform.position.y), Vector2.right * -1, .5f)) {
                GameObject temp = hit.transform.gameObject;
                if (temp.GetComponent<DescPlat>() != null) {
                    neighbours.Add(temp.GetComponent<DescPlat>());
                }
            }
            neighbours.Add(this);
            RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x + 0.6f, transform.position.y), Vector2.right, .5f);
            if (Physics2D.Raycast(new Vector2(transform.position.x + 0.6f, transform.position.y), Vector2.right, .5f)) {
                GameObject temp2 = hit2.transform.gameObject;
                if (temp2.GetComponent<DescPlat>() != null) {
                    neighbours.Add(temp2.GetComponent<DescPlat>());
                }
            }
            int i = 0;
            foreach (DescPlat t in neighbours) {//call each fall with delays for middle and right
                t.SendMessage("fallin", 0.5f * i);
                i++;
            }
        }
    }
}
