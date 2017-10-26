using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;

namespace Ovidiu.StringUtil
{
    public class Punct3D : IComparable<Punct3D>
    {
        int nrPunct = 1;
        double xPunct = 0;
        double yPunct = 0;
        double zPunct = 0;
        double kmPunct = 0;
        double offPunct = 0;
        string desPunct = string.Empty;

        //Proprietate care stocheaza numarul punctului
        public int Nr
        {
            set { nrPunct = value; }
            get { return nrPunct; }
        }

        //Proprietate care stocheaza coordonata X a punctului
        public double X
        {
            set { xPunct = value; }
            get { return xPunct; }
        }

        //Proprietate care stocheaza coordonata Y a punctului
        public double Y
        {
            set { yPunct = value; }
            get { return yPunct; }
        }

        //Proprietate care stocheaza coordonata Z a punctului
        public double Z
        {
            set { zPunct = value; }
            get { return zPunct; }
        }

        //Proprietate care stocheaza pozitia kilometrica a punctului
        public double KM
        {
            set { kmPunct = value; }
            get { return kmPunct; }
        }

        //Proprietate care stocheaza offsetul punctului
        public double Offset
        {
            set { offPunct = value; }
            get { return offPunct; }
        }

        //Proprietate care stocheaza descrierea punctului
        public string D
        {
            set { desPunct = value; }
            get { return desPunct; }
        }

        public enum Format { ENZ, KmOZ, KmOZD, KmOENZ, NrENZ, NrKmOZ, NrKmOENZ, NrENZD, NrKmOZD, NrKmOENZD };
        public enum DelimitedBy { Comma, Space };

        //Implementarea functiei de comparare
        #region
        //Prima modalitate (fara parametri specificati) - functie de numarul punctului
        public int CompareTo(Punct3D punct)
        {
            return this.Nr.CompareTo(punct.Nr);
        }
        //A doua modalitate (Generic Comparison)
        //Delegat
        public delegate int Comparison<in Punct3D>(Punct3D P1, Punct3D P2);
        //Generic Comparison
        public static int ComparePointNr(Punct3D P1, Punct3D P2)
        {
            return P1.Nr.CompareTo(P2.Nr);
        }
        public static int ComparePointKM(Punct3D P1, Punct3D P2)
        {
            return P1.KM.CompareTo(P2.KM);
        }
        public static int ComparePointOffset(Punct3D P1, Punct3D P2)
        {
            return P1.Offset.CompareTo(P2.Offset);
        }
        public static int ComparePointX(Punct3D P1, Punct3D P2)
        {
            return P1.X.CompareTo(P2.X);
        }
        public static int ComparePointY(Punct3D P1, Punct3D P2)
        {
            return P1.Y.CompareTo(P2.Y);
        }
        public static int ComparePointZ(Punct3D P1, Punct3D P2)
        {
            return P1.Z.CompareTo(P2.Z);
        }
        public static int ComparePointD(Punct3D P1, Punct3D P2)
        {
            return P1.D.CompareTo(P2.D);
        }
        //Metodele a treia si a patra (Generic IComparer)
        class Punct3DSortByNr : IComparer<Punct3D>
        {
            public int Compare(Punct3D P1, Punct3D P2)
            {
                return P1.Nr.CompareTo(P2.Nr);
            }
        }
        class Punct3DSortByKM : IComparer<Punct3D>
        {
            public int Compare(Punct3D P1, Punct3D P2)
            {
                return P1.KM.CompareTo(P2.KM);
            }
        }
        class Punct3DSortByOffset : IComparer<Punct3D>
        {
            public int Compare(Punct3D P1, Punct3D P2)
            {
                return P1.Offset.CompareTo(P2.Offset);
            }
        }
        class Punct3DSortByX : IComparer<Punct3D>
        {
            public int Compare(Punct3D P1, Punct3D P2)
            {
                return P1.X.CompareTo(P2.X);
            }
        }
        class Punct3DSortByY : IComparer<Punct3D>
        {
            public int Compare(Punct3D P1, Punct3D P2)
            {
                return P1.Y.CompareTo(P2.Y);
            }
        }
        class Punct3DSortByZ : IComparer<Punct3D>
        {
            public int Compare(Punct3D P1, Punct3D P2)
            {
                return P1.Z.CompareTo(P2.Z);
            }
        }
        class Punct3DSortByD : IComparer<Punct3D>
        {
            public int Compare(Punct3D P1, Punct3D P2)
            {
                return P1.D.CompareTo(P2.D);
            }
        }
        #endregion

