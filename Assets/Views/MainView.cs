using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JiangH.Views
{
    public class MainView : MonoBehaviour
    {
        public SectView sect;

        public ContainerView departs;

        public List<PersonView> persons = new List<PersonView>();

        public Button nextTurn;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
    }


    [Serializable]
    public class PersonView
    {
        public string uuid;
        public string name;
        public float money;
    }
}
