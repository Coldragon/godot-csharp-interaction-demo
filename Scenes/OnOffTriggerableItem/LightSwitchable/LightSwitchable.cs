using Godot;
using System;

public class LightSwitchable : OnOffTriggerableItem
{
	private Color on_color = new Color(0.0f,1.0f,0.0f,1.0f);
	[Export] public Color OnColor
	{
		get {return on_color;}
		set{
			on_color = value;
		}
	}
	
	private Color off_color = new Color(1.0f,0.0f,0.0f,1.0f);
	[Export] public Color OffColor
	{
		get {return off_color;}
		set{
			off_color = value;
		}
	}
	
	LightVarying lv;

	public override void _Ready()
	{
		lv = GetNode<LightVarying>("LightVarying");
		IsOn = IsOn;
	}

	public override void On()
	{
		if(lv != null)
			lv.CurrentColor = OnColor;
	}
	
	public override void Off()
	{
		if(lv != null)
			lv.CurrentColor = OffColor;
	}
}
