class_name Enemy extends Sprite2D

@export var max_health: int = 400
var current_health: int
var player: Node2D = null
@export var speed: int = 45
@export var damage:int = 1000

func _ready() -> void:
	current_health = max_health
	var players = get_tree().get_nodes_in_group("player")
	if players.size() > 0:
		player = players[0]
		
func _process(delta: float) -> void:
	if player and is_instance_valid(player):
		var direction = (player.global_position - global_position).normalized()
		global_position += direction * speed * delta
		look_at(player.global_position)

func take_damage(amount: int) -> void:
	current_health -= amount
	if current_health <= 0:
		die()

func die() -> void:
	queue_free() 


func _on_area_2d_area_entered(area: Area2D) -> void:
	var enemy = area.get_parent()
	if enemy.has_method("take_damage"):
		enemy.take_damage(damage)
		queue_free()
