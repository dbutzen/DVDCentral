<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Directors.aspx.cs" Inherits="DTB.DVDCentral.WFUI.Directors" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="header rounded-top">
        <h3>Directors</h3>
    </div>
    <p>

    </p>
    <div class="form-row ml-2 mt-2">
        <div class="control-label col-md-2">
            <asp:Label ID="lblDirectorPick" runat="server" Text="Director:" >

            </asp:Label>
        </div>
        <div class="control-label col-md-3">
            <asp:DropDownList ID="ddlDirectors" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDirectors_SelectedIndexChanged" >

            </asp:DropDownList>
            
        </div>
    </div>

    <div class="form-row ml-2 mt-2">
        <div class="control-label col-md-2">
            <asp:Label ID="Label1" runat="server" Text="First Name:">

            </asp:Label>
        </div>
        <div class="control-label col-md-3">
            <asp:TextBox ID="txtFirstName" runat="server">

            </asp:TextBox>
        </div>
    </div>
    <div class="form-row ml-2 mt-2">
        <div class="control-label col-md-2">
            <asp:Label ID="Label2" runat="server" Text="Last Name:">

            </asp:Label>
        </div>

        <div class="control-label col-md-3">
            <asp:TextBox ID="txtLastName" runat="server">

            </asp:TextBox>
        </div>
    </div>    
    <div class="form-row ml-2 mt-2">
        <div class="form-group mt-2 ml-5">
            <asp:Button ID="btnInsert" runat="server" CssClass="btn btn-primary btn-lg ml-3" Text="Insert" OnClick="btnInsert_Click" />
            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary btn-lg ml-3" Text="Update" OnClick="btnUpdate_Click" />
            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary btn-lg ml-3" Text="Delete" OnClick="btnDelete_Click" />
        </div>
    </div>
</asp:Content>
