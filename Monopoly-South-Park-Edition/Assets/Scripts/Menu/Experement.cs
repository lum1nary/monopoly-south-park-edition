using UnityEngine;
using System.Collections;

public class Experement : MonoBehaviour {
	
	// Use this for initialization
	void Start () 
	{



		SpriteRenderer sr = GameObject.Find("Back").GetComponent<SpriteRenderer>();
		Debug.Log(sr.bounds.min.x.ToString() + " " + sr.bounds.min.y.ToString());
		Debug.Log(sr.bounds.extents.x.ToString() + " " + sr.bounds.extents.y.ToString());
		Debug.Log(sr.bounds.size.x.ToString());
	}

	void OnMouseEnter()
	{
		print("mouse entered!");
	}
	// Update is called once per frame
	void Update () {

	}
}

