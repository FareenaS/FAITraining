<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TimeControl.ascx.cs" Inherits="SampleWebFormApp.UserControls.TimeControl" %>
<%@ OutputCache Duration="60" VaryByParam="None" %>
<div>
   <h2>
       The Time is :<asp:Label Text="" runat="server" ID="lblTime" />
   </h2>  
</div>