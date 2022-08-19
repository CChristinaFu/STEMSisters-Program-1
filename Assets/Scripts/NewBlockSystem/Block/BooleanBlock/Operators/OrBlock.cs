
[BlockDefinitionAttribute(BlockDefinitionAttribute.BlockDefinitionType.BooleanBlockInput, "or", BlockDefinitionAttribute.BlockDefinitionType.BooleanBlockInput)]
public sealed class OrBlock : BinaryComparisonTwoBooleanBlock
{
    sealed public override bool GetBooleanValue(Interpreter interpreter)
    {
        return base.Input1.GetBooleanValue(interpreter) || base.Input2.GetBooleanValue(interpreter);
    }
}
