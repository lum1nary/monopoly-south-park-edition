using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Rules : MonoBehaviour {


	Text rulesText;
	TextAsset TextReader;
	void Awake()
	{
		TextReader = Resources.Load<TextAsset>("Rules");
		rulesText = GetComponent<Text>();
		rulesText.supportRichText = true;
		rulesText.text = TextReader.text;
		Debug.Log(TextReader.text);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
