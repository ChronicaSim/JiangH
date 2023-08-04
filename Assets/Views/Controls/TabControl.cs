using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JiangH.Views.Controls
{
    [RequireComponent(typeof(ToggleGroup))]
    class TabControl : MonoBehaviour
    {
        [Serializable]
        public class Group
        {
            public Toggle toggle;
            public GameObject content;
        }

        public List<Group> groups = new List<Group>();

        void Start()
        {
            Initialize();
        }

        internal void Initialize()
        {
            var toggeGroup = GetComponent<ToggleGroup>();
            toggeGroup.allowSwitchOff = false;

            foreach (var group in groups)
            {
                if(group.toggle == null || group.content == null)
                {
                    continue;
                }

                group.toggle.group = toggeGroup;
                group.toggle.onValueChanged.RemoveAllListeners();

                group.toggle.onValueChanged.AddListener((flag) =>
                {
                    group.content.SetActive(flag);
                });
            }
        }
    }
}
