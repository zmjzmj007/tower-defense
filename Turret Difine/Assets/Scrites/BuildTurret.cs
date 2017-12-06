using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildTurret : MonoBehaviour {
    //三种炮台的数据，在面板重要填入
    public TurretType standardData;
    public TurretType missileData;
    public TurretType laserData;

    public Text text;//money的显示
    public Animator animator;
    public GameObject upgradeCanves;
    public Button upgradeButton;
    

    private int Money = 1000;
    //当前选择的炮塔
    private TurretType selectedTurret;

    private Animator canvsAnim;
    //当前选择的mapcube
    private MapCube selectedMapcube;
	// Use this for initialization
	void Start () {
        canvsAnim = upgradeCanves.GetComponent<Animator>();
	}

    void ChangeMoney(int cost=0)
    {
        Money += cost;
        text.text = "$" + Money;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject()==false)//确保鼠标不是点击在UI层上面
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray,out hit,1000,LayerMask.GetMask("MapCube")))
                {
                    MapCube cube = hit.collider.GetComponent<MapCube>();
                    if(selectedTurret!=null&&cube.turretGo==null)
                    {
                        if(Money>selectedTurret.cost)
                        {
                            //钱足够，可以建造
                            ChangeMoney(-selectedTurret.cost);
                            cube.Build(selectedTurret);

                        }
                        else
                        {
                            //钱不够不可以建造，提示动画
                            animator.SetTrigger("filcker");
                        }
                    }
                    else if(cube.turretGo!=null)
                    {
                        //升级处理
                        if(selectedMapcube==cube&&upgradeCanves.activeInHierarchy)
                        {
                            StartCoroutine(HideUpgrade());
                        }
                        else
                        {
                            
                            ShowUpgradeUI(cube.transform.position, cube.isUpgrade);
                        }

                        selectedMapcube = cube;//更新cube

                    }
                    
                }

            }
        }
	}


    //通过事件来保存选择的炮台
    public void OnStandardSelected(bool isOn)
    {
        if(isOn)
        {
            selectedTurret = standardData;
        }
    }

    public void OnMissileSelected(bool isOn)
    {
        if(isOn)
        {
            selectedTurret = missileData;
        }
    }

    public void OnLaserSelected(bool isOn)
    {
        selectedTurret = laserData;
    }

    public void OnUpgradeButtonDown()
    {
        if(Money>=selectedMapcube.type.costUpgrad)
        {
            ChangeMoney(-selectedMapcube.type.costUpgrad);
            selectedMapcube.UpgradeTurret();
        }
        else
        {
            animator.SetTrigger("filcker");
        }
        
        StartCoroutine(HideUpgrade());
    }

    public void OnDestoryButtonDown()
    {
        ChangeMoney(+selectedMapcube.type.destoryMoney);
        selectedMapcube.DestoryTurret();
        
        StartCoroutine(HideUpgrade());
        
    }

     void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade=false)
    {
        StopCoroutine("HideUpgrade");//为了防止显示完一个升级菜单后，马上又点了别的炮台的升级菜单显示，需要停止隐藏菜单的协程
        upgradeCanves.SetActive(false);//重置，使得播放动画时是从头开始放，不然升级菜单会直接显示出来
        upgradeCanves.SetActive(true);
        upgradeCanves.transform.position = pos;
        upgradeButton.interactable = !isDisableUpgrade;
    }

    
    //不能直接禁用升级菜单，因为这样会不能播放隐藏菜单动画，所以要用协程
     IEnumerator HideUpgrade()
    {
        canvsAnim.SetTrigger("hide");
        yield return new WaitForSeconds(1f);
        upgradeCanves.SetActive(false);
    }

}
