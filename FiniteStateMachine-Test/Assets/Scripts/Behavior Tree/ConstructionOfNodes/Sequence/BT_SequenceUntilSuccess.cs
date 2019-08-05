using UnityEngine;
using System.Collections;

public class BT_SequenceUntilSuccess<T> : BT_SequenceNode<T> where T : class
{
	protected override bool ShouldContinueOnChildState(State childState)
	{
		return childState == State.Fail;
	}
	
	protected override State GetStateToReturn(int lastChildIndex, State lastIndexState)
	{
		if (lastIndexState == State.Success)
			return State.Success;
		else
			return State.Fail;
	}
	
	protected override State GetStateToReturnIterationFinished()
	{
		return State.Fail;
	}
}
