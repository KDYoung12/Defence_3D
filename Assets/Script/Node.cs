using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector3 positionOffset;
    public GameObject turret;

    private void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("�̹� ��ž�� ����");
            return;
        }
            
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }
}
