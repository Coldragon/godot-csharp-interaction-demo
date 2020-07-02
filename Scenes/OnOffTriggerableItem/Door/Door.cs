using Godot;
using System;

public class Door : OnOffTriggerableItem
{
	Tween tw;
	
	[Export]
	Vector3 openning_offset = new Vector3(2.0f, 0.0f, 0.0f);
	Vector3 initial_position;
	[Export]
	float duration = 1.0f;
	
	public override void _Ready()
	{
		tw = GetNode<Tween>("Tween");
		initial_position = Transform.origin;
		IsOn = IsOn;
	}

	public override void On()
	{
		if(tw != null)
		{
			tw.InterpolateProperty(this, "transform:origin", Transform.origin, initial_position+openning_offset, duration);
			tw.Start();
		}
	}

	public override void Off()
	{
		if(tw != null)
		{
			tw.InterpolateProperty(this, "transform:origin", Transform.origin, initial_position, duration);
			tw.Start();
		}
	}
}
