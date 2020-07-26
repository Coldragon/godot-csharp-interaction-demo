using Godot;
using System;

public abstract class TriggerOnOffItems : InteractiveItem
{	
	[Export] public Godot.Collections.Array<NodePath> OpenableItemsPath = new Godot.Collections.Array<NodePath>();
	private readonly Godot.Collections.Array<OnOffTriggerableItem> _openableItemsNodes =  new Godot.Collections.Array<OnOffTriggerableItem>();
	protected bool Activated = false;

	public override void _Ready()
	{
		foreach(var path in OpenableItemsPath)
		{
			var node = GetNode(path);
			if(node is OnOffTriggerableItem castnode)
				_openableItemsNodes.Add(castnode);
		}
	}

	protected void Switch()
	{
		Activated = !Activated;
		foreach(var oi in _openableItemsNodes)
			oi.IsOn = !oi.IsOn;
	}
}
