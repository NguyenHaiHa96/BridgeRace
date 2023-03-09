using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

public class Player : MonoBehaviour
{
    public Transform Transform { get => transform; }
    public Vector3 WorldPosition { get => Transform.position;  set => Transform.position = value; }

    [SerializeField] private Rigidbody rb;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask stairLayer;
    [SerializeField] private float speed = 5f;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float y = CheckGround();
        rb.velocity = new Vector3(joystick.Horizontal * speed * Time.deltaTime, y, joystick.Vertical * speed * Time.deltaTime);
    }

    private float CheckGround()
    {
        RaycastHit hit;

        if (Physics.Raycast(WorldPosition, Vector3.down, out hit, 5f, groundLayer))
        {
            return WorldPosition.y;
        }

        return 0;
    }

    private bool CheckStair(Vector3 nextPoint)
    {
        bool canMove = true;
        RaycastHit hit;
        Debug.DrawLine(nextPoint, nextPoint + Vector3.down * 5f);
        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 5f, stairLayer))
        {
            return canMove;
        }
        return canMove;
    }
}
