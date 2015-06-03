using UnityEngine;
using System.Collections;

public class BaseCardInfo 
{
	public string Title {get;set;}	
	public Group Group {get;set;}

	public BaseCardInfo(string title, Group group)
	{
		Title = title;
		Group = group;
	}
}
