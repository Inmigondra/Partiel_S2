using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public int speed;
    public int score;
    public GameObject projectile;
    public Sprite[] avatars = new Sprite[3];
    GameObject gameManager;
    GameManager GM;
    Vector2 mousePosition;
    static int avatar;
    Image image;

    void Awake() {
        gameManager = GameObject.Find("GameManager");
        GM = gameManager.GetComponent<GameManager>();
        image = gameObject.GetComponent<Image>();
    }

	void Start () {
        
        if (GM.selectedAvatar == 1) {
            image.sprite = avatars[0];
        }
        if (GM.selectedAvatar == 2) {
            image.sprite = avatars[1];
        }
        if (GM.selectedAvatar == 3) {
            image.sprite = avatars[2];
        }
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
        if (score > 5) {
            GM.firstOptional = true;
        }
        if (score > 10) {
            GM.secondOptional = true;
        }
        if (score > GM.highscore) {
            GM.highscore = score;
        }
    }
}
