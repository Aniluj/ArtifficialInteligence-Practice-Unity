using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BT_NodeWithChildren<T> : BT_Node<T> where T : class
{
	protected List<BT_Node<T>> children = new List<BT_Node<T>>();

	public BT_NodeWithChildren(string debugName = null) : base(null, debugName)
	{
	}

	public void AddChild(BT_Node<T> child)
	{
		if (CanAddChild(child))
		{
			child.Blackboard = this.Blackboard;
			children.Add(child);
		}
	}
	
	public virtual bool CanAddChild(BT_Node<T> child)
	{
		return true;
	}
}
