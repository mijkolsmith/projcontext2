using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    private Animator animator;
    private bool running;
    private bool jump;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("rigIdle"))
		{
            Debug.Log("Current state is Idle");
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("rigRun"))
        {
            Debug.Log("Current state is Run");
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("rigJump"))
        {
            Debug.Log("Current state is Jump");
        }

        animator.SetFloat("Speed", running ? 1 : 0);
        animator.SetBool("Jump", jump);
    }

    public void Run(float speed)
	{
        if(speed > 1 || speed < -1)
		{
            running = true;
		}
        else
		{
            running = false;
		}
	}

    public void StartJumping()
	{
        jump = true;
	}

    public void StopJumping()
	{
        jump = false;
	}
}