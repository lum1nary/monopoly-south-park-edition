using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateHeroPanel : MonoBehaviour {

	public InputField Name;
	public Button button;

	// Use this for initialization
	void Start () {
		button.enabled = false;
		Name.onEndEdit.AddListener(new UnityEngine.Events.UnityAction<string>((string arg0) => {
			if(arg0 != string.Empty)
				button.enabled = true;

		}));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
