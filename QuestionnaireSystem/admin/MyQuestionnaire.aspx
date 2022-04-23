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
        <div id="QuestionnaireLis">
            <asp:Repeater ID="RptrQtnir" runat="server">
                <HeaderTemplate>
                    <table border="1" width="90%" id="table2">
                        <tr>
                            <td bgcolor="#000000"><font color="#FFFFFF"> - </font></td>
                            <td bgcolor="#000000"><font color="#FFFFFF"><b>#</b></font></td>
                            <td bgcolor="#000000"><font color="#FFFFFF"><b>問卷</b></font></td>
                            <td bgcolor="#000000"><font color="#FFFFFF"><b>狀態</b></font></td>
                            <td bgcolor="#000000"><font color="#FFFFFF"><b>開始時間</b></font></td>
                            <td bgcolor="#000000"><font color="#FFFFFF"><b>結束時間</b></font></td>
                            <td bgcolor="#000000"><font color="#FFFFFF"><b>觀看統計</b></font></td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><asp:CheckBox ID="CheckBox1" runat="server" /></td>
                        <td><%# Eval("QuestionnaireID")%></td>
                        <td><a href="page.aspx?QuestionnaireID=link" target="_blank"><%# Eval("Caption")%></a></td>
                        <td><%# Eval("VoidStatus")%></td>
                        <td><%# Eval("StartDate", "{0:yyyy/MM/dd}")%></td>
                        <td><%# Eval("EndDate", "{0:yyyy/MM/dd}")%></td>
                        <td><a href="page.aspx?QuestionnaireID=link" target="_blank">前往</a></td>
                    </tr>
                </ItemTemplate>

<%--                <AlternatingItemTemplate>
                    <tr>
                        <td bgcolor="#CECEFF">
                            <a href="Repeater_NorthWind_Table2.aspx?OrderID=<%# Eval("OrderID")%>" target="_blank">
                                <big><b>
                                    <%# Eval("OrderID")%></b></big> </a>
                        </td>
                        <td bgcolor="#CECEFF">
                            <%# Eval("CustomerID")%>
                        --
                        <%# Eval("B_CompanyName")%>
                        </td>
                        <td bgcolor="#CECEFF">
                            <%# Eval("EmployeeID")%>
                        --
                        <%# Eval("C_LastName")%>
                        </td>
                        <td bgcolor="#CECEFF">
                            <%# Eval("OrderDate", "{0:yyyy/MM/dd}")%>
                        </td>
                    </tr>
                </AlternatingItemTemplate>--%>

                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>


        </div>
    </div>
</asp:Content>
