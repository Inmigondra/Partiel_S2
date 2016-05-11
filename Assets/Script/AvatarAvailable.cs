using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AvatarAvailable : MonoBehaviour {
    public int typeAvatar;
    Image image;
    public Sprite blue;
    public Sprite red;
    GameManager GM;
    GameObject gameManager;
	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager");
        GM = gameManager.GetComponent<GameManager>();
        image = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (typeAvatar == 1) {
            if (GM.firstOptional == true)
                image.sprite = blue;
        }
        if (typeAvatar == 2){
            if (GM.secondOptional == true)
                image.sprite = red;
        }
	}
}
