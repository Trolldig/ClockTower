﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
	public CharacterController player_controller;
	public float run_speed;
	public float jump_height;
	public float fall_speed;
	public float terminal_vel;
	public float air_drift;
	public Vector2 walljump_dist;
	public float walljump_dir;
	public int hover_duration;
	private int hover_delta;
	public Vector2 to_move;

	public enum F_STATE{
		GROUNDED,
		RISING,
		FALLING,
		IN_GETUP,
		HOVERING,
		WALLJUMP
	}
	public int fall_state;
	public enum DIRECTION{
		RIGHT = 1,
		LEFT = -1,
		STILL = 0
	}
	public int dir;

	void Start () {
		player_controller = GetComponent<CharacterController>();
		fall_state = (int)F_STATE.GROUNDED;
		
	}
	
	void FixedUpdate () {
		if(transform.position.z != 0){transform.position = new Vector3(transform.position.x, transform.position.y, 0);}

		if(Input.GetAxisRaw("Horizontal") > 0){
			dir = (int)DIRECTION.RIGHT;
		}else if (Input.GetAxisRaw("Horizontal") < 0){
			dir = (int)DIRECTION.LEFT;
		}else{dir = (int)DIRECTION.STILL;}
		
		switch(fall_state){

			case (int)F_STATE.GROUNDED:
				hover_delta = hover_duration;
				to_move = walk_func();
				if(Input.GetButton("Jump")){
					to_move = jump_func();
					fall_state = (int)F_STATE.RISING;
				}
				if(!player_controller.isGrounded){fall_state = (int)F_STATE.FALLING;}
				if(Input.GetButtonDown("Fire3")){fall_state = (int)F_STATE.HOVERING;}
			break;

			case (int)F_STATE.RISING:

				to_move = air_func(to_move);
				if(to_move.y < 0){
					fall_state = (int)F_STATE.FALLING;
				}
				if(Input.GetButtonDown("Fire3")){fall_state = (int)F_STATE.HOVERING;}
			break;

			case (int)F_STATE.FALLING:

				to_move = air_func(to_move);
				if(player_controller.isGrounded){
					fall_state = (int)F_STATE.GROUNDED;
				}
				if(Input.GetButtonDown("Fire3")){fall_state = (int)F_STATE.HOVERING;}
			break;

			case (int)F_STATE.WALLJUMP:
				if(player_controller.isGrounded){
					fall_state = (int)F_STATE.GROUNDED;
				}
				to_move = new Vector2(0,0);
				if(Input.GetButtonDown("Jump")){
					hover_delta = hover_duration;
					to_move = new Vector2(walljump_dist.x * walljump_dir, walljump_dist.y);
					fall_state = (int)F_STATE.RISING;
				}
			break;

			case (int)F_STATE.HOVERING:

			if(Input.GetButton("Fire3") && (hover_delta > 1)){
				hover_delta--;
				to_move = new Vector2(run_speed * dir, run_speed * Input.GetAxisRaw("Vertical"));
			}else {
				if(player_controller.isGrounded){
					fall_state = (int)F_STATE.GROUNDED;
				}else {fall_state = (int)F_STATE.FALLING;}
			}
			break;

			default:
				fall_state = (int)F_STATE.GROUNDED;
			break;

		}

		player_controller.Move(new Vector3(to_move.x, to_move.y, 0));
	}

	private Vector2 walk_func(){
		float x,y;
		x = run_speed * dir;
		y = -0.1f;
		return new Vector2(x,y);
	}
	private Vector2 jump_func(){
		float x,y;
		y = jump_height;
		x = run_speed * dir;
		return new Vector2(x,y);
	}
	private Vector2 air_func(Vector2 Mover){
		if(Mathf.Abs(Mover.y) < terminal_vel){
			if(!Input.GetButton("Jump")){
				Mover.y -= fall_speed * 2;
			}else{
				Mover.y-= fall_speed;
			}
		}
		if(Mathf.Abs(Mover.x) < run_speed){Mover.x += air_drift * dir;}
		if( dir == (int)DIRECTION.STILL){
			Mover.x += air_drift * dir;
			if(Mathf.Abs(Mover.x) < run_speed /4 && Mover.x > 0){Mover.x =run_speed/4 ;}
			else if(Mathf.Abs(Mover.x)< run_speed/4 && Mover.x < 0){Mover.x = -run_speed/4;}
		}

		return Mover;
	}
	void OnControllerColliderHit(ControllerColliderHit hit){
		if((player_controller.collisionFlags == CollisionFlags.Sides) && ( fall_state == (int)F_STATE.FALLING)){
			walljump_dir = hit.normal.x;
			fall_state = (int)F_STATE.WALLJUMP;
		}
	}

}