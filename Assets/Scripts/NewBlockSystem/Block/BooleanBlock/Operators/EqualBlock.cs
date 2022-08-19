using UnityEngine;
[BlockDefinitionAttribute(BlockDefinitionAttribute.BlockDefinitionType.ReporterBlockInput, "=", BlockDefinitionAttribute.BlockDefinitionType.ReporterBlockInput)]
public sealed class EqualBlock : BinaryComparisonTwoReporterBlock
{
    sealed public override bool GetBooleanValue(Interpreter interpreter)
    {
        return Mathf.Approximately(base.Input1.GetReporterNumberValue(interpreter), base.Input2.GetReporterNumberValue(interpreter));

    }
}
