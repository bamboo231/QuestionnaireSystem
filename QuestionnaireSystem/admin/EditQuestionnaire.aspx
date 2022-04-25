<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminMain.Master" AutoEventWireup="true" CodeBehind="EditQuestionnaire.aspx.cs" Inherits="QuestionnaireSystem.admin.EditQuestionnaire" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /*表格奇數偶數行底色*/
        tr:nth-child(even) {
            background-color: #ffffff;
        }

        tr:nth-child(odd) {
            background-color: #CECECF;
        }

        #btnBasicDelete {
            float: right;
        }

        #btnBasicSummit {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <nav>
        <div class="nav nav-tabs" id="nav-tab" role="tablist">
            <asp:Button runat="server" ID="bookmark1" class="nav-link" aria-selected="true" Text="問卷" OnClick="bookmark1_Click"></asp:Button>
            <asp:Button runat="server" ID="bookmark2" class="nav-link" aria-selected="false" Text="問題" OnClick="bookmark2_Click"></asp:Button>
            <asp:Button runat="server" ID="bookmark3" class="nav-link" aria-selected="false" Text="填寫資料" OnClick="bookmark3_Click"></asp:Button>
        </div>
    </nav>
    <div id="nav-tabContent">
        <%--問卷固定問題頁--%>
        <asp:PlaceHolder ID="plhbookmark1" runat="server">
            <div>問卷名稱<asp:TextBox ID="textCaption" runat="server"></asp:TextBox></div>
            <br />
            <div>描述內容<asp:TextBox ID="textQuestionnaireContent" runat="server"></asp:TextBox></div>
            <br />
            <div>開始時間<asp:TextBox ID="textStartDate" runat="server"></asp:TextBox></div>
            <br />
            <div>結束時間<asp:TextBox ID="textEndDate" runat="server"></asp:TextBox></div>
            <br />
            <asp:CheckBox ID="ChkBxVoidStatus" runat="server" Text="已啟用" /><br />
            <asp:Button ID="btnBasicCancel" Style="margin-left: 40%;" runat="server" Text="取消" OnClick="btnBasicCancel_Click" />
            <asp:Button ID="btnBasicSummit" Style="float: right;" runat="server" Text="送出" OnClick="btnBasicSummit_Click" />
            <br />
        </asp:PlaceHolder>
        <%--撰寫問卷問題頁--%>
        <asp:PlaceHolder ID="plhbookmark2" runat="server" Visible="false">
            <%--撰寫問題--%>
            <div>
                種類<asp:DropDownList ID="setQuestType" runat="server">
                    <asp:ListItem>自訂問題</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div>
                問題<asp:TextBox ID="setQuest" runat="server"></asp:TextBox>
                <asp:DropDownList ID="setQuestForm" runat="server">
                    <asp:ListItem>文字方塊</asp:ListItem>
                    <asp:ListItem>數字</asp:ListItem>
                    <asp:ListItem>Email</asp:ListItem>
                    <asp:ListItem>日期</asp:ListItem>
                    <asp:ListItem>單選方塊</asp:ListItem>
                    <asp:ListItem>複選方塊</asp:ListItem>
                </asp:DropDownList>
                <asp:CheckBox ID="IsRequired" runat="server" Text="必填" />
            </div>
            <div>
                回答<asp:TextBox ID="textSelectItem" runat="server"></asp:TextBox>(多個答案以,分隔)
            <asp:Button ID="btnAddToQuest" runat="server" Text="加入" OnClick="btnAddToQuest_Click" />
            </div>
            <br />
            <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="/images/deleteIcon.png" Width="20px" OnClick="imgBtnDelete_Click" />
            <%--問題列表--%>
            <asp:Repeater ID="RptrQuest" runat="server">
                <HeaderTemplate>
                    <table border="1" width="90%" id="table2">
                        <tr>
                            <td bgcolor="#000000"><font color="#FFFFFF">- </font></td>
                            <td bgcolor="#000000"><font color="#FFFFFF"><b>#</b></font></td>
                            <td bgcolor="#000000"><font color="#FFFFFF"><b>問題</b></font></td>
                            <td bgcolor="#000000"><font color="#FFFFFF"><b>種類</b></font></td>
                            <td bgcolor="#000000"><font color="#FFFFFF"><b>必須</b></font></td>
                            <td bgcolor="#000000"><font color="#FFFFFF">- </font></td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><asp:CheckBox ID="chkBxQuest" runat="server" /></td>
                        <td><%# Eval("QuestOrder")%></td>
                        <td><%# Eval("QuestContent")%></td>
                        <td><%# Eval("AnswerForm")%></td>
                        <td><asp:CheckBox ID="CheckBox6" runat="server" Checked='<%# Eval("Required")%>' /></td>
                        <td><asp:LinkButton ID="lnkBtnEdit" runat="server">編輯</asp:LinkButton></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr>
                        <td bgcolor="#CECECF"><asp:CheckBox ID="chkBxQuest" runat="server" /></td>
                        <td bgcolor="#CECECF"><%# Eval("QuestOrder")%></td>
                        <td bgcolor="#CECECF"><%# Eval("QuestContent")%></td>
                        <td bgcolor="#CECECF"><%# Eval("AnswerForm")%></td>
                        <td bgcolor="#CECECF"><asp:CheckBox ID="CheckBox6" runat="server" Checked='<%# Eval("Required")%>' /></td>
                        <td bgcolor="#CECECF"><asp:LinkButton ID="lnkBtnEdit" runat="server">編輯</asp:LinkButton></td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <%--取消/送出問題--%>
            <asp:Button ID="btnQuestCancel" runat="server" Text="取消" OnClick="btnQuestCancel_Click" />
            <asp:Button ID="btnQuestSummit" runat="server" Text="送出" OnClick="btnQuestSummit_Click" />
        </asp:PlaceHolder>
        <%--填寫資料--%>
        <asp:PlaceHolder ID="plhbookmark3" runat="server" Visible="false">
            <div>
                <asp:Button ID="btnExport" runat="server" Text="匯出" />
            </div>
            <%--繳回的列表--%>
            <asp:PlaceHolder ID="doneList" runat="server">
                <asp:Repeater ID="RptrAnswerList" runat="server">
                    <HeaderTemplate>
                        <table border="1" width="90%" id="table2">
                            <tr>
                                <td bgcolor="#000000"><font color="#FFFFFF"><b>#</b></font></td>
                                <td bgcolor="#000000"><font color="#FFFFFF"><b>姓名</b></font></td>
                                <td bgcolor="#000000"><font color="#FFFFFF"><b>填寫時間</b></font></td>
                                <td bgcolor="#000000"><font color="#FFFFFF"><b>觀看細節</b></font></td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>這裡計算總數後遞減</td>
                            <td><%# Eval("Nickname")%></td>
                            <td><%# Eval("AnswerDate", "{0:yyyy/MM/dd}")%></td>
                            <td><a href="page.aspx?QuestionnaireID=link" target="_blank">前往</a></td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td bgcolor="#CECECF">這裡計算總數後遞減</td>
                            <td bgcolor="#CECECF"><%# Eval("Nickname")%></td>
                            <td bgcolor="#CECECF"><%# Eval("AnswerDate", "{0:yyyy/MM/dd}")%></td>
                            <td bgcolor="#CECECF"><a href="page.aspx?QuestionnaireID=link" target="_blank">前往</a></td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                (分頁待補)
            </asp:PlaceHolder>
            <%--繳回的詳細內頁--%>
<%--            <asp:PlaceHolder ID="plhDoneDetail" runat="server">姓名<asp:TextBox ID="doneName" runat="server"></asp:TextBox>手機<asp:TextBox ID="donePhone" runat="server"></asp:TextBox>
                Email<asp:TextBox ID="doneEmail" runat="server"></asp:TextBox>年齡<asp:TextBox ID="doneAge" runat="server"></asp:TextBox>
                填寫時間<%# Eval("AnswerDate")%>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <%# Eval("QuestOrder")%>.<%# Eval("QuestContent")%>
                    </ItemTemplate>
                </asp:Repeater>
            </asp:PlaceHolder>--%>
        </asp:PlaceHolder>

    </div>
</asp:Content>
