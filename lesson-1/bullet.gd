class_name Bullet extends Sprite2D

var speed_of_flight : int = 500
var inhereted_rotation: Vector2

# Called when the node enters the scene tree for the first time.
func _ready():
  pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
  position += Vector2.RIGHT.rotated(rotation) * speed_of_flight * delta


func _on_area_2d_area_entered(area):
  queue_free()
