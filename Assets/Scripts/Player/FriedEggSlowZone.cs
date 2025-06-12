using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriedEggSlowZone : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.ApplySlow();
        }
    }
}
