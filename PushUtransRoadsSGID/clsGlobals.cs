using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushUtransRoadsSGID
{
    class clsGlobals
    {

        public static IGeoFeatureLayer arcGeoFLayerUtransStreets
        {
            get;
            set;
        }

        // this gets set in the clsBtnMakeNullEmptyString class
        public static IApplication arcApplication
        {
            get;
            set;
        }

        public static IMap pMap
        {
            get;
            set;
        }

        public static IMxDocument pMxDocument
        {
            get;
            set;
        }

        public static IActiveView pActiveView
        {
            get;
            set;
        }

        public static IFeatureClass arcFeatClass_UtransRoads
        {
            get;
            set;
        }

        public static IGeoFeatureLayer pGFlayer
        {
            get;
            set;
        }

        public static IFeatureLayer arcFeatLayer
        {
            get;
            set;
        }

        public static IEditor3 arcEditor
        {
            get;
            set;
        }

        public static IFeatureClass arcFeatClass_Counties
        {
            get;
            set;
        }

        public static IFeatureClass arcFeatClass_Muni
        {
            get;
            set;
        }

        public static IFeatureClass arcFeatClass_ZipCodes
        {
            get;
            set;
        }

        public static IFeatureClass arcFeatClass_AddrSys
        {
            get;
            set;
        }

        public static IFeatureClass arcFeatClass_USNG
        {
            get;
            set;
        }

        public static IWorkspace workspaceSGID
        {
            get;
            set;
        }

        public static IFeatureWorkspace featureWorkspaceSGID
        {
            get;
            set;
        }

        public static ESRI.ArcGIS.Geodatabase.IFeature arcFeatureToEditSpatial
        {
            get;
            set;
        }

    }
}
