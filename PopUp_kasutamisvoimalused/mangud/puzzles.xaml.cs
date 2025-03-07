using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace secondMobileApp.PopUp_kasutamisvoimalused.mangud
{
    public partial class puzzles : ContentPage
    {
        private int _currentRiddleIndex = 0;
        private int _correctAnswers = 0;

        private List<(string riddle, string answer)> _riddles = new List<(string, string)>();

        public puzzles(int k)
        {
            InitializeComponent();
            LoadRiddlesFromFile();
            

        }
        private async void LoadRiddlesFromFile()
        {
            string fileName = "secondMobileApp.Resources.Raw.riddles.txt";
            Console.WriteLine(fileName);
            Debug.WriteLine("FilePath in puzzles: " + fileName);
            var assembly = Assembly.GetExecutingAssembly();

            using Stream stream = assembly.GetManifestResourceStream(fileName);
            if (stream == null)
            {
                Debug.WriteLine("Faili ei leitud!");
                riddleLabel.Text = "Viga: Faili ei leitud!";
                return;
            }


            string[] resources = assembly.GetManifestResourceNames();
            
            foreach(var resource in resources)
            {
                Debug.WriteLine(resource);
            }
            
            if (stream != null)
            {
                using StreamReader reader = new StreamReader(stream);
                string content = await reader.ReadToEndAsync();
   
                
                foreach (var line in content.Split('\n'))
                {
                    var parts = line.Split('|');
                    if (parts.Length == 2)
                    {
                        _riddles.Add((parts[0].Trim(), parts[1].Trim()));
                    }
                }
            }
            if (_riddles.Count > 0)
                ShowRiddle();
            else
                riddleLabel.Text = "Viga";
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
                riddleLabel.Text = $"Palju õnne! Olete kõik mõistatused täitnud. Tervitame, {UserInfo.UserName}!";
                feedbackLabel.Text = $"Teie õiged vastused: {_correctAnswers} {_riddles.Count}st";
                checkAnswerButton.IsEnabled = false; 
            }
        }

        private void OnCheckAnswerClicked(object sender, EventArgs e)
        {
            if (_currentRiddleIndex >= _riddles.Count)
            {
                Debug.WriteLine("Индекс выходит за пределы списка!");
                return;
            }

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
