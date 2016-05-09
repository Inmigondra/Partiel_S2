using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour {
    public static GameManager control;
    public GameObject[] avatars = new GameObject[3];

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

    // LevelLoad is called to change scene
    public void LevelLoad (int level) {
        SceneManager.LoadScene(level);
        nextLevel = level;
    }

    // QuitGame is called to quit the application
    public void QuitGame() {
        Application.Quit();
    }

    // Load is called to load player information
    void Load () {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            GameStat gt = (GameStat)bf.Deserialize(file);
            file.Close();
        }
    }

    // Save is called to save player's data
    void Save () {
        BinaryFormatter bf = new BinaryFormatter() ;
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
        GameStat gt = new GameStat();

        bf.Serialize(file, gt);
        file.Close();
    }

    [Serializable]
    class GameStat {
        int highscore;
        bool firstAvatar;
        bool secondAvatar;
    }
}
