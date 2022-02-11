using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementBehaviour : MonoBehaviour
{
	[SerializeField] private float speed = 1f;

	private IInputHandler inputHandler;
	private Rigidbody2D rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		inputHandler = GetComponent<IInputHandler>();
		inputHandler.Pause += Pause;
	}

	private void FixedUpdate()
	{
		rb.velocity = inputHandler.InputVector * speed * Time.deltaTime;
	}

	private void Pause()
	{
		Debug.Log("C'est la pause");
	}
}
