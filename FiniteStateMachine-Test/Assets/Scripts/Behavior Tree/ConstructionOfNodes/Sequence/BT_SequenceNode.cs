using UnityEngine;
using System.Collections;

public abstract class BT_SequenceNode<T> : BT_NodeWithChildren<T> where T : class
{
	/// <summary>
	/// Resume execution on the last running node
	/// </summary>
	public bool resumeFromLastRunningNode;
	
	/// <summary>
	/// Store the next child to update
	/// </summary>
	private int lastRunningChildrenIndex = -1;

	protected override bool Validate()
	{
		return true;
	}

	protected override void Awake()
	{
	}

	protected override State OnUpdate()
	{
		int fromIndex = 0;
		
		if (lastRunningChildrenIndex >= 0 && resumeFromLastRunningNode)
			fromIndex = lastRunningChildrenIndex;
		
		for (int i = fromIndex; i < children.Count; i++)
		{
			State childState = children[i].Update();
			
			if (childState == State.Running)
			{
				//If the children that returned "running" is before the last "running" children, then reset
				//the state of the last running children.
				if (lastRunningChildrenIndex >= 0 && i < lastRunningChildrenIndex)
					children[lastRunningChildrenIndex].Reset();
				
				lastRunningChildrenIndex = i;
				
				return State.Running;
			}
			else if (childState == State.Fail && i == lastRunningChildrenIndex)
			{
				//If the children that returned "fail" is the same that was "runnning" before, then
				//reset the state of that children.
				children[lastRunningChildrenIndex].Reset();
				
				lastRunningChildrenIndex = -1;
			}
			
			if (!ShouldContinueOnChildState(childState))
			{
				//If we stop execution, then reset the state of the last running children.
				if (lastRunningChildrenIndex >= 0)
					children[lastRunningChildrenIndex].Reset();
				
				lastRunningChildrenIndex = -1;
				
				//Get state to return
				State interruptedStateToReturn = GetStateToReturn(i, childState);
				
				return interruptedStateToReturn;
			}
		}
		
		//End reached, get state to return
		lastRunningChildrenIndex = -1;
		
		State endStateToReturn = GetStateToReturnIterationFinished();
		
		return endStateToReturn;
	}

	protected override void Sleep()
	{
		if (lastRunningChildrenIndex >= 0)
		{
			children[lastRunningChildrenIndex].Reset();
			lastRunningChildrenIndex = -1;
		}
	}

	protected abstract bool ShouldContinueOnChildState(State childState);
	
	protected abstract State GetStateToReturn(int lastChildIndex, State lastIndexState);
	
	protected abstract State GetStateToReturnIterationFinished();
}
