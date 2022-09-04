using UnityEngine.Events;

public static class UtilityConstAndExt
{
    public const string CODE_EDITOR_TAG = "CodeEditor";

}
[System.Serializable]
public class UEvent_str : UnityEvent<string> { }
[System.Serializable]
public class UEvent_int : UnityEvent<int> { }