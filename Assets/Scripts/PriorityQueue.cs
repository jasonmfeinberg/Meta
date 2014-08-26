using UnityEngine;
using System.Collections.Generic;

public class PriorityQueue<T>{

	private List<Element> elements;

	private struct Element{
		public int priority;
		public T obj;
	}

	public PriorityQueue (){
		elements = new List<Element> ();
	}

	public void add(T obj, int priority){
		Element toAdd = new Element ();
		toAdd.priority = priority;
		toAdd.obj = obj;
		elements.Insert (insertSpot (priority),toAdd);
	}

	private int insertSpot(int val){
		for (int i=0; i<elements.Count; i++) {
			Element checkEle = elements[i];
			if (val < checkEle.priority){
				return i;
			}
		}
		return elements.Count;
	}

	public T pop(){
		Element topElement = elements [0];
		T topObj = topElement.obj;
		elements.RemoveAt (0);
		return topObj;
	}

	public bool isEmpty(){
		return elements.Count == 0;
	}
	
	public T top(){
		Element topElement = elements [0];
		T topObj = topElement.obj;
		return topObj;
	}
}