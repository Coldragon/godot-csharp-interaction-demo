using Godot;
using System;

public class LightSwitchable : OnOffTriggerableItem
{
	[Export] public Color OnColor { get; set; } = new Color(0.0f,1.0f,0.0f,1.0f);
	[Export] public Color OffColor { get; set; } = new Color(1.0f,0.0f,0.0f,1.0f);

	private LightVarying _lv;

	public override void _Ready()
	{
		_lv = GetNode<LightVarying>("LightVarying");
		IsOn = IsOn;
	}

	protected override void On()
	{
		if(_lv != null)
			_lv.CurrentColor = OnColor;
	}

	protected override void Off()
	{
		if(_lv != null)
			_lv.CurrentColor = OffColor;
	}
}
