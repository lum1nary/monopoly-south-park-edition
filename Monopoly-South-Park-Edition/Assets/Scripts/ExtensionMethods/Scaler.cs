using UnityEngine;
using System.Collections;
using System;

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

	public static void ScaleToCameraAspectRatio(this GameObject obj)
	{
		float scaleFactor =(float)Math.Round(((Camera.main.orthographicSize*2) * Camera.main.aspect) / obj.GetComponent<SpriteRenderer>().bounds.size.x,2);
		obj.transform.localScale = new Vector3(
			obj.transform.localScale.x * scaleFactor,
			obj.transform.localScale.y * scaleFactor,
			obj.transform.localScale.z * scaleFactor
			);


	}
}
