using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arbo_AVL
{
    class AVL
    {
        public int valor;
        public String tipoRotacion;
        public AVL NodoIzquierdo;
        public AVL NodoDerecho;
        public AVL NodoPadre;
        public int altura;
        public Rectangle prueba;
        private DibujaAVL arbol;
        public AVL()
        {

        }

        public DibujaAVL Arbol
        {
            get { return arbol;}
            set { arbol = value; }
        }

        //Constructor
        public AVL(int valorNuevo, AVL izquierdo, AVL derecho, AVL padre)
        {
            valor = valorNuevo;
            NodoIzquierdo = izquierdo;
            NodoDerecho = derecho;
            NodoPadre = padre;
            altura = 0;
            tipoRotacion = ""; //Inicializa la variable del tipo de rotacion.
        }

        //Funcion para insertar un valor nuevo en el arbol AVL
        public AVL Insertar(int valorNuevo, AVL raiz)
        {
            if (raiz == null)
                raiz = new AVL(valorNuevo, null, null, null);
            else if (valorNuevo < raiz.valor)
                raiz.NodoIzquierdo = Insertar(valorNuevo, raiz.NodoIzquierdo);
            else if (valorNuevo > raiz.valor)
                raiz.NodoDerecho = Insertar(valorNuevo, raiz.NodoDerecho);
            else
                MessageBox.Show("Valor Existente en el Arbol", "Error", MessageBoxButtons.OK);

            //Realiza las rotaciones simples o dobles segun el caso
            if(Alturas(raiz.NodoIzquierdo) - Alturas(raiz.NodoDerecho) == 2)
            {
                if (valorNuevo < raiz.NodoIzquierdo.valor)
                {
                    raiz = RotacionIzquierdaSimple(raiz);
                    raiz.tipoRotacion = "Rotación hacia izquierda simple";
                }
                else
                {
                    raiz = RotacionIzquierdaDoble(raiz);
                    raiz.tipoRotacion = "Rotación hacia izquierda doble";
                }

            }
            if (Alturas(raiz.NodoDerecho) - Alturas(raiz.NodoIzquierdo) == 2)
            {
                if (valorNuevo > raiz.NodoDerecho.valor)
                {
                    raiz = RotacionDerechaSimple(raiz);
                    raiz.tipoRotacion = "Rotación hacia derecha simple";
                }
                else
                {
                    raiz = RotacionDerechaDoble(raiz);
                    raiz.tipoRotacion = "Rotación hacia derecha doble";
                }
            }
            raiz.altura = max(Alturas(raiz.NodoIzquierdo), Alturas(raiz.NodoDerecho) + 1);
            return raiz;
        }

        //Funcion para obtener que rama es mayor
        private static int max(int lhs, int rhs)
        {
            return lhs > rhs ? lhs : rhs;
        }
        //Obtiene la altura
        private static int Alturas(AVL raiz)
        {
            return raiz == null ? -1 : raiz.altura;
        }
        

        AVL nodoE, nodoP;
        //Funcion eliminar
        public AVL Eliminar(int valorEliminar, ref AVL raiz)
        {
            if(raiz != null)
            {
                if(valorEliminar < raiz.valor)
                {
                    nodoE = raiz;
                    Eliminar(valorEliminar, ref raiz.NodoIzquierdo);
                }
                else
                {
                    if(valorEliminar > raiz.valor)
                    {
                        nodoE = raiz;
                        Eliminar(valorEliminar, ref raiz.NodoDerecho);
                    }
                    else
                    {
                        //Posicionado sobre el elemento a eliminar
                        AVL NodoEliminar = raiz;
                        if(NodoEliminar.NodoDerecho == null)
                        {
                            raiz = NodoEliminar.NodoIzquierdo;
                            if(Alturas(nodoE.NodoIzquierdo) - Alturas(nodoE.NodoDerecho) == 2)
                            {
                                //MessageBox.Show("nodoE" + nodoE.valor.ToString());
                                if (valorEliminar < nodoE.valor)
                                    nodoP = RotacionIzquierdaSimple(nodoE);
                                else
                                    nodoE = RotacionDerechaSimple(nodoE);
                            }
                            if(Alturas(nodoE.NodoDerecho) - Alturas(nodoE.NodoIzquierdo) == 2)
                            {
                                if (valorEliminar > nodoE.valor)
                                    nodoE = RotacionDerechaSimple(nodoE);
                                else
                                    nodoE = RotacionDerechaDoble(nodoE);
                                nodoP = RotacionDerechaSimple(nodoE);
                            }
                        }
                        else
                        {
                            if (NodoEliminar.NodoIzquierdo == null)
                                raiz = NodoEliminar.NodoDerecho;
                            else
                            {
                                if(Alturas(raiz.NodoIzquierdo) - Alturas(raiz.NodoDerecho) > 0)
                                {
                                    AVL AuxiliarNodo = null;
                                    AVL Auxiliar = raiz.NodoIzquierdo;
                                    bool Bandera = false;
                                    while(Auxiliar.NodoDerecho != null)
                                    {
                                        AuxiliarNodo = Auxiliar;
                                        Auxiliar = Auxiliar.NodoDerecho;
                                        Bandera = true;
                                    }
                                    raiz.valor = Auxiliar.valor;
                                    NodoEliminar = Auxiliar;
                                    if (Bandera == true)
                                        AuxiliarNodo.NodoDerecho = Auxiliar.NodoIzquierdo;
                                    else
                                        raiz.NodoIzquierdo = Auxiliar.NodoIzquierdo;

                                    //Realiza las rotaciones simples o dobles dependiendo del caso
                                }
                                else
                                {
                                    if (Alturas(raiz.NodoDerecho) - Alturas(raiz.NodoIzquierdo) > 0)
                                    {
                                        AVL AuxiliarNodo = null;
                                        AVL Auxiliar = raiz.NodoIzquierdo;
                                        bool Bandera = false;
                                        while(Auxiliar.NodoDerecho != null)
                                        {
                                            AuxiliarNodo = Auxiliar;
                                            Auxiliar = Auxiliar.NodoDerecho;
                                            Bandera = true;
                                        }
                                        raiz.valor = Auxiliar.valor;
                                        NodoEliminar = Auxiliar;
                                        if (Bandera == true)
                                            AuxiliarNodo.NodoDerecho = Auxiliar.NodoIzquierdo;
                                        else
                                            raiz.NodoDerecho = Auxiliar.NodoIzquierdo;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
                MessageBox.Show("No existe Nodo en el arbol", "Error", MessageBoxButtons.OK);
            return nodoP;
        }

        //Funciones de las rotaciones

        //Rotacion izquierda simple.
        private static AVL RotacionIzquierdaSimple(AVL k2)
        {
            AVL k1 = k2.NodoIzquierdo;
            k2.NodoIzquierdo = k1.NodoDerecho;
            k1.NodoDerecho = k2;
            k2.altura = max(Alturas(k2.NodoIzquierdo), Alturas(k2.NodoDerecho)) + 1;
            k1.altura = max(Alturas(k1.NodoIzquierdo), k2.altura) + 1;
            return k1;
        }

        //Rotacion derecha simple.
        private static AVL RotacionDerechaSimple(AVL k1)
        {
            AVL k2 = k1.NodoDerecho;
            k1.NodoDerecho = k2.NodoIzquierdo;
            k2.NodoIzquierdo = k1;
            k1.altura = max(Alturas(k1.NodoIzquierdo), Alturas(k1.NodoDerecho)) + 1;
            k2.altura = max(Alturas(k2.NodoDerecho), k1.altura) + 1;
            return k2;
        }

        //Doble rotacion izquierda.
        private static AVL RotacionIzquierdaDoble(AVL k3)
        {
            k3.NodoIzquierdo = RotacionDerechaSimple(k3.NodoIzquierdo);
            return RotacionIzquierdaSimple(k3);
        }

        //Doble Rotacion Derecha.
        private static AVL RotacionDerechaDoble(AVL k1)
        {
            k1.NodoDerecho = RotacionIzquierdaSimple(k1.NodoDerecho);
            return RotacionDerechaSimple(k1);
        }

        //Obtiene altura del arbol.
        public int getAltura(AVL nodoActual)
        {
            if (nodoActual == null)
                return 0;
            else
                return 1 + Math.Max(getAltura(nodoActual.NodoIzquierdo), getAltura(nodoActual.NodoDerecho));

        }

        //Buscar un valor en el arbol.
        public void buscar(int valorBuscar, AVL raiz)
        {
            if(raiz != null)
            {
                if(valorBuscar < raiz.valor)
                    buscar(valorBuscar, raiz.NodoIzquierdo);
                else
                {
                    if (valorBuscar > raiz.valor)
                        buscar(valorBuscar, raiz.NodoDerecho);
                }
            }
            else
                MessageBox.Show("No se encontro el valor", "Error", MessageBoxButtons.OK);
        }

        //Funciones para dibujar el arbol.
        private const int Radio = 30;
        private const int DistanciaH = 40;
        private const int DistanciaV = 10;

        private int CoordenadaX;
        private int CoordenadaY;

        //Encuentra la posicion donde debe crearse el nodo.
        public void PosicionNodo(ref int xmin, int ymin)
        {
            int aux1, aux2;
            CoordenadaY = (int)(ymin + Radio / 2);

            //Obtiene la posicion del sub-arbol izquierdo.
            if (NodoIzquierdo != null)
                NodoIzquierdo.PosicionNodo(ref xmin, ymin + Radio + DistanciaV);
            if ((NodoIzquierdo != null) && (NodoDerecho != null))
                xmin += DistanciaH;

            //Si existen ambos nodos dejara espacio entre ellos
            if (NodoDerecho != null)
                NodoDerecho.PosicionNodo(ref xmin, ymin + Radio + DistanciaV);

            // Posicion de ambos nodos
            if(NodoIzquierdo != null)
            {
                if (NodoDerecho != null)
                    //Centro entre los nodos
                    CoordenadaX = (int)((NodoIzquierdo.CoordenadaX + NodoDerecho.CoordenadaX) / 2);
                else
                {
                    //No hay nodo derecho, centrar al izquierdo
                    aux1 = NodoIzquierdo.CoordenadaX;
                    NodoIzquierdo.CoordenadaX = CoordenadaX - 40;
                    CoordenadaX = aux1;
                }
            }
            else if(NodoDerecho != null)
            {
                aux2 = NodoDerecho.CoordenadaX;
                //No hay nodo izquierdo, centrar al derecho
                NodoDerecho.CoordenadaX = CoordenadaX - 40;
                CoordenadaX = aux2;
            }
            else
            {
                //Nodo hoja
                CoordenadaX = (int)(xmin + Radio / 2);
                xmin += Radio;
            }
        }

        //Dibuja las ramas de los nodos izquierdo y derecho
        public void DibujarRamas(Graphics grafo, Pen Lapiz)
        {
            if(NodoIzquierdo != null)
            {
                grafo.DrawLine(Lapiz, CoordenadaX, CoordenadaY, NodoIzquierdo.CoordenadaX, NodoIzquierdo.CoordenadaY);
                NodoIzquierdo.DibujarRamas(grafo, Lapiz);
            }
            if(NodoDerecho != null)
            {
                grafo.DrawLine(Lapiz, CoordenadaX, CoordenadaY, NodoDerecho.CoordenadaX, NodoDerecho.CoordenadaY);
                NodoDerecho.DibujarRamas(grafo, Lapiz);
            }
        }

        //Dibuja el nodo en la posición especificada
        public void DibujarNodo(Graphics grafo, Font fuente, Brush relleno, Brush rellenoFuente, Pen lapiz, int dato, Brush encuentro)
        {
            //Dibuja el contorno del nodo.
            Rectangle rect = new Rectangle(
                (int)(CoordenadaX - Radio/2),
                (int)(CoordenadaY - Radio / 2), Radio, Radio);

            if (valor == dato)
                grafo.FillEllipse(encuentro, rect);
            else
            {
                grafo.FillEllipse(encuentro, rect);
                grafo.FillEllipse(relleno, rect);
            }
            grafo.DrawEllipse(lapiz, rect);

            //Dibuja el valor del nodo.
            StringFormat formato = new StringFormat();
            StringFormat formatoAltura = new StringFormat();
            formatoAltura.Alignment = StringAlignment.Center;
            formatoAltura.LineAlignment = StringAlignment.Center;
            formato.Alignment = StringAlignment.Center;
            formato.LineAlignment = StringAlignment.Center;
            grafo.DrawString(valor.ToString(), fuente, Brushes.Black, CoordenadaX, CoordenadaY, formato);
            //Obtiene el factor balanceo restando las alturas de cada nodo y las muestra
            grafo.DrawString((Alturas(NodoDerecho)-Alturas(NodoIzquierdo)).ToString(), fuente, Brushes.Black, CoordenadaX, CoordenadaY+25, formato);

            //Dibuja los nodos hijos derecho e izquierdo
            if (NodoIzquierdo != null)
                NodoIzquierdo.DibujarNodo(grafo, fuente, Brushes.YellowGreen, rellenoFuente, lapiz, dato, encuentro);
            if(NodoDerecho != null)
                NodoDerecho.DibujarNodo(grafo, fuente, Brushes.YellowGreen, rellenoFuente, lapiz, dato, encuentro);
        }

        public void colorear(Graphics grafo, Font fuente, Brush relleno, Brush rellenoFuente, Pen lapiz)
        {
            //Dibuja el contorno del nodo.
            Rectangle rect = new Rectangle(
                (int)(CoordenadaX - Radio / 2),
                (int)(CoordenadaY - Radio / 2), Radio, Radio);
            prueba = new Rectangle((int)(CoordenadaX - Radio / 2), (int)(CoordenadaY - Radio / 2), Radio, Radio);

            //Dibuja el valor del nombre.
            StringFormat formato = new StringFormat();

            formato.Alignment = StringAlignment.Center;
            formato.LineAlignment = StringAlignment.Center;
            grafo.DrawString(valor.ToString(), fuente, Brushes.Black, CoordenadaX, CoordenadaY, formato);
        }

    }
}
