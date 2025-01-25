extends Control


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

#This playbutton
func _on_start_pressed() -> void:
	get_tree().change_scene_to_file("res://scenes/TestLevel.tscn")

#This is settings
func _on_settings_pressed() -> void:
	print("Settings pressed")

#This quit
func _on_quit_pressed() -> void:
	get_tree().quit()
