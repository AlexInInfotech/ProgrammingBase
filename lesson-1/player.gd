class_name Player extends Sprite2D

@export var walking_speed : int = 150
const COOLDOWN_IT_SECONDS : float = 0.2 
const BULLET =preload("uid://drbxs0hj0qv0o")
@export var parent_scene : NodePath
@export var max_health: int = 500
var current_health: int 

# Called when the node enters the scene tree for the first time.
func _ready():
	current_health = max_health
	pass
func hill(amount: int) -> void:
	current_health += amount
	if current_health > max_health:
		current_health = max_health
func take_damage(amount: int) -> void:
	current_health -= amount
	if current_health <= 0:
		die()

func die() -> void:
	get_tree().quit()
	queue_free() 


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
	bullet_instance.setup_target_layers(2)
