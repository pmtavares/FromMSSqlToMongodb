<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FromMSSqlToMongo._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

 <br /><br /><br />   
<div class="row pt-10">
        <div class="col-md-10">
            <div class="form-group mt-4">
                <div class="row mt-10">
                    <div class="col-md-5">
                        <label class="control-label col-md-6">Mongo Collection</label>
                        <asp:TextBox ID="mongoDb" runat="server" name="mongoDb" class="form-control"></asp:TextBox>
                    </div>
                     <div class="col-md-4">
                        <label class="control-label col-md-4">Source</label>
                         <asp:TextBox ID="tableSource" runat="server" name="tableSource" class="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label col-md-3">Destination</label>
                        <asp:TextBox ID="tableDestination" runat="server" name="tableDestination" class="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
        
            <div class="form-group">
                <div class="row">
                    <div class="col-md-2 col-md-offset-10">
                         <asp:Button ID="Generate" runat="server" Text="Generate" OnClick="GenerateCollection" class="form-control btn btn-info"/>
                    </div>

               
                </div>
            </div>
    </div>
    <div class="col-md-4" >

    </div>
</div>
        
        

    

</asp:Content>
