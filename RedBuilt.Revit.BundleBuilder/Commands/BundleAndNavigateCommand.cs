﻿using RedBuilt.Revit.BundleBuilder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RedBuilt.Revit.BundleBuilder.Commands
{
    public class BundleAndNavigateCommand<T> : Command
                where T : ViewModel
    {
        private readonly NavigationState _navigationState;
        private readonly Func<T> _createViewModel;

        public BundleAndNavigateCommand(NavigationState navigationState, Func<T> createViewModel)
        {
            _navigationState = navigationState;
            _createViewModel = createViewModel;
        }

        public override void Execute(object parameter)
        {
            // Assign panels to project panels only if the ToBundle property is true
            Project.Panels = Project.Panels.Where(x => x.ToBundle = true).ToList();

            // Sort exterior panels by user preference
            string startPanelType = Application.Tools.PanelTools.GetPanelFromName(Settings.StartingPanel).Type.Name;
            string startPanelPlate = Application.Tools.PanelTools.GetPanelFromName(Settings.StartingPanel).Plate.Description;
            Project.Panels = Application.Sort.PanelPreferenceSort.Sort(Project.Panels);

            // Sort panels by type then plate
            Dictionary<string, Dictionary<string, List<Panel>>> panelsByTypeThenPlate = Application.Sort.PanelTypeAndPlateSort.Sort();

            // Solve
            // send list of panels that includes the starting panel
            // send the rest of the panels

            MessageBox.Show("Bundled!");
            Application.Reports.TestReport.Export();

            // Update current view
            _navigationState.CurrentViewModel = _createViewModel();
        }
    }
}
