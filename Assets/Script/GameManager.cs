using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour {
    public static GameManager control;
    GameObject selectedAvatar; //avatar used in game

    public Texture[] avatarImage = new Texture[3];
    int highscore;
    int nextLevel;
    bool firstOptional;
    bool secondOptional;


    // Use this for initialization
    void Awake () {
        if (control == null) {
            DontDestroyOnLoad(this.gameObject);
            control = this;
        }else if (control != null){
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void LevelLoad (int level) {
        SceneManager.LoadScene(level);
    }
    public void QuitGame() {
        Application.Quit();
    }
    void Load () {

    }
    void Save () {
        BinaryFormatter bf = new BinaryFormatter() ;
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
        GameStat gt = new GameStat();

        bf.Serialize(file, gt);
        file.Close();
    }

    [Serializable]
    class GameStat {

    }
}
