using UnityEngine;
using System.Collections;

public class GroupCard : EstateCard {
	

	// Use this for initialization
	void Start () 
	{
		OwnerChanged += UpdatePrice;	
	}



	void UpdatePrice(Object sender, EstateCardEventArgs ese)
	{
		int OwnerHas = 0;
		Debug.Log("ahaha a ya podpisalsya !\n" + gameObject.name);

		
			if(Owner != null)
			{
				foreach (GameObject Card in Owner.Cards) 
				{
					if(Card.GetComponent<EstateCard>().CardInfo.Group == CardInfo.Group)
					{
						OwnerHas ++;
					}
				}
				CurrentPrice = SiteRentPrice * OwnerHas;
			}
			else CurrentPrice = SiteRentPrice;
	}
	#region Initialize
	public void Initialize(string Name)
	{
		gameObject.name = name;
		CardInfo = GameObject.Find("DataReader").GetComponent<DataReader>().GetCardInfo(gameObject.name);
		PurchasePrice = CardInfo.PurchasePrice;
		SiteRentPrice = CardInfo.SitePrice;
		CurrentPrice = SiteRentPrice;
		Title = CardInfo.Title;
		Position = new Position(CardInfo.Position);

	}
	#endregion
}
#region Group Card Event Args
public class GroupCardEventArgs : System.EventArgs 
{
	public GroupCardEventArgs()
	{

	}
}
#endregion
