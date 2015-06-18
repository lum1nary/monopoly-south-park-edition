using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{
	public delegate void PlayerAction(object sender, PlayerEventArgs pe);
	public event PlayerAction OnMoveStart;
	public event PlayerAction OnMoveEnd;
	public event PlayerAction OnMoving;



	public int speed;
	//=============================
	Position  Position    {get; set;}
	int       Money       {get; set;}
	string    Name        {get; set;}
	SpriteRenderer Avatar {get; set;}
	//=============================
	public List<GameObject> Cards { get; set;}

	void Awake()
	{
		Cards = new List<GameObject> ();
		Position = new Position(1);
		Avatar = transform.FindChild("Char_sprite").gameObject.GetComponent<SpriteRenderer>();


	}
	void Start()
	{
		transform.position = Position.GetWorldPoint(GameObject.Find("Game").GetComponent<Game>().GameBoardSize);
	}
	public void Initialize(int money, string name, Sprite picture)
	{
		Money = money;
		Name = name;
		Avatar.sprite = picture;
	}


	bool TryPay(int moneyToPay)
	{
		if((Money - moneyToPay) < 0)
		{
			return false;
		}
		else 
		{
			return true;
		}
	}
	#region Buy
	public void Buy(ref GameObject NewCard)
	{
		NewCard.GetComponent<EstateCard>().Owner = this;
		Cards.Add(NewCard);
		Debug.Log("Card:" + NewCard.GetComponent<EstateCard>().name + "Has Been Byed by:" + this.Name);
	}
	#endregion
	#region Move Function 
	public void Move(int target, bool mf)
	{
		print("Move Invoked!");
		StartCoroutine(Moving(target,mf));
	}
	
	
	IEnumerator Moving (int target, bool moving_forward)
	{
		Vector3 GameBoardSize  = GameObject.Find("Game").GetComponent<Game>().GameBoardSize;
		if(OnMoveStart != null)
			OnMoveStart(this,new PlayerEventArgs(Position));
		
		float step = speed * Time.deltaTime;
		int count  = target;
		while(count > 0)
		{
			if(moving_forward)
				Position.Add(1);
			else Position.Subtract(1);
			
			while(transform.position != Position.GetWorldPoint(GameBoardSize))
			{
				transform.position = Vector3.MoveTowards(transform.position, Position.GetWorldPoint(GameBoardSize), step);
				
				if(OnMoving != null)
					OnMoving(this, new PlayerEventArgs(Position));
				yield return null;
			}
			count --;
		}
		if(OnMoveEnd != null)
			OnMoveEnd(this, new PlayerEventArgs(Position));
	}
	#endregion
}


public class PlayerEventArgs : System.EventArgs
{
	public Position PlayerPosition;

	public PlayerEventArgs(Position pos)
	{
		PlayerPosition = pos;
	}

}
