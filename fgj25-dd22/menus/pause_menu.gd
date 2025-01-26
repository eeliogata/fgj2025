extends Control

@onready var Pausemenu = $"."

func  _ready() -> void:
	Pausemenu.visible = false
	get_tree().paused = false

func resume():
	Pausemenu.visible = false
	get_tree().paused = false

func pause():
	Pausemenu.visible = true
	get_tree().paused = true
	
func testEsc():
	if Input.is_action_just_pressed("resume_pause") and get_tree().paused == false:
		pause()
	elif Input.is_action_just_pressed("resume_pause") and get_tree().paused == true:
		resume()

func _on_resume_button_pressed() -> void:
	resume()

func _on_restart_button_pressed() -> void:
	get_tree().reload_current_scene()

func _on_menu_button_pressed() -> void:
	get_tree().change_scene_to_file("res://menus/MainMenu.tscn")

func _process(delta):
	testEsc()
