<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="StatisticPage.aspx.cs" Inherits="QuestionnaireSystem.StatisticPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-all">
        <%--統計--%>
        <div class="container-fluid">
            <asp:PlaceHolder ID="PlhDym" runat="server"></asp:PlaceHolder>
        </div>
        <asp:Label ID="NAStatistic" runat="server" Text="(查無資料)" Visible="false"></asp:Label>

    </div>

    <input id="currentID" name="currentID" runat="server" type="hidden" />

    <asp:Image runat="server"></asp:Image>
    <script>
</script>
</asp:Content>
