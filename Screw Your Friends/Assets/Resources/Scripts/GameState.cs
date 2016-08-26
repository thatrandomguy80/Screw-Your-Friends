using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameState : MonoBehaviour {
    int score;
    float DeathFloor;
    private Text scoreText;
    private spawner spawn;
    private MapLoader ML;
    private int level = 0;
    // Use this for initialization
    void Start() {
        scoreText = GameObject.Find("Text").GetComponent<Text>();
        ML = this.transform.GetComponent<MapLoader>();
        Debug.Log("GameStart");
        nextMap();
    }

    // Update is called once per frame
    void Update() {
        if (spawn == null)
            spawn = GameObject.Find("Spawner(Clone)").GetComponent<spawner>();

        scoreText.text = "Score: " + score;
    }

    public void MapDim(float ydim) {
        this.transform.position = new Vector3(0, -ydim - DeathFloor, 0);
    }

    public void checkPoint(Vector3 newLoc) {
        spawn.changeLoc(newLoc);
    }

    public void addScore() {
        score++;
    }

    public void nextMap() {
        ML.nextMap(level);
       // Destroy(spawn.gameObject);
        level++;
        score = 0;
        //spawner will respawn for us
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<Movement>() != null) {
            score--;
            spawn.respawn();
        }
    }
}
