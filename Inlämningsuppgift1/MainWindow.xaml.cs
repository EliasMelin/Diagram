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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inlämningsuppgift1
{
    enum UIControls { none, rectangel, ellipse, diamond, romb, Line }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            controls = UIControls.none;
            this.Closing += MainWindow_Closing;
        }
        List<MyShapes> listOfShapes = new List<MyShapes>();
        Shape[] shapesForDrawingLineBetween = new Shape[2];
        DockPanel ActivatedCreationOption = new DockPanel();
        Shape activatedShape;
        double mouseRelationToShapeXCoordinate, mouseRelationToShapeYCoordinate;
        bool isMovable = false;
        Point pt;
        UIControls controls;
        int elements = 0;

        #region CreateShapes
        private void dockPanelLine_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ActivatedCreationOption.Opacity = 1;
            controls = UIControls.Line;
            ActivatedCreationOption = (DockPanel)sender;
            ActivatedCreationOption.Opacity = 0.7;
        }
        private void dockPanelRectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ActivatedCreationOption.Opacity = 1;
            controls = UIControls.rectangel;
            ActivatedCreationOption = (DockPanel)sender;
            ActivatedCreationOption.Opacity = 0.7;

        }

        private void dockPanelEllipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ActivatedCreationOption.Opacity = 1;
            controls = UIControls.ellipse;
            ActivatedCreationOption = (DockPanel)sender;
            ActivatedCreationOption.Opacity = 0.7;
        }

        private void dockPanelDiamond_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ActivatedCreationOption.Opacity = 1;
            controls = UIControls.diamond;
            ActivatedCreationOption = (DockPanel)sender;
            ActivatedCreationOption.Opacity = 0.7;
        }
        private void dockPanelRomb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ActivatedCreationOption.Opacity = 1;
            controls = UIControls.romb;
            ActivatedCreationOption = (DockPanel)sender;
            ActivatedCreationOption.Opacity = 0.7;
        }
        private void AddUIControl(object sender, MouseButtonEventArgs e)
        {
            Point mousePoint = Mouse.GetPosition(canvasBoard);
            switch (controls)
            {
                case UIControls.none:
                    break;
                case UIControls.rectangel:
                    CreateRectangle(mousePoint.X, mousePoint.Y);
                    break;
                case UIControls.ellipse:
                    CreateEllipse(mousePoint.X, mousePoint.Y);
                    break;
                case UIControls.diamond:
                    CreateDiamond(mousePoint.X, mousePoint.Y);
                    break;
                case UIControls.romb:
                    CreaateRomb(mousePoint.X, mousePoint.Y);
                    break;
                case UIControls.Line:
                    CreateLine(mousePoint);
                    break;
                default:
                    break;
            }
        }
        private void CreateLine(Point p)
        {
            Shape s = GetShapeClickedOn(p);
            if (s != null)
            {
                if (elements == 0)
                {
                    shapesForDrawingLineBetween[elements] = s;
                    elements++;
                }
                else
                {
                    if (shapesForDrawingLineBetween[0] == s)
                    {
                        //MessageBox.Show("Obs!  samma objekt");
                    }
                    else
                    {
                        shapesForDrawingLineBetween[elements] = s;
                        elements++;
                        if (elements >= 2)
                        {
                            Connect();
                        }
                    }
                }
            }
        }
        private void CreaateRomb(double xCoordinate, double yCoordinate)
        {
            Rectangle r = new Rectangle();
            r.Width = 60;
            r.Height = 40;
            r.Fill = new SolidColorBrush(Colors.Purple);
            Canvas.SetLeft(r, (xCoordinate - r.Width / 2));
            Canvas.SetTop(r, (yCoordinate - r.Height / 2));
            SkewTransform skev = new SkewTransform()
            {
                AngleX = -20,
                AngleY = 0,
                CenterX = 30,
                CenterY = 0
            };
            r.LayoutTransform = skev;
            r.MouseLeftButtonDown += shape_Activate;
            r.MouseLeftButtonUp += shape_Deactivate;

            TextBlock t = new TextBlock();
            t.Text = "Namn";
            t.Width = 40;
            t.FontSize = 10;
            t.Height = 12;
            t.MouseLeftButtonDown += shape_Activate;
            t.MouseLeftButtonUp += shape_Deactivate;
            t.Background = new SolidColorBrush(Colors.Purple);
            Canvas.SetLeft(t, (xCoordinate - r.Width / 2) + 15);
            Canvas.SetTop(t, (yCoordinate - r.Height / 2) + 14);
            MyShapes s = new MyShapes()
            {
                shape = r,
                textBlock = t,
                textBlockXRelationToShape = 15,
                textBlockYRelationToShape = 14

            };
            listOfShapes.Add(s);

            canvasBoard.Children.Add(r);
            canvasBoard.Children.Add(t);
            ActivatedCreationOption.Opacity = 1;
            controls = UIControls.none;
        }
        private void CreateDiamond(double xCoordinate, double yCoordinate)
        {
            Polygon p = new Polygon();
            p.Points = new PointCollection()
            {
                new Point(30, 0),
                new Point(60, 20),
                new Point(30, 40),
                new Point(0, 20)
            };

            p.Fill = new SolidColorBrush(Colors.Blue);
            Canvas.SetLeft(p, (xCoordinate - 25));
            Canvas.SetTop(p, (yCoordinate - 20));
              
            p.MouseLeftButtonDown += shape_Activate;
            p.MouseLeftButtonUp += shape_Deactivate;

            TextBlock t = new TextBlock();
            t.Text = "Namn";
            t.FontSize = 10;
            t.Height = 12;
            t.MouseLeftButtonDown += shape_Activate;
            t.MouseLeftButtonUp += shape_Deactivate;
            //t.Background = new SolidColorBrush(Colors.Blue);
            Canvas.SetLeft(t, (xCoordinate - 25) + 15);
            Canvas.SetTop(t, (yCoordinate - 20) + 14);
            MyShapes s = new MyShapes()
            {
                shape = p,
                textBlock = t,
                textBlockXRelationToShape = 15,
                textBlockYRelationToShape = 14

            };
            UpdateConnectionPoints(s, p);
            listOfShapes.Add(s);

            canvasBoard.Children.Add(p);
            canvasBoard.Children.Add(t);
            controls = UIControls.none;
            ActivatedCreationOption.Opacity = 1;
        }

        private void CreateEllipse(double xCoordinate, double yCoordinate)
        {
            Ellipse e = new Ellipse();
            e.Width = 60;
            e.Height = 40;
            e.Fill = new SolidColorBrush(Colors.Green);
            Canvas.SetLeft(e, (xCoordinate - e.Width / 2));
            Canvas.SetTop(e, (yCoordinate - e.Height / 2));
            e.MouseLeftButtonDown += shape_Activate;
            e.MouseLeftButtonUp += shape_Deactivate;

            TextBlock t = new TextBlock();            
            t.Text = "Namn";
            t.FontSize = 10;
            t.Height = 12;
            t.AllowDrop = true;        
            t.MouseLeftButtonDown += shape_Activate;
            t.MouseLeftButtonUp += shape_Deactivate;
            //t.Background = new SolidColorBrush(Colors.Green);
            Canvas.SetLeft(t, (xCoordinate - e.Width / 2) + 10);
            Canvas.SetTop(t, (yCoordinate - e.Height / 2) + 14);
            MyShapes s = new MyShapes()
            {
                shape = e,
                textBlock = t,
                textBlockXRelationToShape = 10,
                textBlockYRelationToShape = 14

            };
            UpdateConnectionPoints(s, e);
            listOfShapes.Add(s);
            canvasBoard.Children.Add(e);
            canvasBoard.Children.Add(t);
            controls = UIControls.none;
            ActivatedCreationOption.Opacity = 1;
        }
        private void CreateRectangle(double xCoordinate, double yCoordinate)
        {

            Rectangle r = new Rectangle();
            r.Fill = new SolidColorBrush(Colors.Red);
            r.Width = 60;
            r.Height = 40;
            Canvas.SetLeft(r, (xCoordinate - r.Width / 2));
            Canvas.SetTop(r, (yCoordinate - r.Height / 2));
            r.MouseLeftButtonDown += shape_Activate;
            r.MouseLeftButtonUp += shape_Deactivate;

            TextBlock t = new TextBlock();
            t.Text = "Namn";
            t.FontSize = 10;
            t.Height = 12;
            t.MouseLeftButtonDown += shape_Activate;
            t.MouseLeftButtonUp += shape_Deactivate;
            //t.Background = new SolidColorBrush(Colors.Red);
            Canvas.SetLeft(t, (xCoordinate - r.Width / 2) + 10);
            Canvas.SetTop(t, (yCoordinate - r.Height / 2) + 14);
            canvasBoard.Children.Add(r);
            canvasBoard.Children.Add(t);
            controls = UIControls.none;

            MyShapes s = new MyShapes()
            {
                shape = r,
                textBlock = t,
                textBlockXRelationToShape = 10,
                textBlockYRelationToShape = 14,
            };
            UpdateConnectionPoints(s, r);
            listOfShapes.Add(s);
            ActivatedCreationOption.Opacity = 1;
        }

        private void Connect()
        {
            double xAndYDiffBetweenCurrentPoints = 0;
            ConnectionPoint startPoint = new ConnectionPoint();
            ConnectionPoint endPoint = new ConnectionPoint();
            Shape start = shapesForDrawingLineBetween[0];
            Shape end = shapesForDrawingLineBetween[1];
            MyShapes first = null;
            MyShapes second = null;
            // hämta aktuella MyShapes objekt
            foreach (MyShapes myShape in listOfShapes)
            {
                if (start == myShape.shape)
                {
                    if (first == null)
                    {
                        first = myShape;
                    }
                    else
                    {
                        second = myShape;
                    }
                }
                else if (end == myShape.shape)
                {
                    if (first == null)
                    {
                        first = myShape;
                    }
                    else
                    {
                        second = myShape;
                    }
                }
            }
            // jämför varje connectionPoint för start-shape med varje
            // connectionPoint för end-shape för att räkna det alternativ
            // där x + y differansen är minst
            for (int i = 0; i < first.connectionPoints.Count; i++)
            {
                for (int j = 0; j < second.connectionPoints.Count; j++)
                {
                    double xDifference;
                    double yDifference;
                    if (first.connectionPoints.ElementAt(i).x > second.connectionPoints.ElementAt(j).x)
                    {
                        xDifference = first.connectionPoints.ElementAt(i).x - second.connectionPoints.ElementAt(j).x;
                    }
                    else
                    {
                        xDifference = second.connectionPoints.ElementAt(j).x - first.connectionPoints.ElementAt(i).x;
                    }
                    if (first.connectionPoints.ElementAt(i).y > second.connectionPoints.ElementAt(j).y)
                    {
                        yDifference = first.connectionPoints.ElementAt(i).y - second.connectionPoints.ElementAt(j).y;
                    }
                    else
                    {
                        yDifference = second.connectionPoints.ElementAt(j).y - first.connectionPoints.ElementAt(i).y;
                    }
                    // första iterationen är startPoint, endPoint tomma
                    if (i == 0 && j == 0)
                    {
                        xAndYDiffBetweenCurrentPoints = xDifference + yDifference;
                        startPoint = first.connectionPoints.ElementAt(0);
                        endPoint = second.connectionPoints.ElementAt(0);
                    }
                    else
                    {
                        if (xDifference + yDifference < xAndYDiffBetweenCurrentPoints)
                        {
                            xAndYDiffBetweenCurrentPoints = xDifference + yDifference;
                            startPoint = first.connectionPoints.ElementAt(i);
                            endPoint = second.connectionPoints.ElementAt(j);
                        }
                    }
                }
            }
            Line line = new Line()
            {
                X1 = startPoint.x,
                Y1 = startPoint.y,
                X2 = endPoint.x,
                Y2 = endPoint.y,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            first.linesConnected.Add(line);
            second.linesConnected.Add(line);
            canvasBoard.Children.Add(line);
            controls = UIControls.none;
            ActivatedCreationOption.Opacity = 1;
            elements = 0;
        }
        #endregion

        #region MoveShapes
        private Shape GetShapeClickedOn(Point p)
        {
            Shape shape = null;
            HitTestResult result = VisualTreeHelper.HitTest(canvasBoard, pt);
            TextBlock t = result.VisualHit as TextBlock;
            if (t != null)
            {
                foreach (MyShapes myShape in listOfShapes)
                {
                    if (t == myShape.textBlock)
                    {
                        shape = myShape.shape;
                    }
                }
            }
            else
            {
                Shape s = result.VisualHit as Shape;
                if (s != null)
                {
                    shape = s;
                }
            }
            return shape;
        }
        private void shape_Deactivate(object sender, RoutedEventArgs e)
        {
            isMovable = false;
        }
        private void shape_Activate(object sender, RoutedEventArgs e)
        {
            pt = Mouse.GetPosition(canvasBoard);

            Shape s = GetShapeClickedOn(pt);
            if (s != null)
            {
                if(activatedShape != null)
                {  
                    activatedShape.Opacity = 1;
                }
                activatedShape = s;
                activatedShape.Opacity = 0.7;
                mouseRelationToShapeXCoordinate = (pt.X - Canvas.GetLeft(activatedShape));
                mouseRelationToShapeYCoordinate = (pt.Y - Canvas.GetTop(activatedShape));
                isMovable = true;
            }
            else
            {
                if(activatedShape!=null)
                {
                    activatedShape.Opacity = 1;
                }
                isMovable = false;
            }
        }
        private void canvasBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMovable)
            {
                e.Handled = true;
            }
            else
            {
                Point mp = Mouse.GetPosition(canvasBoard);
                foreach (MyShapes myShape in listOfShapes)
                {
                    if (myShape.shape == activatedShape)
                    {
                        Canvas.SetLeft(activatedShape, (mp.X - (double)mouseRelationToShapeXCoordinate));
                        Canvas.SetTop(activatedShape, (mp.Y - (double)mouseRelationToShapeYCoordinate)); //så textblock hamnar i mitten av Shapen
                        Canvas.SetLeft(myShape.textBlock, (mp.X - (double)mouseRelationToShapeXCoordinate) + myShape.textBlockXRelationToShape);
                        Canvas.SetTop(myShape.textBlock, (mp.Y - (double)mouseRelationToShapeYCoordinate) + myShape.textBlockYRelationToShape);
                        UpdateConnectionPoints(myShape, myShape.shape);
                        if (myShape.linesConnected != null)
                        {
                            foreach (Line l in myShape.linesConnected)
                            {
                                UpdateLine(l);
                            }
                        }
                    }
                }
            }
        }
        private void UpdateLine(Line line)
        {
            double xAndYDiffBetweenCurrentPoints = 0;
            ConnectionPoint startPoint = new ConnectionPoint();
            ConnectionPoint endPoint = new ConnectionPoint();
            Shape start = shapesForDrawingLineBetween[0];
            Shape end = shapesForDrawingLineBetween[1];
            MyShapes first = null;
            MyShapes second = null;

            //hämta aktuella shapes lijken är connectad till
            foreach (MyShapes myShape in listOfShapes)
            {
                if (myShape.linesConnected != null)
                {
                    foreach (Line l in myShape.linesConnected)
                    {
                        if (l == line)
                        {
                            if (first == null)
                            {
                                first = myShape;
                            }
                            else
                            {
                                second = myShape;
                            }
                        }
                    }
                }
            }

            // jämför varje connectionPoint för start-shape med varje
            // connectionPoint för end-shape för att räkna det alternativ
            // där x + y differansen är minst
            for (int i = 0; i < first.connectionPoints.Count; i++)
            {
                for (int j = 0; j < second.connectionPoints.Count; j++)
                {
                    double xDifference;
                    double yDifference;
                    if (first.connectionPoints.ElementAt(i).x > second.connectionPoints.ElementAt(j).x)
                    {
                        xDifference = first.connectionPoints.ElementAt(i).x - second.connectionPoints.ElementAt(j).x;
                    }
                    else
                    {
                        xDifference = second.connectionPoints.ElementAt(j).x - first.connectionPoints.ElementAt(i).x;
                    }
                    if (first.connectionPoints.ElementAt(i).y > second.connectionPoints.ElementAt(j).y)
                    {
                        yDifference = first.connectionPoints.ElementAt(i).y - second.connectionPoints.ElementAt(j).y;
                    }
                    else
                    {
                        yDifference = second.connectionPoints.ElementAt(j).y - first.connectionPoints.ElementAt(i).y;
                    }
                    // första iterationen är startPoint, endPoint tomma
                    if (i == 0 && j == 0)
                    {
                        xAndYDiffBetweenCurrentPoints = xDifference + yDifference;
                        startPoint = first.connectionPoints.ElementAt(0);
                        endPoint = second.connectionPoints.ElementAt(0);
                    }
                    else
                    {
                        if (xDifference + yDifference < xAndYDiffBetweenCurrentPoints)
                        {
                            xAndYDiffBetweenCurrentPoints = xDifference + yDifference;
                            startPoint = first.connectionPoints.ElementAt(i);
                            endPoint = second.connectionPoints.ElementAt(j);
                        }
                    }
                }
            }
            line.X1 = startPoint.x;
            line.Y1 = startPoint.y;
            line.X2 = endPoint.x;
            line.Y2 = endPoint.y;
        }

        private void UpdateConnectionPoints(MyShapes myShape, Shape s)
        {
            if(s is Ellipse)
            {
                myShape.connectionPoints = new List<ConnectionPoint>()
                {
                    new ConnectionPoint()
                    {
                        x = (Canvas.GetLeft(myShape.shape) + s.Width/2),
                        y = Canvas.GetTop(myShape.shape)
                    },
                    new ConnectionPoint()
                    {
                        x = (Canvas.GetLeft(myShape.shape) + s.Width),
                        y = (Canvas.GetTop(myShape.shape) + s.Height/2)
                    },
                    new ConnectionPoint()
                    {
                        x = (Canvas.GetLeft(myShape.shape) + s.Width/2),
                        y = (Canvas.GetTop(myShape.shape) + s.Height)
                    },
                    new ConnectionPoint()
                    {
                        x = Canvas.GetLeft(myShape.shape),
                        y = (Canvas.GetTop(myShape.shape) + s.Height/2)
                    },
                };
            }
            else if(s is Rectangle)
            {

            myShape.connectionPoints = new List<ConnectionPoint>()
                {
                    new ConnectionPoint()
                    {
                        x = Canvas.GetLeft(myShape.shape),
                        y = Canvas.GetTop(myShape.shape)
                    },
                    new ConnectionPoint()
                    {
                        x = (Canvas.GetLeft(myShape.shape) + myShape.shape.Width/2),
                        y = Canvas.GetTop(myShape.shape)
                    },
                    new ConnectionPoint()
                    {
                        x = (Canvas.GetLeft(myShape.shape) + myShape.shape.Width),
                        y = Canvas.GetTop(myShape.shape)
                    },
                    new ConnectionPoint()
                    {
                        x = (Canvas.GetLeft(myShape.shape) + myShape.shape.Width),
                        y = (Canvas.GetTop(myShape.shape) + myShape.shape.Height / 2)
                    },
                    new ConnectionPoint()
                    {
                        x = (Canvas.GetLeft(myShape.shape) + myShape.shape.Width),
                        y = (Canvas.GetTop(myShape.shape) + myShape.shape.Height)
                    },
                    new ConnectionPoint()
                    {
                        x = (Canvas.GetLeft(myShape.shape) + myShape.shape.Width / 2),
                        y = (Canvas.GetTop(myShape.shape) + myShape.shape.Height)
                    },
                    new ConnectionPoint()
                    {
                        x = Canvas.GetLeft(myShape.shape),
                        y = (Canvas.GetTop(myShape.shape) + myShape.shape.Height)
                    },
                    new ConnectionPoint()
                    {
                        x = Canvas.GetLeft(myShape.shape),
                        y = (Canvas.GetTop(myShape.shape) + myShape.shape.Height / 2)
                    }
                };
            }
            else if(s is Polygon)
            {
                myShape.connectionPoints = new List<ConnectionPoint>()
                {
                    new ConnectionPoint()
                    {
                        x = (Canvas.GetLeft(myShape.shape) + 30),
                        y = Canvas.GetTop(myShape.shape)
                    },
                    new ConnectionPoint()
                    {
                        x = (Canvas.GetLeft(myShape.shape) + 45),
                        y = (Canvas.GetTop(myShape.shape) + 10)
                    },
                    new ConnectionPoint()
                    {
                        x = (Canvas.GetLeft(myShape.shape) + 60),
                        y = (Canvas.GetTop(myShape.shape) + 20)
                    },
                    new ConnectionPoint()
                    {
                        x = (Canvas.GetLeft(myShape.shape) + 45),
                        y = (Canvas.GetTop(myShape.shape) + 30)
                    },
                    new ConnectionPoint()
                    {
                        x = (Canvas.GetLeft(myShape.shape) + 30),
                        y = (Canvas.GetTop(myShape.shape) + 40)
                    },
                    new ConnectionPoint()
                    {
                        x = (Canvas.GetLeft(myShape.shape) + 15),
                        y = (Canvas.GetTop(myShape.shape) + 30)
                    },
                    new ConnectionPoint()
                    {
                        x = (Canvas.GetLeft(myShape.shape) + 0),
                        y = (Canvas.GetTop(myShape.shape) + 20)
                    },
                    new ConnectionPoint()
                    {
                        x = (Canvas.GetLeft(myShape.shape) + 15),
                        y = (Canvas.GetTop(myShape.shape) + 10)
                    },
                };
            }
        }
        #endregion

        #region DeleteShapes
        private void canvasBoard_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            //get the X,Y location of where the user clicked.
            // store it in pt variable so we can access its value in 
            // contextmenu event which only opens if we hit a shape
            pt = Mouse.GetPosition(canvasBoard);

            // Use the HitTest() method of VisualTreeHelper to see if the user clicked
            // on an item in the canvas.
            HitTestResult result = VisualTreeHelper.HitTest(canvasBoard, pt);

            // see if the item (result) is a Shape
            Shape s = result.VisualHit as Shape;
            if (s != null)
            {
                e.Handled = false;
                activatedShape = s;
            }
            else
            {
                // om inte träfa en shape kanske träffa ett TextBlock
                TextBlock t = result.VisualHit as TextBlock;
                if (t != null)
                {
                    e.Handled = false;
                    // get activated shape related to the textBlock
                    foreach (MyShapes myShape in listOfShapes)
                    {
                        if (t == myShape.textBlock)
                        {
                            activatedShape = myShape.shape;
                        }
                    }
                }
                else
                {
                    // cansel "MenuItem_Click" event from firing
                    e.Handled = true;
                }
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MyShapes shapeToRemove = new MyShapes();
            foreach (MyShapes myShape in listOfShapes)
            {
                if (myShape.shape == activatedShape)
                {
                    int index = listOfShapes.IndexOf(myShape);
                    // för varje linje kopplad till shape vi tar bort
                    foreach (Line l in myShape.linesConnected)
                    {
                        // gå igenom alla shapes                        
                        for (int i = 0; i < listOfShapes.Count; i++)
                        {
                            // alla linjer
                            for (int j = 0; j < listOfShapes.ElementAt(i).linesConnected.Count; j++)
                            {
                                if (listOfShapes.ElementAt(i).linesConnected.ElementAt(j) == l)
                                {
                                    if (i == index)
                                    {
                                        // om det är samma myshape som vi ändå ska ta bort
                                        // för dess shape samt textblock försvinner
                                        // onödigt att ta bort dess linjerna först
                                    }
                                    else
                                    {
                                        listOfShapes.ElementAt(i).linesConnected.Remove(l);
                                    }
                                }
                            }                    
                        }
                        canvasBoard.Children.Remove(l);                        
                    }                       
                    canvasBoard.Children.Remove(activatedShape);
                    canvasBoard.Children.Remove(myShape.textBlock);
                    shapeToRemove = myShape;
                }
            }
            listOfShapes.Remove(shapeToRemove);

        }
        #endregion


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (activatedShape != null)
            {
               activatedShape.Fill = ((ComboBoxItem)(cmbColors).SelectedItem).Background;                                   
            }
        }

        private void ChangeText_Click(object sender, RoutedEventArgs e)
        {
            if(activatedShape != null)
            {
                foreach(MyShapes myShape in listOfShapes)
                {
                    if (activatedShape == myShape.shape)
                    {
                        if(textToChange.Text != string.Empty)
                        {
                            myShape.textBlock.Text = textToChange.Text;
                            textToChange.Text = "";
                        }                        
                    }
                }
            }
        }



        private void MainWindow_Closing(object sender,
 System.ComponentModel.CancelEventArgs e)
        {
            // See if the user really wants to shut down this window.
            string msg = "Do you want to close without saving?";
            MessageBoxResult result = MessageBox.Show(msg,
              "My App", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.No)
            {
                // If user doesn’t want to close, cancel closure.
                e.Cancel = true;
            }
        }
        private void cmbMeny_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s = ((ComboBoxItem)cmbMeny.SelectedItem).Content.ToString();
            if (s == "Rensa")
            {
                foreach (MyShapes myShape in listOfShapes)
                {
                    canvasBoard.Children.Remove(myShape.textBlock);
                    canvasBoard.Children.Remove(myShape.shape);
                    foreach (Line l in myShape.linesConnected)
                    {
                        canvasBoard.Children.Remove(l);
                    }
                }
                listOfShapes = new List<MyShapes>();
            }
            else if (s == "Avsluta")
            {
                this.Close();
            }
        }
    }
}
