using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 0;
    [SerializeField] Animator m_animator;
    private Rigidbody2D _rigidbody2D;
    public bool canMove = true;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (canMove)
        {
            Movement();
        }
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            m_animator.ResetTrigger("WalkLeft");
            m_animator.SetTrigger("WalkRight");

            _rigidbody2D.AddForce(Vector2.right*Speed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            m_animator.ResetTrigger("WalkRight");
            m_animator.SetTrigger("WalkLeft");

            _rigidbody2D.AddForce(Vector2.left * Speed);
        }
        else{
            m_animator.ResetTrigger("WalkLeft");
            m_animator.ResetTrigger("WalkRight");
        }
    }
}
