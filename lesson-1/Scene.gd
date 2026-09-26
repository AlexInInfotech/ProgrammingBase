extends Node2D

const PLAYER =preload("uid://bjge4487bg1t")
const ENEMY = preload("uid://7o47ludhfin5")
const ENEMY2 = preload("uid://bdjb3vgt4k727")
@export var delay: float = 2.5
var current_time: float
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	current_time = delay
	var player_instance : Player = PLAYER.instantiate()
	add_child(player_instance)
	player_instance.position = Vector2(600, 300)
	create_enemy1() 
	create_enemy2()
func _process(delta):
	current_time -= delta
	if current_time <=0:
		if randi_range(1, 2) == 1:
			create_enemy2()
		else:
			create_enemy1()
		current_time = delay
		
func create_enemy2() -> void:
	var enemy_instance2 : Enemy2 = ENEMY2.instantiate()
	add_child(enemy_instance2)
	enemy_instance2.position = Vector2(800, 100)
	
func create_enemy1() -> void:
	var enemy_instance : Enemy = ENEMY.instantiate()
	add_child(enemy_instance)
	enemy_instance.position = Vector2(100, 100)
	

		
