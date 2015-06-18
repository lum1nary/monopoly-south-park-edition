using UnityEngine;
using System.Collections;

public class Position : UnityEngine.Object
{
	public delegate void PositionChangedAction(Object sender, PositionEventArgs pe);
	public event PositionChangedAction PositionAdded;
	public event PositionChangedAction PositionSubtracted;
	public int Value { get;protected set;}
	public Position(int value)
	{
		Value = value;

	}
	#region Add
	public void Add(int count)
	{
		int old = Value;
		if((Value + count) > 40)
		{
			Value +=count -40;
		}
		else Value +=count;
		if(PositionAdded != null)
			PositionAdded(this, new PositionEventArgs(old,Value));
	}
	#endregion
	#region Subtract
	public void Subtract(int count)
	{
		int old = Value;
		if((Value - count) < 1)
		{
			Value +=40 - count;
		}
		else Value -=count;
		if(PositionSubtracted != null)
			PositionSubtracted(this, new PositionEventArgs(old,Value));
	}
	#endregion
	#region GetWorldPoint
	public Vector3 GetWorldPoint(Vector3 backSize)
	{
		return GetWorldPoint(backSize,0);
	}

 	public Vector3 GetWorldPoint(Vector3 backSize, float z_axis)
	{
		float border  =  backSize.y * 0.02f;
		float SinglePart = (backSize.y - (border * 2)) / 24;
		if(Value % 10 == 1)
		{
			switch(Value)
			{
			case 1: { return new Vector3(
					(backSize.y - (border + SinglePart * 1.5f)),
					(border + SinglePart * 1.5f),
					z_axis
					);}
			case 11:{ return new Vector3(
					(border + (SinglePart * 1.5f)),
					(border + (SinglePart * 1.5f)),
					z_axis
					);}
			case 21:{ return new Vector3(
					border + (SinglePart * 1.5f),
					backSize.y - (border + SinglePart* 1.5f),
					z_axis
					);}
			case 31:{ return new Vector3(
					backSize.y - (border + (SinglePart * 1.5f)),
					backSize.y - (border + (SinglePart * 1.5f)),
					z_axis
					);}
			default: {return Vector3.zero;}
			}

		}
		else
		{
			int side = (int)((Value-1) / 10);
			switch(side)
			{
			case 0:
			{
				return new Vector3(
					backSize.y - (border + (SinglePart * ( 3 + (((Value-1) % 10) * 2)  - 1)   )),
					border + (SinglePart * 1.5f),
					z_axis
					 );
			}
			case 1:
			{
				return new Vector3(
					border + (SinglePart * 1.5f),
					border + (SinglePart * (3 + (((Value-1) % 10) * 2) - 1 )),
					z_axis
					);
			}
			case 2:
			{
				return new Vector3(
					/*backSize.y -*/ (border + (SinglePart * ( 3 + (((Value-1) % 10) * 2)  - 1)   )),
					backSize.y - (border + (SinglePart * 1.5f)),
					z_axis
					);
			}
			case 3:
			{
				return new Vector3(
					backSize.y - (border + (SinglePart * 1.5f)),
					backSize.y - (border + (SinglePart * (3 + (((Value-1) % 10) * 2 ) - 1)  )),
					z_axis
					);
			}
			default:{ return Vector3.zero;}
			}
		}
	}
	#endregion
};
#region PositionEventArgs
public class PositionEventArgs : System.EventArgs
{
	public int OldPosition {get; private set;}
	public int NewPosition {get; private set;}
	public PositionEventArgs(int Old_Poisition,int New_Position)
	{
		OldPosition = Old_Poisition;
		NewPosition = New_Position;
	}
}
#endregion
