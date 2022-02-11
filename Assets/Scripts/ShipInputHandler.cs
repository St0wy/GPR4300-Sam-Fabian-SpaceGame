using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipInputHandler : MonoBehaviour, IInputHandler
{
	public Vector2 InputVector { get; private set; }
	public event IInputHandler.InputEventHandler FirePrimary;
	public event IInputHandler.InputEventHandler FireSecondary;
	public event IInputHandler.InputEventHandler Pause;

	[UsedImplicitly]
	private void OnMove(InputValue value)
	{
		InputVector = value.Get<Vector2>();
	}

	[UsedImplicitly]
	private void OnPause()
	{
		Pause?.Invoke();
	}

	[UsedImplicitly]
	private void OnFire()
	{
		FirePrimary?.Invoke();
	}

	[UsedImplicitly]
	private void OnFire2()
	{
		FireSecondary?.Invoke();
	}
}
