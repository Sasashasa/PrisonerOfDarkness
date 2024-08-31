using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
	public static GameInput Instance { get; private set; }
    
	public event EventHandler OnPlatformKeyDown;
	public event EventHandler OnJumpKeyDown;
	public event EventHandler OnJumpKeyUp;
	public event EventHandler OnLeftMouseButtonDown;
	public event EventHandler OnLeftMouseButtonUp;
	public event EventHandler OnPauseButtonDown;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))
		{
			OnPlatformKeyDown?.Invoke(this, EventArgs.Empty);
		}
		else if (Input.GetKeyDown(KeyCode.Space))
		{
			OnJumpKeyDown?.Invoke(this, EventArgs.Empty);
		}
		
		if (Input.GetKeyUp(KeyCode.Space))
		{
			OnJumpKeyUp?.Invoke(this, EventArgs.Empty);
		}

		if (Input.GetMouseButtonDown(0))
		{
			OnLeftMouseButtonDown?.Invoke(this, EventArgs.Empty);
		}
		
		if (Input.GetMouseButtonUp(0))
		{
			OnLeftMouseButtonUp?.Invoke(this, EventArgs.Empty);
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			OnPauseButtonDown?.Invoke(this, EventArgs.Empty);
		}
	}

	public float GetHorizontalMovementValue()
	{
		return Input.GetAxis("Horizontal");
	}

	public Vector3 GetMousePosition()
	{
		return Input.mousePosition;
	}
}