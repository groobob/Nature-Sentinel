using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : BaseUnit
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && CardManager.Instance.canShoot)
        {
            ShootBulletAtMouse(CardManager.Instance.SelectedCard);
        }
    }

    public void ShootBulletAtMouse(BaseCard card)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 direction = mousePos - transform.position;
        direction.Normalize();

        GameObject projectile = Instantiate(card.shotPrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().AddForce(direction * card.speed);
        Destroy(projectile, 10f);
        Destroy(card);
        CardManager.Instance.SelectedCard = null;
        CardManager.Instance.canShoot= false;
    }
}
