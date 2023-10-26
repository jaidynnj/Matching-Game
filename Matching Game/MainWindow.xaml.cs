using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Matching_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        TextBlock lastTextBlockClicked;
        bool findingMatch = false;
        DispatcherTimer timer = new DispatcherTimer();
        int tenthOfSecondsElapsed;
        int matchesFound;

        int rightPicks = 0;
        int wrongPicks = 0;




        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Not Looking for match yet
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            //Looking for match and found one
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
                matchesFound ++;
                rightPicks ++;
                updateStats();
            }
            //Looking for match, didn't find one'
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
                wrongPicks ++;
                updateStats();
            }
        }
    
        private void updateStats()
        {
            textRight.Text = rightPicks.ToString();
            textWrong.Text = wrongPicks.ToString();
            texPercent.Text = (rightPicks *100 / (rightPicks + wrongPicks)).ToString();
        }
                


        
    
        
    
    public MainWindow()
    {

    InitializeComponent();

    timer.Interval = TimeSpan.FromSeconds(.1);
    timer.Tick += Timer_Tick;
     SetUpGame();
    }
        private void Timer_Tick(object? sender, EventArgs e) 
        {
            tenthOfSecondsElapsed++;
            timerTextBlock.Text = (tenthOfSecondsElapsed / 10f).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timerTextBlock.Text = timerTextBlock.Text = "-Play Again?";
            }

        }

  

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
              "😺", "😺",
              "🦁", "🦁",
              "🐴", "🐴",
              "🐶", "🐶",
              "🦊", "🦊",
              "🐼", "🐼",
              "🦈", "🦈",
              "🦧", "🦧",
              
              
            };
            Random random = new Random();
            foreach (TextBlock textblock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textblock.Name != "textTextBlock" && textblock.Name != "textRight"
                    && textblock.Name "textWrong" && textblock.Name !)
                textblock.Visibility = Visibility.Visible;
                if (textblock.Name != "timerTextBlock")
                {
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textblock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
            }
            timer.Start();
            tenthOfSecondsElapsed = 0;
            matchesFound=0;
        }
        private void timerTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(matchesFound == 8)
            {
                SetUpGame();
            }
        }

    }
}
