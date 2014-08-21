using UnityEngine;
using System.Collections;

public class Grid<T>{

	private T[,,] arr;	
	
	public int XDim{
		get{ return arr.GetLength(0); }
	}
	public int YDim{
		get{ return arr.GetLength (1); }
	}
	public int ZDim{
		get{ return arr.GetLength (2); }
	}
	
	public T this[int i, int j, int k]{
		get{
			return arr[i,j,k];
		}
		set{
			arr[i,j,k] = value;
		}
	}
	
	public T this[Vector3 vector]{
		get{
			return arr[(int)vector.x,(int)vector.y,(int)vector.z];
		}
		set{
			arr[(int)vector.x,(int)vector.y,(int)vector.z] = value;
		}
	}
	
	public Grid(int x, int y, int z){
		arr = new T[XDim,YDim,ZDim];
	}
	
	public Grid(T[,,] inputArr){
		arr = (T[,,])inputArr.Clone ();
	}
	
	public static explicit operator T[,,](Grid<T> g){
		return g.arr;
	}
	
	public virtual int GetDistance(Vector3 start, Vector3 end){
		return (int)(start.x+start.y+start.z-end.x-end.y-end.z);
	}
}
