<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="FillOutPage.aspx.cs" Inherits="QuestionnaireSystem.FillOutPage1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="doIt" runat="server">
        <asp:PlaceHolder ID="plhDoneDetail" runat="server">
            <table width="60%">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Caption" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="QuestionnaireContent" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="姓名"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="doneName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="手機"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="donePhone" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Email"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="doneEmail" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="年齡"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="doneAge" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plhDynDetail" runat="server"></asp:PlaceHolder>
        <asp:Button ID="btnQuestCancel" runat="server" Text="取消" OnClick="btnQuestCancel_Click" UseSubmitBehavior="false"/>
        <asp:Button ID="btnQuestSummit" runat="server" Text="送出" OnClick="btnQuestSummit_Click" UseSubmitBehavior="false"/>
    </div>



    <div id="CheckIt" runat="server" visible="false">
        <asp:PlaceHolder ID="plhCheckDetail" runat="server" >
            <table style="border-color:aqua;border:1px;" width="60%">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Caption2" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="QuestionnaireContent2" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="姓名"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="chkName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="手機"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="chkPhone" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Email"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="chkEmail" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="年齡"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="chkAge" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="chkDynDetail" runat="server"></asp:PlaceHolder>
        <asp:Button ID="btnGoBack" runat="server" Text="取消" OnClick="btnGoBack_Click" UseSubmitBehavior="false"/>
        <asp:Button ID="btnChkSummit" runat="server" Text="送出" OnClick="btnChkSummit_Click" UseSubmitBehavior="false"/>
    </div>
    <br />
    <br />
</asp:Content>
