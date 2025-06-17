#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.UI;
using FTOptix.DataLogger;
using FTOptix.HMIProject;
using FTOptix.NativeUI;
using FTOptix.CoreBase;
using FTOptix.Recipe;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.Retentivity;
using FTOptix.NetLogic;
using FTOptix.Core;
using FTOptix.OPCUAServer;
using FTOptix.Alarm;
using FTOptix.CommunicationDriver;
using FTOptix.Modbus;
using FTOptix.OPCUAClient;
#endregion

public class DesignTimeNetLogic1 : BaseNetLogic
{
    [ExportMethod]
    public void MioMetodo()
    {
        // Insert code to be executed by the method
        Log.Info("This is an info message");
    }

    [ExportMethod]
    public void MioMetodo1()
    {
        // Insert code to be executed by the method
        var testovariabile = LogicObject.GetVariable("TestoMsg");
        Log.Info(testovariabile.Value);
        var risultato = LogicObject.GetVariable("Risultato");
        risultato.Value = true;
        var variabileModello = Project.Current.GetVariable("Model/RecipeTags/Variable1");
        Log.Info("La variabile di modello " + variabileModello.BrowseName + " vale " + variabileModello.Value);
    }

    [ExportMethod]
    public void MioMetodo2()
    {
        var cartellaProgetto = Project.Current.Get("Model/RecipeTags");
        foreach (IUAVariable item in cartellaProgetto.Children)
        {
            Log.Info("La variabile " + item.BrowseName + " vale: " + item.Value);
        }
    }

    [ExportMethod]
    public void CreaAllarmi()
    {
        for (int i = 0; i < 10; i++)
        {
            if (Project.Current.Get("Alarms/Allarme_" + i) == null)
            {
                var mioAllarme = InformationModel.Make<DigitalAlarm>("Allarme_" + i);
                mioAllarme.Message = "Testo allarme " + i;
                Project.Current.Get("Alarms").Add(mioAllarme);

            }
          
        }

    }
}
