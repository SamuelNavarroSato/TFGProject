using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    void Awake()
    {
        GameManager.OnGameStateChanged += WhenGameStateChanges;
    }

    private void WhenGameStateChanges(GameState currentState)
    {
        if (currentState == GameState.PHASE_A_P1 || currentState == GameState.PHASE_A_P2)
        {
            SetPosition(0, 0);
        }
        else if (currentState == GameState.PHASE_B_P1 || currentState == GameState.PHASE_B_P2)
        {

        }
        else if (currentState == GameState.PHASE_C_P1 || currentState == GameState.PHASE_C_P2)
        {

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= WhenGameStateChanges;
    }

    public void SetPosition(float x, float y)
    {
        gameObject.transform.position = new Vector3(x, y, -10f);
    }
}
