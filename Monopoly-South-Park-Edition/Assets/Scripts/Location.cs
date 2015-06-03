using UnityEngine;
using System.Collections;



public class Location : MonoBehaviour {
	


	public Player Owner { get; set;}
	public Sprite sprite { get; set;}
	public string Name {get;set;}
	public bool isLaid {get;set;}
	public CardInfo CardInfo {get;set;}


	public Location(string Name, Sprite sp,CardInfo info)
	{
		isLaid = false;
		Owner = null;
		CardInfo = info;
	}

};


