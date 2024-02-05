using System;
using UnityEngine;

    public class GridPlacementSystemManager<T> : MonoBehaviour where T : GridPlacementSystemManager<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance==null)
            {
                Instance = (T) this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        

        protected virtual void OnDestroy()
        {
            if (Instance==this)
            {
                Instance = null;
            }
        }
  
}