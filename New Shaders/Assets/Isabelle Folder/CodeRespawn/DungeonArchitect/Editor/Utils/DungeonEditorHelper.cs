//$ Copyright 2016, Code Respawn Technologies Pvt Ltd - All Rights Reserved $//

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using DungeonArchitect;
using DungeonArchitect.Utils;
using DungeonArchitect.Graphs;
using DungeonArchitect.SpatialConstraints;
using DungeonArchitect.Graphs.SpatialConstraints;
using DungeonArchitect.Grammar;
using DungeonArchitect.Editors.DungeonFlow;
using DungeonArchitect.Editors.GridFlow;
using DungeonArchitect.Builders.GridFlow;
using DungeonArchitect.Landscape;
using DungeonArchitect.UI.Widgets.GraphEditors;
using DungeonArchitect.UI;
using DungeonArchitect.UI.Impl.UnityEditor;

namespace DungeonArchitect.Editors
{
    /// <summary>
    /// Utility functions for various editor based features of Dungeon Architect
    /// </summary>
    public class DungeonEditorHelper
    {
        /// <summary>
        /// Creates a new Dungeon Theme in the specified asset folder.  Access from the Create context menu in the Project window
        /// </summary>
        [MenuItem("Assets/Create/Dungeon Architect/Dungeon Theme")]
        public static void CreateThemeAssetInBrowser()
        {
            var defaultFileName = "DungeonTheme.asset";
            var path = GetAssetBrowserPath();
            var graph = CreateAssetInBrowser<Graph>(path, defaultFileName);
            CreateDefaultMarkerNodes(graph);
            HandlePostAssetCreated(graph);
        }

        /// <summary>
        /// Creates a new Dungeon Theme in the specified asset folder.  Access from the Create context menu in the Project window
        /// </summary>
        [MenuItem("Assets/Create/Dungeon Architect/Flow Graphs/Snap Graph")]
        public static void CreateDungeonFlowAssetInBrowser()
        {
            var defaultFileName = "DungeonSnapFlow.asset";
            var path = GetAssetBrowserPath();
            var dungeonFlow = CreateAssetInBrowser<DungeonFlowAsset>(path, defaultFileName);
            DungeonFlowEditorUtils.InitAsset(dungeonFlow, new UnityEditorUIPlatform());
            HandlePostAssetCreated(dungeonFlow);
        }

        /// <summary>
        /// Creates a new Dungeon Theme in the specified asset folder.  Access from the Create context menu in the Project window
        /// </summary>
        [MenuItem("Assets/Create/Dungeon Architect/Flow Graphs/Grid Graph")]
        public static void CreateDungeonGridCyclicAssetInBrowser()
        {
            var defaultFileName = "DungeonGridFlow.asset";
            var path = GetAssetBrowserPath();
            var gridFlow = CreateAssetInBrowser<DungeonGridFlowAsset>(path, defaultFileName);
            DungeonGridFlowEditorUtils.InitAsset(gridFlow, new UnityEditorUIPlatform());
            HandlePostAssetCreated(gridFlow);
        }

        /// <summary>
        /// Creates a new Dungeon Theme in the specified asset folder.  Access from the Create context menu in the Project window
        /// </summary>
        [MenuItem("Assets/Create/Dungeon Architect/Landscape/Landscape Restoration Cache")]
        public static void CreateDungeonLandscapeRestCacheInBrowser()
        {
            var defaultFileName = "DungeonLandscapeRestorationCache.asset";
            var path = GetAssetBrowserPath();
            var cacheAsset = CreateAssetInBrowser<DungeonLandscapeRestorationCache>(path, defaultFileName);
            HandlePostAssetCreated(cacheAsset);
        }

        /// <summary>
        /// Handle opening of theme graphs.
        /// When the user right clicks on the theme graph and selects open, the graph is shown in the theme editor
        /// </summary>
        /// <param name="instanceID"></param>
        /// <param name="line"></param>
        /// <returns>true if trying to open a dungeon theme, indicating that it has been handled.  false otherwise</returns>
        [UnityEditor.Callbacks.OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            Object activeObject = EditorUtility.InstanceIDToObject(instanceID);
            if (activeObject is Graph)
            {
                var graph = Selection.activeObject as Graph;
                ShowThemeEditor(graph);
                return true; //catch open file
            }
            else if (activeObject is DungeonFlowAsset)
            {
                var dungeonFlow = Selection.activeObject as DungeonFlowAsset;
                ShowDungeonFlowEditor(dungeonFlow);
                return true;
            }
            else if (activeObject is DungeonGridFlowAsset)
            {
                var dungeonFlow = Selection.activeObject as DungeonGridFlowAsset;
                ShowDungeonGridFlowEditor(dungeonFlow);
                return true;
            }
            return false; // let unity open the file
        }

