using PotatoTools.Character;
using PotatoTools.Scene;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PotatoTools.UI
{
    public class PauseController : ActionList
    {
        protected override void Start()
        {
            base.Start();

            GetComponent<Canvas>().worldCamera = Camera.current;

            Time.timeScale = 0;

            actions.Add(Save);
            actions.Add(Title);
            actions.Add(Exit);

            PlayerService.Lock();
        }

        protected override void Update()
        {
            base.Update();

            if (InputManager.GetButtonTrigger(ButtonEnum.B))
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            Time.timeScale = 1;
            PlayerService.Unlock();
        }

        private void Save()
        {
            FileService.Save();
        }

        private void Title()
        {
            SceneService.SetNext(SceneEnum.Title);
        }

        private void Exit()
        {
            Application.Quit();
        }
    }
}