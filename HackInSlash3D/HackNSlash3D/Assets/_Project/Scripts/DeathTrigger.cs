using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        IDamageable dmgAble = col.GetComponent<IDamageable>();
        dmgAble?.TakeDamage(float.MaxValue);
    }
}