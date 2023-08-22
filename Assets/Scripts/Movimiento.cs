using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [SerializeField] private Colision col;
    bool isleft = false;
    bool isright = false;

    public Rigidbody rb;
    public float speedForce;
    private void Start()
    {
        TryGetComponent(out col);
    }
    public void clickleft()
    {
        isleft = true;
    }
    public void releaseleft()
    {
        isleft = false;
    }
    public void clickright()
    {
        isright = true;
    }
    public void releaseright()
    {
        isright = false;
    }

    private void LateUpdate()
    {
        if (!col.Checkd)
        {
            if (Input.GetButtonDown("Horizontal")) rb.AddForce(new Vector3(0, 0, speedForce * Time.deltaTime));
        }
    }

    private void FixedUpdate()
    {
        if (!col.Checkd)
        {
            if (isright) rb.AddForce(new Vector3(0, 0, speedForce) * Time.deltaTime);
            else if (isleft) rb.AddForce(new Vector3(0, 0, -speedForce) * Time.deltaTime);
        }
    }
}
