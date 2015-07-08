using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour {

	public GameObject Background;
	//Main Menu
	[Header("Main Menu")]
	public Button NewGame;
	public Button Rules;
	public Button Options;
	public Button Exit;
	[Header("Options")]
	public Button OptionsBack;
	[Header("Rules")]
	public Button RulesBack;
	[Header("NewGame")]
	public Button TwoPlayers;
	public Button ThreePlayers;
	public Button FourPlayers;
	public Button BackFromNewGame;
	[Header("Panels")]
	public GameObject OptionsPanel;
	public GameObject NewGamePanel;
	public GameObject menuPanel;
	public GameObject RulesPanel;
	[Header("Other")]
	public Animator menuPanelAnimator;
	Fader fader;
	public GameObject PlayerExample;
	public InputField PlayerName;
	public GameObject HeroLobby;
	public GameObject HeroPanel;
	public GameObject PlayersToLoad;
	public GameObject PlayersPreset;
	public Text CurrentPlayer;
	public GameObject GameCreatorPanel;
	public Button LetsGoButton;
	int playerCount=0;
	int activePlayer=1;
	string ActivePlayerName;

	void Awake()
	{
		LetsGoButton.gameObject.SetActive(false);
		InitializeMainMenu();
		InitializeOptionsMenu();
		InitializeNewGameMenu();
		InitializeRulesMenu();
		//Dangerous code...
		foreach (Transform item in HeroPanel.transform) {
			item.gameObject.GetComponent<ChooseIconController>().OnIconChoosed+= OnSpriteChoosen;
		}
		PlayerName.onEndEdit.AddListener(new UnityAction<string>((string arg0) => {ActivePlayerName= arg0; }));
		LetsGoButton.onClick.AddListener(new UnityAction(() =>{
			fader.OnFinishFadingToBlack += () => 
			{
				PlayersToLoad.SetActive(true);
				Application.LoadLevel(1);
			};
			fader.FadeToBlack(true);

		}));
		DontDestroyOnLoad(PlayersToLoad);
	}

	void OnSpriteChoosen(object sender, IconEventArgs ie)
	{
		GameObject new_player = GameObject.Instantiate<GameObject>(PlayerExample);
		new_player.GetComponent<Player>().Initialize(1500,ActivePlayerName,ie.hero_sprite);
		new_player.transform.SetParent(PlayersToLoad.transform);


		GameObject PrepearedPlayer = GameObject.Instantiate<GameObject>(PlayersPreset);
		PrepearedPlayer.GetComponentInChildren<Text>().text = ActivePlayerName;
		PrepearedPlayer.transform.FindChild("Image").GetComponent<Image>().sprite = ie.hero_sprite;
		PrepearedPlayer.transform.SetParent(HeroLobby.transform);
		PrepearedPlayer.transform.localScale = Vector3.one;
		playerCount--;
		if(playerCount >0)
		{
			activePlayer++;
		}
		else LetsGoButton.gameObject.SetActive(true);
		CurrentPlayer.text = "Player " +activePlayer.ToString();
		PlayerName.text = string.Empty;
	}
	void InitializeNewGameMenu()
	{
		BackFromNewGame.onClick.AddListener(new UnityAction(() => {
			NewGamePanel.GetComponent<Animator>().Play("NewGameMenuOnLeaving");
			StartCoroutine(SwitchMenu(NewGamePanel,menuPanel,1f));
			GameCreatorPanel.GetComponent<Animator>().Play("InNewGameMenuOnLeaving");
			StartCoroutine(SwitchMenu(GameCreatorPanel,null,1f));
		}));

		TwoPlayers.onClick.AddListener(new UnityAction(() =>{
			playerCount =2;
			if(!GameCreatorPanel.activeSelf)
			{
				GameCreatorPanel.SetActive(true);

			}

			else 
			{
				GameCreatorPanel.SetActive(false);
				RefreshPanel();
				GameCreatorPanel.SetActive(true);

			}

		}));

		ThreePlayers.onClick.AddListener(new UnityAction(() =>{
			playerCount =3;
			if(!GameCreatorPanel.activeSelf)
			{
				GameCreatorPanel.SetActive(true);
			}
				
			else 
			{
				GameCreatorPanel.SetActive(false);
				RefreshPanel();
				GameCreatorPanel.SetActive(true);
			
			}
		}));
		FourPlayers.onClick.AddListener(new UnityAction(() => {
			playerCount=4;
			if(!GameCreatorPanel.activeSelf)
			{
				GameCreatorPanel.SetActive(true);
			
			}
				else 
			{
				GameCreatorPanel.SetActive(false);
				RefreshPanel();
				GameCreatorPanel.SetActive(true);

			}
		}));


	}

	void RefreshPanel()
	{
		foreach (var item in GameObject.FindObjectsOfType<Player>()) 
		{
			Destroy(item.gameObject);
		}

		GameObject New_HeroPanel =  GameObject.Instantiate<GameObject>(HeroPanel);
		New_HeroPanel.transform.SetParent(GameCreatorPanel.transform);
		foreach (Transform child in HeroLobby.transform) 
		{
			Destroy(child.gameObject);
		}
		activePlayer = 1;
		CurrentPlayer.text = "Player " + activePlayer.ToString();
		
	}

	void InitializeOptionsMenu()
	{
		OptionsBack.onClick.AddListener(new UnityAction(() => {  
			OptionsPanel.GetComponent<Animator>().Play("OptionsMenuOnLeaving");
			StartCoroutine(SwitchMenu(OptionsPanel,menuPanel,2f));
		}));

	}
	void InitializeRulesMenu()
	{
		RulesBack.onClick.AddListener(new UnityAction(() => {
			RulesPanel.GetComponent<Animator>().Play("RulesMenuOnLeaving");
			StartCoroutine(SwitchMenu(RulesPanel,menuPanel,1f));
		}));
	}

	void InitializeMainMenu()
	{
		Background.ScaleToCameraAspectRatio();
		fader = gameObject.GetComponent<Fader>();
		menuPanel.SetActive(false);
		fader.OnFinishFadingToClear += () => menuPanel.SetActive(true);
	

		NewGame.onClick.AddListener(new UnityAction(() => 
		                                            {
			menuPanelAnimator.Play("MainMenuOnLeaving");
			StartCoroutine(SwitchMenu(menuPanel,NewGamePanel,1f));
			
		}));

		Rules.onClick.AddListener(new UnityAction(() => 
		                                          {
			menuPanelAnimator.Play("MainMenuOnLeaving");
			StartCoroutine(SwitchMenu(menuPanel,RulesPanel,1f));
		}));

		Options.onClick.AddListener(new UnityAction(() =>
		                                            {
			menuPanelAnimator.Play("MainMenuOnLeaving");

			StartCoroutine(SwitchMenu(menuPanel,OptionsPanel,1f));
		}));

		Exit.onClick.AddListener(new UnityAction(() => {
			menuPanelAnimator.Play("MainMenuOnLeaving");
			StartCoroutine(SwitchMenu(menuPanel,null,1f));
			fader.OnFinishFadingToBlack += () => Application.Quit();
			fader.fadeSpeed = 2;
			fader.FadeToBlack(true);
		} ));
	}

	void Start () {
		fader.FadeToBlack(false);
		CurrentPlayer.text = "Player " + activePlayer.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator SwitchMenu(GameObject toDisable, GameObject toEnable ,float secounds)
	{
		yield return new WaitForSeconds(secounds);
		if(toDisable != null)
		toDisable.SetActive(false);
		if(toEnable != null)
		toEnable.SetActive(true);
	}


}
