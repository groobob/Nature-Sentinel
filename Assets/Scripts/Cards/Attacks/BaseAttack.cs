using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : MonoBehaviour
{
    public int damage;
    public Vector3 direction;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BaseEnemy>() != null)
        {
            collision.gameObject.GetComponent<BaseEnemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
