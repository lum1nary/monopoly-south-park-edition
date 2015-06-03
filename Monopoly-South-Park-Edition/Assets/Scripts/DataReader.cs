using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataReader : MonoBehaviour {

	TextAsset CardsInfo;
	string[] Data;	
	// Use this for initialization
	void Awake () 
	{
		CardsInfo =  Resources.Load<TextAsset>("Cards");
		Data = CardsInfo.text.Split('\n');
		System.Array.Resize<string>(ref Data,Data.Length-1);
//		CardInfo info = GetCardInfo("Lake Tradicaca");
//		Debug.Log( info.Title +"\n" +
//			"Purchase Price:" + info.PurchasePrice.ToString() +"\n" +
//		    "Lay Price:" + info.LayPrice.ToString() + "\n" +
//		    "Site Price:" + info.SitePrice.ToString() + "\n" +
//			"H1 Price:" + info.H1_Price.ToString() + "\n" +
//			"H2 Price:" + info.H2_Price.ToString() + "\n" +
//			"H3 Price:" + info.H3_Price.ToString() + "\n" +
//			"H4 Price:" + info.H4_Price.ToString() + "\n" +
//		    "Hotel Price:" + info.Hotel_Price.ToString() + "\n" +
//		    "Group:" + info.Group.ToString()
//		          );
	}
	#region Get Group By Name
	public Group GetGroupByName(string Name)
	{
		for (int i = 0; i < Data.Length; i++) 
		{
			if(Data[i].Contains(Name))
			{
				return (Group)int.Parse(Data[i].Split(',')[8]);
			}

		}
		return Group.Activity;
	}
	#endregion
	#region GetCardInfo
	public CardInfo GetCardInfo(string Name)
	{
		for (int i = 0; i < Data.Length; i++) 
		{
			if(Data[i].Contains(Name))	
			{
				string[] temp = Data[i].Split(',');
				return new CardInfo(temp[0],
									int.Parse(temp[1]),
				                    int.Parse(temp[2]),
				                    int.Parse(temp[3]),
				                    int.Parse(temp[4]),
				                    int.Parse(temp[5]),
				                    int.Parse(temp[6]),
				                    int.Parse(temp[7]),
				                    (Group)int.Parse(temp[8]),
				                    int.Parse(temp[9])
				                    );
			}
		}
		return null;
	}
	#endregion
	#region Get Count Cards of Group
	public int GetCountCardsOfGroup(Group group)
	{
		int count  = 0;
		for (int i = 0; i < Data.Length; i++) 
		{
			string[] temp = Data[i].Split(',');
			if(((Group)int.Parse(temp[8])) == group)
			{
				count++;
			}
		}
		return count;
	}
	#endregion
	#region Get Cards Names
	public string[] GetCardsNames()
	{
		string [] temp = new string[Data.Length];
		for (int i = 0; i < Data.Length; i++) 
		{
			temp[i] = Data[i].Split(',')[0];
		}
		return temp;
	}
	#endregion
}

