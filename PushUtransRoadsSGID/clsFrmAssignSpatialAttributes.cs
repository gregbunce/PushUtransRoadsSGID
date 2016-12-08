using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PushUtransRoadsSGID
{
    public partial class clsFrmAssignSpatialAttributes : Form
    {
        public clsFrmAssignSpatialAttributes()
        {
            InitializeComponent();
        }


        // this method is run when the form loads - and populates the combobox with the map's layers
        private void clsFrmAssignSpatialAttributes_Load(object sender, EventArgs e)
        {
            try
            {
                // get access to the current arcmap variables
                clsPushSgidStaticClass.GetCurrentMapDocVariables();

                // load the choose layer combobox with the map's layer names
                for (int i = 0; i < clsGlobals.pMap.LayerCount; i++)
                {
                    cboChooseLayer.Items.Add(clsGlobals.pMap.Layer[i].Name.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "Push Utrans Roads to SGID!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cboChooseLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // show the cursor as busy
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                clsGlobals.pGFlayer = null;
                clsGlobals.arcFeatLayer = null;

                // loop through the map's layers and check for the layer with the targeted name (based on the choose layer combobox)
                for (int i = 0; i < clsGlobals.pMap.LayerCount; i++)
                {
                    if (clsGlobals.pMap.Layer[i].Name == cboChooseLayer.Text)
                    {
                        clsGlobals.pGFlayer = (IGeoFeatureLayer)clsGlobals.pMap.Layer[i];
                        clsGlobals.arcFeatLayer = (IFeatureLayer)clsGlobals.pMap.Layer[i];
                    }
                }

                // clear out the old items in the list
                cboAddressQuad.Items.Clear();
                cboAddressSystem.Items.Clear();
                cboCityLeft.Items.Clear();
                cboCityRight.Items.Clear();
                cboCounty.Items.Clear();
                cboUspsName.Items.Clear();
                cboZipLeft.Items.Clear();
                cboZipRight.Items.Clear();
                cboUSNG.Items.Clear();
                cboGrid100K.Items.Clear();
                cboGrid1Mil.Items.Clear();
                cboMidX.Items.Clear();
                cboMidY.Items.Clear();

                //update the comboboxes with the currently selected layer's field names
                for (int i = 0; i < clsGlobals.pGFlayer.FeatureClass.Fields.FieldCount; i++)
                {
                    cboAddressQuad.Items.Add(clsGlobals.pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());
                    cboAddressSystem.Items.Add(clsGlobals.pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());
                    cboCityLeft.Items.Add(clsGlobals.pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());
                    cboCityRight.Items.Add(clsGlobals.pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());
                    cboCounty.Items.Add(clsGlobals.pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());
                    cboUspsName.Items.Add(clsGlobals.pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());
                    cboZipLeft.Items.Add(clsGlobals.pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());
                    cboZipRight.Items.Add(clsGlobals.pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());
                    cboUSNG.Items.Add(clsGlobals.pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());
                    cboGrid100K.Items.Add(clsGlobals.pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());
                    cboGrid1Mil.Items.Add(clsGlobals.pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());
                    cboMidY.Items.Add(clsGlobals.pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());
                    cboMidX.Items.Add(clsGlobals.pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "Push Utrans Roads to SGID!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // make sure the user selected at least on field to assign
                //var isAnyEmpty = clsPushSgidStaticClass.ScanForControls<ComboBox>(this)
                //   .Where(x => x.SelectedIndex < 0)
                //   .Any();

                //if (isAnyEmpty)
                //    MessageBox.Show("please fill all fields");

                //bool blnNoneSelected = true;
                string strComboBoxValues = "";
                foreach (Control c in this.Controls)
                {
                    foreach (Control childc in c.Controls)
                    {
                        if (childc is ComboBox)
                        {
                            if (childc.Name != cboChooseLayer.Name)
                            {
                                strComboBoxValues += ((ComboBox)childc).Text;
                                //blnNoneSelected = false;
                            }
                        }
                    }
                }

                if (strComboBoxValues == "")
                {
                    MessageBox.Show("You must select at least one field to assign.", "Make Field Selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                //if (cboZipRight.SelectedIndex == -1 & cboZipLeft.SelectedIndex == -1 & cboUspsName.SelectedIndex == -1 & cboCounty.SelectedIndex == -1 & cboCityRight.SelectedIndex == -1 & cboCityLeft.SelectedIndex == -1 & cboAddressSystem.SelectedIndex == -1 & cboAddressQuad.SelectedIndex == -1)
                //{
                //    MessageBox.Show("You must select at least one field to assign.", "Make Field Selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;
                //}

                // make sure the user is editing
                //get the editor extension
                UID arcUID = new UID();
                arcUID.Value = "esriEditor.Editor";
                clsGlobals.arcEditor = clsGlobals.arcApplication.FindExtensionByCLSID(arcUID) as IEditor3;

                // check if editing first
                if (clsGlobals.arcEditor.EditState == ESRI.ArcGIS.Editor.esriEditState.esriStateNotEditing)
                {
                    MessageBox.Show("You must be editing in order to assign the fields spatially.", "Must Be Editing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // show the cursor as busy
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;


                // get access to the layer that is specified in the choose layer dropdown box
                clsGlobals.pGFlayer = null;
                clsGlobals.arcFeatLayer = null;
                // loop through the map's layers and check for the layer with the targeted name (based on the choose layer combobox)
                for (int i = 0; i < clsGlobals.pMap.LayerCount; i++)
                {
                    if (clsGlobals.pMap.Layer[i].Name == cboChooseLayer.Text)
                    {
                        clsGlobals.pGFlayer = (IGeoFeatureLayer)clsGlobals.pMap.Layer[i];
                        clsGlobals.arcFeatLayer = (IFeatureLayer)clsGlobals.pMap.Layer[i];
                    }
                }

                IDataset arcDataset = (IDataset)clsGlobals.arcFeatLayer;
                IWorkspace arcWorkspace = arcDataset.Workspace;
                IWorkspaceEdit arcWorkspaceEdit = (IWorkspaceEdit)arcWorkspace;

                // make sure we're editing the correct workspace
                if (!(arcWorkspaceEdit.IsBeingEdited()))
                {
                    MessageBox.Show("You must be editing the " + cboChooseLayer.Text + " layer in order to proceed.", "Must Be Editing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // get access to the selected features in the user specified layer
                IDisplayTable arcDisplayTable = (IDisplayTable)clsGlobals.arcFeatLayer;
                ISelectionSet arcSelectionSet = arcDisplayTable.DisplaySelectionSet;

                // make sure there's at least one feature selected in the specified layer
                if (arcSelectionSet.Count == 0)
                {
                    MessageBox.Show("You must select at least one feature in the " + cboChooseLayer.Text + " layer to spatially assign values to.", "No Features are Selected.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                // connect to sgid database
                clsGlobals.workspaceSGID = clsPushSgidStaticClass.ConnectToTransactionalVersion("", "sde:sqlserver:sgid.agrc.utah.gov", "SGID10", "DBMS", "sde.DEFAULT", "agrc", "agrc");
                clsGlobals.featureWorkspaceSGID = (IFeatureWorkspace)clsGlobals.workspaceSGID;

                // get access to sgid feature classes for spatial intersects
                clsPushSgidStaticClass.GetAccessToSgidLayers();


                // confirm the user wants to edit that layer's fields on the selected records
                string strFieldsToBeEdited = "";
                if (cboZipRight.SelectedIndex != -1)
                {
                    strFieldsToBeEdited = strFieldsToBeEdited + cboZipRight.Text.ToString() + ", ";
                }
                if (cboZipLeft.SelectedIndex != -1)
                {
                    strFieldsToBeEdited = strFieldsToBeEdited + cboZipLeft.Text.ToString() + ", ";
                }
                if (cboCounty.SelectedIndex != -1)
                {
                    strFieldsToBeEdited = strFieldsToBeEdited + cboCounty.Text.ToString() + ", ";
                }
                if (cboUspsName.SelectedIndex != -1)
                {
                    strFieldsToBeEdited = strFieldsToBeEdited + cboUspsName.Text.ToString() + ", ";
                }
                if (cboCityRight.SelectedIndex != -1)
                {
                    strFieldsToBeEdited = strFieldsToBeEdited + cboCityRight.Text.ToString() + ", ";
                }
                if (cboCityLeft.SelectedIndex != -1)
                {
                    strFieldsToBeEdited = strFieldsToBeEdited + cboCityLeft.Text.ToString() + ", ";
                }
                if (cboAddressSystem.SelectedIndex != -1)
                {
                    strFieldsToBeEdited = strFieldsToBeEdited + cboAddressSystem.Text.ToString() + ", ";
                }
                if (cboAddressQuad.SelectedIndex != -1)
                {
                    strFieldsToBeEdited = strFieldsToBeEdited + cboAddressQuad.Text.ToString() + ", ";
                }
                if (cboGrid100K.SelectedIndex != -1)
                {
                    strFieldsToBeEdited = strFieldsToBeEdited + cboGrid100K.Text.ToString() + ", ";
                }
                if (cboGrid1Mil.SelectedIndex != -1)
                {
                    strFieldsToBeEdited = strFieldsToBeEdited + cboGrid1Mil.Text.ToString() + ", ";
                }
                if (cboUSNG.SelectedIndex != -1)
                {
                    strFieldsToBeEdited = strFieldsToBeEdited + cboUSNG.Text.ToString() + ", ";
                }
                if (cboMidX.SelectedIndex != -1)
                {
                    strFieldsToBeEdited = strFieldsToBeEdited + cboMidX.Text.ToString() + ", ";
                }
                if (cboMidY.SelectedIndex != -1)
                {
                    strFieldsToBeEdited = strFieldsToBeEdited + cboMidY.Text.ToString() + ", ";
                }

                //remove the last two chararcters -- ie: ', '
                strFieldsToBeEdited = strFieldsToBeEdited.Remove(strFieldsToBeEdited.Length - 2);


                DialogResult dialogResult = MessageBox.Show("Would you like to proceed with editing " + arcSelectionSet.Count + " features on the " + cboChooseLayer.Text + " Layer, spatially assigning attribues from SGID polygons in the following fields: [" + strFieldsToBeEdited + "]?", "Confirm Edits", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    // set some variables for editing the selected features
                    //ESRI.ArcGIS.Geodatabase.IFeature arcFeatureToEdit = null;
                    IEnumIDs arcEnumIDs = arcSelectionSet.IDs;
                    int iD;

                    // loop through the selected feature in the user specified layer and make the values in the user specified field empty string
                    while ((iD = arcEnumIDs.Next()) != -1)
                    {
                        clsGlobals.arcFeatureToEditSpatial = clsGlobals.arcFeatLayer.FeatureClass.GetFeature(iD);
                        clsGlobals.arcEditor.StartOperation();

                        // assign values to user choosen fields

                        //get the feature's geometry
                        IGeometry arcEdit_geometry = clsGlobals.arcFeatureToEditSpatial.ShapeCopy;
                        IPolyline arcEdit_polyline = null;
                        IPoint arcEdit_midPoint = null;

                        // check the geometry type (if polyline or point then proceed)
                        if (arcEdit_geometry.GeometryType == esriGeometryType.esriGeometryPolyline)
                        {
                            //get the midpoint of the line segment for doing spatial queries (intersects)
                            arcEdit_polyline = arcEdit_geometry as IPolyline;
                            arcEdit_midPoint = new ESRI.ArcGIS.Geometry.Point();

                            //get the midpoint of the line, pass it into a point
                            arcEdit_polyline.QueryPoint(esriSegmentExtension.esriNoExtension, 0.5, true, arcEdit_midPoint);

                        }
                        else if (arcEdit_geometry.GeometryType == esriGeometryType.esriGeometryPoint)
                        {
                            arcEdit_midPoint = (IPoint)arcEdit_geometry;
                        }
                        else
                        {
                            MessageBox.Show("The geometry type for the features you are trying to edit is not yet supported by this custom tool.  If you need this to be supported talk to Greg Bunce.  As of now Polylines and Points are the supported geometry types.", "Geometry Type Not Supported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        //MessageBox.Show(arcEdit_midPoint.X.ToString() + " " + arcEdit_midPoint.Y.ToString(), "UTM X, Y", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // check to see if we're doing a right/left spatial search...
                        IConstructPoint arcConstructionPoint_posRight;
                        IConstructPoint arcConstructionPoint_negLeft;
                        IPoint outPoint_posRight = null;
                        IPoint outPoint_negLeft = null;
                        if (cboCityLeft.SelectedIndex != -1 | cboCityRight.SelectedIndex != -1 | cboZipLeft.SelectedIndex != -1 | cboZipRight.SelectedIndex != -1)
                        {
                            // the user has chosen at least one of the left/right fields, so create the offset points for them
                            // test the iconstructpoint.constructtooffset mehtod
                            arcConstructionPoint_posRight = new ESRI.ArcGIS.Geometry.PointClass();
                            arcConstructionPoint_negLeft = new ESRI.ArcGIS.Geometry.PointClass();

                            // call offset mehtod to get a point along the curve's midpoint - offsetting in the postive position (esri documentation states that positive offset will always return point on the right side of the curve)
                            arcConstructionPoint_posRight.ConstructOffset(arcEdit_polyline, esriSegmentExtension.esriNoExtension, 0.5, true, 15);  // 10 meters is about 33 feet (15 is about 50 feet)
                            outPoint_posRight = arcConstructionPoint_posRight as IPoint;
                            //MessageBox.Show("for positive/right offset: " + outPoint_posRight.X + " , " + outPoint_posRight.Y);

                            // call offset mehtod to get a point along the curve's midpoint - offsetting in the negative position (esri documentation states that negative offset will always return point on the left-side of curve)
                            arcConstructionPoint_negLeft.ConstructOffset(arcEdit_polyline, esriSegmentExtension.esriNoExtension, 0.5, true, -15);  // -10 meters is about -33 feet (15 is about 50 feet)
                            outPoint_negLeft = arcConstructionPoint_negLeft as IPoint;
                            //MessageBox.Show("for negative/left offset: " + outPoint_negLeft.X + " , " + outPoint_negLeft.Y);


                        }


                        // do a spatial intersect for the user selected fields 
                        if (cboZipRight.SelectedIndex != -1)
                        {
                            // get the intersected boundaries value
                            IFeature arcFeatureIntersected = clsPushSgidStaticClass.GetIntersectedSGIDBoundary(outPoint_posRight, clsGlobals.arcFeatClass_ZipCodes);

                            if (arcFeatureIntersected != null)
                            {
                                string strZipRight = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("ZIP5")).ToString().Trim();

                                clsGlobals.arcFeatureToEditSpatial.set_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField(cboZipRight.Text), strZipRight);
                            }
                            else
                            {
                            }
                        }
                        if (cboZipLeft.SelectedIndex != -1)
                        {
                            // get the intersected boundaries value
                            IFeature arcFeatureIntersected = clsPushSgidStaticClass.GetIntersectedSGIDBoundary(outPoint_negLeft, clsGlobals.arcFeatClass_ZipCodes);

                            if (arcFeatureIntersected != null)
                            {
                                string strZipLeft = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("ZIP5")).ToString().Trim();

                                clsGlobals.arcFeatureToEditSpatial.set_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField(cboZipLeft.Text), strZipLeft);
                            }
                            else
                            {
                            }
                        }
                        if (cboCounty.SelectedIndex != -1)
                        {
                            // get the intersected boundaries value
                            IFeature arcFeatureIntersected = clsPushSgidStaticClass.GetIntersectedSGIDBoundary(arcEdit_midPoint, clsGlobals.arcFeatClass_Counties);

                            if (arcFeatureIntersected != null)
                            {
                                string strCofips = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("FIPS_STR")).ToString().Trim();

                                clsGlobals.arcFeatureToEditSpatial.set_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField(cboCounty.Text), strCofips);
                            }
                            else
                            {
                            }
                        }
                        if (cboUspsName.SelectedIndex != -1)
                        {
                            // get the intersected boundaries value
                            IFeature arcFeatureIntersected = clsPushSgidStaticClass.GetIntersectedSGIDBoundary(arcEdit_midPoint, clsGlobals.arcFeatClass_ZipCodes);

                            if (arcFeatureIntersected != null)
                            {
                                string strUspsName = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("NAME")).ToString().Trim();

                                clsGlobals.arcFeatureToEditSpatial.set_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField(cboUspsName.Text), strUspsName);
                            }
                            else
                            {
                            }
                        }
                        if (cboCityRight.SelectedIndex != -1)
                        {
                            // get the intersected boundaries value
                            IFeature arcFeatureIntersected = clsPushSgidStaticClass.GetIntersectedSGIDBoundary(outPoint_posRight, clsGlobals.arcFeatClass_Muni);

                            if (arcFeatureIntersected != null)
                            {
                                string strCityRight = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("NAME")).ToString().Trim();

                                clsGlobals.arcFeatureToEditSpatial.set_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField(cboCityRight.Text), strCityRight.ToUpper());
                            }
                            else
                            {
                            }
                        }
                        if (cboCityLeft.SelectedIndex != -1)
                        {
                            // get the intersected boundaries value
                            IFeature arcFeatureIntersected = clsPushSgidStaticClass.GetIntersectedSGIDBoundary(outPoint_negLeft, clsGlobals.arcFeatClass_Muni);

                            if (arcFeatureIntersected != null)
                            {
                                string strCityLeft = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("NAME")).ToString().Trim();

                                clsGlobals.arcFeatureToEditSpatial.set_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField(cboCityLeft.Text), strCityLeft.ToUpper());
                            }
                            else
                            {
                            }
                        }
                        if (cboAddressSystem.SelectedIndex != -1 & cboAddressQuad.SelectedIndex != -1) // user wants to populate both grid_name and quadrant 
                        {
                            // get the intersected boundaries value
                            IFeature arcFeatureIntersected = clsPushSgidStaticClass.GetIntersectedSGIDBoundary(arcEdit_midPoint, clsGlobals.arcFeatClass_AddrSys);

                            if (arcFeatureIntersected != null)
                            {
                                string strGridName = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("GRID_NAME")).ToString().Trim();
                                string strQuad = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("QUADRANT")).ToString().Trim();

                                clsGlobals.arcFeatureToEditSpatial.set_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField(cboAddressSystem.Text), strGridName);
                                clsGlobals.arcFeatureToEditSpatial.set_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField(cboAddressQuad.Text), strQuad);
                            }
                            else
                            {
                            }
                        }
                        if (cboAddressSystem.SelectedIndex != -1 & cboAddressQuad.SelectedIndex == -1) // user wants to populate both grid_name and quadrant 
                        {
                            // get the intersected boundaries value
                            IFeature arcFeatureIntersected = clsPushSgidStaticClass.GetIntersectedSGIDBoundary(arcEdit_midPoint, clsGlobals.arcFeatClass_AddrSys);

                            if (arcFeatureIntersected != null)
                            {
                                string strGridName = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("GRID_NAME")).ToString().Trim();
                                //string strQuad = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("QUADRANT")).ToString().Trim();

                                clsGlobals.arcFeatureToEditSpatial.set_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField(cboAddressSystem.Text), strGridName);
                                //clsGlobals.arcFeatureToEditSpatial.set_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField(cboAddressQuad.Text), strQuad);
                            }
                            else
                            {
                            }
                        }
                        if (cboAddressSystem.SelectedIndex == -1 & cboAddressQuad.SelectedIndex != -1) // user wants to populate both grid_name and quadrant 
                        {
                            // get the intersected boundaries value
                            IFeature arcFeatureIntersected = clsPushSgidStaticClass.GetIntersectedSGIDBoundary(arcEdit_midPoint, clsGlobals.arcFeatClass_AddrSys);

                            if (arcFeatureIntersected != null)
                            {
                                //string strGridName = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("GRID_NAME")).ToString().Trim();
                                string strQuad = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("QUADRANT")).ToString().Trim();

                                //clsGlobals.arcFeatureToEditSpatial.set_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField(cboAddressSystem.Text), strGridName);
                                clsGlobals.arcFeatureToEditSpatial.set_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField(cboAddressQuad.Text), strQuad);
                            }
                            else
                            {
                            }
                        }
                        // unique_id field
                        if (cboUSNG.SelectedIndex != -1)
                        {
                            // get the intersected boundaries value
                            IFeature arcFeatureIntersected = clsPushSgidStaticClass.GetIntersectedSGIDBoundary(arcEdit_midPoint, clsGlobals.arcFeatClass_USNG);

                            if (arcFeatureIntersected != null)
                            {
                                //string strGridName = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("GRID_NAME")).ToString().Trim();
                                string strGrid1Mil = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("GRID1MIL")).ToString().Trim();
                                string strGrid100k = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("GRID100K")).ToString().Trim();
                                string strMeterX = arcEdit_midPoint.X.ToString().Trim();
                                string strMeterY = arcEdit_midPoint.Y.ToString().Trim();
                                double dblMeterX = (double)arcEdit_midPoint.X;
                                double dblMeterY = (double)arcEdit_midPoint.Y;
                                // add .5 to so when we conver to long and the value gets truncated, it will still regain our desired value (if you need more info on this, talk to Bert)
                                dblMeterX = dblMeterX + .5;
                                dblMeterY = dblMeterY + .5;
                                long lngMeterX = (long)dblMeterX;
                                long lngMeterY = (long)dblMeterY;
                                string strStreetName = clsGlobals.arcFeatureToEditSpatial.get_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField("STREETNAME")).ToString().Trim();
                                string strStreetType = clsGlobals.arcFeatureToEditSpatial.get_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField("STREETTYPE")).ToString().Trim();
                                string strSufDir = clsGlobals.arcFeatureToEditSpatial.get_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField("SUFDIR")).ToString().Trim();
                                string strFullName = "";
                                string strUSNG_UniqueID = "";

                                // check if the utrans road is numberic or integer
                                int intStreetNameIsNum;
                                bool blnIsNumbericStreetName = int.TryParse(strStreetName, out intStreetNameIsNum);

                                if (blnIsNumbericStreetName)
                                {
                                    if (strStreetName != "")
                                    {
                                        strFullName = "_" + strStreetName + "_" + strSufDir;
                                    }
                                    else
                                    {
                                        strFullName = "";
                                    }
                                }
                                else
                                {
                                    // concatinate the streetname and streettype
                                    if (strStreetName != "")
                                    {
                                        if (strStreetType != "")
                                        {
                                            strFullName = "_" + strStreetName + "_" + strStreetType;
                                        }
                                        else
                                        {
                                            strFullName = "_" + strStreetName;
                                        }
                                    }
                                    else
                                    {
                                        strFullName = "";
                                    }
                                }

                                // trim the x and y meter values to get the needed four characters from each value
                                string strMeterX_NoDecimal = lngMeterX.ToString();
                                string strMeterY_NoDecimal = lngMeterY.ToString();

                                // remove the begining characters
                                strMeterX_NoDecimal = strMeterX_NoDecimal.Remove(0, 1);
                                strMeterY_NoDecimal = strMeterY_NoDecimal.Remove(0,2);

                                //remove the ending characters
                                strMeterY_NoDecimal = strMeterY_NoDecimal.Remove(strMeterY_NoDecimal.Length -1);
                                strMeterX_NoDecimal = strMeterX_NoDecimal.Remove(strMeterX_NoDecimal.Length -1);

                                // piece all the unique_id fields together
                                strUSNG_UniqueID = strGrid1Mil + strGrid100k + strMeterX_NoDecimal + strMeterY_NoDecimal + strFullName;

                                //clsGlobals.arcFeatureToEditSpatial.set_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField(cboAddressSystem.Text), strGridName);
                                clsGlobals.arcFeatureToEditSpatial.set_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField(cboUSNG.Text), strUSNG_UniqueID);

                                // check if user wants to populate the usng grid fields as well
                                if (cboGrid100K.SelectedIndex != -1)
                                {
                                    clsGlobals.arcFeatureToEditSpatial.set_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField(cboGrid100K.Text), strGrid100k);
                                }
                                if (cboGrid1Mil.SelectedIndex != -1)
                                {
                                    clsGlobals.arcFeatureToEditSpatial.set_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField(cboGrid1Mil.Text), strGrid1Mil);
                                }
                            }
                            else
                            {
                            }
                        }

                        // if the user only wants to populate the usng grid fields, and not the unique_id field, then... 
                        if (cboUSNG.SelectedIndex == -1 & (cboGrid100K.SelectedIndex != -1 | cboGrid1Mil.SelectedIndex != -1))
                        {
                            if (cboGrid100K.SelectedIndex != -1)
                            {
                                // get the intersected boundaries value
                                IFeature arcFeatureIntersected = clsPushSgidStaticClass.GetIntersectedSGIDBoundary(arcEdit_midPoint, clsGlobals.arcFeatClass_USNG);

                                if (arcFeatureIntersected != null)
                                {
                                    //string strGridName = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("GRID_NAME")).ToString().Trim();
                                    //string strGrid1Mil = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("GRID1MIL")).ToString().Trim();
                                    string strGrid100k = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("GRID100K")).ToString().Trim();

                                    clsGlobals.arcFeatureToEditSpatial.set_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField(cboGrid100K.Text), strGrid100k);
                                }
                            }
                                
                            if (cboGrid1Mil.SelectedIndex != -1)
                            {
                                // get the intersected boundaries value
                                IFeature arcFeatureIntersected = clsPushSgidStaticClass.GetIntersectedSGIDBoundary(arcEdit_midPoint, clsGlobals.arcFeatClass_USNG);

                                if (arcFeatureIntersected != null)
                                {
                                    //string strGridName = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("GRID_NAME")).ToString().Trim();
                                    string strGrid1Mil = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("GRID1MIL")).ToString().Trim();
                                    //string strGrid100k = arcFeatureIntersected.get_Value(arcFeatureIntersected.Fields.FindField("GRID100K")).ToString().Trim();

                                    clsGlobals.arcFeatureToEditSpatial.set_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField(cboGrid1Mil.Text), strGrid1Mil);
                                }
                            }
                        }

                        // MidX field
                        if (cboMidX.SelectedIndex != -1)
                        {
                            clsGlobals.arcFeatureToEditSpatial.set_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField(cboMidX.Text), arcEdit_midPoint.X.ToString());
                        }

                        // MidY field
                        if (cboMidY.SelectedIndex != -1)
                        {
                            clsGlobals.arcFeatureToEditSpatial.set_Value(clsGlobals.arcFeatureToEditSpatial.Fields.FindField(cboMidY.Text), arcEdit_midPoint.Y.ToString());
                        }

                        clsGlobals.arcFeatureToEditSpatial.Store();
                        clsGlobals.arcEditor.StopOperation("AssignSpatialAttributes");
                    }

                    MessageBox.Show("Done updating " + arcSelectionSet.Count + " features on the " + cboChooseLayer.Text + " Layer, assigning attributes spatially on the following fields: [" + strFieldsToBeEdited + "].  Don't forget to save edits if you want to retain the changes.", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

                clsGlobals.arcEditor.StopOperation("AssignSpatialAttributes");
            }
        }
    }
}
