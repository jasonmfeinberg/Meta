using UnityEngine;
using System.Collections;

/* This class contains all logic general to the instance of the game world (i.e. map)
This includes things like the turn list, players, and it is the parent of every other object (including the camera and lighting)
*/
public class GameWorld : MonoBehaviour
{

	public GameGrid gameGrid; //This is the main GameGrid which is NOT a component
	
	public TurnQueue tq;

	public Character Active;

	void Awake (){
		tq = new TurnQueue();
	
		//For now, this will initialize the world. This logic currently creates blocks 
		//GameGrid contains the logic to properly build the grid from those blocks
		
		//This logic is ONLY temporary, not worth optimizing
		int XDim = 5, YDim = 2, ZDim = 5;
		for (int x = 0; x < XDim; x++){
			for (int y = 0; y < YDim; y++){
				for (int z = 0; z <ZDim; z++){
					CreateGridBlockGameObject(x,y,z);
				}
			}
		}
		gameGrid = new GameGrid(GetComponentsInChildren<GridBlock>());
		GameObject newChar = CreateCharacter(new Vector3(0F,0F,0F));
		Character ch1 = newChar.GetComponent<Character>();
		ch1.SetStats(10,3,0);
		tq.Add (ch1);
		GameObject newChar2 = CreateCharacter(new Vector3(2F,1F,2F));
		Character ch2 = newChar2.GetComponent<Character>();
		ch2.SetStats(15,2,1);
		tq.Add (ch2);
		//Camera Control Change goes here
	}
	
	void Start (){
		Active = tq.Cycle();
		ActivateMoveClicks();
	}
		
	private GameObject CreateGridBlockGameObject(int x, int y, int z){
		GameObject newBlock = GameObject.CreatePrimitive (PrimitiveType.Cube);
		newBlock.transform.localScale = new Vector3 (0.5f,0.5f,0.5f);
		newBlock.transform.parent = this.transform;
		newBlock.AddComponent ("GridBlock");
		GridBlock newBlockComp = newBlock.GetComponent<GridBlock>();
		newBlockComp.Position = new Vector3 (x, y, z);
		newBlock.transform.Translate (x,y,z);
		newBlock.name = "Block(" + x +"," + y + "," + z + ")";
		return newBlock;
	}
	
	private GameObject CreateCharacter(Vector3 loc){
		GameObject newChar = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		int x = (int)loc.x, y = (int)loc.y, z = (int)loc.z;
		newChar.AddComponent("Character");
		newChar.transform.Translate (loc.x, loc.y + 0.5f, loc.z);
		newChar.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		GridBlock gridSpot = gameGrid[x, y, z];
		Character ch = newChar.GetComponent<Character>();
		//Debug.Log (gameGrid[x,y,z]);
		//Debug.Log (x);
		//Debug.Log (y);
		//Debug.Log (z);
		newChar.transform.parent = gridSpot.gameObject.transform;
		return newChar;
	}
	
	private void ActivateMoveClicks(){
		int currMove = Active.Move;
		Vector3 start = Active.Position;
		foreach (GridBlock block in gameGrid){
			Vector3 end = block.Position;
			gameGrid.GetDistance(start,end);
			if (block.MoveState == GridBlockMoveState.Movable && gameGrid.GetDistance(start,end) <= currMove){
				block.ActivateClickable();
			}
		}
		
	}
	
	private void DeactivateMoveClicks(){
		foreach (GridBlock block in gameGrid){
			block.DeactivateClickable();
		}
	}
	
	public void ExecuteMove(GridBlock block){
		DeactivateMoveClicks();
		Active.Position = block.Position;
		Active = tq.Cycle();
		ActivateMoveClicks();
	}
}

