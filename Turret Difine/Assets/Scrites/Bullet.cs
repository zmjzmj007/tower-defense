using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 40;
    public int damage = 20;

    public GameObject explosioneffectPrefab;

    private Transform target;



    public void SetTarget(Transform _target)
    {
        target = _target;
    }


    //这里无法使用碰撞器碰撞来检测，因为子弹移动速度快，会检测不到，下面是用子弹和敌人之间的距离来做判断，靠近一个距离时，子弹就销毁，在产生effect
    void FixedUpdate()
    {
        if(target==null)
        {
            Destroy(gameObject);//如果敌人没了，就返回，不然子弹就停住不动了
            return;
        }

        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Vector3 dir = target.position - transform.position;
        if(dir.magnitude<1.4f)
        {
            target.GetComponent<Enemy>().TakeDamage(damage);
            Die();
        }
        
    }

    void Die()
    {
        GameObject exolosion = Instantiate(explosioneffectPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
        Destroy(exolosion, 1);
    }


    
}
