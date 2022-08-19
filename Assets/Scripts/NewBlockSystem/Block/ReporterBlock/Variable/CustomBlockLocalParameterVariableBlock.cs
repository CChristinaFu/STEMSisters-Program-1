/// <summary>
/// This works like Memory_GetValue
/// But Memory_GetValue doesn't exist. This replace that
/// </summary>
[System.Serializable]
[NotAutomaticallyMadeOnBlockShop]
public sealed class CustomBlockLocalParameterVariableBlock : ReporterBlock
{
    public DefinitionCustomBlock DefinitionCustomBlock;
    /// <summary>
    /// VariableValue는 각 로봇마다 다른 값을 가질 수 있다.
    /// Sync this value to Key of Interpreter.MemoryVariable Dictionary
    /// </summary>
    public string LocalVariableName;

    public CustomBlockLocalParameterVariableBlock(DefinitionCustomBlock definitionCustomBlock, string localVariableName)
    {
        this.DefinitionCustomBlock = definitionCustomBlock;
        this.LocalVariableName = localVariableName;
    }

    sealed public override string GetReporterStringValue(Interpreter interpreter)
    {//Think How To Interpreter.StoredVariableBlock
        if (interpreter != null)
        {
            return interpreter.GetCustomBlockParameterVariables(DefinitionCustomBlock, this.LocalVariableName);
        }
        else
        {
            return System.String.Empty;
        }
    }

    public override Block Clone()
    {
        var block = (CustomBlockLocalParameterVariableBlock)base.Clone();
        block.LocalVariableName = this.LocalVariableName;

        return block;
    }

}
