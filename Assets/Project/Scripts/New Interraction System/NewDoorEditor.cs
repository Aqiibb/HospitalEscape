#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace YourProject.NewInteractionSystem
{
    [CustomEditor(typeof(NewDoor))]
    public class NewDoorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            NewDoor door = (NewDoor)target;
            if (GUILayout.Button("Open Door"))
            {
                door.Interact();
            }
        }
    }
}
#endif
