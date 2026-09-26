class_name Enemy2 extends Sprite2D

@export var max_health: int = 100
var current_health: int
var player: Node2D = null
@export var speed: int = 70
@export var delay: float = 1
var current_time: float
const BULLET =preload("uid://drbxs0hj0qv0o")

func _ready() -> void:
	current_time = delay
	current_health = max_health
	var players = get_tree().get_nodes_in_group("player")
	if players.size() > 0:
		player = players[0]
		
func _process(delta: float) -> void:
	if player and is_instance_valid(player):
		var direction = (player.global_position - global_position).normalized()
		global_position += direction * speed * delta
		look_at(player.global_position)
	current_time -= delta
	if current_time <= 0:
		var bullet_instance : Bullet = BULLET.instantiate()
		bullet_instance.rotation = rotation
		add_sibling(bullet_instance)
		bullet_instance.position = position
		bullet_instance.setup_target_layers(1)
		current_time = delay

func take_damage(amount: int) -> void:
	current_health -= amount
	if current_health <= 0:
		die()

func die() -> void:
	queue_free() 
