extends Sprite2D

@export var power: int = 100



func _on_area_2d_area_entered(area: Area2D) -> void:
	var creature = area.get_parent()
	if creature.has_method("hill"):
		creature.hill(power)
		queue_free() 
