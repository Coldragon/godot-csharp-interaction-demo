using Godot;
using System;

public class LightVarying : Godot.Spatial
{
	private Color _currentColor = new Color(1.0f,0.0f,0.0f,1.0f);
	[Export]
	public Color CurrentColor
	{
		get => _currentColor;
		set{
			_currentColor = value;
			UpdateColor();
		}
	}
	
	private OmniLight _light;
	private Particles _particles;
	
	private void UpdateColor()
	{
		if (_particles == null || _light == null) return;
		var pm = _particles.DrawPass1 as PrimitiveMesh;
		if (pm?.Material is SpatialMaterial sm) //check if pointer not null "?"
		{
			sm.AlbedoColor = _currentColor;
			sm.Emission = _currentColor;
		}
		_light.LightColor = _currentColor;
	}

	private void DuplicateRessources()
	{
		_particles.ProcessMaterial = _particles.ProcessMaterial.Duplicate() as ParticlesMaterial;
		_particles.DrawPass1 = _particles.DrawPass1.Duplicate() as Mesh;
		if (_particles.DrawPass1 is PrimitiveMesh pm) pm.Material = pm.Material.Duplicate() as Material;
	}

	public override void _Ready()
	{
		_light = GetNode<OmniLight>("OmniLight");
		_particles = GetNode<Particles>("Particles");
		DuplicateRessources();
		UpdateColor();
	}
	
	public override void _Process(float delta)
	{
		_light.LightEnergy = 1.0f + (Mathf.Sin(OS.GetTicksMsec()/150.0f))/8.0f;
	}
}