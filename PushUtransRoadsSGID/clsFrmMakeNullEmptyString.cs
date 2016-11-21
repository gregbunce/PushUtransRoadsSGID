using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
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


                // select the records that have nulls or blanks

    





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

                // clear out the old items in the list
                cboChooseLayer.Items.Clear();
                cboChooseFieldToUpdate.Items.Clear();

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

                IGeoFeatureLayer pGFlayer = null;

                // loop through the map's layers and check for the layer with the targeted name (based on the choose layer combobox)
                for (int i = 0; i < clsGlobals.pMap.LayerCount; i++)
                {
                    if (clsGlobals.pMap.Layer[i].Name == cboChooseLayer.Text)
                    {
                        pGFlayer = (IGeoFeatureLayer)clsGlobals.pMap.Layer[i];
                    }
                }


                //update the comboboxes with the currently selected layer's field names
                for (int i = 0; i < pGFlayer.FeatureClass.Fields.FieldCount; i++)
                {
                    cboChooseFields.Items.Add(pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());
                    cboChooseFieldToUpdate.Items.Add(pGFlayer.FeatureClass.Fields.Field[i].Name.ToString());
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
