using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {
    private GameObject Char;
    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        Char = GameObject.Find("CharacterRobotBoy");
        rb = Char.GetComponent<Rigidbody2D>();
        respawn();
	}
    public void changeLoc(Vector3 newLoc) {
        this.transform.position = newLoc;
    }
    public void respawn() {
        Char.transform.position = this.transform.position;
        rb.velocity = Vector3.zero;
    }
}
