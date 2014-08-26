using UnityEngine;
using System.Collections;

public enum GridBlockMoveState{
	Movable, //Fully active for units to land on
	Passable, //Units can pass across it, but can't end on it
	Impassable //Blocks a pathway
}

