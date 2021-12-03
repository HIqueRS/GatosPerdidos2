using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror
{
    public class Buttons : MonoBehaviour
    {
        NetworkManager manager;

        void Awake()
        {
            manager = GetComponent<NetworkManager>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SeverClient()
        {
            if (!NetworkClient.active)
            {
                // Server + Client
                if (Application.platform != RuntimePlatform.WebGLPlayer)
                {
                   
                        manager.StartHost();
                   
                }
            }
        }

        public void Client()
        {
            if (!NetworkClient.active)
            {
               

                // Client + IP
                //GUILayout.BeginHorizontal();
                //if (GUILayout.Button("Client"))
                //{
                    manager.StartClient();
                //}
                //manager.networkAddress = GUILayout.TextField(manager.networkAddress);
                //GUILayout.EndHorizontal();

               
            }
        }
    }
}

