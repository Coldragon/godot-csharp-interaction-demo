using Godot;
using System;

abstract public class OpenableItem : StaticBody
{
	abstract public void Open();
	abstract public void Close();
}
