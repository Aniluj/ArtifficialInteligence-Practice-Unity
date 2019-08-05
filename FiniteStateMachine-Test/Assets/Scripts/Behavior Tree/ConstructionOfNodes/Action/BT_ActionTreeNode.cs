using UnityEngine;
using System.Collections;

public abstract class BT_ActionTreeNode<T> : BT_Node<T> where T : class
{
	protected override bool Validate()
	{
		return true;
	}
	
	protected override void Awake()
	{
	}
	
	protected override void Sleep()
	{
	}
}
