using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Arbo_AVL
{
     class DibujaAVL
    {
        public AVL raiz;
        public AVL aux;

        //Constructor
        public DibujaAVL()
        {
            aux = new AVL();
        }

        public DibujaAVL(AVL raizNueva)
        {
            raiz = raizNueva;
        }

        //Agrega un nuevo valor al arbol
        public void Insertar(int dato)
        {
            if (raiz == null)
                raiz = new AVL(dato, null, null, null);
            else
                raiz = raiz.Insertar(dato, raiz);
        }

        //Eliminar un valor del arbol
        public void Eliminar(int dato)
        {
            if (raiz == null)
                raiz = new AVL(dato, null, null, null);
            else
                raiz.Eliminar(dato, ref raiz);
        }

        private const int Radio = 30;
        private const int DistanciaH = 40;
        private const int DistanciaV = 10;
        private int CoordenadaX;
        private int CoordenadaY;

        public void PosicionNodoRecorrido(ref int xmin, ref int ymin)
        {
            CoordenadaX = (int)(ymin + Radio / 2);
            CoordenadaY = (int)(ymin + Radio / 2);
            xmin += Radio;
        }

        public void colorear(Graphics grafo, Font fuente, Brush relleno, Brush RellenoFuente, Pen lapiz, AVL raiz, bool post, bool inor, bool preor)
        {
            Brush entorno = Brushes.Red;

            if(inor == true)
            {
                if(raiz != null)
                {
                    colorear(grafo, fuente, relleno, RellenoFuente, lapiz, raiz.NodoIzquierdo, post, inor, preor);
                    raiz.colorear(grafo, fuente, entorno, RellenoFuente, lapiz);
                    Thread.Sleep(500);
                    raiz.colorear(grafo, fuente, entorno, RellenoFuente, lapiz);
                    colorear(grafo, fuente, relleno, RellenoFuente, lapiz, raiz.NodoDerecho, post, inor, preor);
                }
            }
            else if(preor == true)
            {
                if (raiz != null)
                {
                    raiz.colorear(grafo, fuente, Brushes.Yellow, Brushes.Blue, Pens.Black);
                    Thread.Sleep(500);
                    raiz.colorear(grafo, fuente, Brushes.Yellow, Brushes.Blue, Pens.Black);
                    colorear(grafo, fuente, relleno, RellenoFuente, lapiz, raiz.NodoIzquierdo, post, inor, preor);
                    colorear(grafo, fuente, relleno, RellenoFuente, lapiz, raiz.NodoDerecho, post, inor, preor);
                }
            }
            else if(post == true)
            {
                if (raiz != null)
                {
                    colorear(grafo, fuente, relleno, RellenoFuente, lapiz, raiz.NodoIzquierdo, post, inor, preor);
                    colorear(grafo, fuente, relleno, RellenoFuente, lapiz, raiz.NodoDerecho, post, inor, preor);
                    raiz.colorear(grafo, fuente, Brushes.Yellow, Brushes.Blue, Pens.Black);
                    Thread.Sleep(500);
                    raiz.colorear(grafo, fuente, Brushes.Yellow, Brushes.Blue, Pens.Black);
                }
            }
        }

        public void colorearB(Graphics grafo, Font fuente, Brush relleno, Brush rellenoFuente, Pen lapiz, AVL raiz, int busqueda)
        {
            Brush entorno = Brushes.Red;
            if(raiz != null)
            {
                raiz.colorear(grafo, fuente, entorno, rellenoFuente, lapiz);

                if(busqueda < raiz.valor)
                {
                    Thread.Sleep(500);
                    raiz.colorear(grafo, fuente, entorno, Brushes.Blue, lapiz);
                    colorearB(grafo, fuente, relleno, rellenoFuente, lapiz, raiz.NodoIzquierdo, busqueda);
                }
                else
                {
                    if (busqueda > raiz.valor)
                    {
                        Thread.Sleep(500);
                        raiz.colorear(grafo, fuente, entorno, Brushes.Blue, lapiz);
                        colorearB(grafo, fuente, relleno, rellenoFuente, lapiz, raiz.NodoDerecho, busqueda);
                    }
                    else
                    {
                        raiz.colorear(grafo, fuente, entorno, rellenoFuente, lapiz);
                        Thread.Sleep(500);
                    }
                }
            }
        }

        public void DibujarArbol(Graphics grafo, Font fuente, Brush relleno, Brush RellenoFuente, Pen lapiz, int dato, Brush encuentro)
        {
            int x = 100;
            int y = 75;
            if (raiz == null) return;

            //Posición de todos los nodos
            raiz.PosicionNodo(ref x, y);

            //Dibuja los enlaces entre nodos
            raiz.DibujarRamas(grafo, lapiz);

            //Dibuja todos los nodos
            raiz.DibujarNodo(grafo, fuente, relleno, RellenoFuente, lapiz, dato, encuentro);
        }

        public int x1 = 100;
        public int y2 = 75;
        public void restablecerValores()
        {
            x1 = 100;
            y2 = 75;
        }

    public void buscar(int x)
        {
            if (raiz == null)
                MessageBox.Show("Arbol Vacio", "Error", MessageBoxButtons.OK);
            else
                raiz.buscar(x, raiz);
        }
    }
}