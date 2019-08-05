using UnityEngine;
using System.Collections;

public abstract class BT_LogicNode<T> : BT_NodeWithChildren<T> where T : class
{
	public override bool CanAddChild(BT_Node<T> child)
	{
		return child is BT_ConditionNode<T> || child is BT_DecoratorNot<T>;
	}
	
	protected override void Awake()
	{
		
	}
	
	protected override void Sleep()
	{
		
	}
	
	protected override bool Validate()
	{
		return true;
	}
}
