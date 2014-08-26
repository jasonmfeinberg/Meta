using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid<T> : IEnumerable<T>{

	protected T[,,] arr;	
	
	public int XDim{
		get{ return arr.GetLength(0); }
	}
	public int YDim{
		get{ return arr.GetLength (1); }
	}
	public int ZDim{
		get{ return arr.GetLength (2); }
	}
	
	public T this[Vector3 vector]{
		get{
			return arr[(int)vector.x,(int)vector.y,(int)vector.z];
		}
		set{
			arr[(int)vector.x,(int)vector.y,(int)vector.z] = value;
		}
	}
	
	public T this[int i, int j, int k]{
		get{
			return arr[i,j,k];
		}
		set{
			arr[i,j,k] = value;
		}
	}
	/*
	public T Get(Vector3 vector){
		return arr[(int)vector.x,(int)vector.y,(int)vector.z];
	} 
	
	public void Set(Vector3 vector, T value){
		arr[(int)vector.x,(int)vector.y,(int)vector.z] = value;
	}
	*/
	public Grid(int x, int y, int z){
		arr = new T[XDim,YDim,ZDim];
	}
	
	public Grid(T[,,] inputArr){
		arr = (T[,,])inputArr.Clone ();
	}
	
	//This constructor should nver be used. I should probably implement something here for safety reasons. The reason that this exists is due to the way inheritance works
	public Grid(){}
	
	public static explicit operator T[,,](Grid<T> g){
		return g.arr;
	}
	
	public virtual int GetDistance(Vector3 start, Vector3 end){
		return (int)(start.x+start.y+start.z-end.x-end.y-end.z);
	}
	
	public IEnumerator<T> GetEnumerator() {
		foreach( T item in arr ) {
			yield return item;
		}
	}
	
	IEnumerator IEnumerable.GetEnumerator() {
		return GetEnumerator();
	}
	
	protected static Vector3 NextX(Vector3 pos){
		pos.x += 1F;
		return pos;
	}
	
	protected static Vector3 PrevX(Vector3 pos){
		pos.x -= 1F;
		return pos;
	}
	
	protected static Vector3 NextY(Vector3 pos){
		pos.y += 1F;
		return pos;
	}
	
	protected static Vector3 PrevY(Vector3 pos){
		pos.y -= 1F;
		return pos;
	}
	
	protected static Vector3 NextZ(Vector3 pos){
		pos.z += 1F;
		return pos;
	}
	
	protected static Vector3 PrevZ(Vector3 pos){
		pos.z -= 1F;
		return pos;
	}
	
}
