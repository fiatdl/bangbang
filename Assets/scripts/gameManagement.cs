using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public  class gameManagement : MonoBehaviour
{
    public event EventHandler OnRespwan;
    [SerializeField] private singletaskTemplate singletTemplate;
    [SerializeField] private Transform taskContainer;
    [SerializeField] private EnemySO poli;
    [SerializeField] private EnemySO enemyboss;
    [SerializeField] private EnemySO enemybot;
    [SerializeField] private EnemySO enemyturret;
    [SerializeField] private EnemySO camp;
    [SerializeField] public statusBar skillBarUI;
     public BulletSO tank1;
    [SerializeField] private Transform VictoryCanvas;
    [SerializeField] public EnemyObject[] enemyObjects;
    [SerializeField] public Transform bulletContainer;
    [SerializeField] public Transform textContainer;
    [SerializeField]public GameInput gameInput;
    [SerializeField] public spawnSpot[] spawnSpots;
    [SerializeField] public Transform[] introductArrow;
    [SerializeField] public Transform resurretContainer;
    [SerializeField] private Transform gameOverText;
    [SerializeField] private Button backToMenu;
    [SerializeField] private Button playAgain;
    [SerializeField] private Button nextSceneBtn;
    [SerializeField] public TextMeshProUGUI textMeshProUGUIAnouncement;
    public event EventHandler OnFinish;
    [SerializeField] public tank mainTank;
    [SerializeField] private TextMeshProUGUI resurrectionText;
    [SerializeField] public Image QUI;
    [SerializeField] public Image EUI;
    [SerializeField] public Image SpaceUI;
    [SerializeField] public Image QUIbg;
    [SerializeField] public Image EUIbg;
    [SerializeField] public Image EUIbg1;
    [SerializeField] public Image SpaceUIbg;
    [SerializeField] public SkillUiSO skillUiSO;
    [SerializeField] public SkillUiSO[] skillUiSOs;
    [SerializeField] private Button pauseBtn;
    [SerializeField] private Transform PauUi;
    [SerializeField] private Image face;
    [SerializeField] private Transform[] gameItemSpot;
    [SerializeField] private GameIntemSo[] gameIntemSos;
    private CinemachineVirtualCamera virtualCam;
    private CinemachineVirtualCamera virtualCamMinimap;

   public  int enemyturretAmount = 0;
    public int  enemybotAmount = 0;
   public int enemybossAmount = 0;
   public int enemypoliAmount = 0;
    public int minionAmount = 0;
    public int campAmount = 0;


    private bool isFiniish;
    private float resurrectTime;
    private int playAmount;
    private bool gameOver;
    private bool isDead;
    private float spawnHelpItemTimer;
    private float spwanHelpItemCurrent;
   [SerializeField] public int nextSceneIndex;
    public static gameManagement Instance { get; private set; }
    private void Awake()
    {
        spawnHelpItemTimer = 8f;
        spwanHelpItemCurrent = 7f;

        tank1 = tankSelectManagement.selectedTank;
        skillUiSO = skillUiSOs[tankSelectManagement.index];
        QUI.sprite = skillUiSO.QSkillUi;
        EUI.sprite = skillUiSO.ESkillUi;
        SpaceUI.sprite = skillUiSO.SpaceSkillUi;
        QUIbg.sprite = skillUiSO.QSkillUi;
        EUIbg.sprite = skillUiSO.ESkillUi;
        SpaceUIbg.sprite = skillUiSO.SpaceSkillUi;
        face.sprite = skillUiSO.face;
        isDead = false;
        gameOver = false;
        resurrectTime = 3f;
        playAmount = 2;

        PauUi.gameObject.SetActive(false);

        if (gameManagement.Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            gameManagement.Instance = this;
        }
        resurretContainer.gameObject.SetActive(false);
        virtualCam = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        virtualCamMinimap= GameObject.FindGameObjectWithTag("minimapcam").GetComponent<CinemachineVirtualCamera>();
        SpwainTank();
        skillBarUI.tankFocusing = mainTank;
        gameOverText.gameObject.SetActive(false);
        VictoryCanvas.gameObject.SetActive(false);
      


    }
    void Start()
    {
        
        isFiniish = false;
       if(spawnSpots.Length>0) { foreach (spawnSpot sp in spawnSpots)
        {
            sp.gameObject.SetActive(false) ;
        } }
        if (introductArrow.Length > 0)
        {
 foreach (Transform sp in introductArrow)
        {
            sp.gameObject.SetActive(false);
        }
        }
       
       
        backToMenu.onClick.AddListener(() =>
        {
         
            gameOverText.gameObject.SetActive(false);
            SceneManager.LoadScene(0);
        });
        nextSceneBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        );
        playAgain.onClick.AddListener(() =>
        {
            isDead = false;
            resurrectTime = 10f;
            resurretContainer.gameObject.SetActive(false);
            mainTank.spawnToNewPosition(new Vector3(mainTank.transform.position.x, 0f, mainTank.transform.position.z), 1f);
            mainTank.CurrentHp = mainTank.hP;
            virtualCam.Follow = mainTank.transform;
            virtualCam.LookAt = mainTank.transform;
            virtualCamMinimap.Follow = mainTank.transform;
            virtualCamMinimap.LookAt = mainTank.transform;
            playAmount = 2;
            gameOver = false;
            gameOverText.gameObject.SetActive(false);
            
        });

        foreach (spawnSpot sp in spawnSpots)
        {
            sp.gameObject.SetActive(true);
        }
        foreach (Transform sp in introductArrow)
        {
            sp.gameObject.SetActive(true);
        }
        pauseBtn.onClick.AddListener(() =>
        {

            Time.timeScale = 0f;
            PauUi.gameObject.SetActive(true);
        });
    }

    // Update is called once per frame
    void Update()
    {
        spwanHelpItemCurrent -= Time.deltaTime;

        if (spwanHelpItemCurrent <= 0)
        {
            for(int i=0;i<gameItemSpot.Length;i++)
            {
               
          
Transform preFap = gameIntemSos[UnityEngine.Random.Range(0, gameIntemSos.Length)].preFab;
                    Transform gameItemHelpTransform = Instantiate(preFap, gameItemSpot[i].transform);
                GameItem gameit=gameItemHelpTransform.GetComponent<GameItem>();
                    gameit.gameObject.transform.position = gameItemSpot[i].transform.position;
                gameit.SetTimeLife(spawnHelpItemTimer-1f);
  
            }
            spwanHelpItemCurrent = spawnHelpItemTimer;
        }

        if (gameOver)
        { resurretContainer.gameObject.SetActive(false);
            gameOverText.gameObject.SetActive(true);
            Time.timeScale = 0f;
           
         
        }
        if (isDead)
        {
            
            resurrectTime -= Time.deltaTime;   
            resurretContainer.gameObject.SetActive(true);
            resurrectionText.text = "You Will be Respawn in " + Math.Round((decimal)resurrectTime, 0) + " second"+ System.Environment.NewLine+"You have "+playAmount.ToString()+" life left";
            if (resurrectTime<=0)
            {
                isDead = false;
                Time.timeScale = 1f;
                resurrectTime = 10f;
                resurretContainer.gameObject.SetActive(false);
                mainTank.spawnToNewPosition(new Vector3(-10f, 0f,0f), 1f);
                mainTank.CurrentHp =mainTank.hP;
                mainTank.dead = false;
                virtualCam.Follow = mainTank.transform;
                virtualCam.LookAt = mainTank.transform;
        
               
            }
        }
        else
        {
            
            resurretContainer.gameObject.SetActive(false);
          
        }
        if (checkAliveComponent()&&isFiniish==false)
        {
            isFiniish=true;
            OnFinish?.Invoke(this, EventArgs.Empty);

            VictoryCanvas.gameObject.SetActive(true);

        }
     
        if (mainTank.CurrentHp<=0f&&isDead==false)
        {
            isDead = true;
            virtualCam.Follow = enemyObjects[0].transform;
            virtualCam.LookAt = enemyObjects[0].transform;
            playAmount--;
            gameOver=true?(playAmount==0) : false;
        



        }
    }
    private void SpwainTank()
    {
        
        Transform playerTank = Instantiate(tank1.preFab, this.transform);
        tank tank = playerTank.GetComponent<tank>();
     
        
        mainTank = tank;
    
        mainTank.gameInput = gameInput;
        mainTank.bullets = bulletContainer;
        mainTank.TextContainer = textContainer;
        virtualCam.Follow = tank.transform;
        virtualCam.LookAt= tank.transform;
        virtualCamMinimap.Follow = tank.transform;
        virtualCamMinimap.LookAt = tank.transform;
      
        foreach (EnemyObject enemyObject in enemyObjects)
        {
            if (enemyObject != null)
            {
                if (enemyObject.GetComponent<EnemyTurret>())
                {
                    EnemyTurret enemyTurret = (EnemyTurret)enemyObject;
                    enemyTurret.target = tank ;
                    enemyturretAmount++;

                }
                else
                {
                    if (enemyObject.GetComponent<enemyBot>())
                    {
                        enemyBot enemyBota = (enemyBot)enemyObject;
                        enemyBota.target = tank ;
                        enemybotAmount++;
                    } 
                    else
                {
                    if (enemyObject.GetComponent<boss>())
                    {
                        boss enemyBota = (boss)enemyObject;
                        enemyBota.target = tank;
                            enemybossAmount++;
                    }
                        else
                        {
                            if (enemyObject.GetComponent<poligonal>())
                            {
                                poligonal enemyBota = (poligonal)enemyObject;
                                enemyBota.target = tank;
                                enemypoliAmount++;
                            }
                            else
                            {
                                if (enemyObject.GetComponent<minion>())
                                {
                                    minion enemyBota = (minion)enemyObject;
                                    enemyBota.target = tank;
                                    minionAmount++;
                                }
                                else
                                {
                                    if (enemyObject.GetComponent<EnemyCamp>())
                                    {
                                       
                                        campAmount++;
                                    }
                                    else
                                    {
                                        if (enemyObject.GetComponent<rockMonster>())
                                        {
                                            rockMonster enemyBota = (rockMonster)enemyObject;
                                            enemyBota.target = tank;
                                            minionAmount++;
                                        }
                                    }
                                }
                                
                            }
                        }
                }
                }
             
            }

        }

        if (enemyturretAmount != 0)
        {
            Transform templateTask = Instantiate(singletTemplate.transform, taskContainer);
            singletaskTemplate sp = templateTask.GetComponent<singletaskTemplate>();
            sp.amount.text="X "+enemyturretAmount.ToString();
            sp.sprite.sprite = enemyturret.avatar;
            sp.gameObject.SetActive(true);

        }

        if (enemybotAmount != 0)
        {
            Transform templateTask = Instantiate(singletTemplate.transform, taskContainer);
            singletaskTemplate sp = templateTask.GetComponent<singletaskTemplate>();
            sp.amount.text = "X " + enemybotAmount.ToString();
            sp.sprite.sprite = enemybot.avatar;
            sp.gameObject.SetActive(true);
        }

        if (enemypoliAmount != 0)
        {
            Transform templateTask = Instantiate(singletTemplate.transform, taskContainer);
            singletaskTemplate sp = templateTask.GetComponent<singletaskTemplate>();
            sp.amount.text = "X " + enemypoliAmount.ToString();
            sp.sprite.sprite = poli.avatar;
            sp.gameObject.SetActive(true);
        }

        if (enemybossAmount != 0)
        {
            Transform templateTask = Instantiate(singletTemplate.transform, taskContainer);
            singletaskTemplate sp = templateTask.GetComponent<singletaskTemplate>();
            sp.amount.text = "X " + enemybossAmount.ToString();
            sp.sprite.sprite = enemyboss.avatar;
            sp.gameObject.SetActive(true);
        }
        if (campAmount != 0)
        {
            Transform templateTask = Instantiate(singletTemplate.transform, taskContainer);
            singletaskTemplate sp = templateTask.GetComponent<singletaskTemplate>();
            sp.amount.text = "X " + enemybossAmount.ToString();
            sp.sprite.sprite = camp.avatar;
            sp.gameObject.SetActive(true);
        }

      



    }
    private void ResetTarget()
    {
        foreach (EnemyObject enemyObject in enemyObjects)
        {
            if(enemyObject != null) {  if (enemyObject.GetComponent<EnemyTurret>() ) {
            EnemyTurret enemyTurret = (EnemyTurret)enemyObject;
                enemyTurret.target = mainTank;

                    }
            else
            {
                if(enemyObject.GetComponent<enemyBot>())
                {
                    enemyBot enemyBota = (enemyBot)enemyObject;
                    enemyBota.target = mainTank;
                }
            }}
           
        }
    }
    private bool checkAliveComponent()
    {
        foreach(EnemyObject enemyObject in enemyObjects)
        {
            if (enemyObject != null)
                return false;
        }
        return true;
    }
}
