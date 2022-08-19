using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox.Editor;
using UnityEngine.Events;
using System.Linq;

public class Interpreter : MonoBehaviour
{
    [SerializeField] SerializedDictionary<string, BlockVariable> variables = new();
    [SerializeField] List<BlockGroup> blockGroups = new();
    public class UEvent_List_Int : UnityEvent<List<string>> { }
    public UEvent_List_Int OnVariableUpdate = new();

    public float WaitingTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var block in FindObjectsOfType<TopBlock>())
        {
            blockGroups.Add(block.Group);
        }
        OnVariableUpdate.Invoke(variables.Keys.ToList());
    }

    public bool Evaluate()
    {
        return false;
    }

    public void PushToBlockCallStack(FlowBlock block)
    {

    }

    public void SetCustomBlockParameterVariables(DefinitionCustomBlock definition, string varName, string report)
    {

    }

    public string GetCustomBlockParameterVariables(DefinitionCustomBlock definition, string varName)
    {
        return "";
    }

    public string GetInterpreterGlobalVariable(string varName)
    {
        return "";
    }



    public bool SetVariable(string varName, string varValue)
    {
        //variables.Add(varName,varValue);
        return true;
    }
    public bool GetVariable(string varName, out string varValue)
    {
        varValue = "";
        //return variables.TryGetValue(varName, out varValue);
        return false;

    }
    public bool Skip() => true;
    public bool IfElse(bool condition, BaseBlock trueBranch, BaseBlock falseBranch)
    {
        if (condition)
        {
            return trueBranch.RunBlock();
        }
        else
        {
            return falseBranch.RunBlock();
        }
        return false;
    }

    public bool RepeatNTimes(int repeatN, BaseBlock loopCode)
    {
        bool LoopValue = true;
        for (int i = 0; i < repeatN; i++)
        {
            LoopValue &= loopCode.RunBlock();
        }
        return LoopValue;
    }

    public bool RepeatUntilLoop(bool condition, BaseBlock loopCode)
    {
        if (condition == true)
        {
            return true;
        }
        for (int i = System.Int32.MinValue; i < System.Int32.MaxValue; i++)
        {
            if (loopCode.RunBlock())
            {
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]

public class BlockGroup
{
    public List<BlockGroup> ListOfBlocks = null;
    public BlockInformation BlockInfo;

}

[System.Serializable]
public class BlockVariable
{
    public List<int> VariableData = new();
    public bool IsList = false;
}
