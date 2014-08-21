using UnityEngine;
using System.Collections;

public class PriorityQueue<T>{

	private ArrayList elements;

	private struct element{
		public int priority;
		public T obj;
	}

	public PriorityQueue (){
		elements = new ArrayList ();
	}

	public void add(T obj, int priority){
		element toAdd = new element ();
		toAdd.priority = priority;
		toAdd.obj = obj;
		elements.Insert (insertSpot (priority),toAdd);
	}

	private int insertSpot(int val){
		for (int i=0; i<elements.Count; i++) {
			element checkEle = (element)elements[i];
			if (val < checkEle.priority){
				return i;
			}
		}
		return elements.Count;
	}

	public T pop(){
		element topElement = (element)elements [0];
		T topObj = topElement.obj;
		elements.RemoveAt (0);
		return topObj;
	}

	public bool isEmpty(){
		return elements.Count == 0;
	}
}