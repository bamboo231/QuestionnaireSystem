<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPager.ascx.cs" Inherits="QuestionnaireSystem.ShareControls.ucPager" %>

<div class="Pager">
    <a runat="server" id="aLinkFirst" href="Index.aspx?Page=1"><< </a>&nbsp
    <a runat="server" id="aLinkPrev" href="Index.aspx?Page=1">＜ </a>
    <a runat="server" id="aLinkPage1" href="Index.aspx?Page=1">1 </a>

    <a runat="server" id="aLinkPage2" href="">2</a>

    <a runat="server" id="aLinkPage3" href="Index.aspx?Page=3">3</a>
    <a runat="server" id="aLinkPage4" href="Index.aspx?Page=4">4</a>
    <a runat="server" id="aLinkPage5" href="Index.aspx?Page=5">5</a>
    <a runat="server" id="aLinkNext" href="Index.aspx?Page=3">＞ </a>&nbsp
    <a runat="server" id="aLinkLast" href="Index.aspx?Page=10">>> </a>
</div>
