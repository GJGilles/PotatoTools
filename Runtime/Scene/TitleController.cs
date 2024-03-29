﻿using PotatoTools.UI;
using UnityEngine;

namespace PotatoTools.Scene
{
    public class TitleController : ActionList
    {
        protected override void Start()
        {
            base.Start();
            actions.Add(NewGame);
            actions.Add(LoadGame);
            actions.Add(Credits);
        }

        private void NewGame()
        {
            SceneService.SetNext(SceneEnum.One);
        }

        private void LoadGame()
        {
            FileService.Load();
            SceneService.SetNext(SceneEnum.One);
        }

        private void Credits()
        {
            SceneService.SetNext(SceneEnum.Credits);
        }

        private void Exit()
        {
            Application.Quit();
        }
    }
}