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

        deletePersonBtn = new Button { Text = "Kustuta inimene" };
        deletePersonBtn.Clicked += DeletePersonBtn_Clicked;

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
                 
                userPhoto,
                addPhotoBtn,
                addPersonBtn,
                showAllBtn,
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

        peopleList.Add((nameEntry.Text, phoneEntry.Text, emailEntry.Text, userPhoto.Source));
    }


    private bool isShowingAll = false;
    private void ShowAllBtn_Clicked(object sender, EventArgs e)
    {
        if (isShowingAll)
        {
            peopleSection.Clear();
            showAllBtn.Text = "Näita kõiki kasutajaid andmed";
            (Content as ScrollView).Content = mainLayout;
        }
        else
        {
            var peopleStack = new StackLayout { Spacing = 10 };

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
                deleteButton.Clicked += (s, args) =>
                {
                    // Найдём индекс элемента в списке
                    var index = peopleList.IndexOf(person);
                    if (index >= 0)
                    {
                        // Удаляем элемент из списка данных
                        peopleList.RemoveAt(index);

                        // Перезаполним секцию актуальными данными
                        peopleSection.Clear();

                        // Добавляем всех пользователей обратно в секцию
                        foreach (var p in peopleList)
                        {
                            peopleSection.Add(new ViewCell
                            {
                                View = new StackLayout
                                {
                                    Orientation = StackOrientation.Horizontal,
                                    Children = {
                        new Label { Text = p.Name },
                        new Label { Text = p.Phone }
                    }
                                }
                            });
                        }

                        // Обновляем TableView
                        tabelview.Root.Clear();

                        // Сначала добавляем форму для нового пользователя
                        tabelview.Root.Add(new TableSection("Lisa uus inimene") { nameEntry, phoneEntry, emailEntry });

                        // Затем добавляем обновлённую секцию с пользователями
                        tabelview.Root.Add(peopleSection);
                    }
                };





                var updateButton = new Button
                {
                    Text = "Uuenda",
                    FontSize = 18,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    VerticalOptions = LayoutOptions.Center
                };



                var frame = new Frame
                {
                    Content = new StackLayout
                    {
                        Children = { stackLayout, emailButton, messageButton, callButton, deleteButton },
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

            mainLayout.Children.Add(peopleStack);
            showAllBtn.Text = "Peida kõik kasutajad andmed";
        }
        isShowingAll = !isShowingAll;
    }







    private void DeletePersonBtn_Clicked(object sender, EventArgs e)
    {
        if (peopleList.Count > 0)
        {
            peopleList.RemoveAt(peopleList.Count - 1);
            peopleSection.RemoveAt(peopleSection.Count - 1);
        }
    }
}
