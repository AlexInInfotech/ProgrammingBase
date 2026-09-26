
class_name Bullet extends Sprite2D

@export var speed_of_flight : int = 500
var inhereted_rotation: Vector2
@export var damage: int = 50
@onready var bullet_area: Area2D = $Area2D
# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	position += Vector2.RIGHT.rotated(rotation) * speed_of_flight * delta

func setup_target_layers(layer: int) -> void:
	# Проверяем, успел ли узел Area2D загрузиться в память
	if not is_node_ready():
		await ready
	bullet_area.collision_mask = 0
	bullet_area.set_collision_mask_value(layer, true)


func _on_area_2d_area_entered(area: Area2D) -> void:
	var enemy = area.get_parent()
	if enemy.has_method("take_damage"):
		enemy.take_damage(damage)
		
	queue_free()
