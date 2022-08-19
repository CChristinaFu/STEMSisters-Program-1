
[BlockDefinitionAttribute(BlockDefinitionAttribute.BlockDefinitionType.ReporterBlockInput, "<", BlockDefinitionAttribute.BlockDefinitionType.ReporterBlockInput)]
public sealed class LessBlock : BinaryComparisonTwoReporterBlock
{
    sealed public override bool GetBooleanValue(Interpreter interpreter)
    {
        return base.Input1.GetReporterNumberValue(interpreter) < base.Input2.GetReporterNumberValue(interpreter);
    }
}
