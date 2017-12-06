using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    public float speed = 10;
    public float health = 100;
    public GameObject enemyExplosionEffectPrefab;
    public Slider slider;

    private float totalHealth;
    private Transform[] points;
    private int index = 0;
	// Use this for initialization
	void Start () {
        points = WayPoint.positions;
        totalHealth = health;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        if (index > points.Length - 1) return;//到终点后就返回
        transform.Translate((points[index].position - transform.position).normalized * speed * Time.deltaTime);
        if(Vector3.Distance(points[index].position,transform.position)<0.2)//判断当前位置离下一个点还有多少距离，就加一找下一个点
        {
            index++;
        }
        if (index > points.Length-1)
        {
            ReachDes();
        }
    }

    //到达终点
    void ReachDes()
    {
        GameManger.Instance.Failed();
        GameObject.Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        EnemySpawm.countEnemyLive--;
    }

    public void TakeDamage(float damage)
    {
        if (health <= 0)
        {
            return;
        }
            
        health -= damage;
        slider.value = health / totalHealth;
        if (health<=0)
        {
            Die();
        }

        
    }

    //死亡后的效果
    void Die()
    {
        GameObject enemyExplosEffect = Instantiate(enemyExplosionEffectPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
        Destroy(enemyExplosEffect, 1.5f);
    }
}
