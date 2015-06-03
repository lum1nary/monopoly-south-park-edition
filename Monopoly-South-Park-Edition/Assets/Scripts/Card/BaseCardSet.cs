using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseCardSet 
{
	public int Capacity {get; private set;}
	public Group Group {get; private set;}

	public BaseCardSet(int capacity, Group group)
	{
		Capacity = capacity;
		Group = group;
	}
}
