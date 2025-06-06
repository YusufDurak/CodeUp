using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Lesson
{
    public string title;
    public string description;
    public string content;
    public string[] exampleCode;
}

public class LessonManager : MonoBehaviour
{
    public List<Lesson> lessons = new List<Lesson>();
    private int currentLessonIndex = 0;

    private void Start()
    {
        InitializeLessons();
    }

    private void InitializeLessons()
    {
        // Basic C# Concepts
        lessons.Add(new Lesson
        {
            title = "Variables and Data Types",
            description = "Learn about different types of variables in C#",
            content = "In C#, variables are containers for storing data values. The most common types are:\n" +
                     "- int: for whole numbers\n" +
                     "- float: for decimal numbers\n" +
                     "- string: for text\n" +
                     "- bool: for true/false values",
            exampleCode = new string[]
            {
                "int age = 25;",
                "float height = 1.75f;",
                "string name = \"John\";",
                "bool isStudent = true;"
            }
        });

        lessons.Add(new Lesson
        {
            title = "If Statements",
            description = "Learn how to make decisions in your code",
            content = "If statements allow your code to make decisions based on conditions.\n" +
                     "They use comparison operators like ==, !=, <, >, <=, >=",
            exampleCode = new string[]
            {
                "int age = 18;",
                "if (age >= 18) {",
                "    Debug.Log(\"You are an adult\");",
                "} else {",
                "    Debug.Log(\"You are a minor\");",
                "}"
            }
        });
    }

    public Lesson GetCurrentLesson()
    {
        if (currentLessonIndex >= 0 && currentLessonIndex < lessons.Count)
        {
            return lessons[currentLessonIndex];
        }
        return null;
    }

    public void NextLesson()
    {
        if (currentLessonIndex < lessons.Count - 1)
        {
            currentLessonIndex++;
        }
    }

    public void PreviousLesson()
    {
        if (currentLessonIndex > 0)
        {
            currentLessonIndex--;
        }
    }
} 