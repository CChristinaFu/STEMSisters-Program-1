using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseBlock : MonoBehaviour
{
    [field: SerializeField]public string BlockName {get; private set;} = "unnamed block";
    virtual public bool RunBlock(){
        Debug.Log("running this block");
        return true;
    }

}
