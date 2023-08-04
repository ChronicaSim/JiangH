using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JiangH.Views
{
    public class ContainerView : MonoBehaviour
    {
        public ItemView defaultItem;

        public IEnumerable<ItemView> items => defaultItem.transform.parent.GetComponentsInChildren<ItemView>();

        public void RemoveItem(ItemView itemView)
        {
            if(items.All(x=>x != itemView))
            {
                throw new Exception();
            }

            Destroy(itemView.gameObject);
        }

        public ItemView AddItem()
        {
            var gameObject = Instantiate(defaultItem.gameObject, defaultItem.gameObject.transform.parent);
            return gameObject.GetComponent<ItemView>();
        }
    }
}
