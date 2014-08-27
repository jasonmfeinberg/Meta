using System;
using System.Collections.Generic;
using UnityEngine;
public sealed class TurnQueue
{

		public class QueueElement : IComparable<QueueElement>{
		
			public readonly Character character;
			
			public int rate;
			
			public int accum;
			
			public int CompareTo(QueueElement other){
				if (accum > other.accum){
					return -1;
				}
				else if (accum < other.accum){
					return 1;
				}
				else{
					return 0;
				}
			}
			
			public QueueElement(Character c){
				character = c;
				rate = character.Speed;
				accum = 0;
			}
			
			public QueueElement(Character c, int r){
				character = c;
				rate = r;
				accum = 0;
			}
			
			public QueueElement(Character c, int r, int a){
				character = c;
				rate = r;
				accum = 0;
			}
			
			//Runs through a rate increase
			public void UpdateAccum(){
				accum += rate;
			}
			
		}
		
		private List<QueueElement> mainList;
		
		public TurnQueue (){
			mainList = new List<QueueElement>();
		}
		
		public void Add(QueueElement ele){
			mainList.Add(ele);
			mainList.Sort();
		}
		
		public void Add(Character c){
			QueueElement ele = new QueueElement(c);
			Add(ele);
		}
		
		public void Add(Character c, int r){
			QueueElement ele = new QueueElement(c,r);
			Add(ele);
		}
		
		private Character NextChar(){
			QueueElement ele = mainList[0];
			mainList.RemoveAt(0);
			ele.accum = 0;
			mainList.Add (ele);
			return ele.character;
		}
		
		private void UpdateAllAccum(){
			//We don't run this when accum is full for one 
			if(mainList[0].accum <= 100){
				foreach (QueueElement ele in mainList){
					ele.UpdateAccum();
				}
				mainList.Sort ();
			}
		}
		
		public Character Cycle(){
			while(!Ready()){
				Debug.Log ("Incrementing Accums");
				UpdateAllAccum();
			}
			return NextChar();
		}
		
		public bool Ready(){
			return mainList[0].accum >= 100;
		}
		
		public List<String> DisplayList(){
			//NYI
			List<String> display = new List<String>();
			display.Add("NYI");
			return display;
		}
}

