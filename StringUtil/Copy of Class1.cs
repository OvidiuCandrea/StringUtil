using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;

namespace Ovidiu.StringUtil
{
    public class Punct3D
    {
        double xPunct = 0;
        double yPunct = 0;
        double zPunct = 0;
        double kmPunct = 0;
        double offPunct = 0;

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

        public enum Format { ENZ, KmOZ, KmOENZ };
        public enum DelimitedBy { Comma, Space };

        //Functie ce completeaza proprietatile selectate ale punctului intr-o matrice
        public double[] toArray(Format tip)
        {
            switch (tip)
            {
                case Format.ENZ:
                    return new double[] { xPunct, yPunct, zPunct };
                //    break;
                case Format.KmOZ:
                    return new double[] { kmPunct, offPunct, zPunct };
                //    break;
                case Format.KmOENZ:
                    return new double[] { kmPunct, offPunct, xPunct, yPunct, zPunct };
                default:
                    return new double[] { xPunct, yPunct, zPunct };
                //    break;
            }
        }

        //Functie ce completeaza proprietatile selectate ale punctului intr-un segment text
        public string toString(Format tip, DelimitedBy separator, int Precision)
        {
            //Setarea separatorului folosit
            string sep;
            if (separator == DelimitedBy.Space) sep = " ";
            else sep = ",";

            //Convertirea valorilor in format text, cu precizia aleasa
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalDigits = Precision;
            string stringX = xPunct.ToString("F", nfi);
            string stringY = yPunct.ToString("F", nfi);
            string stringZ = zPunct.ToString("F", nfi);
            string stringKM = kmPunct.ToString("0+000.000", nfi);
            string stringOffset = offPunct.ToString("F", nfi);
            
            //string stringX = xPunct.ToString("F" + Precision);
            //string stringY = yPunct.ToString("F" + Precision);
            //string stringZ = zPunct.ToString("F" + Precision);
            //string stringKM = kmPunct.ToString("0+000.000");
            //string stringOffset = offPunct.ToString("F" + Precision);

            switch (tip)
            {
                case Format.ENZ:
                    return stringX + sep + stringY + sep + stringZ;
                //    break;
                case Format.KmOZ:
                    return stringKM + sep + stringOffset + sep + stringZ;
                //    break;
                case Format.KmOENZ:
                    return stringKM + sep + stringOffset + sep + stringX + sep + stringY + sep + stringZ;
                default:
                    return stringX + sep + stringY + sep + stringZ;
                //    break;
            }

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
                        kmPunct = double.Parse(segmente[0].Replace("+", ""));
                        offPunct = double.Parse(segmente[1]);
                        if (segmente.Length > 2) zPunct = double.Parse(segmente[2]);
                        break;

                        case Format.KmOENZ:
                        kmPunct = double.Parse(segmente[0].Replace("+", ""));
                        offPunct = double.Parse(segmente[1]);
                        xPunct = double.Parse(segmente[2]);
                        yPunct = double.Parse(segmente[3]);
                        if (segmente.Length > 4) zPunct = double.Parse(segmente[4]);
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

    public class String3D : List<Punct3D>
    {
        //enum Format { ENZ, KmOZ, KmOENZ };
        //enum DelimitedBy { Comma, Space };

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
                        this.Add(Punct3D.StringToPoint(linie, tip, separator));
                    }
                }
                return true;
            }
            catch
            {
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
                    Punct3D punct = XSint4P(S1[i], S1[i + 1], S2[j], S2[j + 1]);
                    if (punct.Offset >= S1[i].Offset && punct.Offset <= S1[i + 1].Offset)
                    {
                        S.Add(punct);
                    }
                }

            }

            if (S.Count > 0) return S;
            else return null;
        }

        public static Punct3D XSint4P(Punct3D A1, Punct3D A2, Punct3D B1, Punct3D B2)
        {
            try
            {
                //Calculul parametrilor segmentului A1A2
                double aA = (A2.Z - A1.Z) / (A2.Offset - A1.Offset);
                double bA = A1.Offset * (A2.Z - A1.Z) / (A1.Offset - A2.Offset) - A1.Z;

                //Calculul parametrilor segmentului B1B2
                double aB = (B2.Z - B1.Z) / (B2.Offset - B1.Offset);
                double bB = B1.Offset * (B2.Z - A1.Z) / (B1.Offset - B2.Offset) - A1.Z;

                //Calculul punctului de intersectie
                double offInt = (bB - bA) / (aA - aB);
                double zInt = A1.Z + (offInt - A1.Offset) * aA;

                Punct3D punct = new Punct3D();
                punct.KM = A1.KM;
                punct.Offset = offInt;
                punct.Z = zInt;

                return punct;
            }
            catch
            {
                return null;
            }
        }
    }
}
