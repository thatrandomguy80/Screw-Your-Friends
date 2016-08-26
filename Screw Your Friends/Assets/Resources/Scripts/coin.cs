using UnityEngine;
using System.Collections;
using System;

public class coin : MonoBehaviour {
    public float speed = 1f;
    public float Rotspeed = 30f;
    private float Y;
    void Awake() {
        Y = transform.position.y + 2f;
    }

    void Update() {
    transform.position = new Vector2(transform.position.x, 
        Y + (Mathf.Sin(Time.time) * speed));
        transform.Rotate(0, Rotspeed * Time.deltaTime, 0);
    }

    public void OnTriggerEnter2D() {
        GameObject.Find("Manager").SendMessage("addScore");
        Destroy(this.gameObject);
    }
}
