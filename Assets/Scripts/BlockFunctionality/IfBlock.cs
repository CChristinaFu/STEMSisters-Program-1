
public sealed class IfBlock : CBlock, IContainingParameter<BooleanBlock>, IOperatorBlockType
{
    public BooleanBlock Input1 { get; set; }
    override public void Operation(Interpreter operatingRobotBase)
    {
        if (Input1.GetBooleanValue(operatingRobotBase))
        {
            operatingRobotBase.PushToBlockCallStack(NextBlock);
        }
    }
}
