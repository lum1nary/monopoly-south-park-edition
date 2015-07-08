using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour 
{
	// Examples 

	public GameObject             SetCardExample;
	public GameObject         JourneyCardExample;
	public GameObject         ScienceCardExample;
	public GameObject              GoCardExample;
	public GameObject       IncomeTaxCardExample;
	public GameObject       LuxuryTaxCardExample;
	public GameObject  ComminutyChestCardExample;
	public GameObject          ChanceCardExample;
	public GameObject            JailCardExample;
	public GameObject              PlayerExample;
	public GameObject                DiceExample;
	public GameObject         FreeParkingExmaple;
	/*=========================================*/

	public GameObject Background;
	public LinkedList <Player> Players {get; set;}
	public List<GameObject> Cards {get;set;}
	public GameObject MidleGround {get;set;}
	public DataReader DataReader {get;set;}
	public Vector3 GameBoardSize {get;set;}
	int playerMovesPerTurn;
	int NumbersToMove;
	public bool PlayerDrowDicesAgain;
	int needToGoinJail;
	Player ActivePlayer;

	GameObject [] Dices;
	const int DicesCount = 2;

	public delegate void PlayerAction(object sender, GameEventArgs ge);


	public event PlayerAction PlayerNext;
	public event PlayerAction onWin;


	void Awake()
	{
		Physics.gravity = new Vector3(0,0,9.81f);
		GameBoardSize = GameObject.Find("GameBoard").GetComponent<SpriteRenderer>().bounds.size;
		NumbersToMove = 0;
	
	}

	public void DetectWinner()
	{
		if(Players.Count == 1)
		{
			if(onWin != null)
			{
				onWin(this, new GameEventArgs(Players.First.Value));
			}
		}
	}

	// Use this for initialization
	void Start () 
	{
		MidleGround = GameObject.Find("1-Midleground");
		DataReader = GameObject.Find("DataReader").GetComponent<DataReader>();
		Background = GameObject.Find("Back");
		Background.ScaleToCameraAspectRatio();
		Players = new LinkedList<Player>();
		GetPlayers();
		CreateCards();
		ActivePlayer = Players.First.Value;
		if(PlayerNext != null)
		{
			PlayerNext(this, new GameEventArgs(ActivePlayer));
		}
	}

	public void NextTurn()
	{
		ActivePlayer = NextPlayer();
	}

	public void DrowDices()
	{
		Dices = new GameObject[DicesCount];
		for (int i = 0; i < DicesCount; i++) 
		{
			Dices[i] =(GameObject)GameObject.Instantiate(DiceExample,new Vector3(7*(i+1),-10, -20), Quaternion.identity);
			Dices[i].transform.FindChild("Cube").gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0,400,-50));
			Dices[i].transform.FindChild("Cube").gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(10,100), Random.Range(10,100), Random.Range(10,100)));
			Dices[i].GetComponent<Dice>().Dropped += DiceDroppetEventHandler;
		}
	}

	// Update is called once per frame
	void Update () 
	{

	}

	void DiceDroppetEventHandler(object sender, DiceActionEventArgs dae)
	{
		print(dae.DiceNumber.ToString());
		bool allDropped = true;
		((GameObject)sender).GetComponent<Dice>().Dropped -=DiceDroppetEventHandler;
		NumbersToMove +=dae.DiceNumber;
		foreach (var item in Dices) 
		{
			if(item.GetComponent<Dice>().hasSubscribers())
			{
				allDropped = false;
			}
		}
		if(allDropped)
		{
			PlayerDrowDicesAgain = CheckIfDicesSame();
			if(ActivePlayer != null)
			{
				ActivePlayer.Move(NumbersToMove,true);
				NumbersToMove = 0;
			}
			foreach (var item in Dices) 
			{
				GameObject.Destroy(item,2f);
			}
			WaitFor(2.1f);
			Dices = null;
		}

	}
	
		
	Player NextPlayer()
	{
		if(Players.Find(ActivePlayer).Next == null)
		{
			if(PlayerNext != null)
				PlayerNext(this, new GameEventArgs(ActivePlayer));
			return Players.First.Value;
		}
		else 
		{
			if(PlayerNext != null)
			PlayerNext(this, new GameEventArgs(ActivePlayer));
			return Players.Find(ActivePlayer).Next.Value;
		}
	}
	public void ExitFromGame()
	{
		Application.Quit();
	}

	void CreateCards()
	{
		string [] Names = DataReader.GetCardsNames();
		GameObject pCards = GameObject.Find("Cards");
		Cards = new List<GameObject>();
		Debug.Log(Names.Length.ToString());
		for (int i = 0; i < Names.Length; i++) 
		{
			switch(DataReader.GetGroupByName(Names[i]))
			{
				#region Group Go
			case Group.GO:
			{
				Cards.Add(GameObject.Instantiate(GoCardExample, new Vector3(12.8f,10.24f), Quaternion.identity) as GameObject);
				Cards[i].GetComponent<GoCard>().Initialize(DataReader.GetCardInfo(Names[i]));
				Cards[i].transform.SetParent(pCards.transform);
				Cards[i].transform.FindChild("Sprite").gameObject.ScaleTo(0.12f);
				Cards[i].transform.position = Cards[i].GetComponent<BaseCard>().GetWorldPoint(GameBoardSize);
				Cards[i].transform.rotation = Quaternion.Euler(0,0, 360 - (int)((Cards[i].GetComponent<BaseCard>().CardInfo.Position -1)/10) *90);
			}break;
				#endregion
				#region FreeParking
			case Group.FreeParking: {
				Cards.Add(GameObject.Instantiate(FreeParkingExmaple, new Vector3(12.8f,10.24f), Quaternion.identity) as GameObject);
				Cards[i].GetComponent<BaseCard>().Initialize(DataReader.GetCardInfo(Names[i]));
				Cards[i].transform.SetParent(pCards.transform);
				Cards[i].transform.FindChild("Sprite").gameObject.ScaleTo(0.12f);
				Cards[i].transform.position = Cards[i].GetComponent<BaseCard>().GetWorldPoint(GameBoardSize);
				Cards[i].transform.rotation = Quaternion.Euler(0,0, 360 - (int)((Cards[i].GetComponent<BaseCard>().CardInfo.Position -1)/10) *90);
			}break;
				#endregion
				#region Chance
			case Group.Chance:
			{
				Cards.Add(GameObject.Instantiate(ChanceCardExample, new Vector3(12.8f,10.24f), Quaternion.identity) as GameObject);
				Cards[i].GetComponent<ChanceCard>().Initialize(DataReader.GetCardInfo(Names[i]));
				Cards[i].transform.SetParent(pCards.transform);
				Cards[i].transform.FindChild("Sprite").gameObject.ScaleTo(0.12f);
				Cards[i].transform.position = Cards[i].GetComponent<BaseCard>().GetWorldPoint(GameBoardSize);
				Cards[i].transform.rotation = Quaternion.Euler(0,0, 360 - (int)((Cards[i].GetComponent<BaseCard>().CardInfo.Position -1)/10) *90);
			}break;
				#endregion
				#region CommunityChest
			case Group.CommunityChest: 
			{
				Cards.Add(GameObject.Instantiate(ComminutyChestCardExample, new Vector3(12.8f,10.24f), Quaternion.identity) as GameObject);
				Cards[i].GetComponent<CommunityChestCard>().Initialize(DataReader.GetCardInfo(Names[i]));
				Cards[i].transform.SetParent(pCards.transform);
				Cards[i].transform.FindChild("Sprite").gameObject.ScaleTo(0.12f);
				Cards[i].transform.position = Cards[i].GetComponent<BaseCard>().GetWorldPoint(GameBoardSize);
				Cards[i].transform.rotation = Quaternion.Euler(0,0, 360 - (int)((Cards[i].GetComponent<BaseCard>().CardInfo.Position -1)/10) *90);
			}break;
				#endregion
				#region IncomeTax
			case Group.IncomeTax:{
				Cards.Add(GameObject.Instantiate(IncomeTaxCardExample, new Vector3(12.8f,10.24f), Quaternion.identity) as GameObject);
				Cards[i].GetComponent<IncomeTaxCard>().Initialize(DataReader.GetCardInfo(Names[i]));
				Cards[i].transform.SetParent(pCards.transform);
				Cards[i].transform.FindChild("Sprite").gameObject.ScaleTo(0.12f);
				Cards[i].transform.position = Cards[i].GetComponent<BaseCard>().GetWorldPoint(GameBoardSize);
				Cards[i].transform.rotation = Quaternion.Euler(0,0, 360 - (int)((Cards[i].GetComponent<BaseCard>().CardInfo.Position -1)/10) *90);
			}break;
				#endregion
			#region LuxuryTax
			case Group.LuxuryTax:{
				Cards.Add(GameObject.Instantiate(LuxuryTaxCardExample, new Vector3(12.8f,10.24f), Quaternion.identity) as GameObject);
				Cards[i].GetComponent<LuxuryTacCard>().Initialize(DataReader.GetCardInfo(Names[i]));
				Cards[i].transform.SetParent(pCards.transform);
				Cards[i].transform.FindChild("Sprite").gameObject.ScaleTo(0.12f);
				Cards[i].transform.position = Cards[i].GetComponent<BaseCard>().GetWorldPoint(GameBoardSize);
				Cards[i].transform.rotation = Quaternion.Euler(0,0, 360 - (int)((Cards[i].GetComponent<BaseCard>().CardInfo.Position -1)/10) *90);
			}break;
				#endregion
			#region Jail
			case Group.Jail:
			{
				Cards.Add(GameObject.Instantiate(JailCardExample, new Vector3(12.8f,10.24f), Quaternion.identity) as GameObject);
				Cards[i].GetComponent<JailCard>().Initialize(DataReader.GetCardInfo(Names[i]));
				Cards[i].transform.SetParent(pCards.transform);
				Cards[i].transform.FindChild("Sprite").gameObject.ScaleTo(0.12f);
				Cards[i].transform.position = Cards[i].GetComponent<BaseCard>().GetWorldPoint(GameBoardSize);
				Cards[i].transform.rotation = Quaternion.Euler(0,0, 360 - (int)((Cards[i].GetComponent<BaseCard>().CardInfo.Position -1)/10) *90);

			}break;
				#endregion
			#region Journey
			case Group.Journey: 
			{
				Cards.Add(GameObject.Instantiate(JourneyCardExample, new Vector3(12.8f,10.24f), Quaternion.identity) as GameObject);
				Cards[i].GetComponent<GroupCard>().Initialize(DataReader.GetCardInfo(Names[i]), DataReader.GetSpriteByPosition(DataReader.GetCardInfo(Names[i]).Position));
				Cards[i].transform.SetParent(pCards.transform);
				Cards[i].transform.FindChild("Sprite").gameObject.ScaleTo(0.12f);
				Cards[i].transform.position = Cards[i].GetComponent<BaseCard>().GetWorldPoint(GameBoardSize);
				Cards[i].transform.rotation = Quaternion.Euler(0,0, 360 - (int)((Cards[i].GetComponent<BaseCard>().CardInfo.Position -1)/10) *90);
			}break;
				#endregion
				#region Science
			case Group.Science:
			{
				Cards.Add(GameObject.Instantiate(ScienceCardExample, new Vector3(12.8f,10.24f), Quaternion.identity) as GameObject);
				Cards[i].GetComponent<ScienceCard>().Initialize(DataReader.GetCardInfo(Names[i]), DataReader.GetSpriteByPosition(DataReader.GetCardInfo(Names[i]).Position));
				Cards[i].transform.SetParent(pCards.transform);
				Cards[i].transform.FindChild("Sprite").gameObject.ScaleTo(0.12f);
				Cards[i].transform.position = Cards[i].GetComponent<BaseCard>().GetWorldPoint(GameBoardSize);
				Cards[i].transform.rotation = Quaternion.Euler(0,0, 360 - (int)((Cards[i].GetComponent<BaseCard>().CardInfo.Position -1)/10) *90);
			}break;
			#endregion
			#region DeFault
			default:{ 
				Cards.Add(GameObject.Instantiate(SetCardExample, new Vector3(12.8f,10.24f), Quaternion.identity) as GameObject); 
				Cards[i].GetComponent<SetCard>().Initialize(DataReader.GetCardInfo(Names[i]), DataReader.GetSpriteByPosition(DataReader.GetCardInfo(Names[i]).Position));
				Cards[i].transform.SetParent(pCards.transform);
				Cards[i].transform.FindChild("Sprite").gameObject.ScaleTo(0.12f);
				Cards[i].transform.position  =  Cards[i].GetComponent<BaseCard>().GetWorldPoint(GameBoardSize);
				Cards[i].transform.rotation = Quaternion.Euler(0,0, 360 - (int)((Cards[i].GetComponent<SetCard>().CardInfo.Position - 1) / 10) * 90);
			}break;
			#endregion
			}
		}
	}
	#region Check If Dices are Same
	bool CheckIfDicesSame()
	{
		bool Same = true;
		for (int i = 0; i < Dices.Length; i++) 
		{
			if(Dices[0].GetComponent<Dice>().Number != Dices[i].GetComponent<Dice>().Number)
			{
				Same = false;
			}
		}
		return Same;
	}
	#endregion
	#region Waiting Function
 	void WaitFor(float Secounds) { StartCoroutine(WaitForTime(Secounds));}
	IEnumerator WaitForTime(float time) { yield return new WaitForSeconds(time);}
	#endregion
	#region GetPlayers
	void GetPlayers()
	{
		GameObject [] players = GameObject.FindGameObjectsWithTag ("Player");
		for (int i = 0; i < players.Length; i++) 
		{
						
			Players.AddLast(players[i].GetComponent<Player>());
			players[i].transform.SetParent(GameObject.Find("2-Foreground").transform);
			players[i].transform.position = players[i].GetComponent<Player>().Position.GetWorldPoint(GameBoardSize);
		}
		Debug.Log("Players Finded:" + players.Length.ToString());
	}
	#endregion
	public GameObject GetCardByPosition(int pos)
	{
		foreach (GameObject item in Cards) {
			if(item.GetComponent<BaseCard>().CardInfo.Position == pos)
			{
				return item;
			}
		}
		return null;
	}

}

public class GameEventArgs : System.EventArgs
{
	public Player ActivePlayer;
	public GameEventArgs(Player Active)
	{
		ActivePlayer = Active;
	}

}


#region Enum - Set Card Status
public enum SetCardStatus{Normal,Doubled, With_1_House, With_2_Houses, With_3_Houses, With_4_Houses,With_Hotel};
#endregion
#region Enum - Color Groups
public enum Group {
	GO = 0, 
	Purple = 1, 
	LightBlue = 2, 
	Pink = 3,
	Orange = 4,
	Red = 5,
	Yellow = 6,
	Green = 7,
	Blue  = 8,
	Journey = 9,
	Science = 10,
	CommunityChest = 11,
	Chance = 12,
	Jail = 13,
	IncomeTax =14,
	LuxuryTax =15,
	FreeParking = 16
};
#endregion
