
using UnityEngine;
using ReadyPlayerMe.Samples.QuickStart;

public class CharacterManager : MonoBehaviour
{
    public Character _char;
    public ThirdPersonLoader thirdPersonLoader;

    public void GetCharacter(GameObject c)
    {
        _char = c.GetComponent<CharacterInfo>().character;
        Debug.Log(_char.characterName);
        Invoke("LoadCharacter", 0.01f);
    }

    public void LoadCharacter()
    {
        thirdPersonLoader.LoadAvatar(_char.characterUrls);
    }
}