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
using System.IO;

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

        StreamWriter streamWriter;
        private IApplication m_application;
        public clsBtnAssignMileposts()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "AGRC"; //localizable text
            base.m_caption = "Assign Mileposts";  //localizable text
            base.m_message = "Make sure the selected layer in the TOC is the desired Roads layer to be edited and that the LRS Layer is the top-most layer. This tool will null out existing From/To mileposts and then reassign those values based on the SGID LRS.  It also honors definition queries.";  //localizable text 
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
                // count how many mileposts were assigned.
                int assignedCounter = 0;

                // show the cursor as busy
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                // create a text file for logging
                // first check if c:\temp exists, if not create it.
                bool tempDirExists = System.IO.Directory.Exists(@"C:\temp");
                if (!tempDirExists)
                {
                    System.IO.Directory.CreateDirectory(@"C:\temp");
                }

                string path = @"C:\temp\AssignMilepostsToRoads" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ".txt";
                System.IO.FileStream fileStream = new System.IO.FileStream(path, FileMode.Create);
                streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine("Assign Mileposts to Road Centerlines began at: " + DateTime.Now.ToString());

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

                // cast the selected layer as a feature layer
                clsGlobals.pGFlayer = (IGeoFeatureLayer)clsGlobals.pMxDocument.SelectedLayer;

                // check if the feaure layer is a line layer
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
                //if (featureLayerLRS.FeatureClass.AliasName != "SGID10.TRANSPORTATION.UDOTRoutes_LRS")
                //{
                //    MessageBox.Show("Make sure the top most layer in the TOC is pointed to SGID10.TRANSPORTATION.UDOTRoutes_LRS", "LRS Layer Missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                if (!(featureLayerLRS.FeatureClass.AliasName.ToString().Contains("LRS")))
                {
                    MessageBox.Show("Make sure the top most layer in the TOC is the UDOTRoutes_LRS layer and that it's alias name contains LRS", "Missing LRS Layer Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //// NULL OUT the existing from/to milepost values so we can track the new ones. ////
                clsGlobals.arcEditor.StartOperation();

                // Null out the existing DOT_F_MILE and DOT_T_MILE values
                IQueryFilter queryFilter = new QueryFilter();
                queryFilter.WhereClause = "DOT_F_MILE >= 0 or DOT_T_MILE >= 0";
                // IFeatureCursor arcFeatureCursor = featureLayerRoads.FeatureClass.Search(queryFilter, false);  // use this if you don't want to honor definition query on layer
                IFeatureCursor arcFeatureCursor = clsGlobals.pGFlayer.Search(queryFilter, false); // use this if you want to honor definition query on layer
                IFeature arcFeature;
                int nulloutCount = 0;

                streamWriter.WriteLine("Began nulling existing From/To values at: " + DateTime.Now.ToString());
                while ((arcFeature = arcFeatureCursor.NextFeature()) != null)
                {
                    nulloutCount = nulloutCount + 1;
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
                streamWriter.WriteLine("Finished nulling " + nulloutCount.ToString() + " existing From/To values at: " + DateTime.Now.ToString());



                //// ASSIGN THE MILEPOST VALUES ////
                streamWriter.WriteLine("Began assigning mileposts at: " + DateTime.Now.ToString());
                double searchOutDist = 15;
                queryFilter = new QueryFilter();

                // check if sde, filegeodatabase, or shapefile - to determine what query syntax to use
                IDataset datasetRoads = (IDataset)featureLayerRoads.FeatureClass;
                if (datasetRoads.Workspace.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
                {
                    // file geodatabase
                    queryFilter.WhereClause = "CHAR_LENGTH(DOT_RTNAME) = 5 and (DOT_RTNAME like '0%')";

                }
                else if (datasetRoads.Workspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    // sde geodatabase
                    queryFilter.WhereClause = "LEN(DOT_RTNAME) = 5 and (DOT_RTNAME like '0%')";
                }
                else if (datasetRoads.Workspace.Type == esriWorkspaceType.esriFileSystemWorkspace)
                {
                    // shapefile
                    queryFilter.WhereClause = "(not DOT_RTNAME is null) and char_length (DOT_RTNAME)= 5";
                }

                // log roads layer name and dataset type
                streamWriter.WriteLine("Roads Layer Name: " + featureLayerRoads.Name.ToString());
                streamWriter.WriteLine("Dataset/Workspace Type: " + datasetRoads.Workspace.Type.ToString());

                //arcFeatureCursor = featureLayerRoads.FeatureClass.Search(queryFilter, false); // use this if you don't want to honor definition query on layer
                arcFeatureCursor = clsGlobals.pGFlayer.Search(queryFilter, false); // use this if you want to honor definition query on layer
                IFeature arcFeature_Roads;
                bool hitStart = false;
                bool hitEnd = false;

                // Start edit operation
                clsGlobals.arcEditor.StartOperation();

                while ((arcFeature_Roads = arcFeatureCursor.NextFeature()) != null)
                {
                    // get the route name from roads
                    var roadsRouteName = arcFeature_Roads.get_Value(arcFeature_Roads.Fields.FindField("DOT_RTNAME"));

                    ////double roads_FromMile = 0;
                    ////double roads_ToMile = 0;

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
                        hitStart = hitTest.HitTest(fromPoint_Roads, searchOutDist, esriGeometryHitPartType.esriGeometryPartVertex, point_Hit, dist, ref partIndex, ref segIndex, right);

                        if (hitStart)
                        {
                            if (point_Hit.M >= 0)
                            {
                                // round to 3 decimals for David Buell's Spillman stuff
                                double dot_f_mile_double = Convert.ToDouble((point_Hit.M * 1000) / 1000);
                                arcFeature_Roads.set_Value(arcFeature_Roads.Fields.FindField("DOT_F_MILE"), Math.Round(dot_f_mile_double,3));
                                //arcFeature_Roads.set_Value(arcFeature_Roads.Fields.FindField("DOT_F_MILE"), Convert.ToDouble((point_Hit.M * 1000) / 1000));
                            }
                            else
                            {
                                streamWriter.WriteLine("Start/From M coordinate missing on ROUTE: " + roadsRouteName + " with OID:" + arcFeature_Roads.OID.ToString() + " pointHit_M Value: " + point_Hit.M.ToString());
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
                                // round to 3 decimals for David Buell's Spillman stuff
                                double dot_f_mile_double = Convert.ToDouble((mcoord * 1000) / 1000) + 0.001;
                                arcFeature_Roads.set_Value(arcFeature_Roads.Fields.FindField("DOT_F_MILE"), Math.Round(dot_f_mile_double,3));
                                // arcFeature_Roads.set_Value(arcFeature_Roads.Fields.FindField("DOT_F_MILE"), Convert.ToDouble((mcoord * 1000) / 1000) + 0.001);
                            }
                            else
                            {
                                streamWriter.WriteLine("  end not found for OID: " + arcFeature_Roads.OID.ToString() + " RTNAME:" + roadsRouteName);
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
                            if (point_Hit.M >= 0)
                            {
                                // round to 3 decimals for David Buell's Spillman stuff
                                double dot_t_mile_double = Convert.ToDouble((point_Hit.M * 1000) / 1000);
                                arcFeature_Roads.set_Value(arcFeature_Roads.Fields.FindField("DOT_T_MILE"), Math.Round(dot_t_mile_double, 3));
                                //arcFeature_Roads.set_Value(arcFeature_Roads.Fields.FindField("DOT_T_MILE"), Convert.ToDouble((point_Hit.M * 1000) / 1000));
                            }
                            else
                            {
                                streamWriter.WriteLine("End/To M coordinate missing on ROUTE: " + roadsRouteName + " with OID:" + arcFeature_Roads.OID.ToString() + " pointHit_M Value: " + point_Hit.M.ToString());
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
                                // round to 3 decimals for David Buell's Spillman stuff
                                double dot_t_mile_double = Convert.ToDouble((mcoord * 1000) / 1000);
                                arcFeature_Roads.set_Value(arcFeature_Roads.Fields.FindField("DOT_T_MILE"), Math.Round(dot_t_mile_double, 3));
                                //arcFeature_Roads.set_Value(arcFeature_Roads.Fields.FindField("DOT_T_MILE"), Convert.ToDouble((mcoord * 1000) / 1000));
                            }
                            else
                            {
                                streamWriter.WriteLine("  end not found for OID: " + arcFeature_Roads.OID.ToString() + " RTNAME:" + roadsRouteName);
                            }
                        }
                    }
                    else
                    {
                        streamWriter.WriteLine("*** No LRS Route Found for OID:" + arcFeature_Roads.OID.ToString() + " RTNAME:" + roadsRouteName);
                    }

                    // store edit
                    if (hitStart || hitEnd)
                    {
                        arcFeature_Roads.Store();
                        assignedCounter = assignedCounter + 1;
                    }

                    // null out variables
                    queryFilter_LRS = null;
                    //arcFeatureCursor_LRS = null;
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(arcFeatureCursor_LRS);
                    arcFeature_Roads = null;
                }

                // Stop Edit Operation.
                clsGlobals.arcEditor.StopOperation("Assign MP Values");

                // Done! //
                streamWriter.WriteLine();
                streamWriter.WriteLine("Done! Finished with zero errors.  This code assigned " + assignedCounter.ToString() + " road segments/features new mileposts.");
                streamWriter.WriteLine("Finished at: " + DateTime.Now.ToString());
                MessageBox.Show("Done assigning milepost values from SGID LRS Layer.  Don't forget to SAVE edits!  Go to the C:/temp folder to see the log file for this code run.", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                streamWriter.WriteLine();
                streamWriter.WriteLine("Errored-out at: " + DateTime.Now.ToString());
                streamWriter.WriteLine("The code below this line is from the try-catch error message.");
                streamWriter.WriteLine("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace);

                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "clsBtnAssignMilepost: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                //close the stream writer
                streamWriter.Close();
            }
        }

        #endregion
    }
}
