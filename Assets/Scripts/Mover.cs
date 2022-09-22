using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;
    protected float moveSpeed = 1.0f;
    protected float activeMoveSpeed;
    protected float dashSpeed = 3.0f;
    protected float dashLength = .1f, dashCooldown = .75f;
    protected float dashCounter, lastDash;
    protected float dashCoolCounter;
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        activeMoveSpeed = moveSpeed;
    }

    protected virtual void UpdateMotor(Vector3 input)
    {

        moveDelta = new Vector3(input.x * activeMoveSpeed, input.y * activeMoveSpeed, 0);

        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        moveDelta += pushDirection;
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRevocerySpeed);

        hit = Physics2D.BoxCast(transform.position,
                                boxCollider.size,
                                0,
                                new Vector2(0, moveDelta.y),
                                Mathf.Abs(moveDelta.y * Time.deltaTime),
                                LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }


        hit = Physics2D.BoxCast(transform.position,
                                boxCollider.size,
                                0,
                                new Vector2(moveDelta.x, 0),
                                Mathf.Abs(moveDelta.x * Time.deltaTime),
                                LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            
            if(dashCoolCounter <=0 && dashCounter <= 0)
            {
                    
                    activeMoveSpeed = dashSpeed;
                    dashCounter = dashLength;
      
            }
        }

        if(dashCounter > 0)
        {
            
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            
            dashCoolCounter -= Time.deltaTime;
        }

    }
}
