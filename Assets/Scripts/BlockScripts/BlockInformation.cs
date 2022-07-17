using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockInformation", menuName = "BlockInformation")]
public class BlockInformation : ScriptableObject
{
    public string blockName;
    public BlockType blockType; 
    public Sprite blockSprite;
    public List<VariableInputType> inputList;
    public bool HasTopConnector;
    public bool HasMiddleConnector;
    public bool HasBottomConnector;

}
public enum BlockType{ACTION, OPERATOR, CONTROL, VARIABLE, FUNCTION, EVENTS}
// Action: Effect game state directly
//  * Example: Water field
//  * Example: Selling 
// (Boolean) Operator: Operate on some variable
//  * Pre-exisitng
//  * Exists
// Control: If and Loops
//  * 
// Function:
//  * Function Call
//  * Parameter Assignment
// Events: 
//  * START
//  * Every n sec
//  * def fn

public enum VariableInputType{DROPDOWN, TYPEABLE, PREDICATE}
public enum EventType{FROM_START, EVERY_N_SECONDS, DEFINE_FUNCTION}


