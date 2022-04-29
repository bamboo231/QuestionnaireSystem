<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminMain.Master" AutoEventWireup="true" CodeBehind="EditQuestionnaire.aspx.cs" Inherits="QuestionnaireSystem.admin.EditQuestionnaire" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <nav>
        <div class="nav nav-tabs" id="nav-tab" role="tablist">
            <asp:Button runat="server" ID="bookmark1" class="nav-link" aria-selected="true" Text="問卷" OnClick="bookmark1_Click"></asp:Button>
            <asp:Button runat="server" ID="bookmark2" class="nav-link" aria-selected="false" Text="問題" OnClick="bookmark2_Click"></asp:Button>
            <asp:Button runat="server" ID="bookmark3" class="nav-link" aria-selected="false" Text="填寫資料" OnClick="bookmark3_Click"></asp:Button>
            <asp:Button runat="server" ID="bookmark4" class="nav-link" aria-selected="false" Text="統計" OnClick="bookmark4_Click"></asp:Button>
        </div>
    </nav>
    <div id="nav-tabContent">
        <%--問卷固定問題頁--%>
        <asp:PlaceHolder ID="plhbookmark1" runat="server">

            <div>問卷名稱<asp:TextBox ID="textCaption" runat="server"></asp:TextBox></div>
            <br />
            <div>描述內容<asp:TextBox ID="textQuestionnaireContent" runat="server"></asp:TextBox></div>
            <br />
            <div>開始時間<asp:TextBox ID="textStartDate" runat="server" TextMode="Date"></asp:TextBox></div>
            <br />
            <div>結束時間<asp:TextBox ID="textEndDate" runat="server" TextMode="Date"></asp:TextBox></div>
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
                        <td>
                            <asp:TextBox ID="tbxTableName" runat="server" Text='<%# Eval("QuestOrder")%>' Style="display: none;" />
                            <asp:CheckBox ID="A" runat="server" value='<%# Eval("QuestOrder")%>' />
                        </td>
                        <td><%# Eval("QuestOrder")%></td>
                        <td><%# Eval("QuestContent")%></td>
                        <td><%# Eval("AnswerForm")%></td>
                        <td>
                            <%--<asp:CheckBox ID="CheckBox6" runat="server" Checked='<%# Eval("Required")%>' />--%></td>
                        <td>
                            <a href="EditQuestionnaire.aspx?QnirID=<%# Eval("QuestionnaireID")%>&updateOrder=<%# Eval("QuestOrder")%>&Targetplh=2">編輯</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr>
                        <td bgcolor="#CECECF">
                            <asp:TextBox ID="tbxTableName2" runat="server" Text='<%# Eval("QuestOrder")%>' Style="display: none;" />
                            <asp:CheckBox ID="A" runat="server" Text='<%# Eval("QuestOrder")%>' />
                        </td>
                        <td bgcolor="#CECECF"><%# Eval("QuestOrder")%></td>
                        <td bgcolor="#CECECF"><%# Eval("QuestContent")%></td>
                        <td bgcolor="#CECECF"><%# Eval("AnswerForm")%></td>
                        <td bgcolor="#CECECF">
                            <%--<asp:CheckBox ID="CheckBox6" runat="server" Checked='<%# Eval("Required")%>' />--%></td>
                        <td bgcolor="#CECECF">
                            <a href="EditQuestionnaire.aspx?QnirID=<%# Eval("QuestionnaireID")%>&updateOrder=<%# Eval("QuestOrder")%>&Targetplh=2">編輯</a>
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
                <asp:Button ID="btnExport" runat="server" Text="匯出" OnClick="btnExport_Click" />
            </div>
            <%--繳回的列表--%>
            <asp:PlaceHolder ID="plhdoneList" runat="server">
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
                            <td><%# Eval("BasicAnswerID")%></td>
                            <td><%# Eval("Nickname")%></td>
                            <td><%# Eval("AnswerDate", "{0:yyyy/MM/dd HH:mm:ss}")%></td>
                            <td><a href="EditQuestionnaire.aspx?QnirID=<%# Eval("QuestionnaireID")%>&BsicAnsID=<%# Eval("BasicAnswerID")%>&targetplh=3">前往</a></td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td bgcolor="#CECECF"><%# Eval("BasicAnswerID")%></td>
                            <td bgcolor="#CECECF"><%# Eval("Nickname")%></td>
                            <td bgcolor="#CECECF"><%# Eval("AnswerDate", "{0:yyyy/MM/dd HH:mm:ss}")%></td>
                            <td bgcolor="#CECECF"><a href="EditQuestionnaire.aspx?QnirID=<%# Eval("QuestionnaireID")%>&BsicAnsID=<%# Eval("BasicAnswerID")%>&targetplh=3">前往</a></td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                (分頁待補)
            </asp:PlaceHolder>

            <%--繳回的詳細內容--%>
            <asp:PlaceHolder ID="plhDoneDetail" runat="server" Visible="false">

                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="姓名"></asp:Label></td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="手機"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="doneName" runat="server" Text='<%# Eval("Nickname")%>' Enabled="false"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="donePhone" runat="server" Text='<%# Eval("Phone")%>' Enabled="false"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Email"></asp:Label></td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="年齡"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="doneEmail" runat="server" Text='<%# Eval("email")%>' Enabled="false"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="doneAge" runat="server" Text='<%# Eval("Age")%>' Enabled="false"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="填寫時間"></asp:Label>
                            <asp:Label ID="AnswerDate" runat="server" Text='<%# Eval("AnswerDate", "{0:yyyy/MM/dd HH:mm:ss}")%>'></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="plhDynDetail" runat="server"></asp:PlaceHolder>
            </asp:PlaceHolder>
        </asp:PlaceHolder>
        <%--統計--%>
        <asp:PlaceHolder ID="plhbookmark4" runat="server" Visible="false"></asp:PlaceHolder>
    </div>
</asp:Content>
