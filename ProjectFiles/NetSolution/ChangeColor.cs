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

public class ChangeColor : BaseNetLogic
{
    private IUAVariable varPLC;

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        varPLC = Project.Current.GetVariable("Model/ColorePLC");
        varPLC.VariableChange += VarPLC_VariableChange;

        for (int i = 0; i < 100; i++)
        {
            var mioLed = InformationModel.Make<Led>("Led_" + i);
            Owner.Owner.Get("HorizontalLayout1").Add(mioLed);
        }
    }

    private void VarPLC_VariableChange(object sender, VariableChangeEventArgs e)
    {
        var mioRettangolo = (Rectangle)Owner;
        switch ((int)e.NewValue)
        {
            case 1:
                mioRettangolo.FillColor = Colors.Blue;
                break;
            case 2:
                mioRettangolo.FillColor = Colors.Red;
                break;

            default:
                mioRettangolo.FillColor = Colors.Lime;
                break;
        }
        var button = Owner.Owner.Get<Button>("Button1");
        if (e.NewValue >= 10)
            button.Visible = false;
        else 
            button.Visible = true;
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        varPLC.VariableChange -= VarPLC_VariableChange;
    }
}
