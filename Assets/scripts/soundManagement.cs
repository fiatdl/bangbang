using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManagement : MonoBehaviour
{
    [SerializeField] private SoundSO soundSO;
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    public static soundManagement Instance { get; private set; }
    private float volume = 10f;
    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

        void Start()
    { 
       
      
       
        
        gameManagement.Instance.OnFinish += Instance_OnFinish;
    }

    private void Instance_OnQ(object sender, System.EventArgs e)
    {
        armyTank armyTank = sender as armyTank;
        PlaySound(soundSO.skillQ, armyTank.transform.position);
    }

    private void Instance_OnFinish(object sender, System.EventArgs e)
    {
        gameManagement gm = sender as gameManagement;
        PlaySound(soundSO.finnishRound,gm.transform.position);
    }

    private void Instance_OnDestroy(object sender, System.EventArgs e)
    {
        enemyBot enemyBot = sender as enemyBot;
        PlaySound(soundSO.destroyed, enemyBot.transform.position);

    }

    private void Instance_OnClone1(object sender, System.EventArgs e)
    {
       
        enemyBot enemyBot=sender as enemyBot;
        PlaySound(soundSO.extract, enemyBot.transform.position);
 
    }



    private void Instance_OnShootingSound(object sender, System.EventArgs e)
    {
        armyTank armyTank = sender as armyTank;
        PlaySound(soundSO.shoot, armyTank.transform.position);
    }

    private void Instance_OnHit(object sender, System.EventArgs e)
    {
        armyTank armyTank = sender as armyTank;
        PlaySound(soundSO.hit, armyTank.transform.position);
    }

    private void Instance_OnSpace(object sender, System.EventArgs e)
    {
        tank armyTank = sender as tank;
        PlaySound(soundSO.skillSpace, armyTank.transform.position);
    }

    private void Instance_OnCastESkillSound(object sender, System.EventArgs e)
    {
        armyTank armyTank = sender as armyTank;
        PlaySound(soundSO.skillE, armyTank.transform.position);
    }

        private void Instance_OnHealSound(object sender, System.EventArgs e)
    {
        armyTank armyTank = sender as armyTank;
        PlaySound(soundSO.heal, armyTank.transform.position);
    }

  

 
  

    private void Instance_OnHited(object sender, System.EventArgs e)
    {
        EnemyTurret enemyTurret = sender as EnemyTurret;
        PlaySound(soundSO.hit, enemyTurret.transform.position);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    


 

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 10f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

}
