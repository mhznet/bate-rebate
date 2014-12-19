using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BateGameManager : MonoBehaviour
{
    private GameObject ball;
    private GameObject bg;
    private GameObject touchRight;
    private GameObject touchLeft;
    private GameObject paddleRight;
    private GameObject paddleLeft;
    private GameObject pauseBtn;
    private GameObject scoreLeftOver;
    private GameObject scoreRightOver;

    private GameObject pauseScene;

    private string pauseScenePreFabUrl = "preFabs/BatePauseScenePreFab";
    private string pauseBtnAssetUrl = "Scenes/Game/pausebt";
    private string paddleRightAssetUrl = "Scenes/Game/gamePaddle2";
    private string paddleLeftAssetUrl = "Scenes/Game/gamePaddle";
    private string bgAsset = "Scenes/Game/gameBg";
    private string ballAsset = "Scenes/Game/gameBall";
    private string hitBoxAsset = "Scenes/Game/gameHitOk";

    public int playerNumber = 1;
    private BateMain main;
    private GameObject wallNorth;
    private GameObject wallSouth;
    private GameObject wallRight;
    private GameObject wallLeft;
    public void Start()
    {
        StartScene();
    }
    public void StartScene(BateMain masterclass = null, int playerNum = 1)
    {
        main = masterclass;
        playerNumber = playerNum;
        VerifyScreenRes();
        CreateBackGround();
        CreateWalls();
        CreateUI();
        CreateBall();
        CreatePaddles();
        CreateTouches();

        //wallNorth = GameObject.Find("wallNorth");
        //wallSouth = GameObject.Find("wallSouth");
        //wallLeft = GameObject.Find("wallLeft");
        //wallRight = GameObject.Find("wallRight");
    }
    private void VerifyScreenRes()
    {
        if (main == null)
        {
            bgAsset = this.AddPlatformAndQualityToUrl(bgAsset);
            pauseBtnAssetUrl = this.AddPlatformAndQualityToUrl(pauseBtnAssetUrl);
            ballAsset = this.AddPlatformAndQualityToUrl(ballAsset);
            hitBoxAsset = this.AddPlatformAndQualityToUrl(hitBoxAsset);
            paddleRightAssetUrl = this.AddPlatformAndQualityToUrl(paddleRightAssetUrl);
            paddleLeftAssetUrl = this.AddPlatformAndQualityToUrl(paddleLeftAssetUrl);
        }
        else
        {
            bgAsset = main.AddPlatformAndQualityToUrl(bgAsset);
            pauseBtnAssetUrl = main.AddPlatformAndQualityToUrl(pauseBtnAssetUrl);
            ballAsset = main.AddPlatformAndQualityToUrl(ballAsset);
            hitBoxAsset = main.AddPlatformAndQualityToUrl(hitBoxAsset);
            paddleRightAssetUrl = main.AddPlatformAndQualityToUrl(paddleRightAssetUrl);
            paddleLeftAssetUrl = main.AddPlatformAndQualityToUrl(paddleLeftAssetUrl);
        }
    }
    public string AddPlatformAndQualityToUrl(string url, string platform = "IOS", string quality = "High")
    {
        char delim = '/';
        url = platform + delim + quality + delim + url;
        return url;
    }
    private void CreateWalls()
    {
        Vector3 bgSize = GetExtents();
        Vector3 bgPos = GetPos();

        wallNorth = new GameObject();
        wallNorth.name = "wallU";
        wallNorth.AddComponent<BoxCollider2D>();
        wallNorth.transform.localScale = new Vector3(bgSize.x*2f, 1f, 1f);
        wallNorth.transform.position = new Vector3(bgPos.x, bgPos.y + bgSize.y + wallNorth.transform.collider2D.bounds.extents.y, 0f);

        wallSouth = new GameObject();
        wallSouth.name = "wallD";
        wallSouth.AddComponent<BoxCollider2D>();
        wallSouth.transform.localScale = new Vector3(bgSize.x * 2f, 1f, 1f);
        wallSouth.transform.position = new Vector3(bgPos.x, bgPos.y - bgSize.y - wallSouth.transform.collider2D.bounds.extents.y, 0f);

        wallLeft = new GameObject();
        wallLeft.name = "wallL";
        wallLeft.AddComponent<BoxCollider2D>();
        wallLeft.transform.localScale = new Vector3(1f, bgSize.y*2f, 1f);
        wallLeft.transform.position = new Vector3(bgPos.x - bgSize.x - wallLeft.transform.collider2D.bounds.extents.x, bgPos.y, 0f);

        wallRight = new GameObject();
        wallRight.name = "wallR";
        wallRight.AddComponent<BoxCollider2D>();
        wallRight.transform.localScale = new Vector3(1f, bgSize.y * 2f, 1f);
        wallRight.transform.position = new Vector3(bgPos.x + bgSize.x + wallRight.transform.collider2D.bounds.extents.x, bgPos.y, 0f);
    }
    private void CreateBackGround()
    {
        bg = new GameObject();
        bg.name = "background";
        bg.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(bgAsset);
        Debug.Log(GetExtents());
        Debug.Log(GetPos());
        bg.transform.position = new Vector3(0f, 0f, 10);
    }
    private Vector3 GetExtents()
    {
        return bg.GetComponent<SpriteRenderer>().sprite.bounds.extents;
    }
    private Vector3 GetPos()
    {
        return bg.transform.position;
    }
    private void CreateUI()
    {
        scoreLeftOver = GameObject.Find("scoreLeftOver");
        scoreRightOver = GameObject.Find("scoreRightOver");

        pauseBtn = GameObject.Find("pausebtn");
        pauseBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>(pauseBtnAssetUrl);
        //Debug.Log(pauseBtn.GetComponent<Image>().sprite.bounds.extents.y);
        //pauseBtn.transform.position = new Vector3(GetPos().x, GetPos().y - GetExtents().y /*+ pauseBtn.GetComponent<Image>().sprite.bounds.extents.y*/,0f);
        pauseBtn.GetComponent<Button>().onClick.AddListener(OnClickPause);
        if (main !=null)
        {
            scoreLeftOver.transform.SetParent(main.uiCanvas.transform);
            scoreRightOver.transform.SetParent(main.uiCanvas.transform);
            pauseBtn.transform.SetParent(main.uiCanvas.transform);
        }
    }
    public void GetReadyToDestroy()
    {
        if (main != null)
        {
            pauseBtn.transform.SetParent(main.gameScene.transform);
            scoreRightOver.transform.SetParent(main.gameScene.transform);
            scoreLeftOver.transform.SetParent(main.gameScene.transform);
        }
    }
    private void CreateBall()
    {
        ball = new GameObject();
        ball.name = "ball";
        ball.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(ballAsset);
        ball.AddComponent<CircleCollider2D>();
        ball.AddComponent<Rigidbody2D>().mass = 0.00001f;
        ball.GetComponent<Rigidbody2D>().gravityScale = 0f;
        ball.collider2D.sharedMaterial = Resources.Load<PhysicsMaterial2D>("Materials/ballMaterial");
        ball.transform.position = new Vector3(GetPos().x, GetPos().y, 0);
        ball.AddComponent<BallBehaviour>();
        ball.GetComponent<BallBehaviour>().scoreLeftOver = scoreLeftOver;
        ball.GetComponent<BallBehaviour>().scoreRightOver = scoreRightOver;
    }
    private void CreateTouches()
    {
        touchLeft = new GameObject();
        touchRight = new GameObject();
        touchLeft.name = "touchBoxLeft";
        touchRight.name = "touchBoxRight";
        touchLeft.AddComponent<BoxCollider2D>().isTrigger = true;
        touchRight.AddComponent<BoxCollider2D>().isTrigger = true;

        touchRight.transform.localScale = new Vector3(GetExtents().x * 0.7f, GetExtents().y * 2f, 1f);
        touchRight.transform.position = new Vector3(GetPos().x + GetExtents().x - touchRight.collider2D.bounds.extents.x, GetPos().y, -9f);

        touchLeft.transform.localScale = new Vector3(GetExtents().x * 0.7f, GetExtents().y * 2f, 1f);
        touchLeft.transform.position = new Vector3(GetPos().x - GetExtents().x + touchLeft.collider2D.bounds.extents.x, GetPos().y, -9f);

        touchRight.AddComponent<PaddleBehaviour>();
        touchRight.GetComponent<PaddleBehaviour>().enableTouch = (playerNumber == 2);
        touchRight.GetComponent<PaddleBehaviour>().paddleAsset = paddleRight;
        touchRight.GetComponent<PaddleBehaviour>().ball = ball;
        touchLeft.AddComponent<PaddleBehaviour>();
        touchLeft.GetComponent<PaddleBehaviour>().enableTouch = true;
        touchLeft.GetComponent<PaddleBehaviour>().paddleAsset = paddleLeft;
        touchLeft.GetComponent<PaddleBehaviour>().ball = ball;
    }
    private void CreatePaddles()
    {
        paddleRight = new GameObject();
        paddleLeft = new GameObject();
        paddleRight.name = "paddleRightAsset";
        paddleLeft.name = "paddleLeftAsset";
        paddleRight.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(paddleRightAssetUrl);
        paddleLeft.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(paddleLeftAssetUrl);
        paddleRight.AddComponent<BoxCollider2D>();
        paddleLeft.AddComponent<BoxCollider2D>();
        paddleRight.AddComponent<Rigidbody2D>().mass = 100000f;
        paddleRight.GetComponent<Rigidbody2D>().fixedAngle = true;
        paddleRight.GetComponent<Rigidbody2D>().gravityScale = 0f;
        paddleLeft.AddComponent<Rigidbody2D>().mass = 100000f;
        paddleLeft.GetComponent<Rigidbody2D>().fixedAngle = true;
        paddleLeft.GetComponent<Rigidbody2D>().gravityScale = 0f;
        paddleRight.transform.position = new Vector3(GetPos().x + GetExtents().x - paddleRight.collider2D.bounds.extents.x * 3f, GetPos().y, 9f);
        paddleLeft.transform.position = new Vector3(GetPos().x - GetExtents().x + paddleLeft.collider2D.bounds.extents.x * 3f, GetPos().y, 9f);
    }
    public void OnClickPause()
    {
        ball.GetComponent<BallBehaviour>().Pause();
        paddleLeft.GetComponent<PaddleBehaviour>().Pause();
        paddleRight.GetComponent<PaddleBehaviour>().Pause();

        /*Application.LoadLevel("sceneName");*/
        if (pauseScene == null)
        {
            pauseScene = Resources.Load<GameObject>(pauseScenePreFabUrl);
            Instantiate(pauseScene);
        }
        pauseScene.transform.position = new Vector3(GetPos().x, GetPos().y, 1f);
    }
    public void OnClickUnPause()
    {
        ball.GetComponent<BallBehaviour>().Pause();
        paddleLeft.GetComponent<PaddleBehaviour>().Pause();
        paddleRight.GetComponent<PaddleBehaviour>().Pause();

        pauseScene.SetActive(false);
    }
}
