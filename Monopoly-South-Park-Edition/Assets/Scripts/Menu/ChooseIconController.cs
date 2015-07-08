using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChooseIconController : MonoBehaviour {


	public delegate void ChooseAction(object sender, IconEventArgs ie);
	public event ChooseAction OnIconChoosed;
	public bool InChooseMode = false;

	public void SetChooseModeON()
	{InChooseMode = true; }
	public void SetChooseModeOFF()
	{InChooseMode = false;}

	void OnMouseDown()
	{
		if(InChooseMode)
		{
			if(OnIconChoosed != null)
			{
				OnIconChoosed(this, new IconEventArgs(
					GetComponentInChildren<Image>().sprite
					));
				foreach (Transform child in transform.parent) {
					child.gameObject.GetComponent<ChooseIconController>().SetChooseModeOFF();
				}
				Destroy(gameObject);
			}


		}
	}
	void OnMouseEnter()
	{
		if(InChooseMode)
		transform.FindChild("Image").gameObject.GetComponent<Image>().color = Color.cyan;
	}
	void OnMouseExit()
	{
		if(InChooseMode)
		transform.FindChild("Image").gameObject.GetComponent<Image>().color = Color.white;
	}
}

public class IconEventArgs : System.EventArgs
{
	public Sprite hero_sprite;
	public IconEventArgs(Sprite sp)
	{
		hero_sprite = sp;
	}

}
