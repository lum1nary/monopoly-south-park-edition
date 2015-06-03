using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour 

{
	public List<Player> Players {get; set;}
	public int MovesCount {get; set;}
	public List<GameObject> Cards {get;set;}
	public GameObject MidleGround {get;set;}
	public DataReader dataReader {get;set;}
	public GameObject SetCardExample;
	public GameObject GroupCardExample;
	public GameObject Background;
	public GameObject PlayerExample;
	public GameObject DiceExample;
	GameObject [] Dices;
	const int DicesCount = 2;
	void Awake()
	{
		Physics.gravity = new Vector3(0,0,9.81f);

	}

	// Use this for initialization
	void Start () 
	{
		MidleGround = GameObject.Find("1-Midleground");
		dataReader = GameObject.Find("DataReader").GetComponent<DataReader>();
		Background = GameObject.Find("Back");
		MovesCount = 0;
		GetPlayers();
		CreateCards();
		DrowDices();
	}

	public void DrowDices()
	{
		Dices = new GameObject[DicesCount];
		for (int i = 0; i < DicesCount; i++) 
		{
			Dices[i] =(GameObject)GameObject.Instantiate(DiceExample,new Vector3(7*(i+1),-10, -20), Quaternion.identity);
			Dices[i].transform.FindChild("Cube").gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0,400,-50));
			Dices[i].transform.FindChild("Cube").gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(10,100), Random.Range(10,100), Random.Range(10,100)));
		}
	}

	// Update is called once per frame
	void Update () 
	{

	}

	void CreateCards()
	{
		string [] Names = dataReader.GetCardsNames();
		GameObject pCards = GameObject.Find("Cards");
		Cards = new List<GameObject>();
		Debug.Log(Names.Length.ToString());
		for (int i = 0; i < Names.Length; i++) 
		{
			switch(dataReader.GetGroupByName(Names[i]))
			{
			case Group.Activity:{}break;
			case Group.Journey: 
			{

			}break;
			case Group.Science:
			{

			}break;

			default:{ Cards.Add(GameObject.Instantiate(SetCardExample, new Vector3(12.8f,10.24f), Quaternion.identity) as GameObject); 
				Cards[i].GetComponent<SetCard>().Initialize(Names[i]);
				Cards[i].transform.SetParent(pCards.transform);
				Cards[i].transform.FindChild("Sprite").gameObject.ScaleTo(0.12f);
				Cards[i].transform.position  =  Cards[i].GetComponent<BaseCard>().Position.GetWorldPoint(GameObject.Find("Back").GetComponent<SpriteRenderer>().bounds.size);
				Cards[i].transform.rotation = Quaternion.Euler(0,0, 360 - (int)((Cards[i].GetComponent<SetCard>().Position.Value - 1) / 10) * 90);
			}break;
			}


		}

	}
	#region GetPlayers
	void GetPlayers()
	{
		GameObject [] players = GameObject.FindGameObjectsWithTag ("Player");
		for (int i = 0; i < players.Length; i++) 
		{
			Players.Add(players[i].GetComponent<Player>());
		}
		Debug.Log("Players Finded:" + players.Length.ToString());
	}
	#endregion
}

#region Enum - Set Card Status
public enum SetCardStatus{Normal,Doubled, With_1_House, With_2_Houses, With_3_Houses, With_4_Houses,With_Hotel};
#endregion
#region Enum - Color Groups
public enum Group {
	Activity = 0, 
	Purple = 1, 
	LightBlue = 2, 
	Pink = 3,
	Orange = 4,
	Red = 5,
	Yellow = 6,
	Green = 7,
	Blue  = 8,
	Journey = 9,
	Science = 10
};
#endregion
