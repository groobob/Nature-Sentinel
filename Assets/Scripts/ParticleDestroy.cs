using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class created to add behaviour to a particle effect
 */

public class ParticleDestroy : MonoBehaviour
{
    // Destroys the game object this script is attached to in 10 seconds
    private void Awake()
    {
        Destroy(gameObject, 10f);
    }
}
