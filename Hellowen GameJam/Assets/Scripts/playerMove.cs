using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float rotationSpeed;
    private Rigidbody rigidbody;
    private Vector3 moveVector;
    private bool isFreze = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isFreze == false)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            moveVector = transform.right * x + transform.forward * z;

            RotatePlayer(moveVector);

            moveVector.y -= gravity * Time.fixedDeltaTime;
            rigidbody.velocity = Vector3.ClampMagnitude(moveVector, 1) * speed;
        }
    }

    public void Freeze(bool isFreze)
    {
        this.isFreze = isFreze;
        if (isFreze)
            rigidbody.velocity = Vector3.zero;
    }

    private void RotatePlayer(Vector3 directionVector)
    {
        if (directionVector.magnitude > Mathf.Abs(0.05f))
        {
            Quaternion rotationPlayer = Quaternion.LookRotation(directionVector);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotationPlayer, rotationSpeed * Time.deltaTime);
        }
    }
}


