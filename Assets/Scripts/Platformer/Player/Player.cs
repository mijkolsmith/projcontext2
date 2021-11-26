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

    private PlayerController controller;
    private float horizontalMove = 0f;
    public float runSpeed = 40f;

    private bool jump = false;
    private string horizontalString;
    private string jumpString;
    private string crouchString;

    public bool facingRight;

    //public DisablePlatform disablePlatform;

    private void Start()
    {
        horizontalString = gameObject.name + " Horizontal";
        jumpString = gameObject.name + " Vertical";
        crouchString = gameObject.name + " Crouch";
        controller = GetComponent<PlayerController>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw(horizontalString) * runSpeed;
        if (Input.GetButtonDown(jumpString))
        {
            jump = true;
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
        facingRight = controller.facingRight;
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