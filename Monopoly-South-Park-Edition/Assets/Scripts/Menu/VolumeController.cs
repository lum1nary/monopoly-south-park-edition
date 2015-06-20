using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour {


	Slider volumeSlider;
	void Awake()
	{
		volumeSlider = GetComponent<Slider>();
		volumeSlider.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<float>((float arg0) => { AudioListener.volume = arg0; }));
	}
}
