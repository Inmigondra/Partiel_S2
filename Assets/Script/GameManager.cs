using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour {
    public static GameManager control;
    public int selectedAvatar; //avatar used in game
    public int highscore;
    public int actualLevel;
    public bool firstOptional;
    public bool secondOptional;
    float timer;
    // Use this for initialization
    void Awake () {
        if (control == null) {
            DontDestroyOnLoad(this.gameObject);
            control = this;
        }else if (control != null){
            Destroy(gameObject);
        }
        Load();
	}
	void Start () {
        
    }
	// Update is called once per frame
	void Update () {
        
        if (actualLevel == 3) {
            timer += 1 * Time.deltaTime;
            if (timer > 5) {
                LevelLoad(0);
                timer = 0;
            }
            if (actualLevel == 0) {
                Load();
            }
            if (actualLevel == 1) {
                Load();
            }
            if (actualLevel == 3) {
                Save();
            }
        }

	}

    // LevelLoad is called to change scene
    public void LevelLoad (int level) {
        SceneManager.LoadScene(level);
        actualLevel = level;
    }
    //Choose the avatar
    public void SelectAvatar(int sel) {
        selectedAvatar = sel;
        if (firstOptional == false && sel == 2)
        {
            selectedAvatar = 1;
        }
        if (secondOptional == false && sel == 3)
        {
            selectedAvatar = 1;
        }
    }

    // QuitGame is called to quit the application
    public void QuitGame() {
        Application.Quit();
    }
  
    // Load is called to load player information
    public void Load () {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            GameStat gs = (GameStat)bf.Deserialize(file);
            highscore = gs.highscore;
            if (gs.firstAvatar == true)
                firstOptional = true;
            if (gs.secondAvatar == true)
                secondOptional = true;
            Debug.Log ("load");
            file.Close();
        }
    }

    // Save is called to save player's data
    public void Save () {
        BinaryFormatter bf = new BinaryFormatter() ;
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        GameStat gs = new GameStat();
        gs.highscore = highscore;
        if (firstOptional == true)
            gs.firstAvatar = true;
        if (secondOptional == true)
            gs.secondAvatar = true;
        bf.Serialize(file, gs);
        Debug.Log("save");
        file.Close();
    }

    [Serializable]
    class GameStat {
        public int highscore;
        public bool firstAvatar;
        public bool secondAvatar;
    }
}
