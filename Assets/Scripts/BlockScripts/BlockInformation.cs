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
public enum BlockType{ACTION, OPERATOR, CONTROL, VARIABLE, FUNCTION}
public enum VariableInputType{DROPDOWN, TYPEABLE, PREDICATE}

