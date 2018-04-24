using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.Geodatabase;

namespace PushUtransRoadsSGID
{
    /// <summary>
    /// Summary description for clsBtnAssignMileposts.
    /// </summary>
    [Guid("47ae1b25-8a26-4dbb-adbf-21fbabeffa30")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("PushUtransRoadsSGID.clsBtnAssignMileposts")]
    public sealed class clsBtnAssignMileposts : BaseCommand
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

        private IApplication m_application;
        public clsBtnAssignMileposts()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "AGRC"; //localizable text
            base.m_caption = "Assign Mileposts";  //localizable text
            base.m_message = "Make sure the selected layer in the TOC is the desired Roads layer to be edited and that the SGID LRS Layer is the top-most layer. This tool will null out existing From/To mileposts and then reassign those values based on the SGID LRS.";  //localizable text 
            base.m_toolTip = "Assign Mileposts";  //localizable text 
            base.m_name = "AssignMilepost";   //unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")
            base.m_bitmap = Properties.Resources.AnimationMoveLayerAlongPath16;
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            m_application = hook as IApplication;

            //Disable if it is not ArcMap
            if (hook is IMxApplication)
                base.m_enabled = true;
            else
                base.m_enabled = false;

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            try
            {
                clsGlobals.pMxDocument = (IMxDocument)clsGlobals.arcApplication.Document;
                clsGlobals.pMap = clsGlobals.pMxDocument.FocusMap;
                clsGlobals.pActiveView = clsGlobals.pMxDocument.ActiveView;

                // make sure the user is editing
                //get the editor extension
                UID arcUID = new UID();
                arcUID.Value = "esriEditor.Editor";
                clsGlobals.arcEditor = clsGlobals.arcApplication.FindExtensionByCLSID(arcUID) as IEditor3;

                // check if editing first
                if (clsGlobals.arcEditor.EditState == ESRI.ArcGIS.Editor.esriEditState.esriStateNotEditing)
                {
                    MessageBox.Show("You must be editing in order to assign mileposts.", "Must Be Editing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                // Get access to the needed layers (UTRANS Roads and LRS layers)
                // make sure the user has selected a layer in the toc
                if (clsGlobals.pMxDocument.SelectedLayer == null)
                {
                    MessageBox.Show("Please select the UTRANS roads layer in ArcMap's TOC.", "Select Layer in TOC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!(clsGlobals.pMxDocument.SelectedLayer is IFeatureLayer))
                {
                    MessageBox.Show("Please select the UTRANS roads layer in ArcMap's TOC.", "Must be Feature Layer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //cast the selected layer as a feature layer
                clsGlobals.pGFlayer = (IGeoFeatureLayer)clsGlobals.pMxDocument.SelectedLayer;

                //check if the feaure layer is a line layer
                if (clsGlobals.pGFlayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolyline)
                {
                    MessageBox.Show("Please select a  line layer (UTRANS Roads) in the TOC.", "Must be a Line Layer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Set the Roads GeoFeatureLayer to a FeatureLayer.
                IFeatureLayer featureLayerRoads = (IFeatureLayer)clsGlobals.pGFlayer;

                // Get access to the UTRANS LRS layer in the map - expect it to be the top-most layer in the TOC.
                IFeatureLayer featureLayerLRS = (IFeatureLayer)clsGlobals.pMap.Layer[0];

                // Make sure the alias name for the LRS layer is as such...
                if (featureLayerLRS.FeatureClass.AliasName != "SGID10.TRANSPORTATION.UDOTRoutes_LRS")
                {
                    MessageBox.Show("Make sure the top most layer in the TOC is pointed to SGID10.TRANSPORTATION.UDOTRoutes_LRS", "LRS Layer Missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Null out the existing from/to milepost values so we can track the new ones.
                clsGlobals.arcEditor.StartOperation();

                // Null out the existing DOT_F_MILE and DOT_T_MILE values
                IQueryFilter queryFilter = new QueryFilter();
                queryFilter.WhereClause = "DOT_F_MILE >= 0 or DOT_T_MILE >= 0";
                IFeatureCursor featureCursor = featureLayerRoads.FeatureClass.Search(queryFilter, false);
                IFeature arcFeature;
                while ((arcFeature = featureCursor.NextFeature()) != null)
                {
                    arcFeature.set_Value(arcFeature.Fields.FindField("DOT_F_MILE"), DBNull.Value);
                    arcFeature.set_Value(arcFeature.Fields.FindField("DOT_T_MILE"), DBNull.Value);
                    arcFeature.Store();
                }

                // null out variables
                queryFilter = null;
                featureCursor = null;
                arcFeature = null;

                //// Null out the existing DOT_T_MILE values
                //queryFilter = new QueryFilter();
                //queryFilter.WhereClause = "DOT_T_MILE >= 0";
                //featureCursor = featureLayerLRS.FeatureClass.Search(queryFilter, false);
                //while ((arcFeature = featureCursor.NextFeature()) != null)
                //{
                //    arcFeature.set_Value(arcFeature.Fields.FindField("DOT_T_MILE"), null);
                //}
                //// null out variables
                //queryFilter = null;
                //featureCursor = null;
                //arcFeature = null;

                clsGlobals.arcEditor.StopOperation("Null MP Values");

                MessageBox.Show("Done updating assigning milepost values from SGID LRS Layer.  Don't forget to SAVE edits!", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "clsBtnAssignMilepost: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }



        }

        #endregion
    }
}
