using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private bool invincible = false;
    SpriteRenderer sr;

    private IEnumerator Invincibility()
    {
        invincible = true;
        for (int i = 0; i < 10; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(.1f);
            sr.enabled = true;
        }
        invincible = false;
    }

    public Joystick joystick;
    private PlayerController controller;
    private float horizontalMove = 0f;
    public float runSpeed = 40f;

    private bool jump = false;
    AnimatorScript animatorScript;

    //public DisablePlatform disablePlatform;

    private void Start()
    {
        Application.targetFrameRate = 30;
        controller = GetComponent<PlayerController>();
        sr = GetComponent<SpriteRenderer>();
        animatorScript = GetComponentInChildren<AnimatorScript>();
    }

    private void Update()
    {
        horizontalMove = joystick.Horizontal == 0 ? 0 : joystick.Horizontal > 0 ? runSpeed : -runSpeed;
        animatorScript.Run(horizontalMove);
        if (controller.GetIsGrounded())
		{
            animatorScript.StopJumping();
		}

        /*if (Input.GetButton(crouchString))
		{
            if (disablePlatform != null)
            {
                StartCoroutine(disablePlatform.Disable());
            }
		}*/
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }

    public void Jump()
	{
        jump = true;
        animatorScript.StartJumping();
    }        

    /*private void OnCollisionEnter2D(Collision2D collision)
	{
        // Crouch
        if (collision.gameObject.layer == 3) // 3 for ground
        {
            if (disablePlatform == null)
            {
                disablePlatform = collision.gameObject.GetComponent<DisablePlatform>();
            }
        }
    }

	private void OnCollisionExit2D(Collision2D collision)
	{
        // Crouch
        if (collision.gameObject.layer == 3) // 3 for ground
        {
            if (disablePlatform != null)
            {
                disablePlatform = null;
            }
        }
    }*/
}