using Godot;
using System;

abstract public class InteractiveItem : Area
{
	abstract public void Interact(Player player);
}
