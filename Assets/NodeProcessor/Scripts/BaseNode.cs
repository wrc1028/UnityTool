using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BaseNode : Node
{
    public string GUID;
    public string text;
    public bool entryPoint = false;
}
