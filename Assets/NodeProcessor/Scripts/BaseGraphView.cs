using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;

public class BaseGraphView : GraphView
{
    private readonly Vector2 defaultNodeSize = new Vector2(100, 150);
    public BaseGraphView()
    {
        styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/NodeProcessor/Resources/ProcessorGraph.uss"));
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();

        // 在这个Graph面板中添加Node
        AddElement(GenerateEntryPointNode());
    }

    /// <summary>
    /// 得到当前窗口内所有的接口
    /// </summary>
    /// <param name="startPort">当前选中的节点</param>
    /// <param name="nodeAdapter"></param>
    /// <returns>可以连接的节点</returns>
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

    // 创建一个接口
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

        var button = new Button(clickEvent: () => { AddChoicePort(newNode); });
        button.text = "New Choice";
        newNode.titleContainer.Add(button);

        newNode.RefreshPorts();
        newNode.RefreshExpandedState();
        newNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));
        var outputPort = GeneratePort(newNode, Direction.Output, Port.Capacity.Multi);

        return newNode;
    }

    private void AddChoicePort(BaseNode node)
    {
        var addChoicePort = GeneratePort(node, Direction.Output);

        var outpurPortCount = node.outputContainer.Query("connector").ToList().Count;
        addChoicePort.portName = $"Choice {outpurPortCount}";

        node.outputContainer.Add(addChoicePort);

        node.RefreshPorts();
        node.RefreshExpandedState();
    }
}