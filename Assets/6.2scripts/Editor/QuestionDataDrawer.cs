using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(Questions))]
[CanEditMultipleObjects]
[System.Serializable]

public class QuestionDataDrawer : Editor
{
   private Questions QuestionsInstance => target as Questions;
   private ReorderableList QuestionsList;

   private void OnEnable()
   {
    InitializeReorderableList(ref QuestionsList, "questionsList","Questions List");
   }
   
   public override void OnInspectorGUI()
   {
    serializedObject.Update();
    QuestionsList.DoLayoutList();
    serializedObject.ApplyModifiedProperties();
   }
   private void InitializeReorderableList(ref ReorderableList list,string propertyName,string listLabel)
   {
    list =new ReorderableList(serializedObject,serializedObject.FindProperty(propertyName),
    true,true,true,true);
    list.onAddCallback= reorderableList => QuestionsInstance.AddQuestion();
    list.drawHeaderCallback=(Rect rect)=>
    {
        EditorGUI.LabelField(rect,listLabel);
    };
    var l =list;
    list.drawElementCallback=(Rect rect,int index,bool isActive,bool isFocused)=>
    {
        var element =l.serializedProperty.GetArrayElementAtIndex(index);
        rect.y+=2;
        EditorGUI.PropertyField(
            new Rect(rect.x,rect.y,300,EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("question"),GUIContent.none);

            EditorGUI.PropertyField(
            new Rect(rect.x+310,rect.y,300,EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("isTrue"),GUIContent.none);
            
        
    };
   }
}
