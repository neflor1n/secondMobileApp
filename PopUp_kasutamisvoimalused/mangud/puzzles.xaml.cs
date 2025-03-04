using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace secondMobileApp.PopUp_kasutamisvoimalused.mangud
{
    public partial class puzzles : ContentPage
    {
        private int _currentRiddleIndex = 0;
        private int _correctAnswers = 0;

        private List<(string riddle, string answer)> _riddles = new List<(string, string)>
        {
            ("Mis on alati ees, aga seda ei ole näha?", "Tulevik"),
            ("Kellel on käed, aga pole jalgu? ", "Kell"),
            ("Mida saab kinni püüda, aga ei saa visata?", "Külm"),
            ("Mis läheb ära, aga tuleb alati tagasi?", "Aeg"),
            ("Milline kuu on kõige lühem?", "Veebruar")
        };

        public puzzles(int k)
        {
            InitializeComponent();
            ShowRiddle();
        }

        private void ShowRiddle()
        {
            if (_currentRiddleIndex < _riddles.Count)
            {
                riddleLabel.Text = _riddles[_currentRiddleIndex].riddle;
                feedbackLabel.Text = "";
                answerEntry.Text = "";
            }
            else
            {
                riddleLabel.Text = "Palju õnne! Olete kõik mõistatused täitnud.";
                feedbackLabel.Text = $"Teie õiged vastused: {_correctAnswers} {_riddles.Count}st";
                checkAnswerButton.IsEnabled = false; 
            }
        }

        // Обработчик нажатия на кнопку "Проверить"
        private void OnCheckAnswerClicked(object sender, EventArgs e)
        {
            string userAnswer = answerEntry.Text?.Trim();

            if (!string.IsNullOrEmpty(userAnswer))
            {
                string correctAnswer = _riddles[_currentRiddleIndex].answer;

                if (userAnswer.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase))
                {
                    _correctAnswers++;
                    feedbackLabel.Text = "Õige vastus!";
                }
                else
                {
                    feedbackLabel.Text = $"Vale vastus. Õige vastus: {correctAnswer}";
                }

                _currentRiddleIndex++;
                ShowRiddle();
            }
            else
            {
                feedbackLabel.Text = "Palun sisestage oma vastus!";
            }
        }
    }
}
