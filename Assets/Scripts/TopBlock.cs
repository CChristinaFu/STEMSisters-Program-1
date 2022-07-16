using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBlock : MonoBehaviour
{
    public BlockGroup Group;
    
    // Start is called before the first frame update
    private void Awake()
    {
        Group.ListOfBlocks = new();
        foreach (var block in GetComponentsInChildren<BaseBlock>())        
        {
            Group.ListOfBlocks.Add(new());

        }
    }


  
}
