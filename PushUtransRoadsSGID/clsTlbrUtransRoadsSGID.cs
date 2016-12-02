using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace PushUtransRoadsSGID
{
    /// <summary>
    /// Summary description for clsTlbrUtransRoadsSGID.
    /// </summary>
    [Guid("66a280f0-67fc-4d49-8da7-c1a0194b3841")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("PushUtransRoadsSGID.clsTlbrUtransRoadsSGID")]
    public sealed class clsTlbrUtransRoadsSGID : BaseToolbar
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
            MxCommandBars.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommandBars.Unregister(regKey);
        }

        #endregion
        #endregion

        public clsTlbrUtransRoadsSGID()
        {
            //
            // TODO: Define your toolbar here by adding items
            //
            //AddItem("esriArcMapUI.ZoomInTool");
            //BeginGroup(); //Separator
            //AddItem("{FBF8C3FB-0480-11D2-8D21-080009EE4E51}", 1); //undo command
            //AddItem(new Guid("FBF8C3FB-0480-11D2-8D21-080009EE4E51"), 2); //redo command
            AddItem("{428d8aba-75d9-4ef1-bdb8-24a0abefd795}");  // check for blanks and nulls in dataset
            AddItem("{e7dab9b0-789d-4027-9634-fa3b85365802}"); // address range checks
            AddItem("{49b2f554-ba62-4768-aed8-e8b51b48dc51}"); // assign attributes spatially
        }

        public override string Caption
        {
            get
            {
                //TODO: Replace bar caption
                return "AGRC Database Maintenance Tools";
            }
        }
        public override string Name
        {
            get
            {
                //TODO: Replace bar ID
                return "clsTlbrUtransRoadsSGID";
            }
        }
    }
}