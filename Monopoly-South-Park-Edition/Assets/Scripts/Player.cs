using UnityEngine;
using System.Collections;

public class Player
{
	uint   Id        { get; set;}
	//uint   Position  { get; set;}
	int    Money     { get; set;}
	string Name      { get; set;}
	Sprite Avatar    { get; set;}
	Location[] Cards { get; set;}
	public Player(uint id, int money, string name, Sprite avatar)
	{
		Id = id;
		Money = money;
		Name = name;
		Avatar = avatar;

	}
}
