using UnityEngine;
using System.Collections;

public class SpawnEnnemi : MonoBehaviour {
    public GameObject ennemi;
    float timer;
    float maxTimer;
	
	void Start () {
        maxTimer = Random.Range(1, 6);
    }
	// Update is called once per frame
	void Update () {
        timer += 1 * Time.deltaTime;
        if (timer >= maxTimer) {
            Instantiate(ennemi, transform.position, Quaternion.identity);
            timer = 0;
            maxTimer = Random.Range(1, 6);
        }
        
	}
}
