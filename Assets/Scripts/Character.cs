using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	public CharClasses CharClass;
	
	//This is to allow us to use the set accessor on Team. Within this code, this will be confusing. Open to suggestions!
	protected int team;
	
	public int Team{
		get {return team;}
		set { 
			team = value;
			renderer.material.color = value == 0 ? Color.blue : Color.red;
		}
	}	
	
	public GameWorld gameWorld{
		get {return transform.parent.GetComponent<GridBlock>().gameWorld; }
		set { }
	}
	
	void Awake(){
		CharClass = CharClasses.Swordsman;
	}
	
	public int Speed;
	
	public int Move;
	
	public void SetStats(int s, int m){
		Speed = s;
		Move = m;
	}
	
	public void SetStats(int s, int m, int t){
		SetStats(s,m);
		Team = t;
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
