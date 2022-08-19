
[BlockDefinitionAttribute(BlockDefinitionAttribute.BlockDefinitionType.ReporterBlockInput, "x", BlockDefinitionAttribute.BlockDefinitionType.ReporterBlockInput)]
public sealed class MultiplyBlock : ArithmeticBlock
{
    sealed public override string GetReporterStringValue(Interpreter interpreter)
    {
        return (base.Input1.GetReporterNumberValue(interpreter) * base.Input2.GetReporterNumberValue(interpreter)).ToString();
    }
}
