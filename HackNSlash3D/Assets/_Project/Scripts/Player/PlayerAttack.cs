using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponMesh;
    [SerializeField] float weaponDmg;

    bool isAttacking = false;
    bool canAttack = true;

    void OnEnable()
    {
        GetComponent<PlayerHealth>().playerDiedEvent += OnPlayerDeath;
    }

    void OnDisable()
    {
        GetComponent<PlayerHealth>().playerDiedEvent -= OnPlayerDeath;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void Attack()
    {
        if (!canAttack) return;

        AnimateAttack();

        RaycastHit raycast;
        int layerMask = 1 << 6;
        if (Physics.Raycast(transform.position, weapon.transform.forward, out raycast, 3.0f, ~layerMask))
        {
            if (raycast.collider.CompareTag("Enemy"))
            {
                IDamageable damageable = raycast.collider.gameObject.GetComponent<IDamageable>();
                damageable?.TakeDamage(weaponDmg);
                SoundManager.instance.PlaySound(0);
            }
            print(raycast.collider.gameObject);
        }
    }

    void AnimateAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            Vector3 punchVec = weaponMesh.transform.forward;
            punchVec.z += 1;
            weaponMesh.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBounce).OnComplete(SetAttackBoolFalse);
        }
    }

    void SetAttackBoolFalse()
    {
        weaponMesh.transform.DOScale(Vector3.zero, 0.1f);
        isAttacking = false;
    }

    void OnPlayerDeath()
    {
        canAttack = false;
        SoundManager.instance.PlaySound(1);
    }
}
