using PotatoTools.Character;
using UnityEngine;

namespace PotatoTools
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class SelectableController : MonoBehaviour
    {
        private GameObject key;

        public abstract void Select();

        public virtual void Highlight(bool h)
        {
            GetComponent<SpriteRenderer>().color = h ? Color.yellow : Color.white;
            
            if (h)
            {
                var col = GetComponent<Collider2D>();
                var spr = InputManager.GetSprite(ButtonEnum.A);

                key = new GameObject();
                key.transform.parent = transform;
                key.transform.position = col.bounds.center + new Vector3(0, col.bounds.extents.y + 0.5f);
                key.AddComponent<SpriteRenderer>().sprite = spr;
            }
            else if (key != null)
            {
                Destroy(key);
                key = null;
            }
        }
    }
}