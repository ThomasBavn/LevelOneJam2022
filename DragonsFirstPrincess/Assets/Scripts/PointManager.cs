using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    private int points;
    private GameObject pointsUI;

    // Start is called before the first frame update
    void Start()
    {
        pointsUI = GameObject.FindGameObjectWithTag("UI").transform.Find("PointsText").gameObject;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoints(int points)
    {
        this.points += points;
        UpdateUI();
    }

    private void UpdateUI()
    {
        pointsUI.GetComponent<Text>().text = points.ToString();
    }
}
