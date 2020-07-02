using Godot;
using System;

public class Levier : InteractiveItem
{
	[Export] Godot.Collections.Array<NodePath> openable_items_path = new Godot.Collections.Array<NodePath>();
	Godot.Collections.Array<OpenableItem> openable_items_nodes =  new Godot.Collections.Array<OpenableItem>();
	AnimationPlayer ap;
	OmniLight ol;
	public bool _activated = false;
	
	public override void _Ready()
	{
		ap = GetNode<AnimationPlayer>("AnimationPlayer");
		ol = GetNode<OmniLight>("OmniLight");
		foreach(NodePath path in openable_items_path)
		{
			Node node = GetNode(path);
			if(node is OpenableItem castnode)
				openable_items_nodes.Add(castnode);
		}
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

				foreach(OpenableItem oi in openable_items_nodes)
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

				foreach(OpenableItem oi in openable_items_nodes)
					oi.Close();
			}

			_activated = new_state;
		}
	}
}
