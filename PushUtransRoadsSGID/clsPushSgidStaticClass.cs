using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using ESRI.ArcGIS.ADF;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PushUtransRoadsSGID
{
    class clsPushSgidStaticClass
    {


        public static void GetCurrentMapDocVariables()
        {
            try
            {
                //get access to the document and the active view

                clsGlobals.pMxDocument = (IMxDocument)clsGlobals.arcApplication.Document;
                clsGlobals.pMap = clsGlobals.pMxDocument.FocusMap;
                clsGlobals.pActiveView = clsGlobals.pMxDocument.ActiveView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "Push Utrans Roads to SGID!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        } 


        public static esriFieldType GetArcGisFieldType(string strFieldName)
        {
            try
            {
                IFields fields = clsGlobals.arcFeatLayer.FeatureClass.Fields;
                IField field = fields.get_Field(clsGlobals.arcFeatLayer.FeatureClass.Fields.FindField(strFieldName));

                return field.Type;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "Push Utrans Roads to SGID!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return 0;
            }

        }

        public static IFeature GetIntersectedSGIDBoundary(IPoint arcPnt, IFeatureClass arcFeatClass)
        {
            try
            {
                ISpatialFilter arcSpatialFilter = new SpatialFilter();
                arcSpatialFilter.Geometry = arcPnt;
                arcSpatialFilter.GeometryField = "SHAPE";
                arcSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                arcSpatialFilter.SubFields = "*";

                IFeatureCursor arcFeatCur = arcFeatClass.Search(arcSpatialFilter, false);
                IFeature arcFeatureToReturn = arcFeatCur.NextFeature();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(arcFeatCur);

                return arcFeatureToReturn;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "Push Utrans Roads to SGID!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }


        public static void GetAccessToSgidLayers()
        {
            try
            {
                // get access to the sgid feature classes
                clsGlobals.arcFeatClass_Counties = clsGlobals.featureWorkspaceSGID.OpenFeatureClass("SGID.BOUNDARIES.Counties");
                clsGlobals.arcFeatClass_Muni = clsGlobals.featureWorkspaceSGID.OpenFeatureClass("SGID.BOUNDARIES.Municipalities");
                clsGlobals.arcFeatClass_ZipCodes = clsGlobals.featureWorkspaceSGID.OpenFeatureClass("SGID.BOUNDARIES.ZipCodes");
                clsGlobals.arcFeatClass_AddrSys = clsGlobals.featureWorkspaceSGID.OpenFeatureClass("SGID.LOCATION.AddressSystemQuadrants");
                clsGlobals.arcFeatClass_USNG = clsGlobals.featureWorkspaceSGID.OpenFeatureClass("SGID.INDICES.NationalGrid");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine +
                "Error Source: " + Environment.NewLine + ex.Source + Environment.NewLine + Environment.NewLine +
                "Error Location:" + Environment.NewLine + ex.StackTrace,
                "Push Utrans Roads to SGID!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }



        //connect to sde - method
        #region "Connect to SDE"
        public static ESRI.ArcGIS.Geodatabase.IWorkspace ConnectToTransactionalVersion(String server, String instance, String database, String authenication, String version)
        {
            IPropertySet propertySet = new PropertySetClass();
            propertySet.SetProperty("SERVER", server);
            //propertySet.SetProperty("DBCLIENT", dbclient);
            propertySet.SetProperty("INSTANCE", instance);
            propertySet.SetProperty("DATABASE", database);
            propertySet.SetProperty("AUTHENTICATION_MODE", authenication);
            propertySet.SetProperty("VERSION", version);

            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory");
            IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
            return workspaceFactory.Open(propertySet, 0);
        }
        #endregion

        //connect to sde - method (this method has the same name so we can use method overloading)
        #region "Connect to SDE"
        public static ESRI.ArcGIS.Geodatabase.IWorkspace ConnectToTransactionalVersion(String server, String instance, String database, String authenication, String version, String username, String pass)
        {
            IPropertySet propertySet = new PropertySetClass();
            propertySet.SetProperty("SERVER", server);
            //propertySet.SetProperty("DBCLIENT", dbclient);
            propertySet.SetProperty("INSTANCE", instance);
            propertySet.SetProperty("DATABASE", database);
            propertySet.SetProperty("AUTHENTICATION_MODE", authenication);
            propertySet.SetProperty("VERSION", version);
            propertySet.SetProperty("USER", username);
            propertySet.SetProperty("PASSWORD", pass);

            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory");
            IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
            return workspaceFactory.Open(propertySet, 0);
        }
        #endregion


        public static IEnumerable<T> ScanForControls<T>(Control parent) where T : Control
        {
            if (parent is T)
                yield return (T)parent;

            foreach (Control child in parent.Controls)
            {
                foreach (var child2 in ScanForControls<T>(child))
                    yield return (T)child2;
            }
        }

    }
}