        //Functie ce completeaza proprietatile selectate ale punctului intr-o matrice double
        public double[] toArray(Format tip)
        {
            switch (tip)
            {
                case Format.ENZ:
                    return new double[] { xPunct, yPunct, zPunct };
                //    break;
                case Format.KmOZ:
                    return new double[] { kmPunct, offPunct, zPunct };
                case Format.KmOZD:
                    return new double[] { kmPunct, offPunct, zPunct };
                //    break;
                case Format.KmOENZ:
                    return new double[] { kmPunct, offPunct, xPunct, yPunct, zPunct };
                case Format.NrENZ:
                    return new double[] { nrPunct, xPunct, yPunct, zPunct };
                case Format.NrENZD:
                    return new double[] { nrPunct, xPunct, yPunct, zPunct };
                //    break;
                case Format.NrKmOZ:
                    return new double[] { nrPunct, kmPunct, offPunct, zPunct };
                case Format.NrKmOZD:
                    return new double[] { nrPunct, kmPunct, offPunct, zPunct };
                //    break;
                case Format.NrKmOENZ:
                    return new double[] { nrPunct, kmPunct, offPunct, xPunct, yPunct, zPunct };
                case Format.NrKmOENZD:
                    return new double[] { nrPunct, kmPunct, offPunct, xPunct, yPunct, zPunct };
                default:
                    return new double[] { xPunct, yPunct, zPunct };
                //    break;
            }
        }

        //Functie ce completeaza proprietatile selectate ale punctului intr-un segment text
        public string toString(Format tip, DelimitedBy separator, int nrZecimale, bool formatKM)
        {
            //Setarea separatorului folosit
            string sep;
            if (separator == DelimitedBy.Space) sep = " ";
            else sep = ",";

            //Convertirea valorilor in format text, cu precizia aleasa
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalDigits = nrZecimale;
            string stringNr = nrPunct.ToString("F", nfi);
            string stringX = xPunct.ToString("F", nfi);
            string stringY = yPunct.ToString("F", nfi);
            string stringZ = zPunct.ToString("F", nfi);
            string stringKM;
            if (formatKM) stringKM = kmPunct.ToString("0+000." + "".PadRight(nrZecimale, '0'), nfi);
            else stringKM = kmPunct.ToString("F", nfi);
            string stringOffset = offPunct.ToString("F", nfi);
            
            //string stringX = xPunct.ToString("F" + Precision);
            //string stringY = yPunct.ToString("F" + Precision);
            //string stringZ = zPunct.ToString("F" + Precision);
            //string stringKM = kmPunct.ToString("0+000.000");
            //string stringOffset = offPunct.ToString("F" + Precision);

            string stringProp = string.Empty;
            if (tip.ToString().Contains("Nr")) stringProp += sep + stringNr;
            if (tip.ToString().Contains("Km")) stringProp += sep + stringKM;
            if (tip.ToString().Contains("O")) stringProp += sep + stringOffset;
            if (tip.ToString().Contains("E")) stringProp += sep + stringX;
            if (tip.ToString().Contains("N")) stringProp += sep + stringY;
            if (tip.ToString().Contains("Z")) stringProp += sep + stringZ;
            if (tip.ToString().Contains("D")) stringProp += sep + desPunct;
            stringProp = stringProp.Substring(1);
            return stringProp;

            //switch (tip)
            //{
            //    case Format.ENZ:
            //        return stringX + sep + stringY + sep + stringZ;
            //    //    break;
            //    case Format.KmOZ:
            //        return stringKM + sep + stringOffset + sep + stringZ;
            //    //    break;
            //    case Format.KmOENZ:
            //        return stringKM + sep + stringOffset + sep + stringX + sep + stringY + sep + stringZ;
            //    default:
            //        return stringX + sep + stringY + sep + stringZ;
            //    //    break;
            //}

        }

