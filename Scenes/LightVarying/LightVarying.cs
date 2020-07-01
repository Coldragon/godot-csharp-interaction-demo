using Godot;
using System;

public class LightVarying : Godot.StaticBody
{
	[Export]
	private Color current_color = new Color(1.0f,0.0f,0.0f,1.0f);
	
	private OmniLight light;
	private Particles particles;
	
	public override void _Ready()
	{
		light = GetNode<OmniLight>("OmniLight");
		particles = GetNode<Particles>("Particles");
		
		particles.ProcessMaterial = particles.ProcessMaterial.Duplicate() as ParticlesMaterial;
		particles.DrawPass1 = particles.DrawPass1.Duplicate() as Mesh;
		PrimitiveMesh pm = particles.DrawPass1 as PrimitiveMesh;
		pm.Material = pm.Material.Duplicate() as Material;
		SpatialMaterial sm = pm.Material as SpatialMaterial;
		sm.AlbedoColor = current_color;
		sm.Emission = current_color;
		light.LightColor = current_color;
	}
	
	public override void _Process(float delta)
	{
		light.LightEnergy = 1.0f + (Mathf.Sin(OS.GetTicksMsec()/150.0f))/8.0f;
	}
}
