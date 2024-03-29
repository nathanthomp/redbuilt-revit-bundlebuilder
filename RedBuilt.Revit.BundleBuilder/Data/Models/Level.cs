﻿using RedBuilt.Revit.BundleBuilder.Application.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBuilt.Revit.BundleBuilder.Data.Models
{
    public class Level
    {
        public int Number { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public Bundle Bundle { get; set; }
        public List<Panel> Panels { get; set; }

        /// <summary>
        /// Level constructor
        /// </summary>
        /// <param name="levelNumber">initial number</param>
        public Level(int levelNumber)
        {
            Number = levelNumber;
            Panels = new List<Panel>();
        }

        /// <summary>
        /// Adds a panel to this level
        /// </summary>
        /// <param name="panel">the panel to add</param>
        /// <returns>true if add was successful, false otherwise</returns>
        public bool Add(Panel panel)
        {
            if (!Panels.Contains(panel))
            {
                Panels.Add(panel);
                Width += panel.Width.AsDouble;
                Length = Panels.Max(x => x.Height.AsDouble);
                Weight += panel.Weight;
                panel.Level = this;
                panel.Bundle = Bundle;
                if (Height == 0)
                    Height = panel.Plate.Width;

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Removes a panel from this level
        /// </summary>
        /// <param name="panel">the panel to remove</param>
        /// <returns>true if remove was successful, false otherwise</returns>
        public bool Remove(Panel panel)
        {
            if (Panels.Contains(panel))
            {
                Panels.Remove(panel);
                Width -= panel.Width.AsDouble;
                Weight -= panel.Weight;

                if (Panels.Count > 0)
                    Length = Panels.Max(x => x.Height.AsDouble);
                else
                    Length = 0;

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Level object ToString() method
        /// </summary>
        /// <returns>level bundle and level number</returns>
        public override string ToString()
        {
            return String.Format("Bundle {0} - Level {1}", Bundle.Number, Number);
        }
    }
}
