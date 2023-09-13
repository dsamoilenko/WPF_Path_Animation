using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Path_Animation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
	// constructor
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Boolean_Animation_Click(object sender, RoutedEventArgs e)
        {
            BooleanAnimationUsingKeyFrames animation = new BooleanAnimationUsingKeyFrames();

            animation.FillBehavior = FillBehavior.HoldEnd;
            animation.RepeatBehavior = RepeatBehavior.Forever;
            animation.Duration = TimeSpan.FromSeconds(5);

            animation.KeyFrames.Add(new DiscreteBooleanKeyFrame(false, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            animation.KeyFrames.Add(new DiscreteBooleanKeyFrame(true, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
            animation.KeyFrames.Add(new DiscreteBooleanKeyFrame(false, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.5))));
            animation.KeyFrames.Add(new DiscreteBooleanKeyFrame(true, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(3))));
            animation.KeyFrames.Add(new DiscreteBooleanKeyFrame(false, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(3.5))));
            animation.KeyFrames.Add(new DiscreteBooleanKeyFrame(true, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(5))));

            button1.BeginAnimation(Button.IsEnabledProperty, animation);
        }

        private void Matrix_Transform_Click(object sender, RoutedEventArgs e)
        {
            // фигура, состоящая из сегментов
            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = new Point(0, 0);

            // коллекция сегментов
            PathSegmentCollection pathSegmentCollection = new PathSegmentCollection();
            pathSegmentCollection.Add(new BezierSegment(new Point(35, 0), new Point(135, 0), new Point(160, 100), true));
            pathSegmentCollection.Add(new BezierSegment(new Point(180, 190), new Point(285, 200), new Point(510, 100), true));

            pathFigure.Segments = pathSegmentCollection;

            // коллекция фигур
            PathFigureCollection pathFigureCollection = new PathFigureCollection();
            pathFigureCollection.Add(pathFigure);

            // геометрия (контур) для движения в анимации
            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures = pathFigureCollection;

            MatrixAnimationUsingPath matrixAnimation = new MatrixAnimationUsingPath();

            // создание геометрии на основе XAML-кода
            matrixAnimation.PathGeometry = PathGeometry.CreateFromGeometry(Geometry.Parse("M 0, 0 C 35, 0 135, 0 160, 100 180, 190 285, 200 510, 100"));

            matrixAnimation.Duration = TimeSpan.FromSeconds(5);
            matrixAnimation.DoesRotateWithTangent = true;

            // задать ускорение при движении
            matrixAnimation.AccelerationRatio = 1.0;

            // заморозка параметров анимации (только для чтения)
            matrixAnimation.Freeze();

            // старт анимации
            buttonMatrixTranform.RenderTransform.BeginAnimation(MatrixTransform.MatrixProperty, matrixAnimation);
        }

        private void Rect_Animation_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // анимация прямоугольника
            RectAnimationUsingKeyFrames animation = new RectAnimationUsingKeyFrames();

            animation.FillBehavior = FillBehavior.HoldEnd;
            animation.Duration = TimeSpan.FromSeconds(4);

            animation.KeyFrames.Add(new LinearRectKeyFrame(new Rect(0, 0, 50, 50), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            animation.KeyFrames.Add(new LinearRectKeyFrame(new Rect(200, 50, 200, 50), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2))));
            animation.KeyFrames.Add(new DiscreteRectKeyFrame(new Rect(200, 50, 200, 10), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.5))));
            animation.KeyFrames.Add(new SplineRectKeyFrame(new Rect(0, 0, 50, 50), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(4)), new KeySpline(1.0, 0.0, 0.0, 1.0)));

            rectPath2.Data.BeginAnimation(RectangleGeometry.RectProperty, animation);
        }
    }
}
