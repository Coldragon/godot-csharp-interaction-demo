using Godot;
using System;

public class Levier : InteractiveItem
{
	
	AnimationPlayer ap;
	OmniLight ol;
	public bool _activated = false;
	
	public override void _Ready()
	{
		ap = GetNode<AnimationPlayer>("AnimationPlayer");
		ol = GetNode<OmniLight>("OmniLight");
	}
	
	public override void Interact(Player player)
	{
		GD.Print(player.Name + "interacted with " + Name);
		SetAnimation(!_activated);
	}
	
	public void SetAnimation(bool new_state)
	{
		if(new_state != _activated)
		{
			if(new_state)
			{
				ap.Play("Activate");
				ol.LightColor = Colors.Green;
			}
			else
			{
				ap.PlayBackwards("Activate");
				ol.LightColor = Colors.Red;
			}
			_activated = new_state;
		}
	}
}
