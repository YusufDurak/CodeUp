using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject moduleSelectionPanel;
    public GameObject activityPanel;
    public GameObject resultPanel;

    [Header("Main Menu")]
    public Button startButton;
    public Button quitButton;

    [Header("Module Selection")]
    public Transform moduleContainer;
    public GameObject moduleButtonPrefab;
    public Button backToMainButton;

    [Header("Activity")]
    public TextMeshProUGUI questionText;
    public GameObject multipleChoiceContainer;
    public GameObject fillInTheBlankContainer;
    public TMP_InputField answerInput;
    public Button submitButton;
    public Button hintButton;
    public TextMeshProUGUI hintText;
    public TextMeshProUGUI starsText;

    [Header("Result")]
    public TextMeshProUGUI resultText;
    public Button nextButton;
    public Button retryButton;

    private void Start()
    {
        InitializeButtons();
        UpdateStarsDisplay();
        ShowMainMenu();
    }

    private void InitializeButtons()
    {
        if (startButton != null)
            startButton.onClick.AddListener(ShowModuleSelection);
        
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitApplication);

        if (backToMainButton != null)
            backToMainButton.onClick.AddListener(ShowMainMenu);

        if (submitButton != null)
            submitButton.onClick.AddListener(SubmitAnswer);

        if (hintButton != null)
            hintButton.onClick.AddListener(ShowHint);

        if (nextButton != null)
            nextButton.onClick.AddListener(NextActivity);

        if (retryButton != null)
            retryButton.onClick.AddListener(RetryActivity);
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        moduleSelectionPanel.SetActive(false);
        activityPanel.SetActive(false);
        resultPanel.SetActive(false);
    }

    public void ShowModuleSelection()
    {
        mainMenuPanel.SetActive(false);
        moduleSelectionPanel.SetActive(true);
        activityPanel.SetActive(false);
        resultPanel.SetActive(false);
        PopulateModuleButtons();
    }

    public void ShowActivity()
    {
        mainMenuPanel.SetActive(false);
        moduleSelectionPanel.SetActive(false);
        activityPanel.SetActive(true);
        resultPanel.SetActive(false);
        DisplayCurrentActivity();
    }

    private void PopulateModuleButtons()
    {
        // Clear existing buttons
        foreach (Transform child in moduleContainer)
        {
            Destroy(child.gameObject);
        }

        // Create new buttons for each module
        foreach (Module module in GameManager.Instance.modules)
        {
            GameObject buttonObj = Instantiate(moduleButtonPrefab, moduleContainer);
            Button button = buttonObj.GetComponent<Button>();
            TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            
            if (buttonText != null)
            {
                buttonText.text = module.title;
            }

            if (button != null)
            {
                button.onClick.AddListener(() => SelectModule(module));
                button.interactable = GameManager.Instance.totalStars >= module.starsRequired;
            }
        }
    }

    private void SelectModule(Module module)
    {
        GameManager.Instance.currentModuleIndex = GameManager.Instance.modules.IndexOf(module);
        GameManager.Instance.currentActivityIndex = 0;
        ShowActivity();
    }

    private void DisplayCurrentActivity()
    {
        Activity currentActivity = GameManager.Instance.GetCurrentActivity();
        if (currentActivity != null)
        {
            questionText.text = currentActivity.question;

            // Show appropriate UI based on activity type
            multipleChoiceContainer.SetActive(currentActivity.type == ActivityType.MultipleChoice);
            fillInTheBlankContainer.SetActive(currentActivity.type == ActivityType.FillInTheBlank);

            if (currentActivity.type == ActivityType.MultipleChoice)
            {
                // Populate multiple choice options
                // TODO: Implement multiple choice UI
            }
        }
    }

    private void SubmitAnswer()
    {
        string answer = answerInput.text;
        bool isCorrect = GameManager.Instance.CheckAnswer(answer);

        if (isCorrect)
        {
            GameManager.Instance.AddStars(1);
            UpdateStarsDisplay();
            ShowResult(true);
        }
        else
        {
            ShowResult(false);
        }
    }

    private void ShowResult(bool isCorrect)
    {
        resultPanel.SetActive(true);
        resultText.text = isCorrect ? "Correct! +1 Star" : "Try again!";
        nextButton.gameObject.SetActive(isCorrect);
        retryButton.gameObject.SetActive(!isCorrect);
    }

    private void ShowHint()
    {
        hintText.text = GameManager.Instance.GetHint();
    }

    private void NextActivity()
    {
        GameManager.Instance.currentActivityIndex++;
        if (GameManager.Instance.GetCurrentActivity() != null)
        {
            ShowActivity();
        }
        else
        {
            ShowModuleSelection();
        }
    }

    private void RetryActivity()
    {
        resultPanel.SetActive(false);
        answerInput.text = "";
    }

    private void UpdateStarsDisplay()
    {
        starsText.text = $"Stars: {GameManager.Instance.totalStars}";
    }

    public void QuitApplication()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
} 