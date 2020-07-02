using Godot;
using System;

abstract public class TriggerOnOffItems : InteractiveItem
{	
	[Export] public Godot.Collections.Array<NodePath> openable_items_path = new Godot.Collections.Array<NodePath>();
	public Godot.Collections.Array<OnOffTriggerableItem> openable_items_nodes =  new Godot.Collections.Array<OnOffTriggerableItem>();
	public bool _activated = false;

	public override void _Ready()
	{
		foreach(NodePath path in openable_items_path)
		{
			Node node = GetNode(path);
			if(node is OnOffTriggerableItem castnode)
				openable_items_nodes.Add(castnode);
		}
	}

	public void Switch()
	{
		_activated = !_activated;
		foreach(OnOffTriggerableItem oi in openable_items_nodes)
			oi.IsOn = !oi.IsOn;
	}
}
