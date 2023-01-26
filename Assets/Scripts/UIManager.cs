using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI DuckCounter;
    public TextMeshProUGUI EndText;
    public DucklingManager manager;
    private int totalDucks;

    // Start is called before the first frame update
    void Start()
    {
        DucklingBehaviour[] ducks = FindObjectsOfType<DucklingBehaviour>();
        totalDucks = ducks.Length;
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.CurrentFollowing == totalDucks)
        {
            DuckCounter.enabled = false;
            EndText.enabled = true;
        }
        else
        {
            DuckCounter.text = manager.CurrentFollowing.ToString() + "/" + totalDucks.ToString();
            DuckCounter.enabled = false;
            EndText.gameObject.SetActive(false);
        }
    }
}
