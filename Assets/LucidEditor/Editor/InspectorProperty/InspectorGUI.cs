using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace AnnulusGames.LucidTools.Editor
{
    /// <summary>
    /// Wrapper around editorlayout and editorgui to deal with reorderable list incompatibility with two
    /// </summary>
    public static class InspectorGUI
    {
        public static bool IsGUIOnly = false;
        public static Rect CurrentRect;
        public static Vector2 cursor;
        public static float lineHeight = EditorGUIUtility.singleLineHeight;
        
        private static Stack<Rect> RectContexts = new Stack<Rect>();
        
        public static Rect BeginArea(Rect rect)
        {
            RectContexts.Push(CurrentRect);
            CurrentRect = rect;
            cursor=Vector2.zero;
            return rect;
        }

        public static Rect GetNextRect()
        {
            
        }
        
        public static Rect EndArea()
        {
            var drawnRect = CurrentRect;
            CurrentRect = RectContexts.Pop();
            cursor = new Vector2(drawnRect.x, drawnRect.y + drawnRect.height);
            return drawnRect;
        }

        public static void DisableEditorLayout() => IsGUIOnly = true;
        public static void EnableEditorLayout() => IsGUIOnly = false;

        public static Rect BeginHorizontal()
        {
            if (IsGUIOnly)
                GUILayout.BeginArea(GetNextLine());
            else
            {
                EditorGUILayout.BeginHorizontal();
            }
            
            //PLACEHOLDER!
            return CurrentRect;
        }

        public static Rect EndHorizontal()
        {
            if (IsGUIOnly)
                GUILayout.EndArea();
            else
            {
                EditorGUILayout.EndHorizontal();
            }
            
            //PLACEHOLDER!
            return CurrentRect;
        }
        
        public static Rect GetNextLine()
        {
            var next = new Rect(cursor.x, cursor.y, CurrentRect.width, lineHeight);
            cursor.y += lineHeight;
            return next;
        }

        public static void PropertyField(SerializedProperty prop)
        {
            EditorGUI.PropertyField(GetNextLine(), prop);
        }


        public static void PropertyField(SerializedProperty serializedProperty, GUIContent label, bool b, GUILayoutOption[] options)
        {
            EditorGUI.PropertyField(GetNextLine(), serializedProperty,label,b);
        }
                

        private static class FlexibleRect
        {
            
        }
    }
}