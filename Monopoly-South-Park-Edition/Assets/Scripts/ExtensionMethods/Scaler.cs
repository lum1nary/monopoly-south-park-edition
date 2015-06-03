using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Renderer))]
public static class Scaler {
	public static void ScaleTo(this GameObject obj, float procent)
	{
		float tHeight = (Camera.main.orthographicSize *2)  * procent;
		//Debug.Log("theigt = " + tHeight.ToString() + "\n" +
		          //"Screen height = " + Screen.height.ToString());

		float factor =  tHeight / (obj.GetComponent<SpriteRenderer>().bounds.size.y);
		obj.transform.localScale *=factor;
		//Debug.Log((obj.GetComponent<SpriteRenderer>().bounds.size.y).ToString());
	}
}
