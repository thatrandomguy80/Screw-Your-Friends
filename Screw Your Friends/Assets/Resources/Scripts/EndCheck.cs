using UnityEngine;
using System.Collections;

public class EndCheck : MonoBehaviour {
    private GameObject man;
    //public bool used = false,triswitch = false;
    // Use this for initialization
    void Start() {
        man = GameObject.Find("Manager");
    }

    // Update is called once per frame
    void Update() {
       
           

    }
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<Movement>() != null) {
            man.GetComponent<GameState>().SendMessage("nextMap");
            Destroy(this.gameObject);
        }
    }
}
