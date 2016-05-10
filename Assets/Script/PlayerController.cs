using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public int speed;
    public GameObject[] projectile= new GameObject[3];
    Vector2 mousePosition;
   
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 playerToCamera = Camera.main.WorldToViewportPoint(transform.position);
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mousePosition);
        Debug.Log(playerToCamera);
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
       
        if (Input.GetMouseButtonDown(0)) {
            Shoot();
        }


    }
    void Shoot () {

    }
}
