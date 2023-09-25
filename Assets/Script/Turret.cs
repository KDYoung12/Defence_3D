using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Ÿ���� ������ ���� ����
    Transform target;

    public GameObject bulletPrefab;
    float fireCountDown = 0f;

    public string enemyTag = "Enemy";
    Enemy targetEnemy;

    public Transform partToRotate;
    public Transform firePos;
    public float turnSpeed = 10f;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 1f);
    }
    void UpdateTarget()
    {
        // Enemy Tag�� ���� ��ü�� ã��
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        // �ʱ�ȭ
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // ���� ����� Ÿ���� �����Ÿ� �������� ������ �߻�
        foreach (GameObject enemy in enemies)
        {
            // �Ÿ� ���ϱ�
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy; 
                nearestEnemy = enemy;
            }

            if (nearestEnemy != null && shortestDistance <= 4f)
            {
                target = nearestEnemy.transform;
                targetEnemy = nearestEnemy.GetComponent<Enemy>(); 
            }
            else
            {
                target = null;
            }
        }
    }

    private void Update()
    {
        // Ÿ���� ������ ������ ���ش�
        if (target == null)
        {
            return;
        }

        // ���� �˻��� �������� ȸ��
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0, rotation.y, 0);

        // �˻��� �� �˻��� �������� �Ѿ� �߻�
        if (fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 0.2f;
        }
        fireCountDown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGo = Instantiate(bulletPrefab, firePos.position, firePos.rotation)as GameObject;
        Bullet bullet = bulletGo.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }
}
