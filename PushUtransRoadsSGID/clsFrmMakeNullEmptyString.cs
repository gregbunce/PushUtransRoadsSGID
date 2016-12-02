using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.esriSystem;
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
                // clear the progress bar, in case it has been used before
                pBar.Value = 1;

                if (cboChooseFields.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a field from the dropdown list", "Must Select Field", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                //show the cursor as busy
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                // cast the layer to itable interface b/c it makes use of the layer's definition query or join, if any.. and does the select based on that
                ITable arcTable = (ITable)clsGlobals.arcFeatLayer;

                IDataset arcDataset = (IDataset)clsGlobals.arcFeatLayer;
                IQueryFilter arcQueryFilter = new QueryFilter();
                arcQueryFilter.WhereClause = null;

                // check what type of layer this is, to determine the query syntax
                //shapefile//
                if (arcDataset.Workspace.WorkspaceFactory.WorkspaceType == esriWorkspaceType.esriFileSystemWorkspace)
                {
                    //check the field type, look for either text or double/integer to determine what type of query to set up
                    if (clsPushSgidStaticClass.GetArcGisFieldType(cboChooseFields.Text) == esriFieldType.esriFieldTypeString)
                    {
                        arcQueryFilter.WhereClause = "\"" + cboChooseFields.Text.ToString().Trim() + "\" is null or \"" + cboChooseFields.Text.ToString().Trim() + "\" = ''";
                    }
                    else if (clsPushSgidStaticClass.GetArcGisFieldType(cboChooseFields.Text) == esriFieldType.esriFieldTypeDouble)
                    {
                        arcQueryFilter.WhereClause = "\"" + cboChooseFields.Text.ToString().Trim() + "\" is null";
                    }
                    else if (clsPushSgidStaticClass.GetArcGisFieldType(cboChooseFields.Text) == esriFieldType.esriFieldTypeInteger)
                    {
                        arcQueryFilter.WhereClause = "\"" + cboChooseFields.Text.ToString().Trim() + "\" is null";
                    }
                    else if (clsPushSgidStaticClass.GetArcGisFieldType(cboChooseFields.Text) == esriFieldType.esriFieldTypeSmallInteger)
                    {
                        arcQueryFilter.WhereClause = "\"" + cboChooseFields.Text.ToString().Trim() + "\" is null";
                    }
                    else
                    {
                        MessageBox.Show("You're asking to do a query on a field type that is not supported by this code.  If you need this field type to be supported talk to Greg Bunce.  He will make it happen.", "Not Supported Field Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                //fgdb//
                if (arcDataset.Workspace.WorkspaceFactory.WorkspaceType == esriWorkspaceType.esriLocalDatabaseWorkspace)
                {
                    //check the field type, look for either text or double/integer to determine what type of query to set up
                    if (clsPushSgidStaticClass.GetArcGisFieldType(cboChooseFields.Text) == esriFieldType.esriFieldTypeString)
                    {
                        arcQueryFilter.WhereClause = cboChooseFields.Text.ToString().Trim() + " is null or " + cboChooseFields.Text.ToString().Trim() + " = ''";
                    }
                    else if (clsPushSgidStaticClass.GetArcGisFieldType(cboChooseFields.Text) == esriFieldType.esriFieldTypeDouble)
                    {
                        arcQueryFilter.WhereClause = cboChooseFields.Text.ToString().Trim() + " is null";
                    }
                    else if (clsPushSgidStaticClass.GetArcGisFieldType(cboChooseFields.Text) == esriFieldType.esriFieldTypeInteger)
                    {
                        arcQueryFilter.WhereClause = cboChooseFields.Text.ToString().Trim() + " is null";
                    }
                    else if (clsPushSgidStaticClass.GetArcGisFieldType(cboChooseFields.Text) == esriFieldType.esriFieldTypeSmallInteger)
                    {
                        arcQueryFilter.WhereClause = cboChooseFields.Text.ToString().Trim() + " is null";
                    }
                    else
                    {
                        MessageBox.Show("You're asking to do a query on a field type that is not supported by this code.  If you need this field type to be supported talk to Greg Bunce.  He will make it happen.", "Not Supported Field Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                //sde//
                if (arcDataset.Workspace.WorkspaceFactory.WorkspaceType == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    //check the field type, look for either text or double/integer to determine what type of query to set up
                    if (clsPushSgidStaticClass.GetArcGisFieldType(cboChooseFields.Text) == esriFieldType.esriFieldTypeString)
                    {
                        arcQueryFilter.WhereClause = cboChooseFields.Text.ToString().Trim() + " is null or (" + "LTRIM(RTRIM(" + cboChooseFields.Text.ToString().Trim() + ")) = '')";
                    }
                    else if (clsPushSgidStaticClass.GetArcGisFieldType(cboChooseFields.Text) == esriFieldType.esriFieldTypeDouble)
                    {
                        arcQueryFilter.WhereClause = cboChooseFields.Text.ToString().Trim() + " is null";
                    }
                    else if (clsPushSgidStaticClass.GetArcGisFieldType(cboChooseFields.Text) == esriFieldType.esriFieldTypeInteger)
                    {
                        arcQueryFilter.WhereClause = cboChooseFields.Text.ToString().Trim() + " is null";
                    }
                    else if (clsPushSgidStaticClass.GetArcGisFieldType(cboChooseFields.Text) == esriFieldType.esriFieldTypeSmallInteger)
                    {
                        arcQueryFilter.WhereClause = cboChooseFields.Text.ToString().Trim() + " is null";
                    }
                    else
                    {
                        MessageBox.Show("You're asking to do a query on a field type that is currently not supported by this code.  If you need this field type to be supported talk to Greg Bunce.  He will make it happen.", "Not Supported Field Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
        
                // select the records that have nulls or blanks
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



        // this method updates the values (to make them empty string) on the selected features in the map, based on the field specified in the dropdown combobox
        private void btnUpdateFieldValuesEmptyString_Click(object sender, EventArgs e)
        {
            try
            {
                // make sure the user selected a field to edit
                if (cboChooseFieldToUpdate.SelectedIndex == -1)
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
                    MessageBox.Show("You must be editing in order to make all selected feature values for the " + cboChooseLayer.Text + " layer on the " + cboChooseFieldToUpdate.Text + " field... to empty string.  Please start editing and then try again.", "Must Be Editing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // show the cursor as busy
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                // set up the progress bar on the form to show progress
                pBar.Visible = true;
                pBar.Minimum = 1;
                pBar.Value = 1;
                pBar.Step = 1;

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
                    MessageBox.Show("You must select at least on feature in the " + cboChooseLayer.Text + " layer to edit values in the " + cboChooseFieldToUpdate.Text + " field... making them empty string.", "No Features are Selected.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                pBar.Maximum = arcSelectionSet.Count;

                // confirm the user wants to edit that layer's fields on the selected records
                DialogResult dialogResult = MessageBox.Show("Would you like to proceed with editing " + arcSelectionSet.Count + " features on the " + cboChooseLayer.Text + " Layer, converting the vaules in the " + cboChooseFieldToUpdate.Text + " Field to empty string (or Zero if the field type is double or int)?", "Confirm Edits", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {

                    // set some variables for editing the selected features
                    IFeature arcFeatureToEdit = null;
                    IEnumIDs arcEnumIDs = arcSelectionSet.IDs;
                    int iD;

                    // check what field type the field is ... to either set values to empty string or zero (if int)
                    esriFieldType updateFieldType = clsPushSgidStaticClass.GetArcGisFieldType(cboChooseFieldToUpdate.Text);

                    if (updateFieldType == esriFieldType.esriFieldTypeString)
                    {
                        // loop through the selected feature in the user specified layer and make the values in the user specified field empty string
                        while ((iD = arcEnumIDs.Next()) != -1)
                        {
                            arcFeatureToEdit = clsGlobals.arcFeatLayer.FeatureClass.GetFeature(iD);
                            clsGlobals.arcEditor.StartOperation();

                            arcFeatureToEdit.set_Value(arcFeatureToEdit.Fields.FindField(cboChooseFieldToUpdate.Text), "");
                            
                            // preform the increment of the progress bar
                            pBar.PerformStep();

                            arcFeatureToEdit.Store();
                            clsGlobals.arcEditor.StopOperation(cboChooseFieldToUpdate.Text.ToString() + " to empty string");
                        }
                    }
                    else if (updateFieldType == esriFieldType.esriFieldTypeDouble | updateFieldType == esriFieldType.esriFieldTypeInteger | updateFieldType == esriFieldType.esriFieldTypeSmallInteger)
                    {
                        // loop through the selected feature in the user specified layer and make the values in the user specified field empty string
                        while ((iD = arcEnumIDs.Next()) != -1)
                        {
                            arcFeatureToEdit = clsGlobals.arcFeatLayer.FeatureClass.GetFeature(iD);
                            clsGlobals.arcEditor.StartOperation();

                            arcFeatureToEdit.set_Value(arcFeatureToEdit.Fields.FindField(cboChooseFieldToUpdate.Text), 0);

                            // preform the increment of the progress bar
                            pBar.PerformStep();

                            arcFeatureToEdit.Store();
                            clsGlobals.arcEditor.StopOperation(cboChooseFieldToUpdate.Text.ToString() + " to zero");
                        }
                    }
                    else
                    {
                        MessageBox.Show("You're asking to do an update on a field type that is currently not supported by this code.  If you need this field type to be supported talk to Greg Bunce.  He will make it happen.", "Not Supported Field Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
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


        // update the fields to update combobox based on the users input in the select combobox (to ensure less errors when editing)
        private void cboChooseFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cboChooseFieldToUpdate.SelectedIndex = cboChooseFields.SelectedIndex;
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
