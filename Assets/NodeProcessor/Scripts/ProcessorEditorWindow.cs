using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

public class ProcessorEditorWindow : EditorWindow
{
    private BaseGraphView graphView;
    private string fileName = "file name";
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

        var fileNameTextField = new TextField("File Name");
        fileNameTextField.SetValueWithoutNotify(fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback(evt => fileName = evt.newValue);
        toolBar.Add(fileNameTextField);

        var loadButton = new ToolbarButton(clickEvent: () => { LoadData(); });
        loadButton.text = "Load Node";
        toolBar.Add(loadButton);

        var saveButton = new ToolbarButton(clickEvent: () => { SaveData(); });
        saveButton.text = "Save Node";
        toolBar.Add(saveButton);

        var nodeCreateButton = new ToolbarButton(clickEvent: () => { graphView.AddNode("Choose Node"); });
        nodeCreateButton.text = "Create Node";
        toolBar.Add(nodeCreateButton);
        
        rootVisualElement.Add(toolBar);
    }

    private void SaveData()
    {
        throw new NotImplementedException();
    }

    private void LoadData()
    {
        throw new NotImplementedException();
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(graphView);
    }
}