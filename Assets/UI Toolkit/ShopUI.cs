using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopUI : MonoBehaviour
{
    public GameObject shopUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement avatarField = root.Q<VisualElement>("AvatarField");

        Button buttonCharacters = root.Q<Button>("ButtonCharacters");
        buttonCharacters.clickable.clicked += () => {
            avatarField.style.display = DisplayStyle.Flex;
        };

        Button buttonClose = root.Q<Button>("ButtonClose");
        buttonClose.clickable.clicked += () => {
            shopUI.SetActive(false);
        };
    }
}
