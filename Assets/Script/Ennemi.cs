using UnityEngine;
using System.Collections;

public class Ennemi : MonoBehaviour {
    public GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        //return the viewportposition of the player on the main camera
        Vector3 ennemiToCamera = Camera.main.WorldToViewportPoint(transform.position);

        //if the viewportposition is out of (0,0) or (1,1)
        if (ennemiToCamera.x > 1 || ennemiToCamera.x < 0 || ennemiToCamera.y > 1 || ennemiToCamera.y < 0)
        {
            Destroy(gameObject);
            PlayerController.score += 1;
        }
        
        transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.001f);
    }
    
}