        //Functie ce actualizeaza proprietatile selectate ale punctului dintr-o matrice double
        public bool ValuesFromArray(double[] matrice, Format tip)
        {
            try
            {
                //Actualizarea datelor
                switch (tip)
                {
                    case Format.ENZ:
                        xPunct = matrice[0];
                        yPunct = matrice[1];
                        if (matrice.Length > 2) zPunct = matrice[2];
                        break;

                    case Format.KmOZ:
                    case Format.KmOZD:
                        kmPunct = matrice[0];
                        offPunct = matrice[1];
                        if (matrice.Length > 2) zPunct = matrice[2];
                        break;

                    case Format.KmOENZ:
                        kmPunct = matrice[0];
                        offPunct = matrice[1];
                        xPunct = matrice[2];
                        yPunct = matrice[3];
                        if (matrice.Length > 4) zPunct = matrice[4];
                        break;
                    case Format.NrENZ:
                    case Format.NrENZD:
                        nrPunct = (int)matrice[0];
                        xPunct = matrice[1];
                        yPunct = matrice[2];
                        if (matrice.Length > 2) zPunct = matrice[2];
                        break;

                    case Format.NrKmOZ:
                        nrPunct = (int)matrice[0];
                        kmPunct = matrice[1];
                        offPunct = matrice[2];
                        if (matrice.Length > 3) zPunct = matrice[3];
                        break;

                    case Format.NrKmOENZ:
                    case Format.NrKmOENZD:
                        nrPunct = (int)matrice[0];
                        kmPunct = matrice[1];
                        offPunct = matrice[2];
                        xPunct = matrice[3];
                        yPunct = matrice[4];
                        if (matrice.Length > 5) zPunct = matrice[5];
                        break;
                }

                return true;
            }

            catch
            {
                return false;
            }
        }
        
        //Functie ce actualizeaza proprietatile selectate ale punctului dintr-un segment text
        public bool ValuesFromString(string text, Format tip, DelimitedBy separator)
        {
            try
            {
                //Setarea separatorului folosit
                char sep;
                if (separator == DelimitedBy.Space) sep = ' ';
                else sep = ',';

                string[] segmente = text.Split(sep);

                //Actualizarea datelor
                switch (tip)
                {
                    case Format.ENZ:
                        xPunct = double.Parse(segmente[0]);
                        yPunct = double.Parse(segmente[1]);
                        if (segmente.Length > 2) zPunct = double.Parse(segmente[2]);
                        break;

                    case Format.KmOZ:
                    case Format.KmOZD:
                        kmPunct = double.Parse(segmente[0].Replace("+", ""));
                        offPunct = double.Parse(segmente[1]);
                        if (segmente.Length > 2) zPunct = double.Parse(segmente[2]);
                        if (segmente.Length > 3) desPunct = segmente[3];
                        break;

                    case Format.KmOENZ:
                        kmPunct = double.Parse(segmente[0].Replace("+", ""));
                        offPunct = double.Parse(segmente[1]);
                        xPunct = double.Parse(segmente[2]);
                        yPunct = double.Parse(segmente[3]);
                        if (segmente.Length > 4) zPunct = double.Parse(segmente[4]);
                        break;

                    case Format.NrENZ:
                    case Format.NrENZD:
                        nrPunct = int.Parse(segmente[0]);
                        xPunct = double.Parse(segmente[1]);
                        yPunct = double.Parse(segmente[2]);
                        if (segmente.Length > 3) zPunct = double.Parse(segmente[3]);
                        if (segmente.Length > 4) desPunct = segmente[4];
                        break;

                    case Format.NrKmOZ:
                    case Format.NrKmOZD:
                        nrPunct = int.Parse(segmente[0]);
                        kmPunct = double.Parse(segmente[1].Replace("+", ""));
                        offPunct = double.Parse(segmente[2]);
                        if (segmente.Length > 3) zPunct = double.Parse(segmente[3]);
                        if (segmente.Length > 4) desPunct = segmente[4];
                        break;

                    case Format.NrKmOENZ:
                    case Format.NrKmOENZD:
                        nrPunct = int.Parse(segmente[0]);
                        kmPunct = double.Parse(segmente[1].Replace("+", ""));
                        offPunct = double.Parse(segmente[2]);
                        xPunct = double.Parse(segmente[3]);
                        yPunct = double.Parse(segmente[4]);
                        if (segmente.Length > 5) zPunct = double.Parse(segmente[5]);
                        if (segmente.Length > 6) desPunct = segmente[6];
                        break;
                        
                }

                return true;
            }

            catch
            {
                return false;
            }
        }

