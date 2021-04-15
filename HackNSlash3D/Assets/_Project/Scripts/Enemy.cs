using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform target;
    [SerializeField] Slider healthSlider;
    [SerializeField] float dmgVal;
    [SerializeField] float attackRange;
    [SerializeField] float attackRate;
    [SerializeField] GameObject weaponMesh;

    float currentHealth;
    Rigidbody rb;
    bool isAttacking = false;


    void Start()
    {
        if (!target)
            target = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        LookAtTarget();
        AttackIfICan();
    }

    void FixedUpdate()
    {
        MoveTorwardTarget();   
    }

    void AttackIfICan()
    {
        if (isAttacking) return;
        
        if (InAttackRange())
            StartCoroutine(AttackLoop());
    }
    
    bool InAttackRange()
    {
        return Vector3.Distance(transform.position, target.position) <= attackRange;
    }

    IEnumerator AttackLoop()
    {
        if (isAttacking) yield break;

        isAttacking = true;
        while (InAttackRange())
        {
            AnimateAttack();
            Attack();
            yield return new WaitForSeconds(attackRate);
        }
        isAttacking = false;
    }

    void Attack()
    {
        int layerMask = 1 << 7;
        RaycastHit raycast;
        if (Physics.Raycast(transform.position, transform.forward, out raycast, attackRange, ~layerMask))
        {
            if (raycast.collider.CompareTag("Player"))
            {
                IDamageable damageable = raycast.collider.gameObject.GetComponent<IDamageable>();
                damageable?.TakeDamage(dmgVal);
                SoundManager.instance.PlaySound(0);
            }
        }
    }

    void AnimateAttack()
    {
        Vector3 punchVec = weaponMesh.transform.forward;
        punchVec.z += 1;
        weaponMesh.transform.DOPunchScale(Vector3.one, 0.5f, 0).SetEase(Ease.OutBounce);
    }

    void LookAtTarget()
    {
        transform.LookAt(target);
    }

    void MoveTorwardTarget()
    {
        rb.MovePosition(Vector3.MoveTowards(rb.position, target.position, moveSpeed * Time.fixedDeltaTime));
    }

    public void TakeDamage(float dmgVal)
    {
        currentHealth -= dmgVal;
        if (currentHealth <= 0) Die();
        UpdateSliderVal();
    }

    void UpdateSliderVal()
    {
        healthSlider.value = currentHealth / maxHealth;
    }

    void Die()
    {
        Destroy(gameObject);
        SoundManager.instance.PlaySound(1);
    }
}
