using Albion.Data;
using System.Windows;

namespace Albion.Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CourseSettingContainer.DataContext = new Course();
            UpdateData();
        }

        private void CoursesLV_Selected(object sender, RoutedEventArgs e)
        {
            CourseSettingContainer.Visibility = Visibility.Visible;
            CourseSettingContainer.DataContext = CoursesLV.SelectedItem;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CourseSettingContainer.Visibility = Visibility.Visible;
            CourseSettingContainer.DataContext = new Course();
        }

        private void CancelCourseButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private async void SaveCourseButton_Click(object sender, RoutedEventArgs e)
        {
            await NetworkTools.PostAsync("/Data/Course/SetCourse", CourseSettingContainer.DataContext);
            UpdateData();
        }

        private async void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (CoursesLV.SelectedItem != null)
            {
                await NetworkTools.PostAsync("/Data/Course/RemoveCourse", $"ID={((Course)CoursesLV.SelectedItem).ID}");
                UpdateData();
            }
        }

        async void UpdateData()
        {
            CoursesLV.ItemsSource = await NetworkTools.GetAsync<Course>("/Data/Course/GetCourses");
            CourseSettingContainer.Visibility = Visibility.Collapsed;
        }
    }
}