        //Functie statica ce returneaza un Punct3D cu valori citite dintr-un segment text
        public static Punct3D StringToPoint(string text, Format tip, DelimitedBy separator)
        {
            
            try
            {
                Punct3D punct = new Punct3D();
                punct.ValuesFromString(text, tip, separator);
                return punct;
            }
            catch
            {
                return null;
            }

        }

    }

    public class Obiect3D<T> : Punct3D
    {
        T obj = default(T);

        //public static implicit operator Obiect3D<T>(Punct3D P)
        //{

        //}

        //Proprietate care stocheaza un obiect atasat de tipul T
        public T ObAtasat
        {
            set { if (value is T) obj = (T)value; }
            get { return obj; }
        }
    }

    public class String3D : List<Punct3D>
    {
        //enum Format { ENZ, KmOZ, KmOENZ };
        //enum DelimitedBy { Comma, Space };
        public System.Exception eroare;

        //Functie de importare a punctelor dintr-un fisier text
        public bool ImportPoints(string cale, Punct3D.Format tip, Punct3D.DelimitedBy separator)
        {
            try
            {
                cale = Path.GetFullPath(cale);
                using (StreamReader cititor = new StreamReader(cale))
                {
                    string linie;

                    while ((linie = cititor.ReadLine()) != null)
                    {
                        this.Add((Punct3D)Punct3D.StringToPoint(linie, tip, separator));
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        //Functie de importare a punctelor dintr-un fisier GSI
        public bool ImportGSI(string cale)
        {
            try
            {
                cale = Path.GetFullPath(cale);
                using (StreamReader cititor = new StreamReader(cale))
                {
                    string linie;

                    while ((linie = cititor.ReadLine()) != null)
                    {
                        string[] campuri = linie.Split(' ');
                        Punct3D punct = new Punct3D();
                        foreach (string camp in campuri)
                        {
                            if (camp.StartsWith("*11"))
                            {
                                punct.Nr = Convert.ToInt32(camp.Substring(3, camp.IndexOf('+') - 3));
                                punct.D = camp.Substring(camp.IndexOf('+') + 1).TrimStart('0');
                            }
                            if (camp.StartsWith("81"))
                            {
                                //punct.X = Double.Parse(camp.Substring(camp.Length - 9)) / 1000;
                                punct.X = Double.Parse(camp.Substring(camp.IndexOf('+') + 1)) / 1000;
                            }
                            if (camp.StartsWith("82"))
                            {
                                punct.Y = Double.Parse(camp.Substring(camp.IndexOf('+') + 1)) / 1000;
                            }
                            if (camp.StartsWith("83"))
                            {
                                punct.Z = Double.Parse(camp.Substring(camp.IndexOf('+') + 1)) / 1000;
                            }
                        }
                        if (punct.X != 0) this.Add(punct);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Functie de exportare a punctelor intr-un fisier text (Atentie! Suprascriere automata!)
        public bool ExportPoints(string cale, Punct3D.Format tip, Punct3D.DelimitedBy separator, int nrZecimale, bool formatKM)
        {
            try
            {
                cale = Path.GetFullPath(cale);
                using (StreamWriter scriitor = new StreamWriter(cale))
                {
                    foreach (Punct3D punct in this)
                    {
                        scriitor.WriteLine(punct.toString(tip, separator, nrZecimale, formatKM));
                    }
                }

                return true;
            }
            catch(SystemException e)
            {
                eroare = e;
                return false;
            }
        }
    }

    public class StringUtil
    {
        public static String3D XSintersect(String3D S1, String3D S2)
        {
            String3D S = new String3D();

            for (int i = 0; i <= S1.Count - 2; i++)
            {
                for (int j = 0; j <= S2.Count - 2; j++)
                {
                    Punct3D punct = (Punct3D)XSint4P(S1[i], S1[i + 1], S2[j], S2[j + 1]);
                    if (punct.Offset >= S1[i].Offset && punct.Offset <= S1[i + 1].Offset)
                    {
                        S.Add(punct);
                    }
                }

            }

            if (S.Count > 0) return S;
            else return null;
        }

        public static String3D XStrimextend(String3D XS, String3D XSTinta)
        {
            String3D XSrezultat = new String3D();

            if (XS.Count < 2 || XSTinta.Count < 2) return XS;

            for (int i = 0; i < XS.Count; i++)
            {
                if (i == 0)
                {
                        for (int j = 0; j < XSTinta.Count - 1; j++)
                        {
                            Punct3D P = (Punct3D)XSint4P(XS[0], XS[1], XSTinta[j], XSTinta[j + 1]);
                            if (P.Offset <= XS[1].Offset && P.Offset >= XSTinta[j].Offset && P.Offset <= XSTinta[j + 1].Offset)
                            {
                                XSrezultat.Add(P);
                            }
                        }
                        if (XSrezultat.Count == 0) XSrezultat.Add(XS[0]);
                }
                else if (i == XS.Count - 1)
                {
                    for (int j = 0; j < XSTinta.Count - 1; j++)
                    {
                        Punct3D P = (Punct3D)XSint4P(XS[XS.Count - 2], XS[XS.Count - 1], XSTinta[j], XSTinta[j + 1]);
                        if (P.Offset >= XS[XS.Count - 2].Offset && P.Offset >= XSTinta[j].Offset && P.Offset <= XSTinta[j + 1].Offset)
                        {
                            XSrezultat.Add(P);
                        }
                    }
                    if (XSrezultat.Count < XS.Count) XSrezultat.Add(XS[XS.Count - 1]);
                }
                else
                {
                    XSrezultat.Add(XS[i]);
                }
            }

            return XSrezultat;
        }

        public static Punct3D XSint4P(Punct3D A1, Punct3D A2, Punct3D B1, Punct3D B2)
        {
            try
            {
                //Calculul parametrilor segmentului A1A2
                double aA = (A2.Z - A1.Z) / (A2.Offset - A1.Offset);
                double bA = A2.Z - A2.Offset / (A2.Offset - A1.Offset) * (A2.Z - A1.Z);

                //Calculul parametrilor segmentului B1B2
                double aB = (B2.Z - B1.Z) / (B2.Offset - B1.Offset);
                double bB = B2.Z - B2.Offset / (B2.Offset - B1.Offset) * (B2.Z - B1.Z);

                //Calculul punctului de intersectie
                double offInt = (bB - bA) / (aA - aB);
                double zInt = aA * offInt + bA;

                Punct3D punct = new Punct3D();
                punct.KM = A1.KM;
                punct.Offset = offInt;
                punct.Z = zInt;
                punct.D = A1.D + "-x-" + B1.D;

                return punct;
            }
            catch
            {
                return null;
            }
        }
    }
}
