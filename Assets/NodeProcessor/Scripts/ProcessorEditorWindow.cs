using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class ProcessorEditorWindow : EditorWindow
{
    private BaseGraphView graphView;
    [MenuItem("UnityTool/Processor")]
    private static void ShowWindow()
    {
        var window = GetWindow<ProcessorEditorWindow>();
        window.titleContent = new GUIContent("Processor");
        window.Show();
    }

    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolBar();
    }

    private void ConstructGraphView()
    {
        graphView = new BaseGraphView()
        {
            name = "Processor"
        };
        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);
    }

    private void GenerateToolBar()
    {
        var toolBar = new Toolbar();

        var nodeCreateButton = new ToolbarButton(clickEvent: () => { graphView.AddNode("Choose Node"); });
        nodeCreateButton.text = "Create Node";
        
        toolBar.Add(nodeCreateButton);
        rootVisualElement.Add(toolBar);
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(graphView);
    }
}