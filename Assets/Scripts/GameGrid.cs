using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class GameGrid : Grid<GridBlock>{
	
	public GameGrid(int x, int y, int z) : base(x,y,z){
		arr = new GridBlock[x,y,z];
	}
	
	public GameGrid(GridBlock[,,] inputArr) : base(inputArr){
		arr = (GridBlock[,,])inputArr.Clone ();
	}
	
	public GameGrid(GridBlock[] blocks){
		//Initialize the world with a bunch of blocks only
		int XDim = 0, YDim = 0, ZDim = 0; //They can't be less than 1...
		string pattern = @"Block\(([0-9]+),([0-9]+),([0-9]+)\)";
		Regex r = new Regex(pattern);
		int testX, testY, testZ;
		List<Tuple<GridBlock,Vector3>> tuples = new List<Tuple<GridBlock, Vector3>>();
		string name;
		Tuple<GridBlock, Vector3> addTuple;
		foreach(GridBlock block in blocks){
			name = block.transform.name;
			Match m = r.Match(block.transform.name);
			testX = Convert.ToInt32(m.Groups[1].Captures[0].ToString());
			testY = Convert.ToInt32(m.Groups[2].Captures[0].ToString());
			testZ = Convert.ToInt32(m.Groups[3].Captures[0].ToString());
			if(testX > XDim){
				XDim = testX;
			}
			if(testY > YDim){
				YDim = testY;
			}
			if(testZ > ZDim){
				ZDim = testZ;
			}
			//Debug.Log (new Vector3((float) testX, (float) testY, (float) testZ));
			Vector3 placement = new Vector3((float) testX, (float) testY, (float) testZ);
			block.Position = placement;
			addTuple = new Tuple<GridBlock, Vector3>( block, placement);
			//Debug.Log (addTuple);
			tuples.Add (addTuple);
			
		}
		//Debug.Log (XDim);
		//Debug.Log (YDim);
		//Debug.Log (ZDim);
		arr = new GridBlock[XDim+1,YDim+1,ZDim+1];
		
		//Debug.Log ("Arr XDim = " + arr.GetLength(0));
		//Debug.Log ("Arr YDim = " + arr.GetLength(1));
		//Debug.Log ("Arr ZDim = " + arr.GetLength(2));
		
		foreach(Tuple<GridBlock, Vector3> tuple in tuples){
			//Debug.Log (tuple);
			this[tuple.Second] = tuple.First;
		}
		//Debug.Log (arr[2,1,2]);
		//Debug.Log (arr[0,0,0]);
		Debug.Log ("Successfully built GridWorld");
	}
	
	public override int GetDistance (Vector3 start, Vector3 end)
	{
		/*float rand = UnityEngine.Random.Range(0F,1F);
		Debug.Log("" + rand + start);
		Debug.Log("" + rand + end);*/
		int[,,] distArr = new int[XDim,YDim,ZDim];
		for (int x = 0; x < XDim; x++){
			for(int y = 0; y < YDim; y++){
				for(int z = 0; z < ZDim; z++){
					if( start == new Vector3((float)x,(float)y,(float)z)){
						distArr[x,y,z] = 0;
					}
					else{
						distArr[x,y,z] = -1;
					}
				}
			}
		}
		int endX = (int)end.x, endY = (int)end.y, endZ = (int)end.z;
		PriorityQueue<Vector3> pq = new PriorityQueue<Vector3>();
		//Add the starting vector for the loop to run through
		pq.add (start, 1);
		while (!pq.isEmpty()) {
			Vector3 nextLoc = pq.pop ();
			int currX = (int)nextLoc.x, currY = (int)nextLoc.y, currZ = (int)nextLoc.z;
			if (nextLoc == end){
				return distArr[currX,currY,currZ];
			}
			if(currX < (XDim - 1) && arr[currX+1,currY,currZ].MoveState != GridBlockMoveState.Impassable && distArr[currX+1,currY,currZ]==-1){
				pq.add(new Vector3(currX+1,currY,currZ), (distArr[currX,currY,currZ]+currX+1-endX+currY-endY+currZ-endZ));
				distArr[currX+1,currY,currZ] = distArr[currX,currY,currZ]+1;
			}
			if (currX > 0 && arr[currX-1,currY,currZ].MoveState != GridBlockMoveState.Impassable && distArr[currX-1,currY,currZ]==-1){
				pq.add(new Vector3(currX-1,currY,currZ), (distArr[currX,currY,currZ]+currX-1-endX+currY-endY+currZ-endZ));
				distArr[currX-1,currY,currZ] = distArr[currX,currY,currZ]+1;
			}
			if(currY < (YDim - 1) && arr[currX,currY+1,currZ].MoveState != GridBlockMoveState.Impassable && distArr[currX,currY+1,currZ]==-1){
				pq.add(new Vector3(currX,currY+1,currZ), (distArr[currX,currY,currZ]+currX-endX+currY+1-endY+currZ-endZ));
				distArr[currX,currY+1,currZ] = distArr[currX,currY,currZ]+1;
			}
			if(currY > 0 && arr[currX,currY-1,currZ].MoveState != GridBlockMoveState.Impassable && distArr[currX,currY-1,currZ]==-1){
				pq.add(new Vector3(currX,currY-1,currZ), (distArr[currX,currY,currZ]+currX-endX+currY-1-endY+currZ-endZ));
				distArr[currX,currY-1,currZ] = distArr[currX,currY,currZ]+1;
			}
			if(currZ < (ZDim - 1) && arr[currX,currY,currZ+1].MoveState != GridBlockMoveState.Impassable && distArr[currX,currY,currZ+1]==-1){
				pq.add(new Vector3(currX,currY,currZ+1), (distArr[currX,currY,currZ]+currX-endX+currY-endY+currZ+1-endZ));
				distArr[currX,currY,currZ+1] = distArr[currX,currY,currZ]+1;
			}
			if(currZ > 0 && arr[currX,currY,currZ-1].MoveState != GridBlockMoveState.Impassable && distArr[currX,currY,currZ-1]==-1){
				pq.add(new Vector3(currX,currY,currZ-1), (distArr[currX,currY,currZ]+currX-endX+currY-endY+currZ-1-endZ));
				distArr[currX,currY,currZ-1] = distArr[currX,currY,currZ]+1;
			}
		}
		Debug.Log ("Uh oh");
		//We work in connected graphs, so there are no sinks / ability for this to come back as -1 for now. The other return statement is the proper one
		return -1;
	}
}