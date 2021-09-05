using PotatoTools.Character;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PotatoTools.Dialog
{
    public enum DialogStateEnum
    {
        Start,
        Reading,
        Paused,
        Slide,
        Done
    }

    public class DialogController : MonoBehaviour
    {
        [NonSerialized] public DialogObject dialog;

        public Canvas canvas;
        public RectTransform area;
        public RectTransform cArea;
        public RectTransform tArea;
        public TMPro.TMP_Text nameArea;
        public TMPro.TMP_Text textObj;

        public float speed = 3f;
        public int spacing = 64;
        public int lines = 5;

        private List<UnityEngine.UI.Image> characters = new List<UnityEngine.UI.Image>();

        private int dIdx = 0;
        private float tPos = 0f;
        private int lMax = 0;
        private float height = 0;
        private TMPro.TMP_Text textbox;
        private DialogStateEnum state = DialogStateEnum.Start;

        private void Start()
        {
            canvas.worldCamera = Camera.current;

            AddCharacter(dialog.characters[0].sprite).anchoredPosition = new Vector2(-cArea.rect.width / 4, 100);
            for (int i = 1; i < dialog.characters.Count; i++)
            {
                int count = dialog.characters.Count - 1;
                int idx = i - 1;

                AddCharacter(dialog.characters[i].sprite).anchoredPosition = 
                    new Vector2((cArea.rect.width / 4) + (idx - ((count - 1) / 2)) * spacing, 100);
            }

            PlayerService.Lock();
        }

        private void OnDestroy()
        {
            PlayerService.Unlock();
        }

        private RectTransform AddCharacter(Sprite spr)
        {
            var img = new GameObject().AddComponent<UnityEngine.UI.Image>();
            img.transform.SetParent(cArea);
            img.sprite = spr;
            img.SetNativeSize();
            characters.Add(img);

            var rect = img.gameObject.GetComponent<RectTransform>();
            rect.localScale = new Vector3(2f, 2f, 1f);
            return rect;
        }

        private void Update()
        {
            switch (state)
            {
                case DialogStateEnum.Start:
                    StartState();
                    break;
                case DialogStateEnum.Reading:
                    ReadingState();
                    break;
                case DialogStateEnum.Paused:
                    PausedState();
                    break;
                case DialogStateEnum.Slide:
                    SlideState();
                    break;
                case DialogStateEnum.Done:
                    DoneState();
                    break;
            }
        }

        private void StartState()
        {
            if (!CommonAnimation.DampedMove(area.anchoredPosition, new Vector2(0, 0), out Vector2 c))
            {
                area.anchoredPosition = c;
            }
            else
            {
                area.anchoredPosition = new Vector2(0, 0);
                state = DialogStateEnum.Reading;
            }
        }

        private void ReadingState()
        {
            var d = dialog.dialogs[dIdx];

            if (textbox == null)
            {
                nameArea.text = dialog.characters[d.speaker].name;
                textbox = Instantiate(textObj, tArea.parent);
                textbox.transform.SetParent(tArea);
                tPos = 0;
                lMax = lines - 1;

                foreach (var c in characters)
                {
                    c.color = Color.gray;
                }
                characters[d.speaker].color = Color.white;
            }

            if (InputManager.GetButtonHeld(ButtonEnum.A) > 0)
            {
                tPos += 4 * speed * Time.deltaTime;
            }
            else
            {
                tPos += speed * Time.deltaTime;
            }

            int pos = Math.Min(Mathf.RoundToInt(tPos), d.text.Length);
            string text = d.text.Substring(0, pos);
            textbox.text = text;

            if (textbox.textInfo.lineCount >= lMax)
            {
                float h = tArea.parent.GetComponent<RectTransform>().rect.height;
                height += h * ((lines / 2) + 1f) / lines;
                textbox.text = textbox.text.Substring(0, textbox.text.Length - 1);
                lMax += (lines / 2) + 1;
                state = DialogStateEnum.Paused;
            }
            else if (pos == d.text.Length)
            {
                float h = tArea.parent.GetComponent<RectTransform>().rect.height;
                height += h;
                textbox = null;
                state = DialogStateEnum.Paused;
                dIdx++;
            }
        }

        private void PausedState()
        {
            if (InputManager.GetButtonTrigger(ButtonEnum.A))
            {
                if (dIdx >= dialog.dialogs.Count)
                {
                    state = DialogStateEnum.Done;
                }
                else
                {
                    state = DialogStateEnum.Slide;
                }
            }
        }

        private void SlideState()
        {
            if (!CommonAnimation.DampedMove(tArea.anchoredPosition, new Vector2(0, height), out Vector2 c))
            {
                tArea.anchoredPosition = c;
            }
            else
            {
                tArea.anchoredPosition = new Vector2(0, height);
                state = DialogStateEnum.Reading;
            }
        }

        private void DoneState()
        {
            if (characters != null)
            {
                foreach (var character in characters)
                {
                    Destroy(character.gameObject);
                }
                characters = null;
            }

            if (!CommonAnimation.DampedMove(area.anchoredPosition, new Vector2(0, -area.rect.height / 2), out Vector2 c))
            {
                area.anchoredPosition = c;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}