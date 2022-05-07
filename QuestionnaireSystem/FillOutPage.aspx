<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="FillOutPage.aspx.cs" Inherits="QuestionnaireSystem.FillOutPage1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="position-absolute top-0 end-0">
        <asp:Label ID="IsVoting" class="position-absolute top-0 end-0" runat="server" Text="已結束"></asp:Label>
        <br />
        <asp:Label ID="Period" runat="server" Text="2021/9/9~2022/5/5"></asp:Label>
        <br />
    </div>
    <div id="doIt" class="container" runat="server">
        <div class="row">
            <div class="col-2">
            </div>
            <div class="col-8">
                <asp:PlaceHolder ID="plhDoneDetail" runat="server">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="Caption" Style="font-size: 22px; text-align: center; line-height: 80px;" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="QuestionnaireContent" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="line-height: 30px;">
                            <td>
                                <br />
                                <asp:Label ID="Label1" runat="server" Text="姓名"></asp:Label>
                            </td>
                            <td>
                                <br />
                                <asp:TextBox ID="doneName" Style="line-height: 20px;" runat="server" placeholder="社子澎恰恰"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="line-height: 30px;">
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="手機"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="donePhone" Style="line-height: 20px;" runat="server" placeholder="0912345678"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="line-height: 30px;">
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Email"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="doneEmail" TextMode="Email" Style="line-height: 20px;" runat="server" placeholder="goodguy@gmail.com"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="line-height: 30px;">
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="年齡"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="doneAge" TextMode="Number" Style="line-height: 20px;" runat="server" placeholder="18(請輸入數字)"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
                <br />

                <asp:PlaceHolder ID="plhDynDetail" runat="server"></asp:PlaceHolder>
                <asp:Button ID="btnQuestCancel" runat="server" Text="取消" OnClick="btnQuestCancel_Click" UseSubmitBehavior="false" />
                <asp:Button ID="btnQuestSummit" runat="server" Text="送出" OnClick="btnQuestSummit_Click" UseSubmitBehavior="false" />
            </div>
        </div>
    </div>
    <div id="CheckIt" runat="server" class="container" visible="false">
        <div class="row">
            <div class="col-2">
            </div>
            <div class="col-8">
                <asp:PlaceHolder ID="plhCheckDetail" runat="server">
                    <table >
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="Caption2" Style="font-size: 22px; text-align: center; line-height: 80px;" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="QuestionnaireContent2" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="line-height: 30px;">
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="姓名"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="chkName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr  style="line-height: 30px;">
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="手機"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="chkPhone" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="line-height: 30px;">
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Email"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="chkEmail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="line-height: 30px;">
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="年齡"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="chkAge" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:PlaceHolder>
                <br />

                <asp:PlaceHolder ID="chkDynDetail" runat="server"></asp:PlaceHolder>
                <asp:Button ID="btnGoBack" runat="server" Text="修改" OnClick="btnGoBack_Click" UseSubmitBehavior="false" />
                <asp:Button ID="btnChkSummit" runat="server" Text="送出" OnClick="btnChkSummit_Click" UseSubmitBehavior="false" />
            </div>
        </div>
    </div>
    <br />
    <br />
</asp:Content>
