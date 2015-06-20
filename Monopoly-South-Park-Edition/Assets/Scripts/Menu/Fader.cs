using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Fader : MonoBehaviour 
{
	public event System.Action OnStartFadingToBlack;
	public event System.Action OnFinishFadingToBlack;

	public event System.Action OnStartFadingToClear;
	public event System.Action OnFinishFadingToClear;


	Texture2D blackTexture;
	public float fadeSpeed;


	void Awake()
	{
		blackTexture = new Texture2D (1, 1);
		blackTexture.SetPixel (0, 0, Color.black);
		blackTexture.Apply ();
		gameObject.AddComponent<GUITexture> ();
		GetComponent<GUITexture>().texture = blackTexture;
		GetComponent<GUITexture>().pixelInset = new Rect (0, 0, Screen.width, Screen.height);
		transform.position = new Vector3 (0, 0, 1);
		GetComponent<GUITexture> ().enabled = false;

	}
	IEnumerator FadeToBlack()
	{
		if(OnStartFadingToBlack != null)
			OnStartFadingToBlack();
		float fading = 0f;
		GetComponent<GUITexture>().color =  new Color(
			GetComponent<GUITexture>().color.r,
			GetComponent<GUITexture>().color.g,
			GetComponent<GUITexture>().color.b,
			fading);
		GetComponent<GUITexture>().enabled = true;
		while(fading < 1)
		{
			GetComponent<GUITexture>().color =  new Color(
				GetComponent<GUITexture>().color.r,
				GetComponent<GUITexture>().color.g,
				GetComponent<GUITexture>().color.b,
				fading);
			fading +=fadeSpeed/100f;
			yield return null;		
		}
		if(OnFinishFadingToBlack != null)
			OnFinishFadingToBlack();
	}

	IEnumerator  FadeToClear()
	{
		if(OnStartFadingToClear != null)
			OnStartFadingToClear();
		GetComponent<GUITexture>().enabled = true;
		float fading = 1.0f;
		GetComponent<GUITexture>().color =  new Color(
			GetComponent<GUITexture>().color.r,
			GetComponent<GUITexture>().color.g,
			GetComponent<GUITexture>().color.b,
			fading);
		while(fading > 0)
		{
			GetComponent<GUITexture>().color = new Color(
				GetComponent<GUITexture>().color.r,
				GetComponent<GUITexture>().color.g,
				GetComponent<GUITexture>().color.b,
				fading);
			fading -=fadeSpeed/100f;
			yield return null;
		}
		GetComponent<GUITexture>().enabled = false;
		if(OnFinishFadingToClear !=null)
			OnFinishFadingToClear();
	}
	public void FadeToBlack(bool fade)
	{

		if(fade)
		{
			StartCoroutine(FadeToBlack());
		}
		else
			StartCoroutine(FadeToClear());
	}

}
