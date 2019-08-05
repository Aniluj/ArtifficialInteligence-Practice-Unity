using UnityEngine;
using System.Collections;

public class BT_LogicOr<T> : BT_LogicNode<T> where T : class
{
	protected override State OnUpdate()
	{
		for (int i = 0; i < children.Count; i++)
		{
			State childState = children[i].Update();
			
			if (childState == State.Success)
				return State.Success;
		}
		
		return State.Fail;
	}
}
