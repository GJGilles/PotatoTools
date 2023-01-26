using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace PotatoTools.Inventory
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class ItemVacuumController : MonoBehaviour
    {
        public float range;
        public float force;

        public ItemInventory inventory;

        private void OnTriggerStay2D (Collider2D collision)
        {
            if (collision.TryGetComponent(out ItemController ctrl))
            {
                if (!inventory.CanPush(new ItemStack(ctrl.item, 1)))
                    return;

                float dist = Mathf.Abs((transform.position - ctrl.transform.position).magnitude);
                if (dist < range)
                {
                    inventory.TryPush(new ItemStack(ctrl.item, 1));
                    Destroy(ctrl.gameObject);
                }
                else
                {
                    ctrl.rb.AddForce(force * (transform.position - ctrl.transform.position).normalized * Time.deltaTime);
                }
            }
        }
    }
}