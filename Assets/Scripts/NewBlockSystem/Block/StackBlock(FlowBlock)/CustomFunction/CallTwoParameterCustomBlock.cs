/// <summary>
/// Definaton Block Of Custom Block

/// </summary>
[System.Serializable]
public sealed class CallTwoParameterCustomBlock : CallCustomBlock, IContainingParameter<ReporterBlock, ReporterBlock>
{
    private DefinitionTwoParameterCustomBlock definitionTwoParameterCustomBlock;
    public override DefinitionCustomBlock CustomBlockDefinitionBlock => definitionTwoParameterCustomBlock;

    public CallTwoParameterCustomBlock(DefinitionTwoParameterCustomBlock definitionTwoParameterCustomBlock)
    {
        this.definitionTwoParameterCustomBlock = definitionTwoParameterCustomBlock;
    }

    /// <summary>
    /// Passed Paramter 1
    /// </summary>
    public ReporterBlock Input1 { get; set; }
    /// <summary>
    /// Passed Paramter 2
    /// </summary>
    public ReporterBlock Input2 { get; set; }

    sealed protected override void PassParameterToOperatingInterpreter(Interpreter interpreter)
    {
        //Set Interpreter.CustomBlockLocalVariables with Input String Value 
        if (this.Input1 != null)
            interpreter.SetCustomBlockParameterVariables(this.CustomBlockDefinitionBlock, definitionTwoParameterCustomBlock.Input1Name, this.Input1.GetReporterStringValue(interpreter));

        if (this.Input2 != null)
            interpreter.SetCustomBlockParameterVariables(this.CustomBlockDefinitionBlock, definitionTwoParameterCustomBlock.Input2Name, this.Input2.GetReporterStringValue(interpreter));

    }


}
