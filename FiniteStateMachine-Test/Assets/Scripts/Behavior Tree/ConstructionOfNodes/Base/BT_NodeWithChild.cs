using UnityEngine;
using System.Collections;

public abstract class BT_NodeWithChild<T> : BT_Node<T> where T : class
{
	protected BT_Node<T> child;

	public BT_NodeWithChild(string debugName = null) : base(null, debugName)
	{
	}

	public void SetChild(BT_Node<T> child)
	{
		if (CanAddChild(child))
		{
			this.child = child;
			this.child.Blackboard = this.Blackboard;
		}
	}
	
	public virtual bool CanAddChild(BT_Node<T> child)
	{
		return true;
	}

}
