
[BlockDefinitionAttribute(BlockDefinitionAttribute.BlockDefinitionType.BooleanBlockInput, "and", BlockDefinitionAttribute.BlockDefinitionType.BooleanBlockInput)]
public sealed class AndBlock : BinaryComparisonTwoBooleanBlock
{
    sealed public override bool GetBooleanValue(Interpreter interpreter)
    {
        return base.Input1.GetBooleanValue(interpreter) && base.Input2.GetBooleanValue(interpreter);
    }
}
