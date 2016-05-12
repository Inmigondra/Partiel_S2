using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public int speed;
    public static int score;
    public GameObject projectile;
    GameObject gameManager;
    GameManager GM;
    Vector2 mousePosition;
    static int avatar;
   

    void Awake() {
        gameManager = GameObject.Find("GameManager");
        GM = gameManager.GetComponent<GameManager>();
       
    }

	void Start () {
        
       
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
        if (Input.GetAxisRaw("Vertical") < 0) {
           transform.Translate(transform.up * -speed * Time.deltaTime);
        }
        if (Input.GetMouseButtonDown(0)) {
            Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3( Input.mousePosition.x,Input.mousePosition.y, 0));
            Vector3 myPos = transform.position;
            Vector2 direction = target - myPos ;
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.x, direction.y ) * 90 ) ;
            direction.Normalize();
            GameObject shoot = (GameObject)Instantiate(projectile, myPos, rotation);
            shoot.GetComponent<Rigidbody2D>().velocity = direction * speed;
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
