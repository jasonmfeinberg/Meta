using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	public CharClasses CharClass;
	
	public bool IsActive;
	
	public GameWorld gameWorld{
		get {return transform.parent.GetComponent<GridBlock>().gameWorld; }
		set { }
	}
	
	void Awake(){
		IsActive = false;
		CharClass = CharClasses.Swordsman;
	}
	
	public int Speed;
	
	public int Move;
	
	public void SetStats(int s, int m){
		Speed = s;
		Move = m;
	}
	
	public Vector3 Position{
		get{ return transform.parent.position; }
		set{
			transform.parent = gameWorld.gameGrid[value].transform;
			Vector3 val = value;
			val.y = val.y + 0.5F;
			transform.position = val;
		}
	}
}
