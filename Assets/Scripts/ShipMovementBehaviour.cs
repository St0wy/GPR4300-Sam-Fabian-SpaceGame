using System;
using UnityEngine;

public class ShipMovementBehaviour : MonoBehaviour
{
	private IInputHandler inputHandler;

	private void Awake()
	{
		inputHandler = GetComponent<IInputHandler>();
		inputHandler.Pause += Pause;
	}

	private void Pause()
	{
		Debug.Log("C'est la pause");
	}
}