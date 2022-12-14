using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonArchitect.UI
{
    public delegate void UIUndoRedoDelegate(UISystem uiSystem);
    public interface UIUndoSystem
    {
        void RecordObject(object obj, string name);
        void RegisterCreatedObjectUndo(object obj, string name);
        void DestroyObjectImmediate(object obj);
        void RegisterCompleteObjectUndo(object obj, string name);
        event UIUndoRedoDelegate UndoRedoPerformed;
    }
}
