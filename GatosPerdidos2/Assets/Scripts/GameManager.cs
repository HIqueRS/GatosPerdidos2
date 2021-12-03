using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{

    public GameObject gato1;
    public GameObject gato2;

    public int[] pexe;

    public float distancia;

    public GameObject fim;

    // Start is called before the first frame update
    void Start()
    {
        pexe = new int[2];

        pexe[0] = 0;
        pexe[1] = 0;

        distancia = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangePexe(int f, int id)
    {
        switch (id)
        {
            case -1:
                pexe[1] = f;
                break;
            case 1:
                pexe[0] = f;
                break;
        }

    }

    public void TryWin()
    {
        
        

        Debug.Log(pexe[0] + " " + pexe[1]);
        if (pexe[0] >= 5 && pexe[1] >= 5)
        {
            if (gato1 && gato2)
            {
                float diferenca = Vector3.Distance(gato1.transform.position, gato2.transform.position);

                Debug.Log(diferenca);

                if (diferenca < distancia)
                {
                    Debug.Log("acabe");


                    fim.SetActive(true);
                    //mas tecinicamente o rcp so pode ser chamado pelo server e o cmd pelo client
                    if (isServer)
                    {
                        RpcFim();
                    }
                    else
                    {
                        CmdFim();
                    }
                }
            }

        }
    }

    [Command(requiresAuthority = false)]
    void CmdFim(NetworkConnectionToClient sender = null)
    {
        fim.SetActive(true);
    }

    [ClientRpc(includeOwner = false)]
    public void RpcFim()
    {
        fim.SetActive(true);
    }
}
