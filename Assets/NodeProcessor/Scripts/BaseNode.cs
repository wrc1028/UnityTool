using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// ����һ�������ڵ�
public class BaseNode : Node
{
    // ÿ���ڵ㶼��һ����һ�޶���id
    public string GUID;
    public string text;
    public bool entryPoint = false;
}
