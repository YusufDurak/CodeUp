using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestion", menuName = "CodeUp/Question")]
public class QuestionData : ScriptableObject
{
    public string questionText;

    public QuestionType type;
    public string[] choices;  // For multiple choice or matching
    public int correctChoiceIndex; // For multiple choice
    public string correctTextAnswer; // For input-based questions

    public string[] leftItems;   // For matching
    public string[] rightItems;  // For matching

    public string aiFeedback;
}

public enum QuestionType
{
    MultipleChoice,
    TextInput,
    Matching
}
