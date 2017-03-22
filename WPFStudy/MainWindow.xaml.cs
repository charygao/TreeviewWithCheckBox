using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace WPFStudy
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			this.InitializeComponent();

			// 在此点下面插入创建对象所需的代码。
		}

		private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			// 在此处添加事件处理程序实现。
        //    <Storyboard x:Key="Storyboard1">
        //    <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)" Storyboard.TargetName="border">
        //        <EasingDoubleKeyFrame KeyTime="0:0:1" Value="103"/>
        //    </DoubleAnimationUsingKeyFrames>
        //    <DoubleAnimationUsingKeyFrames Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)" Storyboard.TargetName="border">
        //        <EasingDoubleKeyFrame KeyTime="0:0:1" Value="100"/>
        //    </DoubleAnimationUsingKeyFrames>
        //</Storyboard>

            this.border.RenderTransform = new TranslateTransform();

            this.border.Name = "border1";
            NameScope.SetNameScope(this, new NameScope());
            this.RegisterName(this.border.Name, this.border);

            DoubleAnimation xAnimation = new DoubleAnimation();
            xAnimation.From = 0;
            xAnimation.To = -1100;
            xAnimation.Duration = new Duration(TimeSpan.FromSeconds(30));

            DependencyProperty[] propertyChain = new DependencyProperty[]
            {
                Image.RenderTransformProperty,
                TranslateTransform.XProperty
            };

            Storyboard story = new Storyboard();
            story.AutoReverse = true;
            story.RepeatBehavior = RepeatBehavior.Forever;
            story.Children.Add(xAnimation);

            Storyboard.SetTargetName(xAnimation, this.border.Name);
            Storyboard.SetTargetProperty(xAnimation, new PropertyPath("(0).(1)", propertyChain));

            story.Begin(this);


		}

		private void button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            
            this.border.RenderTransform = new TranslateTransform();
            this.border.Name = "border";
            NameScope.SetNameScope(this, new NameScope());
            this.RegisterName(this.border.Name, this.border);

            DoubleAnimation xAnimation = new DoubleAnimation();
            xAnimation.From = -100;
            xAnimation.To = -1100;
            xAnimation.Duration = new Duration(TimeSpan.FromSeconds(30));

            DependencyProperty[] propertyChain = new DependencyProperty[]
            {
                Image.RenderTransformProperty,
                TranslateTransform.XProperty
            };

            Storyboard story = new Storyboard();
            story.AutoReverse = true;
            story.RepeatBehavior = RepeatBehavior.Forever;
            story.Children.Add(xAnimation);

            Storyboard.SetTargetName(xAnimation, this.border.Name);
            Storyboard.SetTargetProperty(xAnimation, new PropertyPath("(0).(1)", propertyChain));

            story.Begin(this);

			// 在此处添加事件处理程序实现。
		}
	}
}