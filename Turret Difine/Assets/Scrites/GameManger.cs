using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour {
    public static GameManger Instance;
    public Text endMessage;
    public GameObject end;
    private EnemySpawm enemySpawm;

    private void Awake()
    {
        Instance = this;
        
    }
    // Use this for initialization
    void Start () {
        enemySpawm = GetComponent<EnemySpawm>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Win()
    {
        
        end.SetActive(true);
        endMessage.text = "胜 利";
    }

    public void Failed()
    {
        enemySpawm.StopSpawn();
        end.SetActive(true);
        endMessage.text = "失 败";
    }

    public void OnButtonRestart()
    {
        //SceneManager.LoadScene(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//获取当前的scene 并导入
    }

    public void OnButtonMenu()
    {
        SceneManager.LoadScene(0);
    }
}
