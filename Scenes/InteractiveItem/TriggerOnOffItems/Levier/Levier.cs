using Godot;
using System;

public class Levier : TriggerOnOffItems
{
	AnimationPlayer ap;
	OmniLight ol;
	
	public override void _Ready()
	{
		base._Ready();
		ap = GetNode<AnimationPlayer>("AnimationPlayer");
		ol = GetNode<OmniLight>("OmniLight");
	}
	
	public override void Interact(Player player)
	{
		Switch();
		Animate();
	}
	
	public void Animate()
	{
		if(_activated)
		{
			ap.Play("Activate");
			ol.LightColor = Colors.Green;
			Color col = Colors.Green;
			col.a = 0.5f;
		}
		else
		{
			ap.PlayBackwards("Activate");
			ol.LightColor = Colors.Red;
			Color col = Colors.Red;
			col.a = 0.5f;
		}
	}
}
