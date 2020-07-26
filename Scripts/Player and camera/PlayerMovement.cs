using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 6f;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    float camRayLength = 100f;
    int floormask;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        floormask = LayerMask.GetMask("Floor");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        move(h, v);
      //  turn();
        Animating(h, v);
    }

    void move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }



    void Animating(float h, float v)
    {
        bool run = !((v == 0) && (h == 0));
        anim.SetBool("walk",run ); 

    }

}