        public static T CreateAssetInBrowser<T>(string path, string defaultFilename) where T : ScriptableObject
        {
            var fileName = MakeFilenameUnique(path, defaultFilename);
            var fullPath = path + "/" + fileName;

            var asset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(asset, fullPath);
            
            return asset;
        }

        public static void HandlePostAssetCreated(ScriptableObject asset)
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            ProjectWindowUtil.ShowCreatedAsset(asset);
        }

        public static string GetActiveScenePath()
        {
            var scenePath = UnityEngine.SceneManagement.SceneManager.GetActiveScene().path;
            scenePath = scenePath.Replace('\\', '/');
            int trimIndex = scenePath.LastIndexOf('/');
            scenePath = scenePath.Substring(0, trimIndex);
            return scenePath;
        }

        /// <summary>
        /// Shows the dungeon theme editor window and loads the specified graph into it
        /// </summary>
        /// <param name="graph">The graph to load in the dungeon theme editor window</param>
        public static void ShowThemeEditor(Graph graph)
        {
            if (graph != null)
            {
                var window = EditorWindow.GetWindow<DungeonThemeEditorWindow>();
                if (window != null)
                {
                    window.Init(graph);
                }
            }
            else
            {
                Debug.LogWarning("Invalid Dungeon theme file");
            }
        }


        /// <summary>
        /// Shows the dungeon flow editor window and loads the specified graph into it
        /// </summary>
        /// <param name="graph">The graph to load in the dungeon theme editor window</param>
        public static void ShowDungeonFlowEditor(DungeonFlowAsset dungeonFlow)
        {
            if (dungeonFlow != null)
            {
                var window = EditorWindow.GetWindow<DungeonFlowEditorWindow>();
                if (window != null)
                {
                    window.Init(dungeonFlow);
                }
            }
            else
            {
                Debug.LogWarning("Invalid Dungeon flow file");
            }
        }

        /// <summary>
        /// Shows the dungeon grid flow editor window and loads the specified graph into it
        /// </summary>
        /// <param name="graph">The graph to load in the dungeon theme editor window</param>
        public static void ShowDungeonGridFlowEditor(DungeonGridFlowAsset gridFlow)
        {
            if (gridFlow != null)
            {
                var window = EditorWindow.GetWindow<DungeonGridFlowEditorWindow>();
                if (window != null)
                {
                    window.Init(gridFlow);
                }
            }
            else
            {
                Debug.LogWarning("Invalid Dungeon grid flow file");
            }
        }

        /// <summary>
        /// Creates a unique filename in the specified asset directory
        /// </summary>
        /// <param name="dir">The target directory this file will be placed in.  Used for finding non-colliding filenames</param>
        /// <param name="filename">The prefered filename.  Will add incremental numbers to it till it finds a free filename</param>
        /// <returns>A filename not currently used in the specified directory</returns>
        public static string MakeFilenameUnique(string dir, string filename)
        {
            string fileNamePart = System.IO.Path.GetFileNameWithoutExtension(filename);
            string fileExt = System.IO.Path.GetExtension(filename);
            var indexedFileName = fileNamePart + fileExt;
            string path = System.IO.Path.Combine(dir, indexedFileName);
            for (int i = 1; ; ++i)
            {
                if (!System.IO.File.Exists(path))
                    return indexedFileName;

                indexedFileName = fileNamePart + " " + i + fileExt;
                path = System.IO.Path.Combine(dir, indexedFileName);
            }
        }

        public static T CreateConstraintRule<T>(SpatialConstraintAsset spatialConstraint) where T : ConstraintRule
        {
            if (spatialConstraint == null || spatialConstraint.hostThemeNode == null) return null;

            var rule = ScriptableObject.CreateInstance<T>();
            var assetObject = spatialConstraint.hostThemeNode.Graph;

            AssetDatabase.AddObjectToAsset(rule, assetObject);
            return rule;
        }

        public static void DestroySpatialConstraintAsset(SpatialConstraintAsset spatialConstraint)
        {
            if (spatialConstraint == null)
            {
                return;
            }
            if (spatialConstraint.hostThemeNode == null)
            {
                return;
            }

            var asset = spatialConstraint.hostThemeNode.Graph;
            Undo.RegisterCompleteObjectUndo(asset, "Delete Node Spatial Constraint");

            var objectsToDestroy = new List<ScriptableObject>();
            if (spatialConstraint != null && spatialConstraint.Graph != null)
            {
                foreach (var node in spatialConstraint.Graph.Nodes)
                {
                    if (node is SCRuleNode)
                    {
                        var ruleNode = node as SCRuleNode;
                        foreach (var constraint in ruleNode.constraints)
                        {
                            objectsToDestroy.Add(constraint);
                        }
                    }
                    objectsToDestroy.Add(node);
                }
                objectsToDestroy.Add(spatialConstraint.Graph);
                objectsToDestroy.Add(spatialConstraint);
            }

            foreach (var objectToDestroy in objectsToDestroy)
            {
                if (objectToDestroy != null)
                {
                    Undo.DestroyObjectImmediate(objectToDestroy);
                }
            }
        }

