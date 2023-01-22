using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : MonoBehaviour
{
    public int damage;
    public Vector2 direction;
    public GameObject deathParticle;
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        if (collision.gameObject.GetComponent<BaseEnemy>() != null)
        {
            collision.gameObject.GetComponent<BaseEnemy>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
