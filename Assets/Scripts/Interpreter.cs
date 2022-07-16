using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter : MonoBehaviour
{   
    private Dictionary<string, string> variables = new();
    [SerializeField] List<BlockGroup> blockGroups = new();


    // Start is called before the first frame update
    void Start()
    {
        foreach (var block in FindObjectsOfType<TopBlock>()){
            blockGroups.Add(block.Group);
        }
    }
    public bool SetVariable(string varName, string varValue)
    {
        variables.Add(varName,varValue);
        return true;
    }
    public bool GetVariable(string varName, out string varValue)
    {
        return variables.TryGetValue(varName, out varValue);
        
    }
    public bool Skip() => true;
    public bool IfElse(bool condition, BaseBlock trueBranch, BaseBlock falseBranch)
    {
        if (condition){
            return trueBranch.RunBlock();
        }      
        else {
            return falseBranch.RunBlock();
        }
        return false;
    }

    public bool RepeatNTimes(int repeatN, BaseBlock loopCode)
    {
        bool LoopValue = true;
        for (int i = 0; i<repeatN; i++){
            LoopValue&= loopCode.RunBlock();
        }
        return LoopValue;
    }
    
    public bool RepeatUntilLoop(bool condition, BaseBlock loopCode)
    {
        if (condition == true){
            return true;
        }
        for (int i = System.Int32.MinValue; i<System.Int32.MaxValue; i++){
            if (loopCode.RunBlock()){
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]

public class BlockGroup 
{
    public List <BlockGroup> ListOfBlocks = null;
    public BlockInformation BlockInfo;
    
}
