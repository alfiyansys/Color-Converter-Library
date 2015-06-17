/**
 * Color Model Converter Library (C#)
 * by M. Alfiyan Syamsuddin
 * 2110121030
 * Electronic Engineering Polytechnic Institute of Surabaya 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Color_Model {
    class ColorConverter {
        private static double max(double r, double g, double b) {
            double max = r;
            if (max < g) {
                max = g;
            }
            if (max < b) {
                max = b;
            }
            return max;
        }

        private static double min(double r, double g, double b) {
            double min = r;
            if (min > g) {
                min = g;
            }
            if (min > b) {
                min = b;
            }
            return min;
        }

        private static double[] prosenRGB(int r, int g, int b){
            double[] warna = { (double)r / 255, (double)b / 255, (double)b / 255 };
            return warna;
        }

        public static double[] RGBtoCIE(int r, int g, int b) {
            double Rx = r; double Gx = g; double Bx = b;
            double X = 0.723 * Rx + 0.273 * Gx + 0.166 * Bx;
            double Y = 0.265 * Rx + 0.717 * Gx + 0.008 * Bx;
            double Z = 0.0 * Rx + 0.008 * Gx + 0.824 * Bx;
            
            double x = X / (X + Y + Z);
            double y = Y / (X + Y + Z);
            double z = 1 - X - Y;

            double[] cie = { x, y, z };
            return cie;
        }        

        public static double[] RGBtoCMYK(int r, int g, int b) {
            double[] x = prosenRGB(r,g,b);

            double k = 1 - max(x[0], x[1], x[2]);
            double c = (1 - x[0] - k) / (1 - k);
            double m = (1 - x[1] - k) / (1 - k);
            double y = (1 - x[2] - k) / (1 - k);

            double[] cmyk = { c, m, y, k };
            return cmyk;
        }

        public static double[] RGBtoHSV(int r, int g, int b) {
            double[] x = prosenRGB(r, g, b);

            double k = 1 - max(x[0], x[1], x[2]);
            double c = (1 - x[0] - k) / (1 - k);
            double m = (1 - x[1] - k) / (1 - k);
            double y = (1 - x[2] - k) / (1 - k);

            double[] cmyk = { c, m, y, k };
            return cmyk;
        }

        public static double[] RGBtoHSV(int r, int g, int b) {
            double[] x = prosenRGB(r, g, b);
            double Cmax = max(x[0], x[1], x[2]);
            double Cmin = min(x[0], x[1], x[2]);
            double d = Cmax - Cmin;

            double h = 0, s = 0;
            if (Cmax == x[0]) {
                h = 60 * ((x[1] - x[2]) / d);
            }
            if (Cmax == x[1]) {
                h = 60 * ((x[2] - x[0]) / d + 2);
            }
            if (Cmax == x[2]) {
                h = 60 * ((x[0] - x[1]) / d + 4);
            }
            if (d == 0) {
                s = 0;
            } else {
                s = d / Cmax;
            }

            double v = Cmax;
            double[] hsv = { h, s, v };
            return hsv;
        }

        public static double[] RGBtoHSL(int r, int g, int b) {
            double[] x = prosenRGB(r, g, b);
            double Cmax = max(x[0], x[1], x[2]);
            double Cmin = min(x[0], x[1], x[2]);
            double d = Cmax - Cmin;

            double h = 0, s = 0;
            if (Cmax == x[0]) {
                h = 60 * ((x[1] - x[2]) / d);
            }
            if (Cmax == x[1]) {
                h = 60 * ((x[2] - x[0]) / d + 2);
            }
            if (Cmax == x[2]) {
                h = 60 * ((x[0] - x[1]) / d + 4);
            }
            if (d == 0) {
                s = 0;
            } else {
                s = d / Cmax;
            }

            double l = (Cmax + Cmin) / 2;
            double[] hsv = { h, s, l };
            return hsv;
        }
    }
}
