using Godot;
using System;

public class LightVarying : Godot.Spatial
{
	private Color current_color = new Color(1.0f,0.0f,0.0f,1.0f);
	[Export]
	public Color CurrentColor
	{
		get {return current_color;}
		set{
			current_color = value;
			UpdateColor();
		}
	}
	
	private OmniLight light;
	private Particles particles;
	
	private void UpdateColor()
	{
		if(particles != null && light != null)
		{
			PrimitiveMesh pm = particles.DrawPass1 as PrimitiveMesh;
			SpatialMaterial sm = pm.Material as SpatialMaterial;
			
			sm.AlbedoColor = current_color;
			sm.Emission = current_color;
			light.LightColor = current_color;
		}
	}

	public void DuplicateRessources()
	{
		particles.ProcessMaterial = particles.ProcessMaterial.Duplicate() as ParticlesMaterial;
		particles.DrawPass1 = particles.DrawPass1.Duplicate() as Mesh;
		PrimitiveMesh pm = particles.DrawPass1 as PrimitiveMesh;
		pm.Material = pm.Material.Duplicate() as Material;
	}

	public override void _Ready()
	{
		light = GetNode<OmniLight>("OmniLight");
		particles = GetNode<Particles>("Particles");
		DuplicateRessources();
		UpdateColor();
	}
	
	public override void _Process(float delta)
	{
		light.LightEnergy = 1.0f + (Mathf.Sin(OS.GetTicksMsec()/150.0f))/8.0f;
	}
}
