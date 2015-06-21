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
        public int r,g,b;

        public static int limitRGB(int col) {
            if (col > 255) {
                col = 255;
            } else if (col < 0) {
                col = 0;
            }
            return col;
        }

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

        private static double max(double r, double g) {
            double max = r;
            if (max < g) {
                max = g;
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

        private static double min(double r, double g) {
            double min = r;
            if (min > g) {
                min = g;
            }
            return min;
        }

        private static double[] prosenRGB(int r, int g, int b){
            double[] warna = { (double)r / 255, (double)g / 255, (double)b / 255 };
            //Console.WriteLine(warna[0]+" "+warna[1]+" "+warna[2]);
            return warna;
        }

        private static int[] mulRGB(double r, double g, double b) {
            int[] warna = { (int)(r * 255), (int)(g * 255), (int)(b * 255) };
            return warna;
        }

        /**
         * From RGB
         */

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

        public static double[] RGBtoYCrCb(int r, int g, int b) {
            double y = (0.299 * r) + (0.587 * g) + (0.114 * b);
            double cr = 128 + (0.5 * r) - (0.418688 * g) - (0.081312 * b);
            double cb = 128 - (0.168736 * r) - (0.331264 * g) + (0.5 * b);
            double[] ycrcb = { y, cr, cb };
            return ycrcb;
        }

        /**
         * To RGB
         */

        public static int[] CIEtoRGB(double x, double y, double z) {
            //not implemented yet
            int[] rgb = { 0 };
            return rgb;
        }

        public static int[] YCrCbtoRGB(double y, double cr, double cb) {
            double Y = y;
            double Cb = cb;
            double Cr = cr;

            int r = (int)(Y + 1.40200 * (Cr - 128));
            int g = (int)(Y - 0.34414 * (Cb - 128) - 0.71414 * (Cr - 128));
            int b = (int)(Y + 1.77200 * (Cb - 128));

            r = (int)max(0, min(255, r));
            g = (int)max(0, min(255, g));
            b = (int)max(0, min(255, b));

            int[] rgb = { r, g, b };
            return rgb;
        }

        public static int[] CMYKtoRGB(double c, double m, double y, double k) {
            double r = 255 * (1 - c) * (1 - k);
            double g = 255 * (1 - m) * (1 - k);
            double b = 255 * (1 - y) * (1 - k);

            int[] rgb = { (int)Math.Round(r), (int)Math.Round(g), (int)Math.Round(b) };
            return rgb;
        }

        public static int[] HSVtoRGB(double h, double s, double v) {
            double c = v * s;
            double x = c * (1 - Math.Abs(((h / 60) % 2) - 1));
            double m = v - c;
            double[] xrgb = { 0, 0, 0 };
            if (0 <= h && h < 60) {
                xrgb = new double[]{c, x, 0};
            }else if (60 <= h && h < 120) {
                xrgb = new double[]{x, c, 0};
            }else if (120 <= h && h < 180) {
                xrgb = new double[]{0, c, x};
            }else if (180 <= h && h < 240) {
                xrgb = new double[]{0, x, c};
            }else if (240 <= h && h < 300) {
                xrgb = new double[]{x, 0, c};
            }else if (300 <= h && h < 360) {
                xrgb = new double[]{c, 0, x};
            }

            int[] rgb = { (int)Math.Round((xrgb[0] + m) * 255), (int)Math.Round((xrgb[1] + m) * 255), (int)Math.Round((xrgb[2] + m) * 255) };
            return rgb;
        }

        public static int[] HSLtoRGB(double h, double s, double l) {
            double c = (1-Math.Abs(2*l-1))*s;
            double x = c * (1 - Math.Abs(((h / 60) % 2) - 1));
            double m = l - c / 2;
            double[] xrgb = { 0, 0, 0 };
            if (0 <= h && h < 60) {
                xrgb = new double[] { c, x, 0 };
            } else if (60 <= h && h < 120) {
                xrgb = new double[] { x, c, 0 };
            } else if (120 <= h && h < 180) {
                xrgb = new double[] { 0, c, x };
            } else if (180 <= h && h < 240) {
                xrgb = new double[] { 0, x, c };
            } else if (240 <= h && h < 300) {
                xrgb = new double[] { x, 0, c };
            } else if (300 <= h && h < 360) {
                xrgb = new double[] { c, 0, x };
            }

            int[] rgb = { (int)Math.Round((xrgb[0] + m) * 255), (int)Math.Round((xrgb[1] + m) * 255), (int)Math.Round((xrgb[2] + m) * 255) };
            return rgb;
        }
    }
}
