using UnityEngine;
using System.Collections;



public class Location : MonoBehaviour {
	
	Player Owner { get; set;}
	Sprite sprite { get; set;}



}

public enum Group {
	Activity = 0, 
	Purple = 1, 
	LightBlue = 2, 
	Pink = 3,
	Orange = 4,
	Red = 5,
	Yellow = 6,
	Green = 7,
	Blue  = 8,
	Social = 9
}