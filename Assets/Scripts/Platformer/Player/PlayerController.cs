using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.

	const float k_GroundModx = .15f; //Size of the x component of the groundCheck square
	const float k_GroundMody = .08f; //Size of the y component of the groundCheck square

	private bool m_Grounded; // Whether or not the player is grounded
	private Rigidbody2D m_rb2D;
	private bool m_facingRight = true; // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void Awake()
	{
		m_rb2D= GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	public bool GetIsGrounded()
	{
		return m_Grounded;
	}

	private void FixedUpdate()
	{
		//bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a Physics Raycast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		//Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		Collider2D[] colliders = Physics2D.OverlapAreaAll(
			new Vector2(m_GroundCheck.position.x - k_GroundModx, m_GroundCheck.position.y + k_GroundMody),
			new Vector2(m_GroundCheck.position.x + k_GroundModx, m_GroundCheck.position.y - k_GroundMody),
			m_WhatIsGround);

		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
			}
		}
	}

	public void Move(float move, bool jump)
	{
		// Only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_rb2D.velocity.y);

			// And then smoothing it out and applying it to the character
			m_rb2D.velocity = Vector3.SmoothDamp(m_rb2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move < 0 && m_facingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move > 0 && !m_facingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		// For this game, it's not needed to check whether the player is grounded or not.
		// You can't spam jump and you have a limited amount of jumps, so we need to be as forgiving as possible.
		if (jump) //&& m_Grounded
		{
			// Add a vertical force to the player.
			//m_Grounded = false;
			m_rb2D.velocity = new Vector2(m_rb2D.velocity.x, 0f);
			m_rb2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_facingRight = !m_facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 spriteScale = transform.GetChild(0).localScale;
		spriteScale.z *= -1; //z is up today
		transform.GetChild(0).localScale = spriteScale;
	}
}