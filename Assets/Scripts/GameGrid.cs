using UnityEngine;
using System.Collections;

public class GameGrid : Grid<GridBlock>{
	public override int GetDistance (Vector3 start, Vector3 end)
	{
		return base.GetDistance (start, end);
	}
}