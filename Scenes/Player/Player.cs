using Godot;
using System;

public class Player : KinematicBody
{
 	[Export] public float Gravity = -24.8f;
	[Export] public float MaxSpeed = 20.0f;
	[Export] public float JumpSpeed = 18.0f;
	[Export] public float Accel = 4.5f;
	[Export] public float Deaccel = 16.0f;
	[Export] public float MaxSlopeAngle = 40.0f;
	[Export] public float MouseSensitivity = 0.05f;

	private Vector3 _vel = new Vector3();
	private Vector3 _dir = new Vector3();

	private Camera _camera;
	private Spatial _rotationHelper;
	private RayCast _raycast;
	private TextureRect _interactIcon;
	
	public override void _Ready()
	{
		_camera = GetNode<Camera>("Rotation_Helper/Camera");
		_rotationHelper = GetNode<Spatial>("Rotation_Helper");
		_raycast = GetNode<RayCast>("Rotation_Helper/Camera/RayCast");
		_interactIcon = GetNode<TextureRect>("PlayerUI/Interract");
		Input.SetMouseMode(Input.MouseMode.Captured);
	}

	public override void _PhysicsProcess(float delta)
	{
		ProcessInput(delta);
		ProcessMovement(delta);
		ProcessInteraction(delta);
	}

	private void ProcessInteraction(float delta)
	{
		if(_raycast.GetCollider() is InteractiveItem collider)
		{
			if(Input.IsActionJustPressed("interract"))
			{
				collider.Interact(this);
			}
			_interactIcon.Show();
		}
		else
			_interactIcon.Hide();
	}

	private void ProcessInput(float delta)
	{
		_dir = new Vector3();
		var camXform = _camera.GlobalTransform;
		var inputMovementVector = new Vector2();
		
		if (Input.IsActionPressed("movement_forward"))
			inputMovementVector.y += 1;
		if (Input.IsActionPressed("movement_backward"))
			inputMovementVector.y -= 1;
		if (Input.IsActionPressed("movement_left"))
			inputMovementVector.x -= 1;
		if (Input.IsActionPressed("movement_right"))
			inputMovementVector.x += 1;
		
		inputMovementVector = inputMovementVector.Normalized();
		_dir += -camXform.basis.z * inputMovementVector.y;
		_dir += camXform.basis.x * inputMovementVector.x;
		
		if (IsOnFloor())
			if (Input.IsActionJustPressed("movement_jump"))
				_vel.y = JumpSpeed;
		
		if (!Input.IsActionJustPressed("ui_cancel")) return;
		Input.SetMouseMode(Input.GetMouseMode() == Input.MouseMode.Visible
			? Input.MouseMode.Captured
			: Input.MouseMode.Visible);
	}

	private void ProcessMovement(float delta)
	{
		_dir.y = 0;
		_dir = _dir.Normalized();

		_vel.y += delta * Gravity;

		var hvel = _vel;
		hvel.y = 0;

		var target = _dir;

		target *= MaxSpeed;

		var accel = _dir.Dot(hvel) > 0 ? Accel : Deaccel;

		hvel = hvel.LinearInterpolate(target, accel * delta);
		_vel.x = hvel.x;
		_vel.z = hvel.z;
		_vel = MoveAndSlide(_vel, new Vector3(0, 1, 0), false, 4, Mathf.Deg2Rad(MaxSlopeAngle));
	}

	public override void _Input(InputEvent @event)
	{
		if (!(@event is InputEventMouseMotion mouseEvent) || Input.GetMouseMode() != Input.MouseMode.Captured) return;
		
		_rotationHelper.RotateX(Mathf.Deg2Rad(-mouseEvent.Relative.y * MouseSensitivity));
		RotateY(Mathf.Deg2Rad(-mouseEvent.Relative.x * MouseSensitivity));
		var cameraRot = _rotationHelper.RotationDegrees;
		cameraRot.x = Mathf.Clamp(cameraRot.x, -89, 89);
		_rotationHelper.RotationDegrees = cameraRot;
	}
}