        public static string GetAssetBrowserPath()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }

            else if (System.IO.Path.GetExtension(path) != "")
            {
                path = path.Replace(System.IO.Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }
            return path;
        }
        
        public static T GetWindowIfOpen<T>() where T : Object
        {
            T[] existingWindows = Resources.FindObjectsOfTypeAll<T>();
            T existingWindow = null;
            if (existingWindows.Length > 0)
            {
                existingWindow = existingWindows[0];
            }
            return existingWindow;
        }


        /// <summary>
        /// Marks the graph as dirty so that it is serialized to disk again when saved
        /// </summary>
        /// <param name="graph"></param>
        public static void MarkAsDirty(Graph graph)
        {
            EditorUtility.SetDirty(graph);
        }

        public static void CreateDefaultSpatialConstraintNodes(SpatialConstraintAsset constraintAsset, UIUndoSystem undo)
        {
            var position = SCBaseDomainNode.TileSize * 0.5f * Vector2.one;
            CreateSpatialConstraintNode<SCReferenceNode>(constraintAsset, position, undo);
        }

        public static T CreateSpatialConstraintNode<T>(SpatialConstraintAsset constraintAsset, Vector2 worldPosition, UIUndoSystem undo) where T : SCBaseDomainNode
        {
            var graph = constraintAsset.Graph;
            var node = GraphOperations.CreateNode<T>(graph, undo);
            node.Position = worldPosition;
            node.SnapNode();

            var hostAsset = constraintAsset.hostThemeNode.Graph;
            GraphEditorUtils.AddToAsset(new UnityEditorUIPlatform(), hostAsset, node);

            return node;
        }

        /// <summary>
        /// Creates default marker nodes when a new graph is created
        /// </summary>
        static void CreateDefaultMarkerNodes(Graph graph)
        {
            if (graph == null)
            {
                Debug.LogWarning("Cannot create default marker nodes. graph is null");
                return;
            }
            var markerNames = new string[] {
			    DungeonConstants.ST_GROUND,
			    DungeonConstants.ST_WALL,
			    DungeonConstants.ST_WALLSEPARATOR,
			    DungeonConstants.ST_FENCE,
			    DungeonConstants.ST_FENCESEPARATOR,
			    DungeonConstants.ST_DOOR,
			    DungeonConstants.ST_STAIR,
			    DungeonConstants.ST_STAIR2X,
			    DungeonConstants.ST_WALLHALF,
			    DungeonConstants.ST_WALLHALFSEPARATOR
		    };

            // Make sure we don't have any nodes in the graph
            if (graph.Nodes.Count > 0)
            {
                return;
            }

            const int INTER_NODE_X = 200;
            const int INTER_NODE_Y = 300;
            int itemsPerRow = markerNames.Length / 2;
            for (int i = 0; i < markerNames.Length; i++)
            {
                int ix = i % itemsPerRow;
                int iy = i / itemsPerRow;
                int x = ix * INTER_NODE_X;
                int y = iy * INTER_NODE_Y;
                var node = GraphOperations.CreateNode<MarkerNode>(graph, null);
                GraphEditorUtils.AddToAsset(new UnityEditorUIPlatform(), graph, node);
                node.Position = new Vector2(x, y);
                node.Caption = markerNames[i];
            }
        }

        /// <summary>
        /// Creates an editor tag
        /// </summary>
        /// <param name="tag"></param>
        public static void CreateEditorTag(string tag)
        {
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty tagsProp = tagManager.FindProperty("tags");

            // Check if the tag is already present
            for (int i = 0; i < tagsProp.arraySize; i++)
            {
                SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
                if (t.stringValue.Equals(tag))
                {
                    // Tag already exists.  do not add a duplicate
                    return;
                }
            }

            tagsProp.InsertArrayElementAtIndex(0);
            SerializedProperty n = tagsProp.GetArrayElementAtIndex(0);
            n.stringValue = tag;

            tagManager.ApplyModifiedProperties();
        }


		// Resets the node IDs of the graph. Useful if you have cloned another graph
		//[MenuItem("Debug DA/Fix Node Ids")]
		public static void _Advanced_RecreateGraphNodeIds()
		{
			var editor = EditorWindow.GetWindow<DungeonThemeEditorWindow>();
			if (editor != null && editor.GraphEditor != null && editor.GraphEditor.Graph != null)
			{
				var graph = editor.GraphEditor.Graph;
				foreach (var node in graph.Nodes)
				{
					node.Id = System.Guid.NewGuid().ToString();
				}
			}
			
		}

        public static void MarkSceneDirty()
        {
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
        }
        
    }
}
