using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class SceneSetup : MonoBehaviour
{
    private Canvas mainCanvas;
    private bool hasInitialized = false;
    private UIManager uiManager;

    private void Awake()
    {
        if (hasInitialized) return;
        hasInitialized = true;

        Debug.Log("Starting scene setup...");

        // Create essential Unity components first
        CreateCamera();
        CreateEventSystem();
        
        // Create managers first
        SetupGameManager();
        
        // Then create UI elements
        CreateCanvas();
        CreateMainMenu();
        CreateModuleSelection();
        CreateActivityPanel();
        CreateResultPanel();

        // Connect all UI elements to UIManager
        ConnectUIElements();

        Debug.Log("Scene setup completed!");
    }

    private void ConnectUIElements()
    {
        if (uiManager == null) return;

        // Connect Main Menu elements
        uiManager.mainMenuPanel = GameObject.Find("MainMenuPanel");
        uiManager.startButton = GameObject.Find("StartButton")?.GetComponent<Button>();
        uiManager.quitButton = GameObject.Find("QuitButton")?.GetComponent<Button>();

        // Connect Module Selection elements
        uiManager.moduleSelectionPanel = GameObject.Find("ModuleSelectionPanel");
        uiManager.moduleContainer = GameObject.Find("ModuleContainer")?.transform;
        uiManager.backToMainButton = GameObject.Find("BackButton")?.GetComponent<Button>();

        // Connect Activity Panel elements
        uiManager.activityPanel = GameObject.Find("ActivityPanel");
        uiManager.questionText = GameObject.Find("QuestionText")?.GetComponent<TextMeshProUGUI>();
        uiManager.multipleChoiceContainer = GameObject.Find("MultipleChoiceContainer");
        uiManager.fillInTheBlankContainer = GameObject.Find("FillInTheBlankContainer");
        uiManager.answerInput = GameObject.Find("AnswerInput")?.GetComponent<TMP_InputField>();
        uiManager.submitButton = GameObject.Find("SubmitButton")?.GetComponent<Button>();
        uiManager.hintButton = GameObject.Find("HintButton")?.GetComponent<Button>();
        uiManager.hintText = GameObject.Find("HintText")?.GetComponent<TextMeshProUGUI>();
        uiManager.starsText = GameObject.Find("StarsText")?.GetComponent<TextMeshProUGUI>();

        // Connect Result Panel elements
        uiManager.resultPanel = GameObject.Find("ResultPanel");
        uiManager.resultText = GameObject.Find("ResultText")?.GetComponent<TextMeshProUGUI>();
        uiManager.nextButton = GameObject.Find("NextButton")?.GetComponent<Button>();
        uiManager.retryButton = GameObject.Find("RetryButton")?.GetComponent<Button>();

        // Initialize the UI
        uiManager.SendMessage("Start", SendMessageOptions.DontRequireReceiver);
    }

    private void SetupGameManager()
    {
        Debug.Log("Setting up managers...");
        GameObject gameManager = new GameObject("GameManager");
        gameManager.AddComponent<GameManager>();
        uiManager = gameManager.AddComponent<UIManager>();
    }

    private void CreateCamera()
    {
        if (Camera.main != null) return;

        Debug.Log("Creating camera...");
        GameObject cameraObj = new GameObject("Main Camera");
        Camera camera = cameraObj.AddComponent<Camera>();
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color(0.1f, 0.1f, 0.1f);
        camera.orthographic = true;
        camera.orthographicSize = 5;
        cameraObj.tag = "MainCamera";
    }

    private void CreateEventSystem()
    {
        if (FindObjectOfType<EventSystem>() != null) return;

        Debug.Log("Creating event system...");
        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
    }

    private void CreateCanvas()
    {
        if (mainCanvas != null) return;

        Debug.Log("Creating canvas...");
        GameObject canvasObj = new GameObject("Canvas");
        mainCanvas = canvasObj.AddComponent<Canvas>();
        mainCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        
        canvasObj.AddComponent<GraphicRaycaster>();
        
        // Make sure the canvas is at the root level
        canvasObj.transform.SetParent(null);
    }

    private void CreateMainMenu()
    {
        GameObject mainMenu = CreatePanel("MainMenuPanel", new Vector2(0, 0), new Vector2(1, 1));
        mainMenu.transform.SetParent(mainCanvas.transform, false);

        // Title
        GameObject titleObj = CreateText("Title", "Code Up", 72);
        titleObj.transform.SetParent(mainMenu.transform, false);
        RectTransform titleRect = titleObj.GetComponent<RectTransform>();
        titleRect.anchoredPosition = new Vector2(0, 200);

        // Start Button
        GameObject startBtn = CreateButton("StartButton", "Start", new Vector2(0, 50));
        startBtn.transform.SetParent(mainMenu.transform, false);

        // Quit Button
        GameObject quitBtn = CreateButton("QuitButton", "Quit", new Vector2(0, -50));
        quitBtn.transform.SetParent(mainMenu.transform, false);
    }

    private void CreateModuleSelection()
    {
        GameObject modulePanel = CreatePanel("ModuleSelectionPanel", new Vector2(0, 0), new Vector2(1, 1));
        modulePanel.transform.SetParent(mainCanvas.transform, false);
        modulePanel.SetActive(false);

        // Title
        GameObject titleObj = CreateText("ModuleTitle", "Select Module", 48);
        titleObj.transform.SetParent(modulePanel.transform, false);
        RectTransform titleRect = titleObj.GetComponent<RectTransform>();
        titleRect.anchoredPosition = new Vector2(0, 300);

        // Module Container
        GameObject container = new GameObject("ModuleContainer");
        container.transform.SetParent(modulePanel.transform, false);
        RectTransform containerRect = container.AddComponent<RectTransform>();
        containerRect.anchorMin = new Vector2(0.1f, 0.1f);
        containerRect.anchorMax = new Vector2(0.9f, 0.9f);
        containerRect.offsetMin = Vector2.zero;
        containerRect.offsetMax = Vector2.zero;

        // Back Button
        GameObject backBtn = CreateButton("BackButton", "Back", new Vector2(-400, 300));
        backBtn.transform.SetParent(modulePanel.transform, false);
    }

    private void CreateActivityPanel()
    {
        GameObject activityPanel = CreatePanel("ActivityPanel", new Vector2(0, 0), new Vector2(1, 1));
        activityPanel.transform.SetParent(mainCanvas.transform, false);
        activityPanel.SetActive(false);

        // Question Text
        GameObject questionObj = CreateText("QuestionText", "Question", 36);
        questionObj.transform.SetParent(activityPanel.transform, false);
        RectTransform questionRect = questionObj.GetComponent<RectTransform>();
        questionRect.anchoredPosition = new Vector2(0, 200);

        // Multiple Choice Container
        GameObject mcContainer = CreatePanel("MultipleChoiceContainer", new Vector2(0, 0), new Vector2(0.8f, 0.4f));
        mcContainer.transform.SetParent(activityPanel.transform, false);
        mcContainer.SetActive(false);

        // Fill in the Blank Container
        GameObject fibContainer = CreatePanel("FillInTheBlankContainer", new Vector2(0, 0), new Vector2(0.8f, 0.2f));
        fibContainer.transform.SetParent(activityPanel.transform, false);
        fibContainer.SetActive(false);

        // Answer Input
        GameObject inputObj = new GameObject("AnswerInput");
        inputObj.transform.SetParent(fibContainer.transform, false);
        TMP_InputField input = inputObj.AddComponent<TMP_InputField>();
        RectTransform inputRect = inputObj.GetComponent<RectTransform>();
        inputRect.anchorMin = new Vector2(0.1f, 0.1f);
        inputRect.anchorMax = new Vector2(0.9f, 0.9f);
        inputRect.offsetMin = Vector2.zero;
        inputRect.offsetMax = Vector2.zero;

        // Submit Button
        GameObject submitBtn = CreateButton("SubmitButton", "Submit", new Vector2(0, -100));
        submitBtn.transform.SetParent(activityPanel.transform, false);

        // Hint Button
        GameObject hintBtn = CreateButton("HintButton", "Hint", new Vector2(200, -100));
        hintBtn.transform.SetParent(activityPanel.transform, false);

        // Hint Text
        GameObject hintText = CreateText("HintText", "", 24);
        hintText.transform.SetParent(activityPanel.transform, false);
        RectTransform hintRect = hintText.GetComponent<RectTransform>();
        hintRect.anchoredPosition = new Vector2(0, -150);

        // Stars Text
        GameObject starsText = CreateText("StarsText", "Stars: 0", 24);
        starsText.transform.SetParent(activityPanel.transform, false);
        RectTransform starsRect = starsText.GetComponent<RectTransform>();
        starsRect.anchoredPosition = new Vector2(-400, 300);
    }

    private void CreateResultPanel()
    {
        GameObject resultPanel = CreatePanel("ResultPanel", new Vector2(0, 0), new Vector2(0.5f, 0.3f));
        resultPanel.transform.SetParent(mainCanvas.transform, false);
        resultPanel.SetActive(false);

        // Result Text
        GameObject resultText = CreateText("ResultText", "", 36);
        resultText.transform.SetParent(resultPanel.transform, false);
        RectTransform resultRect = resultText.GetComponent<RectTransform>();
        resultRect.anchoredPosition = new Vector2(0, 50);

        // Next Button
        GameObject nextBtn = CreateButton("NextButton", "Next", new Vector2(0, -50));
        nextBtn.transform.SetParent(resultPanel.transform, false);

        // Retry Button
        GameObject retryBtn = CreateButton("RetryButton", "Retry", new Vector2(0, -50));
        retryBtn.transform.SetParent(resultPanel.transform, false);
    }

    private GameObject CreatePanel(string name, Vector2 anchorMin, Vector2 anchorMax)
    {
        GameObject panel = new GameObject(name);
        Image image = panel.AddComponent<Image>();
        image.color = new Color(0.2f, 0.2f, 0.2f, 0.9f);
        RectTransform rect = panel.GetComponent<RectTransform>();
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        return panel;
    }

    private GameObject CreateButton(string name, string text, Vector2 position)
    {
        GameObject buttonObj = new GameObject(name);
        Button button = buttonObj.AddComponent<Button>();
        Image image = buttonObj.AddComponent<Image>();
        image.color = new Color(0.2f, 0.6f, 1f);
        
        GameObject textObj = CreateText(name + "Text", text, 24);
        textObj.transform.SetParent(buttonObj.transform, false);
        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        RectTransform rect = buttonObj.GetComponent<RectTransform>();
        rect.anchoredPosition = position;
        rect.sizeDelta = new Vector2(160, 40);

        return buttonObj;
    }

    private GameObject CreateText(string name, string text, int fontSize)
    {
        GameObject textObj = new GameObject(name);
        TextMeshProUGUI tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = fontSize;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = Color.white;
        RectTransform rect = textObj.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(400, 100);
        return textObj;
    }
} 