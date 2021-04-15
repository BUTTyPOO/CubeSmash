//
//  Handles mouse look
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    Camera cam;
    [SerializeField] GameObject mesh;

    bool canLook = true;

    void Start()
    {
        cam = Camera.main;
    }

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
        LookAtMouse();
    }

    void LookAtMouse()
    {
        if (!canLook) return;

        Ray mouseWorldRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit mouseRayHit;
        
        if (Physics.Raycast(mouseWorldRay, out mouseRayHit))
        {
            Vector3 targetPosition = new Vector3(mouseRayHit.point.x, transform.position.y, mouseRayHit.point.z);
            mesh.transform.LookAt(targetPosition);
        }
    }

    void OnPlayerDeath()
    {
        canLook = false;
    }

}
