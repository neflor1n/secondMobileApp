using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Text.RegularExpressions;

namespace secondMobileApp.Sobrade_KontakteAndmed;

public partial class kontaktid : ContentPage
{
    TableView tabelview;
    EntryCell nameEntry, phoneEntry, emailEntry;
    TableSection peopleSection;
    List<(string Name, string Phone, string Email, ImageSource Photo)> peopleList;
    Button addPersonBtn, showAllBtn, deletePersonBtn;
    Button sendEmailBtn, callBtn, addPhotoBtn;
    Image userPhoto;

    private StackLayout mainLayout;
    public kontaktid(int l)
    {
        peopleList = new List<(string, string, string, ImageSource)>();
        peopleList.Add(("testInimene", "+37258862265", "bsergachev@gmail.com", null));

        nameEntry = new EntryCell { Label = "Nimi:", Placeholder = "Sisesta inimese nimi", Keyboard = Keyboard.Default };
        phoneEntry = new EntryCell { Label = "Telefon:", Placeholder = "Sisesta telefon", Keyboard = Keyboard.Telephone };
        emailEntry = new EntryCell { Label = "Email:", Placeholder = "Sisesta email", Keyboard = Keyboard.Email };
        userPhoto = new Image { HeightRequest = 100, WidthRequest = 100 };

        addPhotoBtn = new Button { Text = "Lisa foto" };
        addPhotoBtn.Clicked += AddPhotoBtn_Clicked;

        addPersonBtn = new Button { Text = "Lisa inimene" };
        addPersonBtn.Clicked += AddPersonBtn_Clicked;

        showAllBtn = new Button { Text = "Näita kõiki kasutajaid" };
        showAllBtn.Clicked += ShowAllBtn_Clicked;

        peopleSection = new TableSection("Inimesed");

        
        tabelview = new TableView
        {
            Root = new TableRoot
            {
                new TableSection("Lisa uus inimene") { nameEntry, phoneEntry, emailEntry }
            },
            HeightRequest = 250
        };

        mainLayout = new StackLayout
        {
            Children =
            {
                addPhotoBtn,
                addPersonBtn,
                showAllBtn,
                userPhoto,
                tabelview
            }
        };


        Content = new ScrollView { Content = mainLayout };
    }


    bool IsValidPhoneNumber(string phoneNumber)
    {
        string pattern = @"^\+?[0-9\s\-\(\)]{7,20}$"; 
        return Regex.IsMatch(phoneNumber, pattern);
    }


    private async void AddPhotoBtn_Clicked(object sender, EventArgs e)
    {
        var photo = await MediaPicker.PickPhotoAsync();
        if (photo != null)
        {
            userPhoto.Source = ImageSource.FromFile(photo.FullPath);
        }
    }

    bool IsValidEmail(string email)
    {
        // Паттерны для различных доменов
        string gmailPattern = @"@gmail\.com$";
        string mailPattern = @"@mail\.ru$";
        string yahooPattern = @"@yahoo\.com$";
        string outlookPattern = @"@outlook\.com$";
        string protonmailPattern = @"@protonmail\.com$";
        string icloudPattern = @"@icloud\.com$";

        if (Regex.IsMatch(email, gmailPattern) ||
            Regex.IsMatch(email, mailPattern) ||
            Regex.IsMatch(email, yahooPattern) ||
            Regex.IsMatch(email, outlookPattern) ||
            Regex.IsMatch(email, protonmailPattern) ||
            Regex.IsMatch(email, icloudPattern))
        {
            return true;
        }

        return false;
    }


