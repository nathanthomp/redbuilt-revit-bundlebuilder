﻿using RedBuilt.Revit.BundleBuilder.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBuilt.Revit.BundleBuilder.Application.Reports
{
    public class BundleReport
    {
        public static void Export()
        {

            StreamWriter sw = new StreamWriter(@"C:\RedBuilt\Revit\BundleBuilder\RedBuilt.Revit.BundleBuilder\Documents\BundleReport.html");
            PrintHeader(sw);

            for (int i = 0; i < Project.Bundles.Count; i++)
            {
                Bundle bundle = Project.Bundles[i];
                sw.WriteLine("<div class=\"bundle\">");
                sw.WriteLine("<p class=\"header\">B");
                if (bundle.Number < 10)
                {
                    sw.Write("0");
                }
                sw.Write(bundle.Number + " - " + bundle.Type + " " + bundle.Plate + "</p>");

                sw.WriteLine("<div class=\"length-side\">");
                for (int j = bundle.Levels.Count; j > 0; j--)
                {
                    Level level = bundle.Levels.Where(x => x.Number == j).FirstOrDefault();
                    sw.WriteLine("<div class=\"panel\" style=\"width: " + level.Length * 1.75 + "px; height: " + level.Height * 1.75 + "px\"><p class=\"label\">");
                    foreach (Panel panel in level.Panels)
                    {
                        sw.Write(panel.Name + " ");
                    }

                    sw.Write("</div>");
                }
                sw.WriteLine("</p></div>");

                sw.WriteLine("<div class=\"width-side\">");
                for (int j = bundle.Levels.Count; j > 0; j--)
                {
                    Level level = bundle.Levels.Where(x => x.Number == j).FirstOrDefault();
                    sw.WriteLine("<div class=\"panel\" style=\"width: " + level.Width * 1.75 + "px; height: " + level.Height * 1.75 + "px\"></div>");
                }
                sw.WriteLine("</div>");

                for (int k = 0; k < bundle.Levels.Count * 2; k++)
                {
                    sw.WriteLine("<br/>");
                }

                sw.WriteLine("</div>");
            }

            PrintFooter(sw);
            sw.Close();
        }

        private static void PrintHeader(StreamWriter sw)
        {
            sw.WriteLine("<!DOCTYPE html>");
            sw.WriteLine("<html><head><style>");
            sw.WriteLine(".length-side{ float: left; }");
            sw.WriteLine(".width-side{ float: right; }");
            sw.WriteLine(".panel{ border: 1px solid; margin: 1px; }");
            sw.WriteLine(".bundle{ margin-bottom: 10px; }");
            sw.WriteLine(".header{ font-size: 12px; margin-bottom: 1px; }");
            sw.WriteLine(".label{ font-size: 10px; margin: 0px; }");
            sw.WriteLine("</style></head><body>");
        }

        private static void PrintFooter(StreamWriter sw)
        {
            sw.WriteLine("</body></html>");
        }
    }
}
