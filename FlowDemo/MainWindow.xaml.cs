using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

namespace FlowDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CompositionTarget.Rendering += CompositionTarget_Rendering;
            stopwatch.Start();
            InitView();
        }
        Queue<Button> ReadyQueue = new Queue<Button>();
        List<Button> ShowBtn = new List<Button>();
        Button lastBtn;
        float speed = 20;
        private void InitView()
        {
            for (int i = 0; i < 7; i++)
            {
                Button button = new Button();
                button.Width = 50;
                button.Height = 60;
                button.Click += Button_Click;
                button.Content = i.ToString();
                button.MouseEnter += Button_MouseEnter;
                button.MouseLeave += Button_MouseLeave;
                TranslateTransform trans = new TranslateTransform();
                button.RenderTransform = trans;
                trans.Y = i * button.Height;
                if (trans.Y<=300)
                {
                    ShowBtn.Add(button);

                }
                else { ReadyQueue.Enqueue(button); }
                lastBtn = ShowBtn[ShowBtn.Count - 1];
                canvas.Children.Add(button);
            }
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            mouseEnter = false;
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseEnter = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            MessageBox.Show($"这是按钮{btn.Content}");
        }

        private Stopwatch stopwatch = new Stopwatch();
        private TimeSpan prevTime = TimeSpan.Zero;
        bool mouseEnter = false;
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            TimeSpan currentTime = this.stopwatch.Elapsed;
            double elapsedTime = (currentTime - this.prevTime).TotalSeconds;
            this.prevTime = currentTime;
            if (!mouseEnter)
            {
                foreach (var item in ShowBtn.ToArray())
                {
                    var trans = item.RenderTransform as TranslateTransform;
                    trans.Y -= speed * elapsedTime;
                    if (trans.Y <= -item.Height)
                    {
                        ShowBtn.Remove(item);
                        ReadyQueue.Enqueue(item);
                    }

                }
                var Lasttrans = lastBtn.RenderTransform as TranslateTransform;
                var Yvalue = Lasttrans.Y + lastBtn.Height;
                if (Yvalue > 300 && Yvalue < 310)
                {
                    var btn = ReadyQueue.Dequeue();
                    var trans1 = btn.RenderTransform as TranslateTransform;
                    trans1.Y = Yvalue;
                    ShowBtn.Add(btn);
                    lastBtn = btn;
                }

            }

        }
    }
}
