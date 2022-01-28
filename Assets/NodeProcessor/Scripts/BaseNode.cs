using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// 这是一个基础节点
public class BaseNode : Node
{
    // 每个节点都有一个独一无二的id
    public string GUID;
    public string text;
    public bool entryPoint = false;
}
