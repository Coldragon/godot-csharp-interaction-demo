using Godot;
using System;

public class Levier : InteractiveItem
{
	[Export]
	NodePath openable_item;
	OpenableItem oi;
	AnimationPlayer ap;
	OmniLight ol;
	public bool _activated = false;
	
	public override void _Ready()
	{
		ap = GetNode<AnimationPlayer>("AnimationPlayer");
		ol = GetNode<OmniLight>("OmniLight");
		oi = GetNode<OpenableItem>(openable_item);
	}
	
	public override void Interact(Player player)
	{
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
				
				Color col = Colors.Green;
				col.a = 0.5f;
				foreach(Node node in GetTree().GetNodesInGroup("lever1_open"))
					if(node is LightVarying nodecast)
						nodecast.CurrentColor = col;

				if(oi != null)
					oi.Open();
			}
			else
			{
				ap.PlayBackwards("Activate");
				ol.LightColor = Colors.Red;
				
				Color col = Colors.Red;
				col.a = 0.5f;
				foreach(Node node in GetTree().GetNodesInGroup("lever1_open"))
					if(node is LightVarying nodecast)
						nodecast.CurrentColor = col;

				if(oi != null)
					oi.Close();
			}

			_activated = new_state;
		}
	}
}
