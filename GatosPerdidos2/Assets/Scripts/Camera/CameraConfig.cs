using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraConfig : MonoBehaviour
{

    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private string[] masks;

    public bool isTwo;
    // Start is called before the first frame update
    void Start()
    {
        // funcao q receba o netid pra entao trocar 
        // funcao pra girar camera
        isTwo = false;

        //LayerMask.GetMask()
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableCamera()
    {
        playerCamera.enabled = true;
    }

    public void NoCatMask(int id)
    {
        Debug.Log(id);
        playerCamera.cullingMask &= ~(1 << LayerMask.NameToLayer(masks[id]));
    }

    public void MaskNormal()
    {
        playerCamera.cullingMask = -1;
    }

    public void IsPlayerTwo()
    {
        isTwo = true;
        OnPreCull();
    }

    void OnPreCull()
    {
        if(isTwo)
        {
            playerCamera.ResetWorldToCameraMatrix();
            playerCamera.ResetProjectionMatrix();
            playerCamera.projectionMatrix = playerCamera.projectionMatrix * Matrix4x4.Scale(new Vector3(-1, 1, 1));
        }
        
    }

    void OnPreRender()
    {
        GL.invertCulling = true;
    }

    void OnPostRender()
    {
        GL.invertCulling = false;
    }
}
