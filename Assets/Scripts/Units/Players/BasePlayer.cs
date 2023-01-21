using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : BaseUnit
{
    [SerializeField] public int moveDistance;
    public void ShootBulletAtMouse(BaseCard selectedCard)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 direction = mousePos - transform.position;
        direction.Normalize();

        GameObject projectile = Instantiate(selectedCard.shotPrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<BaseAttack>().damage = selectedCard.card.damage;
        projectile.GetComponent<BaseAttack>().direction = direction;
        projectile.GetComponent<BaseAttack>().deathParticle = selectedCard.card.deathParticle;
        projectile.GetComponent<Rigidbody2D>().AddForce(direction * selectedCard.card.speed * 100);

        CardManager.Instance.RemoveCardFromQueue(selectedCard);
        CardManager.Instance.UpdateNewCardQueue();
        Destroy(projectile, selectedCard.card.range);
        Destroy(selectedCard);
        Destroy(CardManager.Instance.SelectedCard.gameObject);
        CardManager.Instance.SetCanShoot(false);
        CardManager.Instance.SetHasShot(true);
        MenuManager.Instance.ShowSelectedCard(null);
    }
}
