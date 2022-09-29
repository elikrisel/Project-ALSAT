﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace DungeonArchitect.Editors.LaunchPad.Actions.Impl
{
    public class LaunchPadActionOpenScene : LaunchPadActionBase
    {
        string path;
        public LaunchPadActionOpenScene(string path)
        {
            this.path = path;
        }

        public override Texture2D GetIcon()
        {
            return ScreenPageLoader.LoadImageAsset("icons/unity_logo");
        }

        public override string GetText()
        {
            return "Open Scene";
        }

        public override void Execute()
        {
            EditorSceneManager.OpenScene(path);
        }
    }
}