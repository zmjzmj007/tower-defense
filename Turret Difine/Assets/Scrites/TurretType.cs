using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretType  {
    public GameObject turretPrefab;
    public GameObject turretUpgradedPrefab;
    public int cost;
    public int costUpgrad;
    public int destoryMoney;

    public Turret turret;
}

public enum Turret
{
    standardTurret,missileTurret,laserTurret
}
