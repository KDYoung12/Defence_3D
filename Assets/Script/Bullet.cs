using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform target;

    public float speed, attackDamage;

    public void Seek(Transform _Target)
    {
        target = _Target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // �Ѿ� �����̴� ����
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        // target.GetComponent<Enemy>().ComputeDamage(attackDamage);
        Destroy(gameObject);
    }
}
