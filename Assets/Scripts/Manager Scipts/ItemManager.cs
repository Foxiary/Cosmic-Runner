using ReadyPlayerMe.Samples.AvatarCreatorWizard;
using ReadyPlayerMe.Samples.QuickStart;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> auraList = new List<GameObject>();
    public Player player;
    private void Update()
    {
        if (player == null)
            player = GameManager.instance.player;
    }
    #region JumpBooster
    public void JumpBooster(float _jumpHeightBoosted,float _time)
    {
        float jumpHeightSave = player.jumpHeight;
        player.jumpHeight = _jumpHeightBoosted;

        StartCoroutine(JumpBoosterCoolDownFor(_time, jumpHeightSave));

    }
    private IEnumerator JumpBoosterCoolDownFor(float _time,float _heightSave)
    {
        GameObject newAura = SpawnNewAura(0);

        yield return new WaitForSeconds(_time);
        player.jumpHeight = _heightSave;
        Destroy(newAura);
    }
    #endregion
    #region magnet
    public void MagnetBooster(float _time)
    {
        StartCoroutine(MagnetBoosterCoolDownFor(_time));
    }
    private IEnumerator MagnetBoosterCoolDownFor(float _time)
    {
        GameObject newAura = SpawnNewAura(2);
        GameManager.instance.player.magnetTrigger.SetActive(true);
        yield return new WaitForSeconds(_time);

        GameManager.instance.player.magnetTrigger.SetActive(false);
        Destroy(newAura);
    }
    #endregion
    #region jetpack
    public void JetpackBooster(float _time)
    {
        StartCoroutine(JetpackBoosterCoolDownFor(_time));
        player.stateMachine.ChangeState(player.jetpackState);
    }
    private IEnumerator JetpackBoosterCoolDownFor(float _time)
    {
        //GameObject newAura = SpawnNewAura(1);
        yield return new WaitForSeconds(_time);

        //Destroy(newAura);
    }
    #endregion
    private GameObject SpawnNewAura(int _num)
    {
        GameObject _newAura = Instantiate(auraList[_num],Vector3.zero,Quaternion.identity, player.gameObject.transform);
        _newAura.transform.localPosition = Vector3.zero;
        _newAura.transform.eulerAngles = new Vector3(-90, 0, 0);
        return _newAura;
    }
}
