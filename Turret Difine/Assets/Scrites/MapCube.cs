using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour {
    [HideInInspector]
    public GameObject turretGo;
    [HideInInspector]
    public bool isUpgrade = false;//0
    public GameObject effectPrefab;
    [HideInInspector]
    public TurretType type;
    private Renderer renderer;
	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public  void Build(TurretType turret)
    {
        this.type = turret;
        isUpgrade = false;
        turretGo= Instantiate(turret.turretPrefab, transform.position, Quaternion.identity);
        GameObject effectGo = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        Destroy(effectGo, 1.5f);
    }

    //升级炮台
    public void UpgradeTurret()
    {
        if (isUpgrade == true) return;//如果isUpgrade为0时

        isUpgrade = true;
        Destroy(turretGo);
        turretGo = Instantiate(type.turretUpgradedPrefab, transform.position, Quaternion.identity);
        GameObject effectGo = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        Destroy(effectGo, 1.5f);


    }

    //摧毁炮台
    public void DestoryTurret()
    {
        GameObject effectGo = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        Destroy(turretGo);
        Destroy(effectGo, 1.5f);
        isUpgrade = false;
        turretGo = null;
        type = null;

    }

    private void OnMouseEnter()
    {
        //检测是否在UI层上//且上面没有炮台
        if(EventSystem.current.IsPointerOverGameObject()==false&&turretGo==null)
        {
            renderer.material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }
}
