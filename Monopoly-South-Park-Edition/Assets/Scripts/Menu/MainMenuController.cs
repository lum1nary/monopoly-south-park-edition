using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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



	void Awake()
	{
		InitializeMainMenu();
		InitializeOptionsMenu();
		InitializeNewGameMenu();
		InitializeRulesMenu();
	}
	void InitializeNewGameMenu()
	{
		BackFromNewGame.onClick.AddListener(new UnityAction(() => {
			NewGamePanel.GetComponent<Animator>().Play("OnChoosingFromMenu");
			StartCoroutine(SwitchMenu(NewGamePanel,menuPanel,1f));
		}));
	}
	void InitializeOptionsMenu()
	{
		OptionsBack.onClick.AddListener(new UnityAction(() => {  
			OptionsPanel.GetComponent<Animator>().Play("OnBackFromOptions");
			StartCoroutine(SwitchMenu(OptionsPanel,menuPanel,1f));
		}));

	}
	void InitializeRulesMenu()
	{
		RulesBack.onClick.AddListener(new UnityAction(() => {
			RulesPanel.GetComponent<Animator>().Play("OnBackFromOptions");
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
			menuPanelAnimator.Play("OnChoosingFromMenu");
			StartCoroutine(SwitchMenu(menuPanel,NewGamePanel,1f));
			
		}));

		Rules.onClick.AddListener(new UnityAction(() => 
		                                          {
			menuPanelAnimator.Play("OnChoosingFromMenu");
			StartCoroutine(SwitchMenu(menuPanel,RulesPanel,1f));
		}));

		Options.onClick.AddListener(new UnityAction(() =>
		                                            {
			menuPanelAnimator.Play("OnChoosingFromMenu");

			StartCoroutine(SwitchMenu(menuPanel,OptionsPanel,1f));
		}));

		Exit.onClick.AddListener(new UnityAction(() => {
			menuPanelAnimator.Play("OnChoosingFromMenu");
			StartCoroutine(SwitchMenu(menuPanel,null,1f));
			fader.OnFinishFadingToBlack += () => Application.Quit();
			fader.fadeSpeed = 2;
			fader.FadeToBlack(true);
		} ));
	}

	void Start () {
		fader.FadeToBlack(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void Reset(object PressedButon)
	{
		((Button)PressedButon).transform.localScale = Vector3.one;
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
