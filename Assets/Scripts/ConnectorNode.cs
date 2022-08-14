using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorNode : MonoBehaviour
{

    [field: SerializeField]
    public Vector2 IndentNode { get; private set; }
    [field: SerializeField]
    public Vector2 OutdentNode { get; private set; }
    [SerializeField] float searchRadius = 1;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if (IndentNode != null)
        {
            Gizmos.DrawWireSphere(IndentNode, searchRadius);
        }
        if (OutdentNode != null)
        {
            Gizmos.DrawWireSphere(OutdentNode, searchRadius);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
