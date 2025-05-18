namespace ZumitoWeb.Data
{
    using System;
    using System.Collections.Generic;

    public class Nodo
    {
        public string Id { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }

        public Nodo(string id, double lat, double lng)
        {
            Id = id;
            Lat = lat;
            Lng = lng;
        }
    }

    public class Arista
    {
        public Nodo Origen { get; set; }
        public Nodo Destino { get; set; }
        public double Peso { get; set; } // distancia

        public Arista(Nodo origen, Nodo destino, double peso)
        {
            Origen = origen;
            Destino = destino;
            Peso = peso;
        }
    }

    public class Grafo
    {
        public List<Nodo> Nodos { get; set; } = new();
        public List<Arista> Aristas { get; set; } = new();

        public void AgregarNodo(Nodo nodo)
        {
            Nodos.Add(nodo);
        }

        public void ConstruirGrafoCompleto()
        {
            Aristas.Clear();

            for (int i = 0; i < Nodos.Count; i++)
            {
                for (int j = i + 1; j < Nodos.Count; j++)
                {
                    double distancia = CalcularDistancia(Nodos[i], Nodos[j]);
                    Aristas.Add(new Arista(Nodos[i], Nodos[j], distancia));
                    Aristas.Add(new Arista(Nodos[j], Nodos[i], distancia));
                }
            }
        }

        private double CalcularDistancia(Nodo a, Nodo b)
        {
            double R = 6371e3;
            double lat1 = a.Lat * Math.PI / 180.0;
            double lat2 = b.Lat * Math.PI / 180.0;
            double deltaLat = (b.Lat - a.Lat) * Math.PI / 180.0;
            double deltaLng = (b.Lng - a.Lng) * Math.PI / 180.0;

            double h = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Sin(deltaLng / 2) * Math.Sin(deltaLng / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(h), Math.Sqrt(1 - h));

            return R * c;
        }
    }
}