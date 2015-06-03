using UnityEngine;
using System.Collections;

public class Resolution_Controller : MonoBehaviour {
	[ExecuteInEditMode]
	// Use this for initialization
	void Start () 
	{
		Resolution [] resolutions = Screen.resolutions;
		Screen.SetResolution (resolutions [resolutions.Length - 1].width, resolutions [resolutions.Length - 1].height, true);
		float targetaspect = 5f / 4f;
		float windowaspect = (float)Screen.width / (float)Screen.height;
		Camera camera = gameObject.GetComponent<Camera> ();
		float scaleheight = windowaspect / targetaspect;
		if (scaleheight < 1.0f)
		{  
			Rect rect = camera.rect;
			
			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;
			
			camera.rect = rect;
		}
		else // add pillarbox
		{
			float scalewidth = 1.0f / scaleheight;
			
			Rect rect = camera.rect;
			
			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;
			
			camera.rect = rect;
		}
	}
}
