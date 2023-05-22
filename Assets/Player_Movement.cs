 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    [SerializeField] float m_speed = 6f;
    [SerializeField] float jump = 5f;
    CharacterController controller;
     void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        float h_input = Input.GetAxis("Horizontal");
        float v_input = Input.GetAxis("Vertical");
        bool isSprint = Input.GetKeyDown(KeyCode.LeftControl);
        float sprint =isSprint ? 7f: 1;

        rb.velocity = new Vector3(h_input * m_speed  * sprint, rb.velocity.y, v_input * m_speed * sprint);
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity=new Vector3(rb.velocity.x,jump,rb.velocity.z);
        }
    }
}
