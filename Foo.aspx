<%@ Page	Title="Foo / bar"
			Language="C#"
			MasterPageFile="~/Site.Master"
			AutoEventWireup="false"
			EnableViewState="false"
			CodeBehind="Foo.aspx.cs"
			Inherits="WebTest01.Foo" %>

<asp:Content ID="head" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">

	Hello world

</asp:Content>



<script runat="server">

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}

</script>
