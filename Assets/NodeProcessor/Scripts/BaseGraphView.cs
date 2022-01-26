using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;

public class BaseGraphView : GraphView
{
    private readonly Vector2 defaultNodeSize = new Vector2(100, 150);
    public BaseGraphView()
    {
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        // 在这个Graph面板中添加Node
        AddElement(GenerateEntryPointNode());
    }

    // 连接之间的节点
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();
        ports.ForEach(port => 
        {
            if (startPort != port && startPort.node != port.node)
                compatiblePorts.Add(port);
        });
        return compatiblePorts;
    }

    private Port GeneratePort(BaseNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {

        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
    }

    private BaseNode GenerateEntryPointNode()
    {
        // 初始化节点
        var node = new BaseNode()
        {
            title = "Start",
            GUID = Guid.NewGuid().ToString(),
            text = "First Node",
            entryPoint = true
        };
        // 创建接口
        Port generatePort = GeneratePort(node, Direction.Output);
        generatePort.portName = "Next";
        node.outputContainer.Add(generatePort);
        node.RefreshExpandedState();
        node.RefreshPorts();
        node.SetPosition(new Rect(100, 100, 100, 150));
        return node;
    }

    public void AddNode(string nodeName)
    {
        AddElement(CreateNode(nodeName));
    }
    private BaseNode CreateNode(string nodeName)
    {
        var newNode = new BaseNode
        {
            title = nodeName,
            text = nodeName,
            GUID = Guid.NewGuid().ToString(),
        };

        var inputPort = GeneratePort(newNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        newNode.outputContainer.Add(inputPort);
        newNode.RefreshExpandedState();
        newNode.RefreshPorts();
        newNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));
        var outputPort = GeneratePort(newNode, Direction.Output, Port.Capacity.Multi);

        return newNode;
    }
}