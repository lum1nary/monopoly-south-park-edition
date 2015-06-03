using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour, IComparable<Player>
{
	int    Id        { get; set;}
	uint   Position  { get; set;}
	int    Money     { get; set;}
	string Name      { get; set;}
	Sprite Avatar    { get; set;}
	public List<GameObject> Cards { get; set;}
	public Player(int money, string name, Sprite avatar)
	{
		Id = -1;
		Money = money;
		Name = name;
		Avatar = avatar;
		Cards = new List<GameObject> ();
		Position = 1;
	}
	
	public void Move (int moves)
	{


	}
	#region Buy
	public void Buy(ref GameObject NewCard)
	{
		NewCard.GetComponent<EstateCard>().Owner = this;
		Cards.Add(NewCard);
		Debug.Log("Card:" + NewCard.GetComponent<EstateCard>().Title + "Has Been Byed by:" + this.Name);
	}
	#endregion
	#region CompareTo
	public int CompareTo(Player other)
	{

		if (other == null) { return 1;}
		return Id - other.Id;
	}
	#endregion
}
