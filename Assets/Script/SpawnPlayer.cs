using UnityEngine;
using System.Collections;

public class SpawnPlayer : MonoBehaviour {
    public GameObject white;
    public GameObject blue;
    public GameObject red;

    GameObject gameManager;
    GameManager GM;
	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager");
        GM = gameManager.GetComponent<GameManager>();
        if (GM.selectedAvatar == 1)
        {
            GameObject player = (GameObject)GameObject.Instantiate(white);
            player.name = "Player";
        }
        if (GM.selectedAvatar == 2)
        {
            GameObject player = (GameObject)GameObject.Instantiate(blue);
            player.name = "Player";
        }
        if (GM.selectedAvatar == 3)
        {
            GameObject player = (GameObject)GameObject.Instantiate(red);
            player.name = "Player";
        }
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
