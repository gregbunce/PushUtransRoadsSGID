﻿using ESRI.ArcGIS.ArcMapUI;
using System;
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



    }
}