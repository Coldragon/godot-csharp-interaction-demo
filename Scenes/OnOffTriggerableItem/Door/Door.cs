using Godot;
using System;

public class Door : OnOffTriggerableItem
{
	[Export] private Vector3 _openningOffset = new Vector3(2.0f, 0.0f, 0.0f);
	private Vector3 _initialPosition;
	[Export] private float _duration = 1.0f;
	private Tween _tw;
	
	public override void _Ready()
	{
		_tw = GetNode<Tween>("Tween");
		_initialPosition = Transform.origin;
		IsOn = IsOn;
	}

	protected override void On()
	{
		if (_tw == null) return;
		_tw.InterpolateProperty(this, "transform:origin", Transform.origin, _initialPosition+_openningOffset, _duration);
		_tw.Start();
	}

	protected override void Off()
	{
		if (_tw == null) return;
		_tw.InterpolateProperty(this, "transform:origin", Transform.origin, _initialPosition, _duration);
		_tw.Start();
	}
}
