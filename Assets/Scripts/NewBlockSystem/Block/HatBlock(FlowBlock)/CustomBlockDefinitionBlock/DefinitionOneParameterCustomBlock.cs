/// <summary>
/// Definaton Block Of Custom Block

/// </summary>
[System.Serializable]
public sealed class DefinitionOneParameterCustomBlock : DefinitionCustomBlock, IContainingParameter<LiteralReporterBlock>
{
    public DefinitionOneParameterCustomBlock(string customBlockName, string input1Name) : base(customBlockName)
    {
        this.Input1Name = input1Name;

        ParameterNames = new string[] { this.Input1Name };
    }


    public LiteralReporterBlock Input1 { get; set; }
    public string Input1Name;

    /// <summary>
    /// Parameter Of Custom Block is Just LiteralBlock
    /// Parameter Of Custom Block is set to passed String Value of ReporterBlock
    /// </summary>
    /// <param name="passedReporterBlock">Passed reporter block.</param>
    public void CopyParamter(Interpreter interpreter, ReporterBlock passedReporterBlock)
    {
        this.Input1 = new LiteralReporterBlock(passedReporterBlock.GetReporterStringValue(interpreter)); // 
    }

    public override void Operation(Interpreter interpreter)
    {
    }
}
