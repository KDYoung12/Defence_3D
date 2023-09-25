using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    GameObject turretToBuild;

    private void Awake()
    {
        instance = this;
    }

    public GameObject standardTurretBuild;

    private void Start()
    {
        turretToBuild = standardTurretBuild;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}
