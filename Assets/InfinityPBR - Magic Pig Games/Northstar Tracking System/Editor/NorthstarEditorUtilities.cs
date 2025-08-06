using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * Utility methods for the Northstar system editor scripts.
 */

namespace MagicPigGames.Northstar
{
    public static class NorthstarEditorUtilities
    {
        public static GameObject FindDefaultScreenOverlayPrefab()
        {
            var foundObject = FindGameObjectByName("Screen Overlay Prefab");
            if (foundObject != null)
                return foundObject;

            Debug.LogWarning("Was not able to find the default Screen Overlay Prefab, called \"Screen " +
                             "Overlay Prefab\". Did it get removed or renamed?");
            return default;
        }
        
        public static GameObject FindGameObjectByName(string objectName) 
            => UnityEditor.AssetDatabase.FindAssets(objectName)
                .Select(UnityEditor.AssetDatabase.GUIDToAssetPath)
                .Select(UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>)
                .FirstOrDefault(prefab => prefab != null);

        public static Camera FindFirstEnabledCamera()
        {
            // Search all enabled objects in scene for a Camera component that is enabled. Return the first found.
            var cameras = Object.FindObjectsOfType<Camera>().Where(c => c.enabled);
            foreach (var camera in cameras)
                return camera;

            Debug.LogWarning("Was not able to find the an enabled Camera in the scene!");
            return default;
        }

        public static List<TrackedTargetOverlay> FindAllTrackedTargetOverlays(string targetType = "", bool inProject = true, bool inScene = true)
        {
            var foundList = new List<TrackedTargetOverlay>();
            // Find all in scene
            if (inScene)
            {
                var foundObjects = Object.FindObjectsOfType(typeof(TrackedTargetOverlay), true);
                foundList.AddRange(foundObjects.Cast<TrackedTargetOverlay>()
                    .Where(foundObject => targetType == "" || foundObject.targetType == targetType));
            }
            
            // Find all in project
            if (!inProject) return foundList;
            var guids = UnityEditor.AssetDatabase.FindAssets("t:Prefab");
            foreach (var guid in guids)
            {
                var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                var prefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (prefab == null) continue;
                var components = prefab.GetComponentsInChildren<TrackedTargetOverlay>(true);
                foundList.AddRange(components.Where(component => targetType == "" || component.targetType == targetType));
            }

            return foundList;
        }
    }
}