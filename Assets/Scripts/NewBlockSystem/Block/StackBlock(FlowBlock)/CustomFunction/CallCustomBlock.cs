using UnityEngine;
/// <summary>
/// Fuction Created By Player
/// This can be used Event through InternetAntenna_SendCommandThroughInternet
/// CustomFunctionBlock Can Have Just ReporterBlock Type !!!
/// 
/// 
/// 
/// Please Set Next to Next Block Of CallCustomBlock
/// </summary>
[System.Serializable]
[NotAutomaticallyMadeOnBlockShopAttribute]
public abstract class CallCustomBlock : StackBlock, ICallCustomBlockType
{
    public abstract DefinitionCustomBlock CustomBlockDefinitionBlock { get; }


    sealed public override void Operation(Interpreter interpreter)
    {
        if (this.CustomBlockDefinitionBlock == null)
        {
            Debug.LogError("CustomBlockDefinitionBlock is null!!!!!!!!!!!!!");
            return;
        }



        //Pass Paramter To 
        this.PassParameterToOperatingInterpreter(interpreter);

        // Push NextBlock Of CallCustomBlock(Returned Block After End Subroutine(DefinitionCustomBlock) ) To Block Call Stack
        interpreter.PushToBlockCallStack(this.NextBlock);
    }

    /// <summary>
    /// Pass Parameter Of CallCustomBlock To Interpreter
    /// </summary>
    /// <param name="interpreter">Operating interpreter.</param>
    protected virtual void PassParameterToOperatingInterpreter(Interpreter interpreter)
    { }

    /// <summary>
    /// EndFlowBlock
    /// </summary>
    /// <param name="interpreter"></param>
    /// <returns>
    /// Next Block
    /// </returns>
    sealed public override FlowBlock EndFlowBlock(Interpreter interpreter)
    {
        return this.CustomBlockDefinitionBlock; // return CustomBlockDefinitionBlock as NextBlock
    }


}