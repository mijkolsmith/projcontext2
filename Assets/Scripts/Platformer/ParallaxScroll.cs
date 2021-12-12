using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    public Player player;
	private Vector2 startPos;

	private void Start()
	{
		startPos = transform.localPosition;
	}

	private void Update()
	{
		transform.localPosition = new Vector3(startPos.x + player.transform.localPosition.x / 1.5f, startPos.y + player.transform.localPosition.y / 2, transform.localPosition.z);
	}
}