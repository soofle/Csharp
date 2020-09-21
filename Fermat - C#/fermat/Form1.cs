using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fermat
{
    public partial class FormP : Form
    {
        //Declaracion variables
 
        public float xa, ya, xb, yb, xc, yc, xd, yd, xe, ye, xf, yf; //Posiciones de los puntos
        public float n1, n2, n3, n4, n5;//Indices refraccion

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void labelxd_Click(object sender, EventArgs e)
        {

        }

        private void labelxe_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void labelXb1_Click(object sender, EventArgs e)
        {

        }

      
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public FormP()
        {
            InitializeComponent();
            n1 = 1.0f;
            n2 = 1.0f;
            n3 = 1.0f;
            n4 = 1.0f;
            n5 = 1.0f;

            //Variables definidas fijas
            xa = 0.0f;
            ya = 0.0f; 
            yb = -100.0f;
            yc = -200.0f;
            yd = -300.0f;
            ye = -400.0f;
            yf = -500.0f;
            xf = 800.0f;

            //Planteo X iniciales, divido el total de 800 pixels por 5
            xb = 160.0f;
            xc = 320.0f;
            xd = 480.0f;
            xe = 640.0f;

        }

        private void buttonSimular_Click(object sender, EventArgs e)
        {
            //leer los editbox y actualizar los n
            try
            {
                n1 = Convert.ToSingle(textBoxN1.Text);
            }
            catch (FormatException)
            {
                n1 = 1.0f;
                textBoxN1.Text = "1,0";
            }

            try
            {
                n2 = Convert.ToSingle(textBoxN2.Text);   //float es single precision
            }
            catch (FormatException)
            {
                n2 = 1.0f;   //f por float
                textBoxN2.Text = "1,0";
            }

            try
            {
                n3 = Convert.ToSingle(textBoxN3.Text);
            }
            catch (FormatException)
            {
                n3 = 1.0f;
                textBoxN3.Text = "1,0";
            }

            try
            {
                n4 = Convert.ToSingle(textBoxN4.Text);
            }
            catch (FormatException)
            {
                n4 = 1.0f;
                textBoxN4.Text = "1,0";
            }

            try
            {
                n5 = Convert.ToSingle(textBoxN5.Text);
            }
            catch (FormatException)
            {
                n5 = 1.0f;
                textBoxN5.Text = "1,0";
            }

            //Calculo los valores de X, voy dando los cruces por 0
            for (int i = 0; i < 1000; i++)
            {
                xb = funcion(xa, ya, yb, xc, yc, n1, n2);
                xc = funcion(xb, yb, yc, xd, yd, n2, n3);
                xd = funcion(xc, yc, yd, xe, ye, n3, n4);
                xe = funcion(xd, yd, ye, xf, yf, n4, n5);
            }

            //Muestro el resultado por pantalla de cada Xi

            textBoxxb.Text = Convert.ToString(xb);
            textBoxxc.Text = Convert.ToString(xc);
            textBoxxd.Text = Convert.ToString(xd);
            textBoxxe.Text = Convert.ToString(xe);

            //// Errores relativos ////

            float tita1, tita2;
            float Error1, Error2, Error3, Error4;
            Error1=Error2=Error3=Error4=0.0f;

            tita1 = (float)Math.Atan((xb-xa)/(yb-ya));
            tita2 = (float)Math.Atan((xc-xa)/(yc-ya));
            Error1 = (float)Math.Abs((Math.Sin(tita2)/Math.Sin(tita1))-(n1/n2))/(n1/n2);
            tita1 = (float)Math.Atan((xc-xb)/(yc-yb));
            tita2 = (float)Math.Atan((xd-xb)/(yd-yb));
            Error2 = (float)Math.Abs((Math.Sin(tita2)/Math.Sin(tita1))-(n2/n3))/(n2/n3);
            tita1 = (float)Math.Atan((xd-xc)/(yd-yc));
            tita2 = (float)Math.Atan((xe-xc)/(ye-yc));
            Error3 = (float)Math.Abs((Math.Sin(tita2)/Math.Sin(tita1))-(n3/n4))/(n3/n4);
            tita1 = (float)Math.Atan((xe-xd)/(ye-yd));
            tita2 = (float)Math.Atan((xf-xd)/(yf-yd));
            Error4 = (float)Math.Abs((Math.Sin(tita2)/Math.Sin(tita1))-(n4/n5))/(n4/n5);

            //Muestro los resultados
            TextBoxError1.Text = Convert.ToString(Error1);
            TextBoxError2.Text = Convert.ToString(Error2);
            TextBoxError3.Text = Convert.ToString(Error3);
            TextBoxError4.Text = Convert.ToString(Error4);

            //// Grafico ////

            Bitmap pBitmap = new Bitmap(pictureBoxRayos.Width, pictureBoxRayos.Height); //crear Bitmap
            //para poder graficar, tiene las func de graficar:

            Graphics gs = Graphics.FromImage(pBitmap);

            Pen lapiz = new Pen(Color.Red);

            gs.DrawLine(lapiz, xa, ya, xb, -yb);
            gs.DrawLine(lapiz, xb, -yb, xc, -yc);
            gs.DrawLine(lapiz, xc, -yc, xd, -yd);
            gs.DrawLine(lapiz, xd, -yd, xe, -ye);
            gs.DrawLine(lapiz, xe, -ye, xf, 499);

            //Dibuja la cuadricula
            lapiz.Color = Color.Black;
            gs.DrawRectangle(lapiz, 0, 0, pictureBoxRayos.Width-1, 100);
            gs.DrawRectangle(lapiz, 0, 0, pictureBoxRayos.Width-1, 200);
            gs.DrawRectangle(lapiz, 0, 0, pictureBoxRayos.Width-1, 300);
            gs.DrawRectangle(lapiz, 0, 0, pictureBoxRayos.Width-1, 400);
            gs.DrawRectangle(lapiz, 0, 0, pictureBoxRayos.Width-1, 499);
            //Valores iniciales de referencia de x
            gs.DrawLine(lapiz, 0, 0, 160, 100);
            gs.DrawLine(lapiz, 160, 100, 320, 200);
            gs.DrawLine(lapiz, 320, 200, 480, 300);
            gs.DrawLine(lapiz, 480, 300, 640, 400);
            gs.DrawLine(lapiz, 640, 400, 800, 499);

            //pegar en la imagen:
            pictureBoxRayos.Image = pBitmap;
            gs.Dispose(); //porque lo cree de la nada y se hace asi para los objeto imagen
            

        }

        //Fermat
        /// <summary>
        /// Funcion a minimizar para encontrar cruce por cero. 
        /// </summary>
        /// <returns></returns>
 
        public float Fermat(float xa, float ya, float xb, float yb, float xc, float yc, float n1, float n2)
        {
            float F;
            F=n1*(xb-xa)/(float)Math.Sqrt(Math.Pow(xb-xa,2)+Math.Pow(yb-ya, 2))+n2*(xb-xc)/(float)Math.Sqrt(Math.Pow(xc-xb, 2)+Math.Pow(yc-yb, 2));
            return F;
        }

        //Funcion minimizar 
        /// <summary>
        /// Minimiza la funcion para determinar el cruce por cero
        /// </summary>
        /// <returns></returns>
        public float funcion(float xa, float ya, float yb, float xc, float yc, float n1, float n2)
        {
            float delta=0.1f;
            float dx=0.001f;
            float P=1.0f;
            float x=xa;
            while (delta>dx)
            {
                while (P>0)
                {
                    P=Fermat(xa, ya, x, yb, xc, yc, n1, n2)*Fermat(xa, ya, x+delta, yb, xc, yc, n1, n2);
                    //Cuando este producto es negativo significa que la función cruzó por cero
                    x=x+delta;
                }              
                x=x-delta;
                delta=delta/2.0f;
            }
            return x;
        }

    }
}
