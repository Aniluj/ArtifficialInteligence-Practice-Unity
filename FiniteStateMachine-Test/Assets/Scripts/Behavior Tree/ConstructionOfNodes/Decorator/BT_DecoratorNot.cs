using UnityEngine;
using System.Collections;

public class BT_DecoratorNot<T> : BT_DecoratorNode<T> where T : class
{
	public BT_DecoratorNot() : base("Not")
	{
	}
	
	protected override State OnUpdate()
	{
		State otherConditionState = child.Update();
		
		switch (otherConditionState)
		{
			case State.Running:
				return State.Running;
				
			case State.Success:
				return State.Fail; //negation
				
			case State.Fail:
				return State.Success; //negation
		}
		
		return State.Fail;
	}

}
