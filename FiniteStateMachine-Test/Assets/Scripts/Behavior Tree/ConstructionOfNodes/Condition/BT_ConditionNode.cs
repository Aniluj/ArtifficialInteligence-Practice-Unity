using UnityEngine;
using System.Collections;

public abstract class BT_ConditionNode<T> : BT_Node<T> where T : class
{
	protected override State OnUpdate()
	{
		if (Evaluate())
			return State.Success;
		else
			return State.Fail;
	}
	
	protected override void Sleep()
	{
		
	}
	
	protected override void Awake()
	{
		
	}
	
	protected override bool Validate()
	{
		return true;
	}
	
	protected abstract bool Evaluate();
}
