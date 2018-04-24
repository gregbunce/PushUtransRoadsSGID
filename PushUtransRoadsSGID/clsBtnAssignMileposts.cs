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

                //// NULL OUT the existing from/to milepost values so we can track the new ones. ////
                clsGlobals.arcEditor.StartOperation();

                // Null out the existing DOT_F_MILE and DOT_T_MILE values
                IQueryFilter queryFilter = new QueryFilter();
                queryFilter.WhereClause = "DOT_F_MILE >= 0 or DOT_T_MILE >= 0";
                IFeatureCursor arcFeatureCursor = featureLayerRoads.FeatureClass.Search(queryFilter, false);
                IFeature arcFeature;

                while ((arcFeature = arcFeatureCursor.NextFeature()) != null)
                {
                    arcFeature.set_Value(arcFeature.Fields.FindField("DOT_F_MILE"), DBNull.Value);
                    arcFeature.set_Value(arcFeature.Fields.FindField("DOT_T_MILE"), DBNull.Value);

                    // store edit
                    arcFeature.Store();
                }

                // null out variables
                queryFilter = null;
                arcFeatureCursor = null;
                arcFeature = null;

                // Stop Edit Operation.
                clsGlobals.arcEditor.StopOperation("Null MP Values");



                //// ASSIGN THE MILEPOST VALUES ////
                double searchOutDist = 15;
                queryFilter = new QueryFilter();
                // queryFilter.WhereClause = "LEN(DOT_RTNAME) = 5 and (DOT_RTNAME like '0%')"; // sde
                queryFilter.WhereClause = "CHAR_LENGTH(DOT_RTNAME) = 5 and (DOT_RTNAME like '0%')";  // .gdb
                //queryFilter.WhereClause = "(not DOT_RTNAME is null) and char_length (DOT_RTNAME)= 5"; // .shp

                arcFeatureCursor = featureLayerRoads.FeatureClass.Search(queryFilter, false);
                IFeature arcFeature_Roads;
                bool hitStart = false;
                bool hitEnd = false;

                // Start edit operation
                clsGlobals.arcEditor.StartOperation();

                while ((arcFeature_Roads = arcFeatureCursor.NextFeature()) != null)
                {
                    // get the route name from roads
                    var roadsRouteName = arcFeature_Roads.get_Value(arcFeature_Roads.Fields.FindField("DOT_RTNAME"));
                    
                    ////double roads_FromMile;
                    ////double roads_ToMile;

                    ////// get the from mile value from roads
                    ////if (arcFeature_Roads.get_Value(arcFeature_Roads.Fields.FindField("DOT_F_MILE")) != DBNull.Value || arcFeature_Roads.get_Value(arcFeature_Roads.Fields.FindField("DOT_F_MILE")).ToString() != "")
                    ////{
                    ////    roads_FromMile = Convert.ToDouble(arcFeature_Roads.get_Value(arcFeature_Roads.Fields.FindField("DOT_F_MILE")));
                    ////}

                    ////// get the to mile value from roads
                    ////if (arcFeature_Roads.get_Value(arcFeature_Roads.Fields.FindField("DOT_T_MILE")) != DBNull.Value || arcFeature_Roads.get_Value(arcFeature_Roads.Fields.FindField("DOT_T_MILE")).ToString() != "")
                    ////{
                    ////    roads_ToMile = Convert.ToDouble(arcFeature_Roads.get_Value(arcFeature_Roads.Fields.FindField("DOT_T_MILE")));
                    ////}

                    // Set up query filter and feature cursor for LRS layer.
                    IQueryFilter queryFilter_LRS = new QueryFilter();
                    queryFilter_LRS.WhereClause = "LABEL = '" + roadsRouteName + "'";
                    IFeatureCursor arcFeatureCursor_LRS = featureLayerLRS.FeatureClass.Search(queryFilter_LRS, false);
                    IFeature arcFeature_LRS = arcFeatureCursor_LRS.NextFeature();

                    if (arcFeature_LRS != null)
                    {
                        // START (FROM) POINT //
                        IPolyline polyline_Roads = (IPolyline)arcFeature_Roads.Shape;
                        IPolyline polyline_LRS = (IPolyline)arcFeature_LRS.Shape;
                        IPoint fromPoint_Roads = polyline_Roads.FromPoint;

                        double dist = 0;
                        int partIndex = 0;
                        int segIndex = 0;
                        bool right = false;
                        hitStart = false;

                        IPoint point_Hit = new ESRI.ArcGIS.Geometry.Point();
                        IHitTest hitTest = (IHitTest)polyline_LRS;

                        // Hit test to see if a vertex is hit.
                        hitStart = hitTest.HitTest(fromPoint_Roads,searchOutDist,esriGeometryHitPartType.esriGeometryPartVertex, point_Hit, dist, ref partIndex, ref segIndex, right);

                        if (hitStart)
                        {
                            if (fromPoint_Roads.M >= 0)
                            {
                                arcFeature_Roads.set_Value(arcFeature_Roads.Fields.FindField("DOT_F_MILE"), Convert.ToDouble((fromPoint_Roads.M * 1000) / 1000));
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                            // if no vertex to vertex hit, then interpolate along route segment.
                            // hit test to see if polyline is hit anywhere, not necessarily at vertex
                            hitStart = hitTest.HitTest(fromPoint_Roads, searchOutDist, esriGeometryHitPartType.esriGeometryPartBoundary, point_Hit, dist, ref partIndex, ref segIndex, right);

                            if (hitStart)
                            {
                                IGeometryCollection geometryCollection = (IGeometryCollection)polyline_LRS;
                                ISegmentCollection segmentCollection = (ISegmentCollection)geometryCollection.Geometry[partIndex];
                                ISegment segment = segmentCollection.Segment[segIndex];
                                ESRI.ArcGIS.Geometry.Point outPoint = new ESRI.ArcGIS.Geometry.Point();
                                double outDist = 0;
                                double awayDist = 0;
                                double mcoord = 0;

                                // interpolate
                                segment.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, fromPoint_Roads, true, outPoint, outDist, awayDist, true);
                                mcoord = segment.FromPoint.M + ((segment.ToPoint.M - segment.FromPoint.M) * outDist);
                                arcFeature_Roads.set_Value(arcFeature_Roads.Fields.FindField("DOT_F_MILE"), Convert.ToDouble((mcoord * 1000) / 1000) + 0.001);
                            }
                            else
                            {
                            }
                        }



                        // END (TO) POINT //
                        ////polyline_Roads = (IPolyline)arcFeature_Roads.Shape;
                        ////polyline_LRS = (IPolyline)arcFeature_LRS.Shape;
                        IPoint toPoint_Roads = polyline_Roads.ToPoint;

                        dist = 0;
                        partIndex = 0;
                        segIndex = 0;
                        right = false;
                        hitEnd = false;

                        point_Hit = new ESRI.ArcGIS.Geometry.Point();
                        hitTest = (IHitTest)polyline_LRS;

                        // Hit test to see if a vertex is hit.
                        hitEnd = hitTest.HitTest(toPoint_Roads, searchOutDist, esriGeometryHitPartType.esriGeometryPartVertex, point_Hit, dist, ref partIndex, ref segIndex, right);

                        if (hitEnd)
                        {
                            if (toPoint_Roads.M >= 0)
                            {
                                arcFeature_Roads.set_Value(arcFeature_Roads.Fields.FindField("DOT_T_MILE"), Convert.ToDouble((toPoint_Roads.M * 1000) / 1000));
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                            // if no vertex to vertex hit, then interpolate along route segment.
                            // hit test to see if polyline is hit anywhere, not necessarily at vertex
                            hitEnd = hitTest.HitTest(toPoint_Roads, searchOutDist, esriGeometryHitPartType.esriGeometryPartBoundary, point_Hit, dist, ref partIndex, ref segIndex, right);

                            if (hitEnd)
                            {
                                IGeometryCollection geometryCollection = (IGeometryCollection)polyline_LRS;
                                ISegmentCollection segmentCollection = (ISegmentCollection)geometryCollection.Geometry[partIndex];
                                ISegment segment = segmentCollection.Segment[segIndex];
                                ESRI.ArcGIS.Geometry.Point outPoint = new ESRI.ArcGIS.Geometry.Point();
                                double outDist = 0;
                                double awayDist = 0;
                                double mcoord = 0;

                                // interpolate
                                segment.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, toPoint_Roads, true, outPoint, outDist, awayDist, true);
                                mcoord = segment.FromPoint.M + ((segment.ToPoint.M - segment.FromPoint.M) * outDist);
                                arcFeature_Roads.set_Value(arcFeature_Roads.Fields.FindField("DOT_T_MILE"), Convert.ToDouble((mcoord * 1000) / 1000));
                            }
                            else
                            {
                            }
                        }
                    }

                    // store edit
                    if (hitStart || hitEnd)
                    {
                        arcFeature_Roads.Store();
                    }

                    // null out variables
                    queryFilter_LRS = null;
                    arcFeatureCursor_LRS = null;
                    arcFeature_Roads = null;
                }

                // Stop Edit Operation.
                clsGlobals.arcEditor.StopOperation("Assign MP Values");

                // Done! //
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
