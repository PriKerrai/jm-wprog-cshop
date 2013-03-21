<%@ Page Title="Welcome" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
                 CodeBehind="Default.aspx.cs" Inherits="Webb3._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %></h1>
                <h2>Random Store is the best place to find that random gift</h2>
            </hgroup>
            <p>
                We will do anything to get that random gift to that random person
                that you randomly like. Go go magic random!<br />
                So when we send something to you, we dont even know what it is!
            </p>
        </div>
    </section>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <section style="vertical-align: middle">
    </section>
</asp:Content>
