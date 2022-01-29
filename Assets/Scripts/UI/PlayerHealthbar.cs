using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthbar : MonoBehaviour
{
    public static PlayerHealthbar instance;

    [SerializeField] private float xOffset;
    [SerializeField] private GameObject heartUI;
    [SerializeField] private GameObject halfHeartUI;

    private List<GameObject> hearts;

    // Start is called before the first frame update
    void Start()
    {
        hearts = new List<GameObject>();
        instance = this;
        UpdateHealth();
    }

    // Clears the heath bar UI
    private void ClearHearts()
    {
        foreach (Transform go in transform)
        {
            Destroy(go.gameObject, 0);
        }
        transform.DetachChildren();
    }

    // Update is called once per frame
    public void UpdateHealth()
    {
        ClearHearts();

        int playerHealth = Player.instance.GetHealth();
        // Number of Hearts
        int heartNum = (int)Mathf.Floor(playerHealth / 2);

        // If there is gonna be a half heart or not
        int remainder = playerHealth % 2;

        for (int i = 0; i < heartNum; i++)
        {
            GameObject newHeart = Instantiate<GameObject>(heartUI);
            newHeart.transform.position = new Vector3((i * xOffset) + 30, 0);
            newHeart.transform.SetParent(transform, false);
        }

        if(remainder != 0)
        {
            GameObject newHeart = Instantiate<GameObject>(halfHeartUI);
            newHeart.transform.position = new Vector3((transform.childCount * xOffset) + 20, 0);
            newHeart.transform.SetParent(transform, false);
        }
    }
}
