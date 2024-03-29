﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBuilt.Revit.BundleBuilder.Data.Models
{
    public static class Project
    {
        public static string Name { get; set; }
        public static string Number { get; set; }
        public static string Location { get; set; }
        public static List<Truck> Trucks { get; set; } = new List<Truck>();
        public static List<Bundle> Bundles { get; set; } = new List<Bundle>();
        public static List<Panel> Panels { get; set; } = new List<Panel>();
        public static int CurrentBundleNumber { get; set; } = 1;

        /// <summary>
        /// Removes a bundle from Bundles
        /// </summary>
        /// <param name="bundle">bundle to remove</param>
        public static void Remove(Bundle bundle)
        {
            if (Bundles.Contains(bundle))
                Bundles.Remove(bundle);
        }
    }
}
