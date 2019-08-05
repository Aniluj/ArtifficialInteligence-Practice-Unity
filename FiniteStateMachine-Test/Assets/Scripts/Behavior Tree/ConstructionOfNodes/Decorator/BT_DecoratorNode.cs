using UnityEngine;
using System.Collections;

public abstract class BT_DecoratorNode<T> : BT_NodeWithChild<T> where T : class
{
	public BT_DecoratorNode(string debugName = null) : base(debugName)
	{
	}

	protected override void Awake()
	{
	}	
	
	protected override void Sleep()
	{
		child.Reset();
	}
	
	protected override bool Validate()
	{
		return true;
	}
}
