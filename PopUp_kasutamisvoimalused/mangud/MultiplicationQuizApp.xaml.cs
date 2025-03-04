using Microsoft.Maui.Controls;
using System;

namespace secondMobileApp.PopUp_kasutamisvoimalused.mangud
{
    public partial class MultiplicationQuizApp : ContentPage
    {
        private int _currentQuestionIndex = 0;
        private int _correctAnswers = 0;

        private string[] _questions = new string[]
        {
            "2 * 3 = ?",
            "4 * 5 = ?",
            "7 * 8 = ?",
            "9 * 6 = ?",
            "12 * 11 = ?"
        };

        private int[] _answers = new int[] { 6, 20, 56, 54, 132 };

        public MultiplicationQuizApp(int k)
        {
            InitializeComponent();
            UpdateGreeting(); 
        }

        private void UpdateGreeting()
        {
            if (!string.IsNullOrEmpty(UserInfo.UserName))
            {
                greetingLabel.Text = $"Tere, {UserInfo.UserName}!"; 
            }
            else
            {
                greetingLabel.Text = "Tere, kasutaja!"; 
            }
        }

        private async void OnStartQuizButtonClicked(object sender, EventArgs e)
        {
            _currentQuestionIndex = 0;
            _correctAnswers = 0;

            await AskQuestion();
        }

        private async System.Threading.Tasks.Task AskQuestion()
        {
            if (_currentQuestionIndex < _questions.Length)
            {
                var question = _questions[_currentQuestionIndex];
                var correctAnswer = _answers[_currentQuestionIndex];

                string result = await DisplayPromptAsync("Viktoriini küsimus", question, maxLength: 2);

                if (int.TryParse(result, out int userAnswer) && userAnswer == correctAnswer)
                {
                    _correctAnswers++;
                }

                _currentQuestionIndex++;
                await AskQuestion();
            }
            else
            {
                await DisplayAlert("Tulemused", $"Sa vastasid õigesti {_correctAnswers} küsimusele {_questions.Length} küsimusest.", "OK");
            }
        }
    }
}
