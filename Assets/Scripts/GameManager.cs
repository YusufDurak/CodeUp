using UnityEngine;
using System.Collections.Generic;

public enum ActivityType
{
    FillInTheBlank,
    MultipleChoice,
    Matching,
    CodeAnalysis
}

[System.Serializable]
public class Activity
{
    public string question;
    public string[] options;
    public string correctAnswer;
    public string[] hints;
    public ActivityType type;
}

[System.Serializable]
public class Module
{
    public string title;
    public string description;
    public string introduction;
    public List<Activity> activities;
    public int starsRequired;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    public int currentModuleIndex = 0;
    public int currentActivityIndex = 0;
    public int totalStars = 0;

    [Header("Modules")]
    public List<Module> modules = new List<Module>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeModules();
    }

    private void InitializeModules()
    {
        // Module 1: Variables and Data Types
        Module variablesModule = new Module
        {
            title = "Variables and Data Types",
            description = "Learn about different types of variables in C#",
            introduction = "Variables are containers for storing data values. Let's learn about the different types!",
            activities = new List<Activity>
            {
                new Activity
                {
                    question = "What type of variable would you use to store someone's age?",
                    options = new string[] { "int", "float", "string", "bool" },
                    correctAnswer = "int",
                    hints = new string[] { "Think about whole numbers", "Age is always a whole number" },
                    type = ActivityType.MultipleChoice
                },
                new Activity
                {
                    question = "Complete the code: string name = \"___\";",
                    correctAnswer = "John",
                    hints = new string[] { "Try a common name", "The name should be in quotes" },
                    type = ActivityType.FillInTheBlank
                }
            },
            starsRequired = 0
        };

        // Module 2: Control Flow
        Module controlFlowModule = new Module
        {
            title = "Control Flow",
            description = "Learn about if statements and loops",
            introduction = "Control flow helps your program make decisions and repeat actions.",
            activities = new List<Activity>
            {
                new Activity
                {
                    question = "Which operator checks if two values are equal?",
                    options = new string[] { "=", "==", "!=", ">" },
                    correctAnswer = "==",
                    hints = new string[] { "It's two symbols", "Not the assignment operator" },
                    type = ActivityType.MultipleChoice
                }
            },
            starsRequired = 2
        };

        modules.Add(variablesModule);
        modules.Add(controlFlowModule);
    }

    public Module GetCurrentModule()
    {
        if (currentModuleIndex >= 0 && currentModuleIndex < modules.Count)
        {
            return modules[currentModuleIndex];
        }
        return null;
    }

    public Activity GetCurrentActivity()
    {
        Module currentModule = GetCurrentModule();
        if (currentModule != null && currentActivityIndex >= 0 && currentActivityIndex < currentModule.activities.Count)
        {
            return currentModule.activities[currentActivityIndex];
        }
        return null;
    }

    public void AddStars(int amount)
    {
        totalStars += amount;
        // Check if player can unlock new modules
        CheckModuleUnlocks();
    }

    private void CheckModuleUnlocks()
    {
        foreach (Module module in modules)
        {
            if (totalStars >= module.starsRequired)
            {
                // Module is unlocked
                // TODO: Update UI to show unlocked status
            }
        }
    }

    public bool CheckAnswer(string answer)
    {
        Activity currentActivity = GetCurrentActivity();
        if (currentActivity != null)
        {
            return answer.Trim().ToLower() == currentActivity.correctAnswer.Trim().ToLower();
        }
        return false;
    }

    public string GetHint()
    {
        Activity currentActivity = GetCurrentActivity();
        if (currentActivity != null && currentActivity.hints.Length > 0)
        {
            return currentActivity.hints[Random.Range(0, currentActivity.hints.Length)];
        }
        return "No hints available";
    }
} 