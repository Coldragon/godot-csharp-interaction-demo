using Godot;
using System;

abstract public class OpenableItem : KinematicBody
{
	abstract public void Open();
	abstract public void Close();
}
