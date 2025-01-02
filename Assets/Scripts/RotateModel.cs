using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    // Start is called before the first frame update

    public float rotationSpeed;
  
    // Update is called once per frame
    void Update()
    {
        float rotation = Time.deltaTime * rotationSpeed;
        Vector3 rotate = new Vector3(0,rotation,0);
        transform.Rotate(rotate);
    }
}
