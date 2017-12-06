using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawm : MonoBehaviour {
    public EnemyType[] types;
    public Transform StartPos;
    public float waveRate;
    private Coroutine coroutine;

    public static int countEnemyLive = 0;
	// Use this for initialization
	void Start () {
        coroutine= StartCoroutine(Spawm());
    }
	
	// Update is called once per frame
	void Update () {
       
	}
    public void StopSpawn()
    {
        StopCoroutine(coroutine);
    }

    IEnumerator Spawm()
    {
        foreach(EnemyType wave in types)
        {
            for(int i=0;i<wave.count;i++)
            {
                countEnemyLive++;
                Instantiate(wave.enemy, StartPos.position, Quaternion.identity);
                yield return new WaitForSeconds(wave.rate);
            }
            //使用while可以做到无限暂停
            while (countEnemyLive > 0)
                yield return 0;
            yield return new WaitForSeconds(waveRate);
        }

        while (countEnemyLive > 0)//有敌人在场时，无限暂停
            yield return 0;
        GameManger.Instance.Win();
    }


    
}
