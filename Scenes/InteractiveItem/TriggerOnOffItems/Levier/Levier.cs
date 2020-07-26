using Godot;
using System;

public class Levier : TriggerOnOffItems
{
	private AnimationPlayer _ap;
	private OmniLight _ol;
	
	public override void _Ready()
	{
		base._Ready();
		_ap = GetNode<AnimationPlayer>("AnimationPlayer");
		_ol = GetNode<OmniLight>("OmniLight");
	}
	
	public override void Interact(Player player)
	{
		Switch();
		Animate();
	}

	private void Animate()
	{
		if(Activated)
		{
			_ap.Play("Activate");
			_ol.LightColor = Colors.Green;
		}
		else
		{
			_ap.PlayBackwards("Activate");
			_ol.LightColor = Colors.Red;
		}
	}
}
