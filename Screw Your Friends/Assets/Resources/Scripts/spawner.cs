using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {
    private GameObject Char;
    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        Char = GameObject.Find("Character");
        rb = Char.GetComponentInChildren<Rigidbody2D>();
        respawn();
	}
    public void changeLoc(Vector3 newLoc) {
        this.transform.position = newLoc;
    }
    public void respawn() {
        Vector3 pos = transform.position;
        pos.y += 1f;
        Char.transform.position = pos;
        rb.velocity = Vector3.zero;
    }
}
