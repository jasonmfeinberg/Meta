using System;

//Two element tuple class. NOTE: Struct inheritance
public struct Tuple<T1,T2>{
	public T1 First;
	public T2 Second;
	
	public Tuple(T1 first, T2 second){
		First = first;
		Second = second;
	}
	
	public override string ToString ()
	{
		return string.Format ("<"+First+","+Second+">");
	}
}

