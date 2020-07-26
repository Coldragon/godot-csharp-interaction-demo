using Godot;
using System;

public abstract class OnOffTriggerableItem : Spatial
{
	private bool _isOn = false;
	[Export] public bool IsOn
	{
		get => _isOn;
		set
		{
			_isOn = value;
			if (_isOn)
				On();
			else
				Off();
		}
	}

	protected abstract void On();
	protected abstract void Off();
}
