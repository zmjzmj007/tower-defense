using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : MonoBehaviour {
    private   List<GameObject> col = new List<GameObject>();//在炮台上加触发器，标记为trigger，每过一个敌人List都会增加一项，走出去就减少一个

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Enemy")
        {
            col.Add(other.gameObject);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
            col.Remove(other.gameObject);
    }

    public GameObject bulletPrefab;
    public Transform firePosition;
    public Transform head;
    public bool useLaser = false;
    public float fireRate = 1;
    public float damageRate = 40;
    private float timer;
    public LineRenderer line;
    public GameObject laserEffect;

    private void Start()
    {
        timer = fireRate;
    }

    private void Update()
    {
        //炮头的转向
        if(col.Count>0&&col[0]!=null)
        {
            Vector3 targetPos = col[0].transform.position;
            targetPos.y = head.position.y;
            head.transform.LookAt(targetPos);
        }
        
        if(useLaser==false)//不使用激光
        {
            timer += Time.deltaTime;
            if (col.Count > 0 && timer > fireRate)//当列表中有enemy时，且发射时间正确
            {
                Attack();
                timer = 0;
            }
        }
        else//使用激光
        {
            if(col.Count>0)//判断是否有敌人
            {
                laserEffect.SetActive(true);
                if(line.enabled==false)
                {
                    line.enabled = true;   //如果激光是关闭的，打开功能
                }
                if (col[0] == null)
                {
                    UpdataEnemys();//如果第一个敌人是空，则更新
                }
                if (col.Count > 0)//再次判断是否有敌人
                {
                    line.SetPosition(0, firePosition.position);
                    line.SetPosition(1, col[0].transform.position);
                    laserEffect.transform.position = col[0].transform.position;
                    Vector3 pos = transform.position;
                    pos.y = transform.position.y;//y轴对齐，不然会看不见
                    laserEffect.transform.LookAt(pos);
                    col[0].GetComponent<Enemy>().TakeDamage(damageRate * Time.deltaTime);
                }
            }
            else
            {
                line.enabled = false;
                laserEffect.SetActive(false);
            }
        }
        

    }
 

    void Attack()
    {
        if(col[0]==null)
        {
            UpdataEnemys();
        }

        if(col.Count>0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bullet>().SetTarget(col[0].transform);
        }
        else
        {
            timer = fireRate;
        }
            

        
    }

    //更新列表的null，敌人死亡时就会产生null，因此需要排除寻找下一个敌人
    void UpdataEnemys()
    {
        List<int> emptyEnemy = new List<int>();
        //遍历储存的敌人列表，如果遇到null就记下来
        for(int index=0;index<col.Count;index++)
        {
            if(col[index]==null)
            {
                emptyEnemy.Add(index);
            }
        }

        //移除null
        for(int i=0;i<emptyEnemy.Count;i++)
        {
            col.RemoveAt(emptyEnemy[i]-i);
        }
    }
}
