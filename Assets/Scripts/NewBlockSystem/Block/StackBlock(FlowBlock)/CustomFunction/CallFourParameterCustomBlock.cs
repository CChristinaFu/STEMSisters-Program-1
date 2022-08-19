/// <summary>
/// Definaton Block Of Custom Block

/// </summary>
[System.Serializable]
public sealed class CallFourParameterCustomBlock : CallCustomBlock, IContainingParameter<ReporterBlock, ReporterBlock, ReporterBlock, ReporterBlock>
{

    private DefinitionFourParameterCustomBlock definitionFourParameterCustomBlock;

    public override DefinitionCustomBlock CustomBlockDefinitionBlock => definitionFourParameterCustomBlock;

    public CallFourParameterCustomBlock(DefinitionFourParameterCustomBlock definitionFourParameterCustomBlock)
    {
        this.definitionFourParameterCustomBlock = definitionFourParameterCustomBlock;
    }

    /// <summary>
    /// Passed Paramter 1
    /// </summary>
    public ReporterBlock Input1 { get; set; }
    /// <summary>
    /// Passed Paramter 2
    /// </summary>
    public ReporterBlock Input2 { get; set; }
    /// <summary>
    /// Passed Paramter 3
    /// </summary>
    public ReporterBlock Input3 { get; set; }
    /// <summary>
    /// Passed Paramter 4
    /// </summary>
    public ReporterBlock Input4 { get; set; }



    sealed protected override void PassParameterToOperatingInterpreter(Interpreter interpreter)
    {
        //Set Interpreter.CustomBlockLocalVariables with Input String Value 
        if (this.Input1 != null)
            interpreter.SetCustomBlockParameterVariables(this.CustomBlockDefinitionBlock, definitionFourParameterCustomBlock.Input1Name, this.Input1.GetReporterStringValue(interpreter));

        if (this.Input2 != null)
            interpreter.SetCustomBlockParameterVariables(this.CustomBlockDefinitionBlock, definitionFourParameterCustomBlock.Input2Name, this.Input2.GetReporterStringValue(interpreter));

        if (this.Input3 != null)
            interpreter.SetCustomBlockParameterVariables(this.CustomBlockDefinitionBlock, definitionFourParameterCustomBlock.Input3Name, this.Input3.GetReporterStringValue(interpreter));

        if (this.Input4 != null)
            interpreter.SetCustomBlockParameterVariables(this.CustomBlockDefinitionBlock, definitionFourParameterCustomBlock.Input4Name, this.Input4.GetReporterStringValue(interpreter));

    }


}
