using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataReader : MonoBehaviour {

	TextAsset CardsInfo;
	Sprite [] CardSprites;
	string[] Data;	
	// Use this for initialization
	void Awake () 
	{
		CardsInfo =  Resources.Load<TextAsset>("Cards");
		Data = CardsInfo.text.Split('\n');
		System.Array.Resize<string>(ref Data,Data.Length-1);
		CardSprites = Resources.LoadAll<Sprite>("");
	}



	public Sprite GetSpriteByPosition(int pos)
	{
		for (int i = 0; i < CardSprites.Length; i++) 
		{
			if(int.Parse(CardSprites[i].name) == pos)
				return CardSprites[i];
		}
		return null;
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
		return Group.GO;
	}
	#endregion
	#region GetCardInfo

	/// <summary>
	///  IN Case of Estate and upper Card 
	/// 0 = name; 1 = PurchasePrice, 2 = RentPrice, 3 = Price with 1 home
	/// 4 = Price with 2 home, 5 = 3homes, 6 = 4 homes, 7 = Hotel, 8 group, 
	/// 9 position on board 
	/// IN case of ACTIVITY CARD
	/// 0 = name, 1 = 
	/// </summary>
	/// <returns>The card info.</returns>
	/// <param name="Name">Name.</param>
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

