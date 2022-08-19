﻿using Newtonsoft.Json;
/// <summary>
/// Event Block
/// This is HatBlock
[NotAutomaticallyMadeOnBlockShop]
[BlockDefinitionAttribute("Init Hat Block")]
[System.Serializable]
public sealed class InitHatBlock : HatBlock
{

    sealed public override void Operation(Interpreter interpreter)
    {
        //DO NOTHING, JUST CALL NEXT BLOCK
    }
}
