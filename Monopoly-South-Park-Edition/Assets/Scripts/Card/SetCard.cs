using UnityEngine;
using System.Collections;

public class SetCard : EstateCard 
{
	public delegate void SetCardAction(UnityEngine.Object sender, SetCardEventArgs sce);
	public event SetCardAction StatusChanged;
	public event SetCardAction CurrentPriceChanged;

	public SetCardStatus Status {get; set;}
	
	public override void Initialize(CardInfo ci, Sprite sp)
	{
		base.Initialize(ci, sp);
		Owner = null;
		CurrentPrice = CardInfo.PurchasePrice;
		ChangeStatus(SetCardStatus.Normal);
	}



	#region Change Status
	public void ChangeStatus(SetCardStatus NewStatus)
	{
		Status = NewStatus;
		if(StatusChanged != null)
			StatusChanged(this, new SetCardEventArgs(Status,CurrentPrice));
		ChangeCurrentPrice();
	}
	#endregion
	#region Change Current Price
	protected void ChangeCurrentPrice()
	{
		switch(Status)
		{
		case SetCardStatus.Normal:          { CurrentPrice = CardInfo.SitePrice;}break;
		case SetCardStatus.Doubled:         { CurrentPrice = CardInfo.SitePrice * 2; }break;
		case SetCardStatus.With_1_House:    { CurrentPrice = CardInfo.H1_Price; }break;
		case SetCardStatus.With_2_Houses:   { CurrentPrice = CardInfo.H2_Price; }break;
		case SetCardStatus.With_3_Houses:   { CurrentPrice = CardInfo.H3_Price; }break;
		case SetCardStatus.With_4_Houses:   { CurrentPrice = CardInfo.H4_Price; }break;
		case SetCardStatus.With_Hotel:      { CurrentPrice = CardInfo.Hotel_Price;}break;
		}
		if(CurrentPriceChanged != null)
		{
			CurrentPriceChanged(this, new SetCardEventArgs(Status,CurrentPrice));
		}
	}
	#endregion
};
#region SetCardEventArgs
public class SetCardEventArgs : System.EventArgs
{
	public SetCardStatus NewStatus {get; private set;}
	public int NewPrice {get; private set;}
	public SetCardEventArgs(SetCardStatus status, int price)
	{
		NewStatus = status;
		NewPrice = price;
	}
};
#endregion