using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody rb;

    bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        GetComponent<PlayerHealth>().playerDiedEvent += OnPlayerDeath;
    }

    void OnDisable()
    {
        GetComponent<PlayerHealth>().playerDiedEvent -= OnPlayerDeath;
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    void ApplyMovement()
    {
        if (canMove)
        {
            Vector3 movementVec = GetInputVec();
            movementVec = rb.rotation * movementVec;    //Tranlate to local space
            rb.MovePosition(rb.position + movementVec * moveSpeed * Time.fixedDeltaTime);
        }
    }

    Vector3 GetInputVec()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        return new Vector3(horizontal, 0, vertical);
    }

    void OnPlayerDeath()
    {
        canMove = false;
    }
}
