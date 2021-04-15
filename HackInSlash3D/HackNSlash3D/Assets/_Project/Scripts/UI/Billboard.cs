//
//  Adds BillBoard effect to UI, always faces the camera
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform camTransform;
    Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        camTransform = Camera.main.transform;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camTransform.rotation * originalRotation;
    }
}
