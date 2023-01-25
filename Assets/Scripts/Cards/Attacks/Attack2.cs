using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Child script for the second attack for any extra special functionality unique to this attack
 */

public class Attack2 : BaseAttack
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        if(collision.gameObject.GetComponent<Tile>() != null)
        {
            Vector2 treeLocation = collision.gameObject.GetComponent<Tile>().gridLocation;
            Destroy(collision.gameObject);
            var deadTile = Instantiate(GridManager.Instance.GetDeadTile(), (Vector3)treeLocation, Quaternion.identity);
            deadTile.name = $"tile {treeLocation.x} {treeLocation.y}";
            deadTile.GetComponent<Tile>().gridLocation = new Vector3(treeLocation.x, treeLocation.y);
            deadTile.init((int) treeLocation.x, (int) treeLocation.y);
            GridManager.Instance.tiles[treeLocation] = deadTile;
        }
        if (collision.gameObject.GetComponent<BaseEnemy>() != null)
        {
            collision.gameObject.GetComponent<BaseEnemy>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
