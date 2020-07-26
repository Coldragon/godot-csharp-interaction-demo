using Godot;
using System;

public abstract class InteractiveItem : Area
{
	public abstract void Interact(Player player);
}
