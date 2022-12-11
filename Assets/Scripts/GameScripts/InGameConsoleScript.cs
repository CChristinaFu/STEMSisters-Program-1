using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameConsoleScript : MonoBehaviour
{
    [SerializeField] GameObject MessagePrefab;
    [SerializeField] Transform MessageParent;
    public static InGameConsoleScript mainConsole = null;
    // Start is called before the first frame update
    void Awake()
    {
        if (mainConsole == null)
        {
            mainConsole = this;
        }
    }

    // Update is called once per frame
    public static void LogMessage(string message)
    {
        if (mainConsole == null)
        {
            Debug.LogError(message);
        }
        else
        {
            var newMessage = Instantiate(mainConsole.MessagePrefab, mainConsole.MessageParent);
            if (newMessage.GetComponent<TMP_Text>() is TMP_Text messageText)
            {
                messageText.text = message;
            }
        }
    }
    public static void ClearMessages()
    {
        if (mainConsole != null)
        {
            List<GameObject> children = new();
            foreach (Transform c in mainConsole.MessageParent)
            {
                children.Add(c.gameObject);
            }
            children.ForEach(x => Destroy(x));

        }
    }
}
