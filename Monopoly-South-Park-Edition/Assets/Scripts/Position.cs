using UnityEngine;
using System.Collections;

public class Position : UnityEngine.Object
{
	public delegate void PositionChangedAction(Object sender, PositionEventArgs pe);
	public event PositionChangedAction PositionAdded;
	public event PositionChangedAction PositionSubtracted;
	int _value;
	public int Value {
		get
		{
			return _value;
		}
	}
	public Position(int value)
	{
		_value = value;

	}
	#region Add
	public void Add(int count)
	{
		int old = _value;
		if((_value + count) > 40)
		{
			_value +=count -40;
		}
		else _value +=count;
		PositionAdded(this, new PositionEventArgs(old,_value));
	}
	#endregion
	#region Subtract
	public void Subtract(int count)
	{
		int old = _value;
		if((_value - count) < 1)
		{
			_value +=40 - count;
		}
		else _value -=count;
		PositionSubtracted(this, new PositionEventArgs(old,_value));
	}
	#endregion
	#region GetWorldPoint
 	public Vector3 GetWorldPoint(Vector3 backSize)
	{
		float border  =  backSize.y * 0.02f;
		float SinglePart = (backSize.y - (border * 2)) / 24;
		if(_value % 10 == 1)
		{
			switch(_value)
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
			int side = (int)((_value-1) / 10);
			switch(side)
			{
			case 0:
			{
				return new Vector3(
					backSize.y - (border + (SinglePart * ( 3 + (((--_value) % 10) * 2)  - 1)   )),
					border + (SinglePart * 1.5f)
					);
			}
			case 1:
			{
				return new Vector3(
					border + (SinglePart * 1.5f),
					border + (SinglePart * (3 + (((--_value) % 10) * 2) - 1 ))
					);
			}
			case 2:
			{
				return new Vector3(
					/*backSize.y -*/ (border + (SinglePart * ( 3 + (((--_value) % 10) * 2)  - 1)   )),
					backSize.y - (border + (SinglePart * 1.5f))
					);
			}
			case 3:
			{
				return new Vector3(
					backSize.y - (border + (SinglePart * 1.5f)),
					backSize.y - (border + (SinglePart * (3 + (((--_value) % 10) * 2 ) - 1)  ))
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
