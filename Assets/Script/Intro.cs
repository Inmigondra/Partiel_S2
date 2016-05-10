using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Intro : MonoBehaviour {

	public void LoadLevel(int level){
        SceneManager.LoadScene(level);
    }
    public void QuitGame() {
        Application.Quit();
    }
}
