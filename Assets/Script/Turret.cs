using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // 타겟을 지정할 변수 생성
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
        // Enemy Tag를 가진 객체를 찾음
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        // 초기화
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // 가장 가까운 타겟이 사정거리 안쪽으로 들어오면 발사
        foreach (GameObject enemy in enemies)
        {
            // 거리 구하기
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
        // 타겟이 없으면 리턴을 해준다
        if (target == null)
        {
            return;
        }

        // 적을 검색한 방향으로 회전
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0, rotation.y, 0);

        // 검색한 후 검색한 방향으로 총알 발사
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
