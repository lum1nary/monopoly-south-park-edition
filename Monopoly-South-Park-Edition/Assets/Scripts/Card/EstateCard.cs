using UnityEngine;
using System.Collections;

public class EstateCard : BaseCard 
{
	public delegate void EstateCardOwnerAction(UnityEngine.Object sender, EstateCardEventArgs ese);
	public event EstateCardOwnerAction OwnerChanged;
	public int PurchasePrice {get; set;}
	protected int LayPrice {get; set;}
	public int SiteRentPrice {get; set;}
	public int CurrentPrice {get; set;}
	public bool IsLaid {get; set;}
	public CardInfo CardInfo {get; set;}
	Player owner;
	public Player Owner {get 
		{
			return owner;
		} 
		set 
		{
			owner = value;
			if(OwnerChanged != null)
				OwnerChanged(this, new EstateCardEventArgs(Owner));
		}
	}

	void Start()
	{
		LayPrice = PurchasePrice / 2;
		Owner = null;
	}



};
#region Estate Card Event Args
public class EstateCardEventArgs : System.EventArgs
{

	public Player NewOwner {get;private set;}
	public EstateCardEventArgs(Player Owner)
	{
		NewOwner = Owner;
	}
};
#endregion