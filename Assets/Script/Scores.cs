using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scores : MonoBehaviour {
    public bool isHighScore;
    Text text;
    GameManager GM;
    GameObject gameManager;

    
    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager");
        GM = gameManager.GetComponent<GameManager>();
        text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (isHighScore == true) {
            text.text = GM.highscore.ToString();
        }
        else {
            text.text = "Score: " + PlayerController.score.ToString();
        }
	}
}
