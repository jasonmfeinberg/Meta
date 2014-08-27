using UnityEngine;
using System;

public class GridBlock : MonoBehaviour {
	protected GridBlockMoveState baseMoveState;
	
	public GridBlockMoveState MoveState{
		get { 
			if (baseMoveState != GridBlockMoveState.Movable || !Occupied){
				return baseMoveState;
			}
			else{
				if(Occupant.Team == gameWorld.Active.Team){
					return GridBlockMoveState.Passable;
				}
				else{
					return GridBlockMoveState.Impassable;
				}
			}
		}
		set { }
	}
	
	public GameWorld gameWorld{
		get{return transform.parent.GetComponent<GameWorld>();}
		set{ }
	}
	
	public GameGrid gameGrid{
		get{return gameWorld.gameGrid;}
		set{ }
	}
	
	public Vector3 Position;
	
	private bool Clickable = false;
	
	public Character Occupant{
		get {
			if (transform.childCount == 0){
				return null;
			}
			else if (transform.childCount == 1){
				return transform.GetChild(0).GetComponent<Character>();
			}
			else{
				throw new MultipleOccupantsException();
			}
		}
		set { 
			if(transform.childCount == 0){
				value.transform.parent = transform;
			}
			else{
				throw new AlreadyOccupiedException();
			}
		}
	}
	
	public bool Occupied{
		get{return (transform.childCount == 1);}
		set{ }
	}
	
	public void ActivateClickable(){
		Clickable = true;
		renderer.material.color = Color.green;
	}
	
	public void DeactivateClickable(){
		Clickable = false;
		renderer.material.color = Color.white;
	}
	
	void Awake(){
		SetMovable();
	}
	
	void SetMovable(){
		baseMoveState = GridBlockMoveState.Movable;
	}
	
	void SetPassable(){
		baseMoveState = GridBlockMoveState.Passable;
	}
	
	void SetImpassable(){
		baseMoveState = GridBlockMoveState.Impassable;
	}
	
	//Below are mostly placeholder exceptions
	public class MultipleOccupantsException : InvalidOperationException
	{
		public MultipleOccupantsException(){
			Debug.Log ("MULTIPLE OCCUPANTS EXCEPTION RAISED");		
		}
	}
	
	public class AlreadyOccupiedException : InvalidOperationException
	{
		public AlreadyOccupiedException(){
			Debug.Log ("ATTEMPTED TO OCCUPY OCCUPIED SPOT");		
		}
	}
	
	void OnMouseDown(){
		if (Clickable && !Occupied){
			gameWorld.ExecuteMove(this);
		}
	}
	
}