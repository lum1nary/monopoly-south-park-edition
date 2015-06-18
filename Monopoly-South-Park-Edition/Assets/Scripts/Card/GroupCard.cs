using UnityEngine;
using System.Collections;

public class GroupCard : EstateCard {
	
	public  delegate void GroupCardAction(object sender, GroupCardEventArgs gae);
	public static event GroupCardAction DrowDicesRequest;
	// Use this for initialization
	void Start () 
	{
		OwnerChanged += UpdatePrice;	
	}



	void UpdatePrice(Object sender, EstateCardEventArgs ese)
	{
		switch(CardInfo.Group)
		{
		case Group.Journey:{CurrentPrice = CardInfo.SitePrice * GetCardCount(); }break;
		case Group.Science:{
			DrowDicesRequest(this, new GroupCardEventArgs()); 
		
		}break;
		}

	}

	int GetCardCount()
	{
		int OwnerHas = 0;
		if(Owner != null)
		{
			foreach (GameObject Card in Owner.Cards) 
			{
				if(Card.GetComponent<EstateCard>().CardInfo.Group == CardInfo.Group)
				{
					OwnerHas ++;
				}
			}

		}
		return OwnerHas;
	}
	#region Initialize
	public override void Initialize(CardInfo ci, Sprite sp)
	{
		base.Initialize(ci, sp);
		Owner = null;
		CurrentPrice = CardInfo.PurchasePrice;

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
