class_name Player extends Sprite2D

var walking_speed : int = 150
const COOLDOWN_IT_SECONDS : float = 0.2 
const BULLET =preload("uid://drbxs0hj0qv0o")
@export var parent_scene : NodePath

# Called when the node enters the scene tree for the first time.
func _ready():
	pass


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if Input.is_action_pressed("move_up"):
		position.y -= walking_speed * delta
	if Input.is_action_pressed("move_down"):
		position.y += walking_speed * delta
	if Input.is_action_pressed("move_right"):
		position.x += walking_speed * delta
	if Input.is_action_pressed("move_left"):
		position.x -= walking_speed * delta
	look_at(get_global_mouse_position())

func _input(event: InputEvent) -> void:
	if event is InputEventKey and event.is_pressed():
		if event.keycode == KEY_ESCAPE:
			print("Leaving game...")
			get_tree().quit()
		if event.is_action("shoot"):
			shoot_bullet()
  
  
func shoot_bullet() -> void:
	var bullet_instance : Bullet = BULLET.instantiate()
	bullet_instance.rotation = rotation
	add_sibling(bullet_instance)
	bullet_instance.position = position
