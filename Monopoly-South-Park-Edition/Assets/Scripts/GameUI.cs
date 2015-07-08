using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameUI : MonoBehaviour {

	public GameObject Menu;

	public Text ActivePlayerName;
	public Text ActivePlayerMoney;

	public GameObject UI_Cards;

	public GameObject UI_CardInfo_Panel;

	public GameObject BuyMenu;

	public Button MainMenu;

	public GameObject SurrenderMenu;

	public GameObject WinText;

	Fader fader;

	GameObject [] Cards;
	Game game;
	Player currentPlayer;
	bool isAlreadyshowing;

	// Use this for initialization
	void Start () 
	{
		fader = GetComponent<Fader>();
		fader.FadeToBlack(false);
		game = GameObject.Find("Game").GetComponent<Game>();
		game.PlayerNext += OnPlayerChanged;
		fader.OnFinishFadingToBlack += () => Application.LoadLevel(0);
		StartCoroutine(SubscribeOnEvents());
		MainMenu.onClick.AddListener(new UnityAction(() => {
			fader.FadeToBlack(true);
		}));
		BuyMenu.transform.FindChild("Buy").GetComponent<Button>().onClick.AddListener(new UnityAction(() =>{
			currentPlayer.Buy(game.GetCardByPosition(currentPlayer.Position.Value));
			DisplayPlayersCards();
			BuyMenu.SetActive(false);
			if(game.PlayerDrowDicesAgain)
			{
				Menu.SetActive(true);
			}
			else game.NextTurn();
		}));
		SurrenderMenu.transform.FindChild("Panel").FindChild("Surrender").gameObject.GetComponent<Button>().onClick.AddListener(new UnityAction(() => {
			game.Players.Remove(currentPlayer);
			Destroy(currentPlayer.gameObject);
			game.DetectWinner();
		}));
		game.onWin += (object sender, GameEventArgs ge) => 
		{
			WinText.SetActive(true);
			WinText.GetComponent<Text>().text = ge.ActivePlayer.Name + " Win!";
		};
		foreach(Text item in UI_Cards.GetComponentsInChildren<Text>())
		{
			item.gameObject.SetActive(false);
		}

	}
		
	IEnumerator SubscribeOnEvents()
	{
		yield return new WaitForSeconds(0.1f);
		for (int i = 0; i < GameObject.FindGameObjectsWithTag("Card").Length; i++) {
			GameObject.FindGameObjectsWithTag("Card")[i].GetComponent<BaseCard>().OnClick += OnCardClicked;	
		}
	}

	void OnPlayerChanged(object sender, GameEventArgs ge)
	{

		currentPlayer = ge.ActivePlayer;

		ActivePlayerName.text = currentPlayer.Name;
		ActivePlayerMoney.text = currentPlayer.Money.ToString()+"$";
		DisplayPlayersCards();
		Menu.SetActive(true);
		currentPlayer.MoveEnd += OnFinishinMoving;
	}
	void OnFinishinMoving(object sender, PlayerEventArgs pe)
	{
		print("He is finished movement");	
		if(!game.GetCardByPosition(currentPlayer.Position.Value).GetComponent<EstateCard>().Equals(null))
		{
			print("He is finished movement 111");
			if(game.GetCardByPosition(pe.PlayerPosition.Value).GetComponent<SetCard>().Owner != currentPlayer)
			{
				if(currentPlayer.TryPay(game.GetCardByPosition(currentPlayer.Position.Value).GetComponent<BaseCard>().CardInfo.PurchasePrice))
				{
					BuyMenu.SetActive(true);
				}
				else 
				{
					SurrenderMenu.SetActive(true);
				}
			}
		}
		else
		{
			if(game.PlayerDrowDicesAgain)					
			{
				Menu.SetActive(true);
			}
			else
			{
				currentPlayer.MoveEnd -= OnFinishinMoving;
				game.NextTurn();
			}

		}
	}
	void OnMoneyChanged(object sender, PlayerEventArgs pe)
	{
		ActivePlayerMoney.text  = currentPlayer.Money.ToString() + "$";
	}

	void DisplayPlayersCards()
	{
		foreach(Text item in UI_Cards.GetComponentsInChildren<Text>(true))
		{
			for (int i = 0; i < currentPlayer.Cards.Count; i++) {
				if(item.text == currentPlayer.Cards[i].name)
				{
					item.gameObject.SetActive(true);
				}
				else item.gameObject.SetActive(false);
			}
		}
	}


	void OnCardClicked(object sender, BaseCardEventArgs be)
	{
		UI_CardInfo_Panel.SetActive(true);
		UI_CardInfo_Panel.transform.FindChild("Image").GetComponent<Image>().sprite = be.sprite;
		UI_CardInfo_Panel.transform.FindChild("Purchase").FindChild("Value").gameObject.GetComponent<Text>().text = be.info.PurchasePrice.ToString();
		UI_CardInfo_Panel.transform.FindChild("Rent").FindChild("Value").gameObject.GetComponent<Text>().text = be.info.SitePrice.ToString();
		UI_CardInfo_Panel.transform.FindChild("1House").FindChild("Value").gameObject.GetComponent<Text>().text = be.info.PurchasePrice.ToString();
		UI_CardInfo_Panel.transform.FindChild("2Houses").FindChild("Value").gameObject.GetComponent<Text>().text = be.info.PurchasePrice.ToString();
		UI_CardInfo_Panel.transform.FindChild("3Houses").FindChild("Value").gameObject.GetComponent<Text>().text = be.info.PurchasePrice.ToString();
		UI_CardInfo_Panel.transform.FindChild("4Houses").FindChild("Value").gameObject.GetComponent<Text>().text = be.info.PurchasePrice.ToString();
		UI_CardInfo_Panel.transform.FindChild("Hotel").FindChild("Value").gameObject.GetComponent<Text>().text = be.info.PurchasePrice.ToString();
		if(isAlreadyshowing)
		{
			StopAllCoroutines();
			StartCoroutine(HidePanelAfter(3f));
		}
		else StartCoroutine(HidePanelAfter(3f));

	}

	IEnumerator HidePanelAfter(float seconds)
	{
		isAlreadyshowing = true;
		yield return new WaitForSeconds(seconds);
		UI_CardInfo_Panel.SetActive(false);
		print(GameObject.FindGameObjectsWithTag("Card").Length.ToString());
		isAlreadyshowing = false;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
