using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMaxBullets;
    [SerializeField] private TextMeshProUGUI textCurrentBullets;

    [SerializeField] private ShootRaycast shootRaycast;

    [SerializeField] private int maxBullets;
    [SerializeField] private int currentBullets;

    // Start is called before the first frame update
    void Start()
    {
        maxBullets = shootRaycast.getMaxBullets();
        currentBullets = shootRaycast.getCurrentBullets();
    }

    // Update is called once per frame
    void Update()
    {
        currentBullets = shootRaycast.getCurrentBullets();
        textCurrentBullets.text = currentBullets.ToString();

        textMaxBullets.text = maxBullets.ToString();
    }
}
