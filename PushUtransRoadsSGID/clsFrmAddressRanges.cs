using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
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
    public partial class clsFrmAddressRanges : Form
    {
        public clsFrmAddressRanges()
        {
            InitializeComponent();
        }


        // this method is called when the selection in the cobobox changes
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
                cboFromLeft.Items.Clear();
                cboFromRight.Items.Clear();
                cboToLeft.Items.Clear();
                cboToRight.Items.Clear();
                cboCheckDecimals.Items.Clear();

                //update the comboboxes with the currently selected layer's field names
                for (int i = 0; i < clsGlobals.pGFlayer.FeatureClass.Fields.FieldCount; i++)
                {
                    if (clsGlobals.pGFlayer.FeatureClass.Fields.Field[i].Type == ESRI.ArcGIS.Geodatabase.esriFieldType.esriFieldTypeDouble)
                    {
                        cboFromLeft.Items.Add(clsGlobals.pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());
                        cboFromRight.Items.Add(clsGlobals.pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());
                        cboToLeft.Items.Add(clsGlobals.pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());
                        cboToRight.Items.Add(clsGlobals.pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());
                        cboCheckDecimals.Items.Add(clsGlobals.pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());
                    }
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


        // load the map's layer when the form initializes
        private void clsFrmAddressRanges_Load(object sender, EventArgs e)
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }


        // check a numberic field for decimals 
        private void btnDecimalsCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboCheckDecimals.SelectedIndex == -1)
                {
                    MessageBox.Show("You must select a field to check.", "Select Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // show the cursor as busy
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                // query for layer's specified field looking for decimals
                IQueryFilter arcQFilter = new QueryFilter();
                arcQFilter.WhereClause = ""; // strActiveComboBox + " not like '%.0000%'";

                ITable arcTable = (ITable)clsGlobals.arcFeatLayer;
                IDataset arcDataset = (IDataset)clsGlobals.arcFeatLayer;

                if (arcDataset.Workspace.WorkspaceFactory.WorkspaceType == esriWorkspaceType.esriLocalDatabaseWorkspace)
                {
                    // fgdb
                    MessageBox.Show("This function has not been coded to work on a file geodatabase... yet.  Talk to Greg Bunce.", "Not on FGDB", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (arcDataset.Workspace.WorkspaceFactory.WorkspaceType == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    //sde
                    arcQFilter.WhereClause =  cboCheckDecimals.Text.ToString() + " not like '%.0000%'";
                }
                else
                {
                    // shapefile
                    MessageBox.Show("This function has not been coded to work on a shapefile... yet.  Talk to Greg Bunce.", "Not on Shapefile", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // select the records that have decimals
                ISelectionSet arcSelSet = arcTable.Select(arcQFilter, esriSelectionType.esriSelectionTypeHybrid, esriSelectionOption.esriSelectionOptionNormal, arcDataset.Workspace);

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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // make sure the user selected a field to edit
                if (cboCheckDecimals.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a field from the drop-down menu to edit.", "Select Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // make sure the user has selected a layer to edit in the choose layer combobox
                if (cboChooseLayer.SelectedIndex == -1)
                {
                    MessageBox.Show("Please choose a layer from the top drop-down list to base edits on.", "Select Layer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // make sure the user is editing
                //get the editor extension
                UID arcUID = new UID();
                arcUID.Value = "esriEditor.Editor";
                clsGlobals.arcEditor = clsGlobals.arcApplication.FindExtensionByCLSID(arcUID) as IEditor3;

                // check if editing first
                if (clsGlobals.arcEditor.EditState == ESRI.ArcGIS.Editor.esriEditState.esriStateNotEditing)
                {
                    MessageBox.Show("You must be editing in order to make all selected feature values for the " + cboChooseLayer.Text + " layer on the " + cboCheckDecimals.Text + " field... to empty string.  Please start editing and then try again.", "Must Be Editing", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("You must select at least one feature in the " + cboChooseLayer.Text + " layer to edit values in the " + cboCheckDecimals.Text + " field... making them empty string.", "No Features are Selected.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // confirm the user wants to edit that layer's fields on the selected records
                DialogResult dialogResult = MessageBox.Show("Would you like to proceed with editing " + arcSelectionSet.Count + " features on the " + cboChooseLayer.Text + " Layer, rounding the values in the " + cboCheckDecimals.Text + " Field to the nearest whole number?", "Confirm Edits", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    // set some variables for editing the selected features
                    IFeature arcFeatureToEdit = null;
                    IEnumIDs arcEnumIDs = arcSelectionSet.IDs;
                    int iD;

                    // loop through the selected feature in the user specified layer and make the values in the user specified field empty string
                    while ((iD = arcEnumIDs.Next()) != -1)
                    {
                        arcFeatureToEdit = clsGlobals.arcFeatLayer.FeatureClass.GetFeature(iD);
                        clsGlobals.arcEditor.StartOperation();

                        double d = (double)arcFeatureToEdit.get_Value(arcFeatureToEdit.Fields.FindField(cboCheckDecimals.Text));
                        int intRounded = Convert.ToInt32(d);

                        arcFeatureToEdit.set_Value(arcFeatureToEdit.Fields.FindField(cboCheckDecimals.Text), intRounded);

                        arcFeatureToEdit.Store();
                        clsGlobals.arcEditor.StopOperation(cboCheckDecimals.Text.ToString() + " values rounded");
                    }

                    MessageBox.Show("Done rounding decimal places for " + arcSelectionSet.Count + " features on the " + cboChooseLayer.Text + " Layer, rounding the vaules in the " + cboCheckDecimals.Text + " Field to the nearest whole number.  Don't forget to save edits if you want to retain the changes.", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

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
            }
        }
    }
}
