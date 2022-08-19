/// <summary>
/// This works like Memory_GetValue
/// But Memory_GetValue doesn't exist. This replace that
/// </summary>
[System.Serializable]
[NotAutomaticallyMadeOnBlockShop]
[BlockDefinitionAttribute(BlockDefinitionAttribute.BlockDefinitionType.GlobalVariableSelector)]
public sealed class GetVariableValueBlock : ReporterBlock, IContainingParameter<ReporterBlock>, IVariableBlockType
{
    public ReporterBlock Input1 { get; set; }

    sealed public override string GetReporterStringValue(Interpreter interpreter)
    {//Think How To Interpreter.StoredVariableBlock
        if (interpreter != null)
        {
            return interpreter.GetInterpreterGlobalVariable(Input1.GetReporterStringValue(interpreter));
        }
        else
        {
            return System.String.Empty;
        }
    }
}
