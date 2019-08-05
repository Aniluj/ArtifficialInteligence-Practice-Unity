using UnityEngine;
using System.Collections;

public class BT_LogicAnd<T> : BT_LogicNode<T> where T : class
{
	protected override State OnUpdate()
	{
		for (int i = 0; i < children.Count; i++)
		{
			State childState = children[i].Update();
			
			if (childState == State.Fail)
				return State.Fail;
		}
		
		return State.Success;
	}

}
