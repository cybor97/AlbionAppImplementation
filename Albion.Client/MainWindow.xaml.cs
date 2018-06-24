using Albion.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Albion.Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (CoursesLV.SelectedItem != null)
            {
                if (CoursesTab.IsSelected)
                    await NetworkTools.PostAsync("/Data/Course/RemoveCourse", $"ID={((Course)CoursesLV.SelectedItem).ID}");
                else if (MarksTab.IsSelected)
                    await NetworkTools.PostAsync("/Data/Mark/RemoveMark", $"ID={((Mark)MarksLV.SelectedItem).ID}");
                else if (StatementsTab.IsSelected)
                    await NetworkTools.PostAsync("/Data/Statement/RemoveStatement", $"ID={((Statement)StatementsLV.SelectedItem).ID}");

                UpdateData();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (CoursesTab.IsSelected)
            {
                CourseSettingContainer.Visibility = Visibility.Visible;
                CourseSettingContainer.DataContext = new Course();
            }
            else if (MarksTab.IsSelected)
            {
                MarkSettingContainer.Visibility = Visibility.Visible;
                MarkSettingContainer.DataContext = new Mark();
            }
        }

        async void UpdateData(bool resetSettingsContainers = true)
        {
            var accounts = (await NetworkTools.GetAsync<Account>("/Data/Account/GetAccounts"))/*.Where(c => c.AccessLevel == Account.AL_USER)*/;
            var courses = await NetworkTools.GetAsync<Course>("/Data/Course/GetCourses");
            var marks = await NetworkTools.GetAsync<Mark>("/Data/Mark/GetMarks");

            marks.ForEach(mark =>
            {
                mark.Student = accounts.Find(account => account.ID == mark.AccountID);
                mark.Course = courses.Find(course => course.ID == mark.CourseID);
            });


            CoursesLV.ItemsSource = CoursesCB.ItemsSource = courses;
            MarksLV.ItemsSource = marks;
            StatementsLV.ItemsSource = await NetworkTools.GetAsync<Mark>("/Data/Statement/GetStatements");
            StudentsCB.ItemsSource = accounts;

            CourseSettingContainer.Visibility = Visibility.Collapsed;
            MarkSettingContainer.Visibility = Visibility.Collapsed;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CourseSettingContainer.DataContext = new Course();
            MarkSettingContainer.DataContext = new Mark();
            UpdateData();
        }

        private void CoursesLV_Selected(object sender, RoutedEventArgs e)
        {
            CourseSettingContainer.Visibility = Visibility.Visible;
            CourseSettingContainer.DataContext = CoursesLV.SelectedItem;
        }

        private async void SaveCourseButton_Click(object sender, RoutedEventArgs e)
        {
            await NetworkTools.PostAsync("/Data/Course/SetCourse", CourseSettingContainer.DataContext);
            UpdateData();
        }

        private void MarksLV_Selected(object sender, SelectionChangedEventArgs e)
        {
            MarkSettingContainer.Visibility = Visibility.Visible;
            MarkSettingContainer.DataContext = MarksLV.SelectedItem;
        }

        private async void SaveMarkButton_Click(object sender, RoutedEventArgs e)
        {
            await NetworkTools.PostAsync("/Data/Mark/SetMark", MarkSettingContainer.DataContext);
            UpdateData();
        }

        private void StatementsLV_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            new StatementWindow().ShowDialog();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }
    }
}
