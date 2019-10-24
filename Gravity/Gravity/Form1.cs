using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Gravity
{
    public partial class Form1 : Form
    {
        //RAD - radius of grav add icon, ADDGRAVPADDING - pixels b/ween circ. of add icon and outermost of plus, ADDGRAVWIDTH - width of plus icon lines
        private readonly int RAD = 10, ADDGRAVPADDING = 3, ADDGRAVWIDTH = 2, MAXINT = 1000000, MASSHANDLINGINT = 11, CURSORWIDTH = 2, BRACKETWIDTH = 4;
        //ADDGRAVBRUSH - background of source icon, ADDOBJBRUSH - background of obj icon, ADDGRAVBRUSHACCENT - colour of plus icon on add source
        //SOURCEBRUSH - colour of placed source, [placed objects use ADDOBJBRUSH]
        private readonly SolidBrush ADDGRAVBRUSH = new SolidBrush(Color.Red), ADDOBJBRUSH = new SolidBrush(Color.White),
            ADDGRAVBRUSHACCENT = new SolidBrush(Color.White), SOURCEBRUSH = new SolidBrush(Color.Green), CURSORBRUSH = new SolidBrush(Color.Cyan);
        private readonly Pen BRACKETPEN = new Pen(new SolidBrush(Color.SkyBlue));
        private readonly double SCALE = 3;
        //lists for objects
        private List<Source> sources = new List<Source>(); private List<Obj> objs = new List<Obj>();
        private Number n;
        private Random rng = new Random();

        //trail globals
        List<DoublePoint> p1;
        List<Tuple<Point, Point>> lines;

        //private readonly long DISTANCESCALER = Convert.ToInt64(Math.Pow(10, 6));
        private readonly long DISTANCESCALER = 1;
        private readonly int TRAILLENGTH = 20;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PB_main.BackColor = Color.Black;
            TSTXT_vX.Text = "0"; TSTXT_vY.Text = "0"; TSTXT_mass.Text = "1e12";
            MessageBox.Show("Thanks for trying out my gravity app, I hope you enjoy!\nSources are masses which cannot be move, these are particularly easy to create orbits around.\n" +
                "Objects are moving masses which you can choose to affect or not affect eachother gravitationally.\nThe best masses I've found are in the range of 1e13 and 1e17, beyond these there's not much point.\n" +
                "Velocities you use will be relative to the masses you use, as a general rule of thumb, use about <1 on 1e14 and below, use 1 on 1e15, and >1 on 1e16 and above.\n" +
                "To create an orbit, place a source of roughly mass 1e15 anywhere and place an object of velocity 1 nearby, how close is up to you: play with it.\n" +
                "For those who want to make their own \".GRAVPROJ\" files, the format for objects is: (PosX,PosY),Mass,(VX,VY),(R,G,B) (note that the app will break if the RGB values aren't close to 255).\n" +
                "The format for sources is: (PosX,PosY),Mass) (note that the last bracket is not a mistake, it is a finishing character.", "Hector's Gravity App");
        }

        //makes sure both adding options cannot be checked simulatanously.
        private void TS_addGrav_CheckedChanged(object sender, EventArgs e)
        {
            if (TS_addGrav.Checked) { TS_addObj.Checked = false; }
        }
        private void TS_addObj_CheckedChanged(object sender, EventArgs e)
        {
            if (TS_addObj.Checked) { TS_addGrav.Checked = false; }
        }

        private string SubstringFromIndeces(int sI, int eI, string input)
        {
            return input.Substring(sI, eI - sI + 1);
        }

        private void PB_main_Click(object sender, EventArgs e)
        {
            //checks what you are adding
            if (TS_addGrav.Checked)
            {
                CreateSource(ConvertMass(TSTXT_mass.Text), PB_main.PointToClient(MousePosition));
            }
            else if (TS_addObj.Checked)
            {
                try
                {
                    CreateObject(Convert.ToDouble(TSTXT_vX.Text), Convert.ToDouble(TSTXT_vY.Text), ConvertMass(TSTXT_mass.Text), PB_main.PointToClient(MousePosition));
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid mass or velocity.");
                }
            }
        }

        private void PB_main_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Hide();
        }
        private void PB_main_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
        }

        private void TS_clear_Click(object sender, EventArgs e)
        {
            sources = new List<Source>(); objs = new List<Obj>();
            if (TS_trail.Checked)
            {
                p1 = new List<DoublePoint>();
            }
        }

        private void PB_main_Paint(object sender, PaintEventArgs e)
        {
            //gets current pos of mouse
            Point mousePos = PB_main.PointToClient(MousePosition);
            Graphics g = e.Graphics;

            if (!(lines == null))
            {
                foreach (Tuple<Point, Point> l in lines)
                {
                    g.DrawLine(Pens.Red, l.Item1, l.Item2);
                }
            }

            //draws all sources and objs
            foreach (Source source in sources)
            {
                DrawSourceIcon(g, source.Pos.Round(), source);
            }
            foreach (Obj obj in objs)
            {
                DrawObjIcon(g, obj.Pos.Round(), obj);
            }

            //draws hovering add icon
            if (TS_addGrav.Checked) { DrawGravAddIcon(g, mousePos); }
            else if (TS_addObj.Checked) { DrawObjAddIcon(g, mousePos); }
            else { DrawCursor(g, mousePos); }
        }

        private void TS_random_Click(object sender, EventArgs e)
        {
            n = new Number(this);
            n.Show();
        }

        public void RandomObjs()
        {
            for (int i = 0; i <= n.Result - 1; i++)
            {
                CreateObject(rng.Next(0, 6), rng.Next(0, 6), ConvertMass(rng.Next(1, 10) + "e" + rng.Next(11, 15)), new Point(rng.Next(0, PB_main.Width), rng.Next(0, PB_main.Height)));
            }
        }

        private void TS_trail_CheckedChanged(object sender, EventArgs e)
        {
            if (TS_trail.Checked) { TIM_trail.Enabled = true; }
            else { TIM_trail.Enabled = false; p1 = null; lines = null; }
        }

        private void TIM_trail_Tick(object sender, EventArgs e)
        {
            if (p1 == null)
            {
                p1 = new List<DoublePoint>();
                foreach (Obj o in objs)
                {
                    p1.Add(new DoublePoint(o.Pos.X, o.Pos.Y));
                }
                lines = new List<Tuple<Point, Point>>();
            }
            else
            {
                if (p1.Count < objs.Count)
                {
                    int diff = objs.Count - p1.Count;
                    for (int i = objs.Count - diff; i <= objs.Count - 1; i++)
                    {
                        p1.Add(new DoublePoint(objs[i].Pos.X, objs[i].Pos.Y));
                    }
                }
                for (int i = 0; i <= objs.Count - 1; i++)
                {
                    Obj o = objs[i];
                    lines.Add(new Tuple<Point, Point>(new Point(Convert.ToInt32(p1[i].X), Convert.ToInt32(p1[i].Y)), new Point(Convert.ToInt32(o.Pos.X), Convert.ToInt32(o.Pos.Y))));
                    p1[i] = new DoublePoint(o.Pos.X, o.Pos.Y);
                }
            }
        }

        private void TS_save_Click(object sender, EventArgs e)
        {
            SFD_main.ShowDialog();
        }

        private void SFD_main_FileOk(object sender, CancelEventArgs e)
        {
            StreamWriter strW = new StreamWriter(SFD_main.FileName);
            foreach (Obj o in objs)
            {
                strW.WriteLine(o.GetSummary());
            }
            foreach (Source s in sources)
            {
                strW.WriteLine(s.GetSummary());
            }
            strW.Close();
        }

        private void TS_open_Click(object sender, EventArgs e)
        {
            OFD_main.ShowDialog();
        }

        private void OFD_main_FileOk(object sender, CancelEventArgs e)
        {
            StreamReader strR = new StreamReader(OFD_main.FileName);
            List<string> linesS = new List<string>();
            objs = new List<Obj>(); sources = new List<Source>(); p1 = null;
            while (!(strR.EndOfStream))
            {
                linesS.Add(strR.ReadLine());
            }
            strR.Close();

            foreach (string line in linesS)
            {
                if (line[0] == 'O')
                {
                    CreateObjectFromString(line);
                }
                else
                {
                    CreateSourceFromString(line);
                }
            }
        }

        private void DrawCursor(Graphics g, Point p)
        {
            g.FillRectangle(CURSORBRUSH, new Rectangle(p.X - CURSORWIDTH / 2, p.Y - CURSORWIDTH / 2, CURSORWIDTH, CURSORWIDTH));
        }
        private void DrawGravAddIcon(Graphics g, Point p)
        {
            //background icon
            g.FillEllipse(ADDGRAVBRUSH, new Rectangle(p.X - RAD, p.Y - RAD, 2 * RAD, 2 * RAD));
            //vert line
            g.FillRectangle(ADDGRAVBRUSHACCENT, new Rectangle(p.X - (ADDGRAVWIDTH / 2),
                p.Y - RAD + ADDGRAVPADDING,
                ADDGRAVWIDTH,
                2 * RAD - 2 * ADDGRAVPADDING));
            //horizontal line
            g.FillRectangle(ADDGRAVBRUSHACCENT, new Rectangle(p.X - RAD + ADDGRAVPADDING,
                p.Y - (ADDGRAVWIDTH / 2),
                2 * RAD - 2 * ADDGRAVPADDING,
                ADDGRAVWIDTH));
        }
        private void DrawObjAddIcon(Graphics g, Point p)
        {
            //background
            g.FillEllipse(ADDOBJBRUSH, new Rectangle(p.X - RAD, p.Y - RAD, 2 * RAD, 2 * RAD));
        }

        private void TS_help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Config for binary star system:\n" +
                "\"O:(300,200),10000,(0,2),(255,255,255)\nO:(400,200),10000,(0,-2),(255,255,255)\"\n" +
                "Config for small orbit deviation demonstrator:\n" +
                "\"O:(400,180),10,(5,0),(255,255,255)\nO:(400,178),10,(5,0),(255,255,255)\nO:(400,176),10,(5,0),(255,255,255)\nO:(400,174),10,(5,0),(255,255,255)\n" +
                "O:(400,172),10,(5,0),(255,255,255)\nO:(400,170),10,(5,0),(255,255,255)\nO:(400,168),10,(5,0),(255,255,255)\nO:(400,166),10,(5,0),(255,255,255)\n" +
                "O:(400,164),10,(5,0),(255,255,255)\nO:(400,162),10,(5,0),(255,255,255)\nO:(400,160),10,(5,0),(255,255,255)\nO:(400,158),10,(5,0),(255,255,255)\n" +
                "O:(400,156),10,(5,0),(255,255,255)\nO:(400,154),10,(5,0),(255,255,255)\nO:(400,152),10,(5,0),(255,255,255)\nO:(400,150),10,(5,0),(255,255,255)\n" +
                "O:(400,148),10,(5,0),(255,255,255)\nO:(400,146),10,(5,0),(255,255,255)\nS:(400,200),10000)\"", "Fun Configurations");
        }

        private void DrawSourceIcon(Graphics g, Point p, Source s)
        {
            //sets source radius to its mass
            int sourceRad = Convert.ToInt32(Math.Log10(s.Mass) * SCALE);
            //draws background
            g.FillEllipse(SOURCEBRUSH, new Rectangle(p.X - sourceRad, p.Y - sourceRad, 2 * sourceRad, 2 * sourceRad));
        }
        private void DrawObjIcon(Graphics g, Point p, Obj o)
        {
            //sets obj rad to mass
            //int objRad = Convert.ToInt32(o.Mass * SCALE);
            int objRad = RAD;
            DoublePoint[] trail = o.Trail;

            List<Tuple<SolidBrush, Rectangle>> trailRects = new List<Tuple<SolidBrush, Rectangle>>();

            //draws background
            if (o.Focused)
            {
                DrawBrackets(o.Container, g);
            }

            for (int i = trail.Length - 1; i >= 0; i--)
            {
                if (!(objRad == 1) && i % 3 == 0)
                {
                    objRad--;
                }
                trailRects.Add(new Tuple<SolidBrush, Rectangle>(new SolidBrush(Color.FromArgb(Convert.ToInt32((Convert.ToDouble(i) / (trail.Length - 1)) * 55.0 + (o.Color.Color.R - 55)),
                    Convert.ToInt32((Convert.ToDouble(i) / (trail.Length - 1)) * 55.0 + (o.Color.Color.G - 55)),
                    Convert.ToInt32((Convert.ToDouble(i) / (trail.Length - 1)) * 55.0 + (o.Color.Color.B - 55)))),
                    new Rectangle(Convert.ToInt32(trail[i].X - objRad), Convert.ToInt32(trail[i].Y - objRad), 2 * objRad, 2 * objRad)));
            }
            trailRects.Reverse();
            foreach (Tuple<SolidBrush, Rectangle> t in trailRects)
            {
                g.FillEllipse(t.Item1, t.Item2);
            }

            g.FillEllipse(o.Color, new Rectangle(p.X - objRad, p.Y - objRad, 2 * objRad, 2 * objRad));
        }
        private void DrawBrackets(Rectangle input, Graphics g)
        {
            g.DrawLine(BRACKETPEN, new Point(input.Left, input.Top), new Point(input.Left + BRACKETWIDTH, input.Top));
            g.DrawLine(BRACKETPEN, new Point(input.Left, input.Top), new Point(input.Left, input.Top + BRACKETWIDTH));

            g.DrawLine(BRACKETPEN, new Point(input.Left, input.Bottom), new Point(input.Left, input.Bottom - BRACKETWIDTH));
            g.DrawLine(BRACKETPEN, new Point(input.Left, input.Bottom), new Point(input.Left + BRACKETWIDTH, input.Bottom));

            g.DrawLine(BRACKETPEN, new Point(input.Right, input.Top), new Point(input.Right - BRACKETWIDTH, input.Top));
            g.DrawLine(BRACKETPEN, new Point(input.Right, input.Top), new Point(input.Right, input.Top + BRACKETWIDTH));

            g.DrawLine(BRACKETPEN, new Point(input.Right, input.Bottom), new Point(input.Right - BRACKETWIDTH, input.Bottom));
            g.DrawLine(BRACKETPEN, new Point(input.Right, input.Bottom), new Point(input.Right, input.Bottom - BRACKETWIDTH));
        }

        private void TIM_main_Tick(object sender, EventArgs e)
        {
            //totalstrength is resultant accel, indiv is accel due to individual sources
            DoublePoint totalStrength, indivStrength;
            Obj obj;
            bool delete = false;
            for (int i = 0; i <= objs.Count - 1; i++)
            {
            obj = objs[i];
                //steps accel and v
                obj.Step();
                totalStrength = new DoublePoint(0, 0);
                //for every source, find accel on this obj, and add to resultant
                foreach (Source s in sources)
                {
                    indivStrength = s.GetStrengthAtPoint(obj.Pos);
                    if (!(indivStrength == null))
                    { 
                        totalStrength.X += indivStrength.X; totalStrength.Y += indivStrength.Y;
                    }
                    else { delete = true; }
                }
                if (TS_objGrav.Checked)
                {
                    for (int a = 0; a <= objs.Count - 1; a++)
                    {
                        if (!(a == i))
                        {
                            indivStrength = objs[a].GetStrengthAtPoint(obj.Pos);
                            if (!(indivStrength == null))
                            {
                                totalStrength.X += indivStrength.X; totalStrength.Y += indivStrength.Y;
                            }
                            else { delete = true; }
                        }
                    }
                    if (delete)
                    {
                        objs.Remove(objs[i]);
                        if (TS_trail.Checked) { p1.Remove(p1[i]); }
                    }
                }
                //change accel by resultant
                obj.ApplyAccel(totalStrength);
            }
            for (int i = 0; i <= objs.Count - 1; i++)
            {
                if (objs[i].Pos.X > MAXINT || objs[i].Pos.X < -MAXINT
                    || objs[i].Pos.Y > MAXINT || objs[i].Pos.X < -MAXINT)
                {
                    objs.Remove(objs[i]);
                    if (TS_trail.Checked)
                    {
                        p1.Remove(p1[i]);
                    }
                }
                
            }

            //focus section
            foreach (Obj o in objs)
            {
                if (o.Container.Contains(PB_main.PointToClient(MousePosition)))
                {
                    o.Focused = true;
                    foreach (Obj a in objs)
                    {
                        if (!(o == a))
                        {
                            a.Focused = false;
                        }
                    }
                }
            }
            foreach (Obj o in objs)
            {
                if (o.Focused)
                {
                    RTXT_focused.Text = o.GetReadout();
                }
            }
            PB_main.Refresh();
        }

        private long ConvertMass(string input)
        {
            double coeff, mantissa;
            mantissa = Convert.ToInt64(input.Substring(0, input.IndexOf('e')));
            coeff = Convert.ToInt64(input.Substring(input.IndexOf('e') + 1, input.Length - input.IndexOf('e') - 1));
            coeff -= MASSHANDLINGINT;
            return Convert.ToInt64(mantissa * Math.Pow(10, coeff));
        }
        private void CreateObject(double vX, double vY, long mass, Point pos)
        {
            DoublePoint v;
            try
            {
                //creates obj with specified mass and u
                v = new DoublePoint(vX, vY);
                SolidBrush color = new SolidBrush(Color.FromArgb(rng.Next(150, 256), rng.Next(150, 256), rng.Next(150, 256)));
                objs.Add(new Obj(DoublePoint.FromPoint(pos), mass, v, color, TIM_main, DISTANCESCALER, TRAILLENGTH, MASSHANDLINGINT, RAD));
            }
            catch (Exception)
            {
                MessageBox.Show("That is not a valid mass or velocity.");
            }
        }
        private void CreateObject(double vX, double vY, long mass, DoublePoint pos, Color color)
        {
            DoublePoint v;
            try
            {
                //creates obj with specified mass and u
                v = new DoublePoint(vX, vY);
                objs.Add(new Obj(pos, mass, v, new SolidBrush(color), TIM_main, DISTANCESCALER, TRAILLENGTH, MASSHANDLINGINT, RAD));
            }
            catch (Exception)
            {
                MessageBox.Show("That is not a valid mass or velocity.");
            }
        }
        private void CreateSource(long mass, Point pos)
        {
            try
            {
                //creates source with specified mass
                sources.Add(new Source(DoublePoint.FromPoint(pos), mass, TIM_main, MASSHANDLINGINT, DISTANCESCALER));
            }
            catch (Exception)
            {
                MessageBox.Show("That is not a valid mass.");
            }
        }

        private void CreateObjectFromString(string input)
        {
            int scannerLeft, scannerRight, R, G, B;
            double vX, vY, posX, posY; long mass;

            scannerLeft = 3; scannerRight = 3;
            while(!(input[scannerRight] == ','))
            {
                scannerRight++;
            }
            scannerRight--;
            posX = Convert.ToDouble(SubstringFromIndeces(scannerLeft, scannerRight, input));

            scannerLeft = scannerRight + 2; scannerRight = scannerLeft;
            while (!(input[scannerRight] == ')'))
            {
                scannerRight++;
            }
            scannerRight--;
            posY = Convert.ToDouble(SubstringFromIndeces(scannerLeft, scannerRight, input));

            scannerLeft = scannerRight + 3; scannerRight = scannerLeft;
            while (!(input[scannerRight] == ','))
            {
                scannerRight++;
            }
            scannerRight--;
            mass = Convert.ToInt64(SubstringFromIndeces(scannerLeft, scannerRight, input));

            scannerLeft = scannerRight + 3; scannerRight = scannerLeft;
            while (!(input[scannerRight] == ','))
            {
                scannerRight++;
            }
            scannerRight--;
            vX = Convert.ToDouble(SubstringFromIndeces(scannerLeft, scannerRight, input));

            scannerLeft = scannerRight + 2; scannerRight = scannerLeft;
            while (!(input[scannerRight] == ')'))
            {
                scannerRight++;
            }
            scannerRight--;
            vY = Convert.ToDouble(SubstringFromIndeces(scannerLeft, scannerRight, input));

            scannerLeft = scannerRight + 4; scannerRight = scannerLeft;
            while (!(input[scannerRight] == ','))
            {
                scannerRight++;
            }
            scannerRight--;
            R = Convert.ToInt32(SubstringFromIndeces(scannerLeft, scannerRight, input));

            scannerLeft = scannerRight + 2; scannerRight = scannerLeft;
            while (!(input[scannerRight] == ','))
            {
                scannerRight++;
            }
            scannerRight--;
            G = Convert.ToInt32(SubstringFromIndeces(scannerLeft, scannerRight, input));

            scannerLeft = scannerRight + 2; scannerRight = scannerLeft;
            while (!(input[scannerRight] == ')'))
            {
                scannerRight++;
            }
            scannerRight--;
            B = Convert.ToInt32(SubstringFromIndeces(scannerLeft, scannerRight, input));

            CreateObject(vX, vY, mass, new DoublePoint(posX, posY), Color.FromArgb(R, G, B));
        }
        private void CreateSourceFromString(string input)
        {
            long mass; int posX, posY;
            int scannerLeft, scannerRight;

            scannerLeft = 3; scannerRight = 3;
            while (!(input[scannerRight] == ','))
            {
                scannerRight++;
            }
            scannerRight--;
            posX = Convert.ToInt32(SubstringFromIndeces(scannerLeft, scannerRight, input));

            scannerLeft = scannerRight + 2; scannerRight = scannerLeft;
            while (!(input[scannerRight] == ')'))
            {
                scannerRight++;
            }
            scannerRight--;
            posY = Convert.ToInt32(SubstringFromIndeces(scannerLeft, scannerRight, input));

            scannerLeft = scannerRight + 3; scannerRight = scannerLeft;
            while (!(input[scannerRight] == ')'))
            {
                scannerRight++;
            }
            scannerRight--;
            mass = Convert.ToInt64(SubstringFromIndeces(scannerLeft, scannerRight, input));

            CreateSource(mass, new Point(posX, posY));
        }
    }

    public class Source
    {
        //gravitational constant
        private readonly long DISTANCESCALER;
        private readonly double G;
        public DoublePoint Pos { private set; get; }
        public long Mass { private set; get; }

        public Source(DoublePoint initPos, long initMass, Timer stepTimer, int MASSHANDLER, long DISTANCESCALERIn)
        {
            Pos = initPos; Mass = initMass;
            G = 6.7 * Math.Pow(10, -11 + MASSHANDLER) * stepTimer.Interval / 1000;
            DISTANCESCALER = DISTANCESCALERIn;
        }

        public DoublePoint GetStrengthAtPoint(DoublePoint target)
        {
            //vector distance of source from a target
            DoublePoint distanceFrom = GetDistanceFrom(target);
            double angle = GetBearingOf(distanceFrom);
            double fieldStrength = GetFieldStrength(Math.Sqrt(Math.Pow(distanceFrom.X, 2) + Math.Pow(distanceFrom.Y, 2)));
            if (fieldStrength < 0) { return null; }
            angle += Math.PI; if (angle > 2 * Math.PI) { angle -= 2 * Math.PI; }
            return GetTangent(angle, fieldStrength);
        }
        private DoublePoint GetDistanceFrom(DoublePoint target)
        {
            //diff b/ween points
            return new DoublePoint(target.X - Pos.X, target.Y - Pos.Y);
        }
        private double GetFieldStrength(double r)
        {
            // GM/r^2
            r *= DISTANCESCALER;
            if (r < 10)
            {
                return -1;
            }
            return (G * Mass) / Math.Pow(r, 2);
        }
        private double GetBearingOf(DoublePoint tangent)
        {
            if (tangent.X == 0 && tangent.Y < 0)
            {
                return 0;
            }
            else if (tangent.X > 0 && tangent.Y < 0)
            {
                return Math.PI / 2 - AbsTan(tangent);
            }
            else if (tangent.X > 0 && tangent.Y == 0)
            {
                return Math.PI / 2;
            }
            else if (tangent.X > 0 && tangent.Y > 0)
            {
                return Math.PI / 2 + AbsTan(tangent);
            }
            else if (tangent.X == 0 && tangent.Y > 0)
            {
                return Math.PI;
            }
            else if (tangent.X < 0 && tangent.Y > 0)
            {
                return Math.PI * 3 / 2 - AbsTan(tangent);
            }
            else if (tangent.X < 0 && tangent.Y == 0)
            {
                return Math.PI * 3 / 2;
            }
            else
            {
                return Math.PI * 3 / 2 + AbsTan(tangent);
            }
        }
        private double AbsTan(DoublePoint input)
        {
            return Math.Atan(Math.Abs(input.Y) / Math.Abs(input.X));
        }
        private DoublePoint GetTangent(double bearing, double mag)
        {
            if (bearing == 0)
            {
                return new DoublePoint(0, -mag);
            }
            else if (bearing > 0 && bearing < Math.PI / 2)
            {
                return new DoublePoint(Math.Sin(bearing) * mag, -Math.Cos(bearing) * mag);
            }
            else if (bearing == Math.PI / 2)
            {
                return new DoublePoint(mag, 0);
            }
            else if (bearing > Math.PI / 2 && bearing < Math.PI)
            {
                bearing -= Math.PI / 2;
                return new DoublePoint(Math.Cos(bearing) * mag, Math.Sin(bearing) * mag);
            }
            else if (bearing == Math.PI)
            {
                return new DoublePoint(0, mag);
            }
            else if (bearing > Math.PI && bearing < Math.PI * 3 / 2)
            {
                bearing = Math.PI * 3 / 2 - bearing;
                return new DoublePoint(-Math.Cos(bearing) * mag, Math.Sin(bearing) * mag);
            }
            else if (bearing == Math.PI * 3 / 2)
            {
                return new DoublePoint(-mag, 0);
            }
            else
            {
                bearing -= Math.PI * 3 / 2;
                return new DoublePoint(-Math.Cos(bearing) * mag, -Math.Sin(bearing) * mag);
            }
        }

        public string GetSummary()
        {
            return "S:(" + Pos.X + "," + Pos.Y + ")" + "," + Mass + ")";
        }
    }

    public class Obj
    {
        public DoublePoint Pos { get { return pos; } }
        private DoublePoint pos;
        public long Mass { private set; get; }
        private DoublePoint v;
        public DoublePoint[] Trail { private set; get; }
        public SolidBrush Color;
        public Rectangle Container
        {
            get
            {
                Rectangle value = new Rectangle(Convert.ToInt32(Pos.X - width / 2), Convert.ToInt32(Pos.Y - width / 2), width, width);
                return value;
            }
        }
        public bool Focused = false;

        private readonly long DISTANCESCALER;
        private readonly double G;
        private readonly int width;
        private readonly int MASSADJUST;
        private DoublePoint lastA = new DoublePoint(0, 0);

        public Obj(DoublePoint initPos, long initMass, DoublePoint initV, SolidBrush colorIn, Timer stepTimer, long DISTANCESCALERIn, int TRAILLENGTHIn, int MASSHANDLER, int RAD)
        {
            pos = initPos; Mass = initMass; v = initV;
            DISTANCESCALER = DISTANCESCALERIn;
            Trail = new DoublePoint[TRAILLENGTHIn];
            for (int i = 0; i <= TRAILLENGTHIn - 1; i++)
            {
                Trail[i] = new DoublePoint(initPos.X, initPos.Y);
            }
            G = 6.7 * Math.Pow(10, -11 + MASSHANDLER) * stepTimer.Interval / 1000;
            Color = colorIn;
            width = RAD * 2;
            MASSADJUST = MASSHANDLER;
        }

        public void Step()
        {
            //steps position with v, updates trail
            for (int i = 0; i <= Trail.Length - 2; i++)
            {
                Trail[i] = new DoublePoint(Trail[i + 1].X, Trail[i + 1].Y);
            }
            Trail[Trail.Length - 1] = new DoublePoint(pos.X, pos.Y);
            pos.X += v.X; pos.Y += v.Y;
        }
        public void ApplyAccel(DoublePoint aIn)
        {
            //changes accel
            v.X += aIn.X; v.Y += aIn.Y;
            //MessageBox.Show("Pos: (" + Pos.X + ", " + Pos.Y + ")\n" +
            //    "Mass: (" + Mass + ")\n" +
            //    "V: (" + v.X + ", " + v.Y + ")\n" +
            //    "A: (" + a.X + ", " + a.Y + ")\n" +
            //    "AIn: (" + aIn.X + ", " + aIn.Y + ")");
            lastA = aIn;
        }

        public DoublePoint GetStrengthAtPoint(DoublePoint target)
        {
            //vector distance of source from a target
            DoublePoint distanceFrom = GetDistanceFrom(target);
            double angle = GetBearingOf(distanceFrom);
            double fieldStrength = GetFieldStrength(Math.Sqrt(Math.Pow(distanceFrom.X, 2) + Math.Pow(distanceFrom.Y, 2)));
            if (fieldStrength < 0) { return null; }
            angle += Math.PI; if (angle > 2 * Math.PI) { angle -= 2 * Math.PI; }
            return GetTangent(angle, fieldStrength);
        }
        private DoublePoint GetDistanceFrom(DoublePoint target)
        {
            //diff b/ween points
            return new DoublePoint(target.X - Pos.X, target.Y - Pos.Y);
        }
        private double GetFieldStrength(double r)
        {
            // GM/r^2
            r *= DISTANCESCALER;
            if (r < 10)
            {
                return -1;
            }
            return (G * Mass) / Math.Pow(r, 2);
        }
        private double GetBearingOf(DoublePoint tangent)
        {
            if (tangent.X == 0 && tangent.Y < 0)
            {
                return 0;
            }
            else if (tangent.X > 0 && tangent.Y < 0)
            {
                return Math.PI / 2 - AbsTan(tangent);
            }
            else if (tangent.X > 0 && tangent.Y == 0)
            {
                return Math.PI / 2;
            }
            else if (tangent.X > 0 && tangent.Y > 0)
            {
                return Math.PI / 2 + AbsTan(tangent);
            }
            else if (tangent.X == 0 && tangent.Y > 0)
            {
                return Math.PI;
            }
            else if (tangent.X < 0 && tangent.Y > 0)
            {
                return Math.PI * 3 / 2 - AbsTan(tangent);
            }
            else if (tangent.X < 0 && tangent.Y == 0)
            {
                return Math.PI * 3 / 2;
            }
            else
            {
                return Math.PI * 3 / 2 + AbsTan(tangent);
            }
        }
        private double AbsTan(DoublePoint input)
        {
            return Math.Atan(Math.Abs(input.Y) / Math.Abs(input.X));
        }
        private DoublePoint GetTangent(double bearing, double mag)
        {
            if (bearing == 0)
            {
                return new DoublePoint(0, -mag);
            }
            else if (bearing > 0 && bearing < Math.PI / 2)
            {
                return new DoublePoint(Math.Sin(bearing) * mag, -Math.Cos(bearing) * mag);
            }
            else if (bearing == Math.PI / 2)
            {
                return new DoublePoint(mag, 0);
            }
            else if (bearing > Math.PI / 2 && bearing < Math.PI)
            {
                bearing -= Math.PI / 2;
                return new DoublePoint(Math.Cos(bearing) * mag, Math.Sin(bearing) * mag);
            }
            else if (bearing == Math.PI)
            {
                return new DoublePoint(0, mag);
            }
            else if (bearing > Math.PI && bearing < Math.PI * 3 / 2)
            {
                bearing = Math.PI * 3 / 2 - bearing;
                return new DoublePoint(-Math.Cos(bearing) * mag, Math.Sin(bearing) * mag);
            }
            else if (bearing == Math.PI * 3 / 2)
            {
                return new DoublePoint(-mag, 0);
            }
            else
            {
                bearing -= Math.PI * 3 / 2;
                return new DoublePoint(-Math.Cos(bearing) * mag, -Math.Sin(bearing) * mag);
            }
        }

        public string GetSummary()
        {
            return "O:(" + Pos.X + "," + Pos.Y + ")" + "," + Mass + ",(" + v.X + "," + v.Y + "),(" + Color.Color.R + "," + Color.Color.G + "," + Color.Color.B + ")";
        }
        public string GetReadout()
        {
            string readout = "";
            readout += "Position: (" + pos.X.ToString("0.00") + ", " + pos.Y.ToString("0.00") + ")\n";
            readout += "Mass (x10^x): " + (Mass.ToString().Length - 1 + MASSADJUST) + "\n";
            readout += "Velocity: (" + v.X.ToString("0.00") + ", " + v.Y.ToString("0.00") + ")\n";
            readout += "Acceleration: (" + lastA.X.ToString("0.00") + ", " + lastA.Y.ToString("0.00") + ")\n";
            readout += "Colour (R,G,B): (" + Color.Color.R + ", " + Color.Color.G + ", " + Color.Color.B + ")";
            return readout;
        }
    }

    public class DoublePoint
    {
        public double X;
        public double Y;

        public DoublePoint(double x, double y)
        {
            X = x; Y = y;
        }

        public Point Round()
        {
            return new Point(Convert.ToInt32(X), Convert.ToInt32(Y));
        }
        
        public static DoublePoint FromPoint(Point p)
        {
            return new DoublePoint(p.X, p.Y);
        }
    }
}