extends Node2D

const PLAYER =preload("uid://bjge4487bg1t")
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var player_instance : Player = PLAYER.instantiate()
	add_child(player_instance)
	player_instance.position = Vector2(600, 300)
