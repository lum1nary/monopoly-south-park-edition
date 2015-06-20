using UnityEngine;
using System.Collections;

public class BaseCard : MonoBehaviour
{	
	protected GameObject sprite {get;set;}
	protected BoxCollider2D CardCollider;
	protected SpriteRenderer spritePicture {get;set;}
	protected DataReader DataReader;
	public CardInfo CardInfo {get;protected set;}
	void Awake()
	{
		sprite = transform.FindChild("Sprite").gameObject;
		spritePicture = sprite.GetComponent<SpriteRenderer>();
		CardCollider = gameObject.GetComponent<BoxCollider2D>();
		CardCollider.size *=0.32f;
		DataReader = GameObject.Find("DataReader").GetComponent<DataReader>();
	}
		
	public virtual void OnPlayerEnter(object sender, PlayerEventArgs pe)
	{

	}
	public virtual void Initialize(CardInfo ci)
	{
		Initialize(ci,spritePicture.sprite);
	}

	public virtual void Initialize(CardInfo ci, Sprite sp)
	{
		CardInfo = ci;
		spritePicture.sprite = sp;
		gameObject.name = CardInfo.Title;
	}

	#region GetWorldPoint
	public Vector3 GetWorldPoint(Vector3 backSize)
	{
		float border  =  backSize.y * 0.02f;
		float SinglePart = (backSize.y - (border * 2)) / 24;
		if(CardInfo.Position % 10 == 1)
		{
			switch(CardInfo.Position)
			{
			case 1: { return new Vector3(
					(backSize.y - (border + SinglePart * 1.5f)),
					(border + SinglePart * 1.5f)
					);}
			case 11:{ return new Vector3(
					(border + (SinglePart * 1.5f)),
					(border + (SinglePart * 1.5f))
					);}
			case 21:{ return new Vector3(
					border + (SinglePart * 1.5f),
					backSize.y - (border + SinglePart* 1.5f)
					);}
			case 31:{ return new Vector3(
					backSize.y - (border + (SinglePart * 1.5f)),
					backSize.y - (border + (SinglePart * 1.5f))
					);}
			default: {return Vector3.zero;}
			}
			
		}
		else
		{
			int side = (int)((CardInfo.Position-1) / 10);
			switch(side)
			{
			case 0:
			{
				return new Vector3(
					backSize.y - (border + (SinglePart * ( 3 + (((--CardInfo.Position) % 10) * 2)  - 1)   )),
					border + (SinglePart * 1.5f)
					);
			}
			case 1:
			{
				return new Vector3(
					border + (SinglePart * 1.5f),
					border + (SinglePart * (3 + (((--CardInfo.Position) % 10) * 2) - 1 ))
					);
			}
			case 2:
			{
				return new Vector3(
					/*backSize.y -*/ (border + (SinglePart * ( 3 + (((--CardInfo.Position) % 10) * 2)  - 1)   )),
					backSize.y - (border + (SinglePart * 1.5f))
					);
			}
			case 3:
			{
				return new Vector3(
					backSize.y - (border + (SinglePart * 1.5f)),
					backSize.y - (border + (SinglePart * (3 + (((--CardInfo.Position) % 10) * 2 ) - 1)  ))
					);
			}
			default:{ return Vector3.zero;}
			}
		}
	}
	#endregion
	void OnMouseEnter()
	{
		spritePicture.color =Color.white;
	}
	void OnMouseExit()
	{
		spritePicture.color = new Color32(231,231,231,255);
	}


}