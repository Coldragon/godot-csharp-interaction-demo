using Godot;
using System;

abstract public class OnOffTriggerableItem : Spatial
{
	private bool is_on = false;
	[Export] public bool IsOn
	{
		get {return is_on;}
		set{
			is_on = value;
			if(is_on)
				On();
			else
				Off();
			}
	}

	abstract public void On();
	abstract public void Off();
}
