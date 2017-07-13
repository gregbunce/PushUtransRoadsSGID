using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace PushUtransRoadsSGID
{
    /// <summary>
    /// Summary description for RemoveDuplicateVertices.
    /// </summary>
    [Guid("c69b83f0-0d8d-4e36-8a95-86935303eaf8")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("PushUtransRoadsSGID.RemoveDuplicateVertices")]
    public sealed class clsBtnRemoveDuplicateVertices : BaseCommand
    {
        #region COM Registration Function(s)

        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code

        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);

        }

        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);

        }

        #endregion

        #endregion

        //private IApplication m_application;
        public clsBtnRemoveDuplicateVertices()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "AGRC"; //localizable text
            base.m_caption = "  Remove Duplicate Vertices"; //localizable text
            base.m_message =
                "Works on the selected features in REMOVE_DUP_VERTS layer and removes the vertices if they are within 1 meter, but keeps the coincident one (shared with other feature one)."; //localizable text
            base.m_toolTip =
                "Works on the selected features in REMOVE_DUP_VERTS layer and removes the vertices if they are within 1 meter, but keeps the coincident one (shared with other feature one)."; //localizable text 
            base.m_name = "RemoveDupVert"; //unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")
            base.m_bitmap = Properties.Resources.TmCompareSelectedFeatures16;
        }


        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            clsGlobals.arcApplication = hook as IApplication;

            //Disable if it is not ArcMap
            if (hook is IMxApplication)
                base.m_enabled = true;
            else
                base.m_enabled = false;
        }


        public override void OnClick()
        {
            try
            {
                // get access to the current arcmap variables
                clsPushSgidStaticClass.GetCurrentMapDocVariables();

                // show the cursor as busy
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                clsGlobals.pGFlayer = null;
                clsGlobals.arcFeatLayer = null;

                // loop through the map's layers and check for the layer with the targeted name
                for (int i = 0; i < clsGlobals.pMap.LayerCount; i++)
                {
                    if (clsGlobals.pMap.Layer[i].Name == "REMOVE_DUP_VERTS")
                    {
                        clsGlobals.pGFlayer = (IGeoFeatureLayer) clsGlobals.pMap.Layer[i];
                        clsGlobals.arcFeatLayer = (IFeatureLayer) clsGlobals.pMap.Layer[i];
                    }
                }

                // make sure the user is editing
                //get the editor extension
                UID arcUID = new UID();
                arcUID.Value = "esriEditor.Editor";
                clsGlobals.arcEditor = clsGlobals.arcApplication.FindExtensionByCLSID(arcUID) as IEditor3;

                // check if editing first
                if (clsGlobals.arcEditor.EditState == ESRI.ArcGIS.Editor.esriEditState.esriStateNotEditing)
                {
                    MessageBox.Show("You must be editing in order to remove duplicate vertices.", "Must Be Editing",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                IDataset arcDataset = (IDataset) clsGlobals.arcFeatLayer;
                IWorkspace arcWorkspace = arcDataset.Workspace;
                IWorkspaceEdit arcWorkspaceEdit = (IWorkspaceEdit) arcWorkspace;

                // make sure we're editing the correct workspace
                if (!(arcWorkspaceEdit.IsBeingEdited()))
                {
                    MessageBox.Show("You must be editing the REMOVE_DUP_VERTS layer in order to proceed.",
                        "Must Be Editing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // get access to the selected features in the user specified layer
                IDisplayTable arcDisplayTable = (IDisplayTable) clsGlobals.arcFeatLayer;
                ISelectionSet arcSelectionSet = arcDisplayTable.DisplaySelectionSet;

                // make sure there's at least one feature selected in the specified layer
                if (arcSelectionSet.Count == 0)
                {
                    MessageBox.Show(
                        "You must select at least one feature in the REMOVE_DUP_VERTS layer to spatially assign values to.",
                        "No Features are Selected.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // confirm the user wants to edit that layer's fields on the selected records
                DialogResult dialogResult =
                    MessageBox.Show(
                        "Would you like to proceed with editing " + arcSelectionSet.Count +
                        " features on the REMOVE_DUP_VERTS Layer, removing non-coincident verticies that are within 1 meter of eachother?",
                        "Confirm Edits", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    // loop through the selected feature remove_dup_verts layer
                    IEnumIDs arcEnumIDs = arcSelectionSet.IDs;
                    int iD;
                    while ((iD = arcEnumIDs.Next()) != -1)
                    {
                        clsGlobals.arcFeatureToEditSpatial = clsGlobals.arcFeatLayer.FeatureClass.GetFeature(iD);
                        clsGlobals.arcEditor.StartOperation();

                        // Loop through this features verticies.
                        //get the feature's geometry
                        IGeometry arcEdit_geometry = clsGlobals.arcFeatureToEditSpatial.ShapeCopy;
                        IPolyline arcEdit_polyline = arcEdit_geometry as IPolyline;

                        // get a point collection
                        IPointCollection pointCollection = (IPointCollection) arcEdit_polyline;

                        // Iterate the array (the first point is the start of the line and last is the end of the line)
                        for (int i = 0; i < pointCollection.PointCount; i++)
                        {
                            IPoint point = pointCollection.get_Point(i);

                            MessageBox.Show("X:" + point.X + " , Y:" + point.Y);
                            // ...and do something with each vertex
                        }




                        clsGlobals.arcFeatureToEditSpatial.Store();
                        clsGlobals.arcEditor.StopOperation("RemovedDuplicateVertices");
                    }

                    MessageBox.Show(
                        "Done updating " + arcSelectionSet.Count +
                        " features on the REMOVE_DUP_VERTS Layer, removing non-coincident vertices that were within 1 meter of eachother.  Don't forget to save edits if you want to retain the changes.",
                        "Done!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                                    "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                                    "Error Location:" + Environment.NewLine + ex.StackTrace,
                                    "Push Utrans Roads to SGID!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    clsGlobals.arcEditor.StopOperation("RemovedDuplicateVertices");
                }
        }
    }
}
