<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminMain.Master" AutoEventWireup="true" CodeBehind="MyQuestionnaire.aspx.cs" Inherits="QuestionnaireSystem.admin.MyQuestionnaire" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="srchArea">
        問卷標題
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        開始/結束<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><asp:Button ID="Button1" runat="server" Text="Button" />
        <br />
    </div>
    <div id="ListArea">
        <%--問卷總列表--%>
        <div id="QuestionnaireLis" >
            <asp:Repeater ID="RptrQtnir" runat="server">
                <ItemTemplate>
                    溝選方塊
                    <asp:Literal ID="ltlBlkID" runat="server" Text='<%# Eval("QuestionnaireID") %>' />
                    <asp:Literal ID="Literal4" runat="server" Text='<%# Eval("Caption") %>' />
                    <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("VoidStatus") %>' />
                    <asp:Literal ID="ltlDate" runat="server" Text='<%# Eval("StartDate", "{0:yyyy/MM/dd}")%>' />
                    <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("EndDate", "{0:yyyy/MM/dd}")%>' />
                    <asp:Literal ID="Literal3" runat="server" Text='前往' /><br />
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
