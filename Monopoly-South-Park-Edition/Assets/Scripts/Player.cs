using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{
	public delegate void PlayerAction(object sender, PlayerEventArgs pe);
	public event PlayerAction MoveStart;
	public event PlayerAction MoveEnd;
	public event PlayerAction isMoving;
	public event PlayerAction BuyProperty;




	public int speed;
	//=============================
	public Position  Position    {get; protected set;}
	public int       Money       {get; protected set;}
	public string    Name        {get; protected set;}
	public SpriteRenderer Avatar {get; protected set;}
	//=============================
	public List<GameObject> Cards { get; set;}


	void Awake()
	{
		Cards = new List<GameObject> ();
		Position = new Position(1);
		Avatar = transform.FindChild("Char_sprite").gameObject.GetComponent<SpriteRenderer>();
		Name =name;
		Money = 0;

	}

	void Start()
	{

		if(Application.loadedLevel == 1)
			transform.position = Position.GetWorldPoint(GameObject.Find("Game").GetComponent<Game>().GameBoardSize);
//		GameObject.Find("Game").GetComponent<Game>().PlayerNext += (object sender, GameEventArgs ge) => 
//		{
//			for (int i = 0; i < GameObject.FindGameObjectsWithTag("Card").Length; i++) {
//				Cards.Add(GameObject.FindGameObjectsWithTag("Card")[i]);
//				print(GameObject.FindGameObjectsWithTag("Card")[i].name);
//			}
//		};

			

	}
	public void Initialize(int money, string name, Sprite picture)
	{
		Money = money;
		Name = name;
		Avatar.sprite = picture;

	}


	public bool TryPay(int moneyToPay)
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
	public void Buy( GameObject NewCard)
	{
		NewCard.GetComponent<EstateCard>().Owner = this;
		Money -=NewCard.GetComponent<BaseCard>().CardInfo.PurchasePrice;
		Cards.Add(NewCard);
		if(BuyProperty != null)
		{
			BuyProperty(this, new PlayerEventArgs(Position));
		}
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
		if(MoveStart != null)
			MoveStart(this,new PlayerEventArgs(Position));
		
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
				
				if(isMoving != null)
					isMoving(this, new PlayerEventArgs(Position));
				yield return null;
			}
			count --;
		}
		if(MoveEnd != null)
			MoveEnd(this, new PlayerEventArgs(Position));
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
