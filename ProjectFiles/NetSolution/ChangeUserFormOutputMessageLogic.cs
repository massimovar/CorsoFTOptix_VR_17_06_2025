#region Using directives
using System;
using FTOptix.CoreBase;
using FTOptix.HMIProject;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.NetLogic;
using FTOptix.UI;
using FTOptix.OPCUAServer;
using FTOptix.Alarm;
using FTOptix.CommunicationDriver;
using FTOptix.Modbus;
using FTOptix.OPCUAClient;
#endregion

public class ChangeUserFormOutputMessageLogic : BaseNetLogic
{
    public override void Start()
    {
        HideMessageLabel();
        loginResultCodeVariable = Owner.GetVariable("LoginResultCode");

        task = new DelayedTask(() =>
        {
            HideMessageLabel();
            taskStarted = false;
        }, 10000, LogicObject);
    }

    public override void Stop()
    {
        task?.Dispose();
    }

    [ExportMethod]
    public void SetOutputMessage(int resultCode)
    {
        if (loginResultCodeVariable == null)
        {
            Log.Error("ChangeUserOutputMessageLogic", "Unable to find LoginResultCode variable in ChangeUserFormOutputMessage label");
            return;
        }

        loginResultCodeVariable.Value = resultCode;
        ShowMessageLabel();

        if (taskStarted)
        {
            task?.Cancel();
            taskStarted = false;
        }

        task.Start();
        taskStarted = true;
    }

    private void ShowMessageLabel()
    {
        var messageLabel = (Label)Owner;
        messageLabel.Visible = true;
    }

    private void HideMessageLabel()
    {
        var messageLabel = (Label)Owner;
        messageLabel.Visible = false;
    }

    private DelayedTask task;
    private bool taskStarted = false;
    private IUAVariable loginResultCodeVariable;
}
