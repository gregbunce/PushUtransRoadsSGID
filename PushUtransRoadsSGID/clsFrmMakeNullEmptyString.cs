using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
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
    public partial class clsFrmMakeNullEmptyString : Form
    {
        public clsFrmMakeNullEmptyString()
        {
            InitializeComponent();
        }


        // select all the records from the specified field that have null or blank values
        private void btnSelectBlankNulls_Click(object sender, EventArgs e)
        {
            try
            {

                if (cboChooseFields.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a field from the dropdown list", "Must Select Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                //show the cursor as busy
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;


                ITable arcTable = (ITable)clsGlobals.arcFeatLayer;



                // select the records that have nulls or blanks
                IQueryFilter arcQueryFilter = new QueryFilter();
                arcQueryFilter.WhereClause = cboChooseFields.Text.ToString().Trim() + " is null or (" + "LTRIM(RTRIM(" + cboChooseFields.Text.ToString().Trim() + ")) = '')";

                IDataset arcDataset = (IDataset)clsGlobals.arcFeatLayer;
                ISelectionSet arcSelSet = arcTable.Select(arcQueryFilter,esriSelectionType.esriSelectionTypeHybrid, esriSelectionOption.esriSelectionOptionNormal, arcDataset.Workspace);


                ISelectFeaturesOperation arcSeleFeatOperation;
                arcSeleFeatOperation = new SelectFeaturesOperationClass();
                arcSeleFeatOperation.ActiveView = clsGlobals.pActiveView;
                arcSeleFeatOperation.Layer = clsGlobals.arcFeatLayer;
                arcSeleFeatOperation.SelectionSet = arcSelSet;

                //perform the operation
                clsGlobals.pMxDocument.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);

                IOperationStack arcOperationStack = clsGlobals.pMxDocument.OperationStack;
                arcOperationStack.Do((IOperation)arcSeleFeatOperation);

                clsGlobals.pMxDocument.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);


                ////IFeatureLayer arcFeatLayer = clsGlobals.pGFlayer;
                //IFeatureLayerDefinition arcFeatureLayerDef = (IFeatureLayerDefinition)clsGlobals.pGFlayer;
                //string strExistingDefQuery = arcFeatureLayerDef.DefinitionExpression; 


                //// select the records that have nulls or blanks
                //IQueryFilter arcQueryFilter = new QueryFilter();
                //arcQueryFilter.WhereClause = "(" + strExistingDefQuery + ") AND " + cboChooseFields.Text.ToString().Trim() + " is null";




                //IDataset arcDataset = (IDataset)clsGlobals.pGFlayer.FeatureClass;
                //ISelectionSet arcSelSet = clsGlobals.pGFlayer.FeatureClass.Select(arcQueryFilter,esriSelectionType.esriSelectionTypeHybrid, esriSelectionOption.esriSelectionOptionNormal, arcDataset.Workspace);

                //clsGlobals.pActiveView.Refresh();




            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "Push Utrans Roads to SGID error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }






        }

        private void clsFrmMakeNullEmptyString_Load(object sender, EventArgs e)
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



        // this method is called when the user select a layer in the combobox... then populates the field name comboboxes with the layer's field names
        private void cboChooseLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (cboChooseFields.SelectedIndex == -1)
                //{
                //    MessageBox.Show("Please select a field from the dropdown list", "Must Select Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;
                //}

                // show the cursor as busy
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                //make sure the user has selected a polyline layer
                //if (clsGlobals.pMxDocument.SelectedLayer == null)
                //{
                //    MessageBox.Show("Please select the UTRANS roads layer in the TOC.", "Select Layer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                //if (!(clsGlobals.pMxDocument.SelectedLayer is IFeatureLayer))
                //{
                //    MessageBox.Show("Please select the UTRANS Centerline.", "Must be PolyLine", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                ////check if the feaure layer is a polyline layer
                //if (pGFlayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolyline)
                //{
                //    MessageBox.Show("Please select a polyline layer.", "Must be Polyline", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                ////cast the selected layer as a feature layer
                //IGeoFeatureLayer pGFlayer = (IGeoFeatureLayer)clsGlobals.pMxDocument.SelectedLayer;

                //if (pGFlayer.FeatureClass.AliasName != "UTRANS.TRANSADMIN.StatewideStreets")
                //{
                //    MessageBox.Show("Make sure the selected layer in the TOC is UTRANS.TRANSADMIN.StatewideStreets", "Check Layer Selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;
                //}

                clsGlobals.pGFlayer = null;

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
                cboChooseFieldToUpdate.Items.Clear();
                cboChooseFields.Items.Clear();

                //update the comboboxes with the currently selected layer's field names
                for (int i = 0; i < clsGlobals.pGFlayer.FeatureClass.Fields.FieldCount; i++)
                {
                    cboChooseFields.Items.Add(clsGlobals.pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());
                    cboChooseFieldToUpdate.Items.Add(clsGlobals.pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());
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
    }
}
