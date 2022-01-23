namespace WinFormsApp1
{
    //The task is not fully completed yet :(
    public partial class Form1 : Form
    {
        bool c1 = true;
        List<Point> points = new List<Point>();
        Point p1 = new Point();
        Point p2 = new Point();
        Pen pen;
        Graphics g;
        public Form1()
        {
            InitializeComponent();

            g = pictureBox1.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pen = new Pen(Color.Black, 1);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (c1)
            {
                label1.Text = "x: " + e.X.ToString() + " | y:" + e.Y.ToString();
                points.Add(new Point(e.X, e.Y));
                p1 = new Point(e.X, e.Y);
                c1 = false;
            }
            else
            {
                label2.Text = "x: " + e.X.ToString() + " | y:" + e.Y.ToString();
                p2 = new Point(e.X, e.Y);
                List<Point> pointsIn = new List<Point>();
                List<List<Point>> pointsIns = new List<List<Point>>();
                Point p3 = p1;

                List<double> li = new List<double>();


                while (p3 != p2)
                {

                    Point no = new Point(p3.X, p3.Y - 1);//North point
                    Point so = new Point(p3.X, p3.Y + 1);//South
                    Point ea = new Point(p3.X - 1, p3.Y);//East
                    Point we = new Point(p3.X + 1, p3.Y);//West

                    //Calculating the shortest distance between points
                    double dNo = distance(p2, no);
                    double dSo = distance(p2, so);
                    double dEa = distance(p2, ea);
                    double dWe = distance(p2, we);
                    li.AddRange(new double[] { dNo, dSo, dEa, dWe });

                    //Making a path
                    if (!points.Contains(no) && no.Y > 0 && !pointsIn.Contains(no) && li.OrderByDescending(d => d).ToList().Last() == dNo)
                    {
                        p3 = no;
                        pointsIn.Add(no);
                    }
                    else if(!points.Contains(so) && so.Y < pictureBox1.Height && !pointsIn.Contains(so) && li.OrderByDescending(d => d).ToList().Last() == dSo)
                    {
                        p3 = so;
                        pointsIn.Add(so);
                    }
                    else if (!points.Contains(ea) && ea.X > 0 && !pointsIn.Contains(ea) && li.OrderByDescending(d => d).ToList().Last() == dEa)
                    {
                        p3 = ea;
                        pointsIn.Add(ea);
                    }
                    else if (!points.Contains(we) && we.X < pictureBox1.Width  && !pointsIn.Contains(we) && li.OrderByDescending(d => d).ToList().Last() == dWe)
                    {
                        p3 = we;
                        pointsIn.Add(we);
                    }
                    else//When we encounter an obstacle
                    {
                        if (!points.Contains(no) && no.Y > 0 && !pointsIn.Contains(no) && li.OrderByDescending(d => d).ToList().Last() != dNo)
                        {
                            p3 = no;
                            pointsIn.Add(no);
                        }
                        else if (!points.Contains(so) && so.Y < pictureBox1.Height && !pointsIn.Contains(so) && li.OrderByDescending(d => d).ToList().Last() != dSo)
                        {
                            p3 = so;
                            pointsIn.Add(so);
                        }
                        else if (!points.Contains(ea) && ea.X > 0 && !pointsIn.Contains(ea) && li.OrderByDescending(d => d).ToList().Last() != dEa)
                        {
                            p3 = ea;
                            pointsIn.Add(ea);
                        }
                        else if (!points.Contains(we) && we.X < pictureBox1.Width && !pointsIn.Contains(we) && li.OrderByDescending(d => d).ToList().Last() != dWe)
                        {
                            p3 = we;
                            pointsIn.Add(we);
                        }
                        else { return; }//Point 2 not found
                        g.DrawLines(pen, pointsIn.ToArray());
                    }

                }
                points.AddRange(pointsIn);
                g.DrawLines(pen, pointsIn.ToArray());
                c1 = true;
            }
            


        }
        //Calculating distance between points
        public double distance(Point p1, Point p2)
        {
            var distance = Math.Sqrt((Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2)));
            return distance;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label3.Text = "x: " + e.X + " | y:" + e.Y;
        }
    }
}