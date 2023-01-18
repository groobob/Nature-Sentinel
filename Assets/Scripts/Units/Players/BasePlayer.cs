using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : BaseUnit
{
    [SerializeField] public int moveDistance;
    public void ShootBulletAtMouse(BaseCard selectedCard)
    {
        Debug.Log("shot triggered");
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 direction = mousePos - transform.position;
        direction.Normalize();

        GameObject projectile = Instantiate(selectedCard.card.shotPrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<BaseAttack>().damage = selectedCard.card.damage;
        projectile.GetComponent<Rigidbody2D>().AddForce(direction * selectedCard.card.speed * 1000);
        projectile.GetComponent<BaseAttack>().direction = direction;
        Destroy(projectile, selectedCard.card.range);
        Destroy(selectedCard);
        Destroy(CardManager.Instance.SelectedCard.gameObject);
        Debug.Log(CardManager.Instance.SelectedCard);
        CardManager.Instance.canShoot = false;
    }
}
