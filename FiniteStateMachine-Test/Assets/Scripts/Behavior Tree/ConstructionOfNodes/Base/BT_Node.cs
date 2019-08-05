using UnityEngine;
using System.Collections;

public abstract class BT_Node<T> where T : class
{
	public enum State
	{
		None,
		Success,
		Fail,
		Running,
	}

	public T Blackboard;

	protected State currentState = State.None;
	protected State lastState = State.None;
	private string debugName;

	public BT_Node(T blackboard = null, string debugName = null)
	{
		this.Blackboard = blackboard;
		this.debugName = debugName;
	}
		
	public override string ToString()
	{
		if (!string.IsNullOrEmpty(debugName))
			return debugName;
		
		return base.ToString();
	}

	protected bool IsRunning
	{
		get { return currentState == State.Running; }
	}

	protected bool Failed
	{
		get { return currentState == State.Fail; }
	}

	protected bool Success
	{
		get { return currentState == State.Success; }
	}

	protected abstract bool Validate();
	
	protected abstract void Awake();
	
	protected abstract State OnUpdate();
	
	protected abstract void Sleep();

	public void Reset()
	{
		if (currentState == State.Running) 
		{
			Sleep();
			currentState = State.None;
		}
	}

	public State Update()
	{
		if (Validate()) {
			if (currentState != State.Running) {
				Awake();
				currentState = State.Running;
			}

			lastState = currentState = OnUpdate();

			if (currentState != State.Running) {
				Reset();
			}
		}
		else 
		{
			lastState = State.Fail;
			Reset();
		}
	
		return lastState; 
	}


}
