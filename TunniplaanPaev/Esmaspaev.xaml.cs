namespace secondMobileApp.TunniplaanPaev
{
    public partial class Esmaspaev : ContentPage
    {
        public Esmaspaev(int k)
        {
            InitializeComponent();

            var schedule = new Schedule();

            // Добавляем уроки в расписание
            schedule.AddLesson("Keel ja kirjandus, Eesti kirjandus", "Maarand", "B117", new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0));  
            schedule.AddLesson("Свободный", "", "", TimeSpan.Zero, TimeSpan.Zero);  
            schedule.AddLesson("Свободный", "", "", TimeSpan.Zero, TimeSpan.Zero);  
            schedule.AddLesson("Mobiilirakendused", "Oleinik", "E07", new TimeSpan(11, 15, 0), new TimeSpan(12, 45, 0));  
            schedule.AddLesson("Võrgurakendused", "Podkopaev", "A123", new TimeSpan(13, 0, 0), new TimeSpan(15, 0, 0));  

            // Отображаем расписание на странице
            DisplaySchedule(schedule);
        }

        private void DisplaySchedule(Schedule schedule)
        {
            // Добавление меток в Grid, корректно указывая позиции (столбец и строку)
            scheduleGrid.Children.Add(new Label { Text = "Урок", HorizontalTextAlignment = TextAlignment.Center }, 0, 0);
            scheduleGrid.Children.Add(new Label { Text = "Учитель", HorizontalTextAlignment = TextAlignment.Center }, 1, 0);
            scheduleGrid.Children.Add(new Label { Text = "Класс", HorizontalTextAlignment = TextAlignment.Center }, 2, 0);
            scheduleGrid.Children.Add(new Label { Text = "Время", HorizontalTextAlignment = TextAlignment.Center }, 3, 0);

            // Добавление данных для каждого урока
            int row = 1;
            foreach (var lesson in schedule.Lessons)
            {
                // Добавляем информацию о предмете, учителе и классе
                scheduleGrid.Children.Add(new Label { Text = lesson.Subject }, 0, row);
                scheduleGrid.Children.Add(new Label { Text = lesson.Teacher }, 1, row);
                scheduleGrid.Children.Add(new Label { Text = lesson.Class }, 2, row);

                // Если урок имеет время, отображаем его
                if (lesson.StartTime != TimeSpan.Zero && lesson.EndTime != TimeSpan.Zero)
                {
                    scheduleGrid.Children.Add(new Label { Text = $"{lesson.StartTime:hh\\:mm} - {lesson.EndTime:hh\\:mm}" }, 3, row);
                }
                else
                {
                    // Для свободных уроков ставим знак "—"
                    scheduleGrid.Children.Add(new Label { Text = "—" }, 3, row);
                }

                row++;
            }
        }
    }
}
