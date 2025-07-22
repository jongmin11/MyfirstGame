using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Metaverse
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        void Start()
        {

        }

        void Update()
        {

        }
    }
}

