using UnityEngine;
using System.Collections;

public class EstateCard : BaseCard 
{
	public delegate void EstateCardOwnerAction(UnityEngine.Object sender, EstateCardEventArgs ese);
	public event EstateCardOwnerAction OwnerChanged;
	public int CurrentPrice {get; set;}
	public bool IsLaid {get; set;}
	Player owner;
	public Player Owner {get 
		{
			return owner;
		} 
		set 
		{
			owner = value;
			if(OwnerChanged != null)
				OwnerChanged(this, new EstateCardEventArgs(owner));
		}
	}
	public override void OnPlayerEnter (object sender, PlayerEventArgs pe)
	{
		Player enteredPlayer = ((GameObject)sender).GetComponent<Player>();
		if( enteredPlayer != owner)
		{

		}
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