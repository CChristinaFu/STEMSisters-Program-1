
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SourceCodeEditorWindow))]
public class SourceCodeEditorWindowEditor : Editor
{
    private SourceCodeEditorWindow _SourceCodeEditorWindow;

    private void Awake()
    {
        _SourceCodeEditorWindow = target as SourceCodeEditorWindow;
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        GUILayout.Space(30);

        if (GUILayout.Button("Create RobotSourceCode"))
        {
            RobotSystem.instance.CreateRobotSourceCode(System.DateTime.Now.ToString());
        }

        if (GUILayout.Button("Set First Robot Sourcode"))
        {
            _SourceCodeEditorWindow._RobotSourceCode = RobotSystem.instance.RobotSourceCodeList[0];
        }

    }

}
#endif