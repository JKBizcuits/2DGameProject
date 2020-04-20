using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    private Animator animator;
    public Vector2 movement;
    
    float deltaX;
    float absDeltaX;
    float deltaY;
    float absDeltaY;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Patrol>().movement;
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

        deltaX = movement.x;
        deltaY = movement.y;
        absDeltaX = System.Math.Abs(deltaX);
        absDeltaY = System.Math.Abs(deltaY);

        if(absDeltaX > absDeltaY)
        {
            if(deltaX > 0)
            {
                animator.SetFloat("IdleHorizontal", 1);
                animator.SetFloat("IdleVertical", 0);
                animator.SetFloat("Horizontal", 1);
                animator.SetFloat("Vertical", 0);
            }
            else if (deltaX < 0)
            {

                animator.SetFloat("IdleHorizontal", -1);
                animator.SetFloat("IdleVertical", 0);
                animator.SetFloat("Horizontal", -1);
                animator.SetFloat("Vertical", 0);
            }
        }
        if (absDeltaX < absDeltaY)
        {
            if (deltaY > 0)
            {
                animator.SetFloat("IdleHorizontal", 0);
                animator.SetFloat("IdleVertical", 1);
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", 1);
            }
            else if (deltaY < 0)
            {
                animator.SetFloat("IdleHorizontal", 0);
                animator.SetFloat("IdleVertical", -1);
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", -1);
            }
           
        }

        
    }
}
