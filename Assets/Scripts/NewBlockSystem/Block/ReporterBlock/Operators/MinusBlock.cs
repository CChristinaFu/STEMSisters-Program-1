
[BlockDefinitionAttribute(BlockDefinitionAttribute.BlockDefinitionType.ReporterBlockInput, "-", BlockDefinitionAttribute.BlockDefinitionType.ReporterBlockInput)]
public sealed class MinusBlock : ArithmeticBlock
{
    sealed public override string GetReporterStringValue(Interpreter interpreter)
    {
        return (base.Input1.GetReporterNumberValue(interpreter) - base.Input2.GetReporterNumberValue(interpreter)).ToString();
    }
}
