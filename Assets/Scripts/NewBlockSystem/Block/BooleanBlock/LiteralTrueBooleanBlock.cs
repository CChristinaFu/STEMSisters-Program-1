
[BlockDefinitionAttribute("True")]
public sealed class LiteralTrueBooleanBlock : BooleanBlock, IOperatorBlockType
{
    sealed public override bool GetBooleanValue(Interpreter interpreter)
    {
        return true;
    }


}
