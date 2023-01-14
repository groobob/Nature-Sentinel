using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : BaseAttack
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
