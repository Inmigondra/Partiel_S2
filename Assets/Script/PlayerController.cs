using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public int speed;
    public GameObject[] projectile= new GameObject[3];
    GameObject gameManager;
    GameManager GM;
    Vector2 mousePosition;
    static int avatar;
	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager");
        GM = gameManager.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        //return the viewportposition of the player on the main camera
        Vector3 playerToCamera = Camera.main.WorldToViewportPoint(transform.position);

        //if the viewportposition is out of (0,0) or (1,1)
        if (playerToCamera.x > 1 || playerToCamera.x < 0 || playerToCamera.y > 1 || playerToCamera.y < 0) {
            Debug.Log("gameover");
            GM.LevelLoad(3);
        }

      
        if (Input.GetAxisRaw("Horizontal") > 0) {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") < 0)  {
            transform.Translate(transform.right *-speed * Time.deltaTime);
        }
        if(Input.GetAxisRaw("Vertical") > 0) {
            transform.Translate(transform.up * speed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Vertical") <0) {
           transform.Translate(transform.up * -speed * Time.deltaTime);
        }
    }
}
