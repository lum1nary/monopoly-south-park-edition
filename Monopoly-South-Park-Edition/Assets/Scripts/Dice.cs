using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class Dice : MonoBehaviour {

	public delegate void DiceAction(object sender, DiceActionEventArgs dae);
	public event DiceAction Dropped;
	GameObject cube;
	public int Number;
	// Use this for initialization
	void Start () 
	{
		cube = transform.FindChild("Cube").gameObject;
		Number = 0;
	}
	public bool hasSubscribers() { if(Dropped == null) return false; else return true;}

	// Update is called once per frame
	void Update () 
	{
		if(cube.GetComponent<Rigidbody>().IsSleeping())
		{

			if(Dropped != null)
			{
				Number = DetectNumber();
				this.Dropped(gameObject,  new DiceActionEventArgs(Number));
			}
		}
	}
	#region Detect Number
	int DetectNumber()
	{
		Vector3 R = new Vector3(roundToSide(cube.transform.rotation.eulerAngles.x),
		                        roundToSide(cube.transform.rotation.eulerAngles.y), 
		                        roundToSide(cube.transform.rotation.eulerAngles.z));
		//print(R.ToString());
		if(R.x == 0 && R.y == 0) return 6;
		if( (R.x == 0f || R.x == 360f) && R.y == 180f) return 1;
		if((((360f - R.x) >= 0 && (360f - R.x) <=360f) && ((R.y == 270f && R.z == 270f) || (R.y == 90f && R.z ==90f) )) ||
		   ((R.x == 270f) && (R.y ==180f && R.z ==0f)) ||
		   ((R.x == 90f ) && (R.y == 0f  && R.z ==0f))
		   ) return 3;
		if( ((R.x - 360f) >=-360f &&  (R.x - 360f) <=0) && ((R.y == 90f && R.z == 180f) || (R.y == 270   && (R.z == 0f || R.z == 360f ))) ) return 2;
		if( ((R.x - 360f) >=-360f &&  (R.x - 360f) <=0) && ((R.y ==270f && R.z == 180f) || (R.y == 90.0f && (R.z == 0f || R.z == 360f ))) ) return 5;
		return 4;
	}
	#endregion
	#region Round To Side
	int roundToSide(float value)
	{
		int factor = Mathf.Abs((int)(value / 90.0F));
		float res = value - (factor * 90.0F);
		if((90.0f - res) < 45.0f)
			factor++;
		return factor * 90;
	}
	#endregion
}
#region Dice Action Event Args
public class DiceActionEventArgs : System.EventArgs 
{
	public int DiceNumber;

	public DiceActionEventArgs(int DN)
	{
		DiceNumber = DN;
	}
}
#endregion