    private void AddPersonBtn_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(nameEntry.Text) || string.IsNullOrWhiteSpace(phoneEntry.Text))
        {
            DisplayAlert("Viga", "Nimi ja telefoninumber ei tohi olla tühjad", "OK");
            return;
        }

        if (!IsValidPhoneNumber(phoneEntry.Text))
        {
            DisplayAlert("Viga", "Sisestage kehtiv telefoninumber", "OK");
            return;
        }

        if (!IsValidEmail(emailEntry.Text))
        {
            DisplayAlert("Viga", "Sisestage kehtiv email address", "OK");
            return;
        }

        MainThread.BeginInvokeOnMainThread(() =>
        {
            peopleList.Add((nameEntry.Text, phoneEntry.Text, emailEntry.Text, userPhoto.Source));
            nameEntry.Text = string.Empty;
            emailEntry.Text = string.Empty;
            phoneEntry.Text = string.Empty;
        });
    }

    private async void AddOrUpdatePhoto_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;

        var person = (button.BindingContext as (string Name, string Phone, string Email, ImageSource Photo)?);
        if (person == null) return;

        string action = await DisplayActionSheet("Lisa foto", "Tühista", null, "Kasuta kaamerat", "Vali galeriist");

        FileResult photo = null;
        if (action == "Kasuta kaamerat")
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                photo = await MediaPicker.CapturePhotoAsync();
            }
        }
        else if (action == "Vali galeriist")
        {
            photo = await MediaPicker.PickPhotoAsync();
        }

        if (photo != null)
        {
            var newPhoto = ImageSource.FromFile(photo.FullPath);

            // Обновление фото у пользователя
            int index = peopleList.FindIndex(p => p.Phone == person.Value.Phone);
            if (index >= 0)
            {
                peopleList[index] = (person.Value.Name, person.Value.Phone, person.Value.Email, newPhoto);
            }

            // Обновляем UI 
            MainThread.BeginInvokeOnMainThread(() =>
            {
                foreach (var frame in ((mainLayout.Children.Last() as StackLayout)?.Children.Cast<View>() ?? new List<View>()))
                {
                    if (frame is Frame userFrame && userFrame.Content is StackLayout userStack)
                    {
                        var img = userStack.Children[0] as Image;
                        var nameLabel = (userStack.Children[1] as StackLayout)?.Children[0] as Label;

                        if (nameLabel?.Text == person.Value.Name)
                        {
                            img.Source = newPhoto;
                            break;
                        }
                    }
                }
            });
        }
    }


    private bool isShowingAll = false;
    private void ShowAllBtn_Clicked(object sender, EventArgs e)
    {
        if (isShowingAll)
        {
            // Убираю всех пользователей из mainLayout
            var peopleStack = mainLayout.Children.FirstOrDefault(x => x is StackLayout && ((StackLayout)x).Children.FirstOrDefault() is Frame);
            if (peopleStack != null)
            {
                mainLayout.Children.Remove(peopleStack);  
            }

            // Восстанавливаю первоначальный интерфейс 
            if (mainLayout.Children.Contains(tabelview))
            {
                mainLayout.Children.Remove(tabelview);  // Убираб tabelview из родителя, если оно там уже есть
            }

            mainLayout.Children.Add(tabelview);  

            showAllBtn.Text = "Näita kõiki kasutajaid andmed";
        }
        else
        {
            // Создаю StackLayout для отображения всех пользователей
            var peopleStack = new StackLayout { Spacing = 10 };

            // Добавляю каждого пользователя
            foreach (var person in peopleList)
            {
                var stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding = new Thickness(10),
                    Spacing = 10,
                    Children =
                {
                    new Image { Source = person.Photo, HeightRequest = 50, WidthRequest = 50 },
                    new StackLayout
                    {
                        Children =
                        {
                            new Label { Text = person.Name, FontAttributes = FontAttributes.Bold, FontSize = 16 },
                            new Label { Text = $"Tel: {person.Phone}" },
                            new Label { Text = $"Email: {person.Email}"}
                        }
                    }
                }
                };

                var emailButton = new Button
                {
                    Text = "✉ Lisa Email",
                    FontSize = 14
                };
                emailButton.Clicked += (s, args) => Launcher.OpenAsync($"mailto:{person.Email}");

                var messageButton = new Button
                {
                    Text = "💬 Lisa sõnum",
                    FontSize = 14
                };
                messageButton.Clicked += async (s, args) =>
                {
                    var smsUri = $"sms:{person.Phone}";
                    await Launcher.OpenAsync(smsUri);
                };

                var addPhotoButton = new Button
                {
                    Text = "📸 Lisa foto",
                    FontSize = 14
                };
                addPhotoButton.BindingContext = person;
                addPhotoButton.Clicked += AddOrUpdatePhoto_Clicked;

                var callButton = new Button
                {
                    Text = "📞 Helista",
                    FontSize = 14
                };
                callButton.Clicked += async (s, args) =>
                {
                    var telUri = $"tel:{person.Phone}";
                    await Launcher.OpenAsync(telUri);
                };

                var deleteButton = new Button
                {
                    Text = "❌",
                    FontSize = 18,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    VerticalOptions = LayoutOptions.Center
                };
                /*
                deleteButton.Clicked += (s, args) =>
                {
                    // Найдем индекс элемента в списке
                    var index = peopleList.IndexOf(person);
                    if (index >= 0)
                    {
                        peopleList.RemoveAt(index);

                        // Удаляем соответствующие записи
                        var cellsToRemove = peopleSection.Where((value, idx) => idx == index).ToList();
                        foreach (var cell in cellsToRemove)
                        {
                            peopleSection.Remove(cell);  // Удаляем только ячейку с пользователем
                        }

                        // Обновляем TableView только для пользователей, не касаясь формы
                        tabelview.Root.Clear();
                        tabelview.Root.Add(new TableSection("Lisa uus inimene") { nameEntry, phoneEntry, emailEntry }); // Форма для добавления пользователя
                        tabelview.Root.Add(peopleSection); // Секция с обновленными пользователями
                    }
                };
                */
                var frame = new Frame
                {
                    Content = new StackLayout
                    {
                        Children = { stackLayout, emailButton, messageButton, callButton, addPhotoButton, deleteButton },
                        Spacing = 5
                    },
                    Padding = new Thickness(10),
                    Margin = new Thickness(10, 5),
                    BorderColor = Colors.Gray,
                    CornerRadius = 10,
                    HasShadow = true
                };

                peopleStack.Children.Add(frame);
            }

            // Добавляем новый список пользователей в mainLayout
            mainLayout.Children.Add(peopleStack);
            showAllBtn.Text = "Peida kõik kasutajad andmed";
        }
        isShowingAll = !isShowingAll;
    }
}
