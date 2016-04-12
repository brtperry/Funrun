<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="_Default" ValidateRequest="false" %>

<!DOCTYPE html>

<html>

    <head>

        <meta charset="utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=Edge;chrome=1" />
        <meta name="keywords" content="Beaverbrooks,Beaverbrooks Blackpool 10K, Blackpool, 10K, fun run, funrun" />  	
	    <meta name="description" content="The home page of Beaverbrooks Blackpool 10K" />  	
	    <meta name="robots" content="index, follow, noarchive" />  	
	    <meta name="googlebot" content="noarchive" />
        <title>Beaverbrooks Blackpool 10K Registration</title>

        <link rel="stylesheet" href="css/screen.css" type="text/css" media="all" />
        <link rel="shortcut icon" href="img/micon.ico" type="image/x-icon" />

        <script type="text/javascript" charset="utf-8">
	    //<![CDATA[

            function isNumeric(x) {
                // I use this function like this: if (isNumeric(myVar)) { } 
                // regular expression that validates a value is numeric 
                var RegExp = /^(-)?(\d*)(\.?)(\d*)$/; // Note: this WILL allow a number that ends in a decimal: -452. 
                // compare the argument to the RegEx 
                // the 'match' function returns 0 if the value didn't match 
                var result = x.match(RegExp);
                return result;
            }

            function ValidateInfo() {

                var HNN = document.getElementById("txtHouseNumber");
                var Postcode = document.getElementById("txtPostcode");
                var FName = document.getElementById("txtForename");
                var SName = document.getElementById("txtSurname");
                var Age = document.getElementById("txtAge");

                if (HNN.value == "") {
                    alert("Please confirm your House number or name.");
                    HNN.focus();
                    return false;
                }
                if (Postcode.value == "") {
                    alert("Please confirm your Postcode.");
                    Postcode.focus();
                    return false;
                }
                if (FName.value == "") {
                    alert("Please confirm your Forename.");
                    FName.focus();
                    return false;
                }
                if (SName.value == "") {
                    alert("Please confirm your Surname.");
                    SName.focus();
                    return false;
                }

                if (!Number(Age.value)) {
                    alert("Please confirm your Age.\n\nThis is required to identify you in the results listings.");
                    Age.focus();
                    return false;
                }

                var email = document.getElementById("txtMail");
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

                if (!filter.test(email.value)) {
                    alert("Please confirm your Email Address.");
                    email.focus();
                    return false;
                }

                return true;
            }

    	//]]>
    	</script>

    </head>

    <body class="reghome">

    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <div id="container">

            <section id="registration" class="container_24 grid_20">

                <hr class="clear" />

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                    <ContentTemplate>

                        <section class="grid_10 prefix_1">

                            <article>

                                <header> <h3>Registration</h3> </header>

                                <fieldset id="pt1"> 
                	
	                                <legend>Step 1: Details</legend> 
                                    <br /> 
                                    <br />

	                                <label for="txtHouseNumber">House number</label> 
	                                <asp:TextBox ID="txtHouseNumber" runat="server" tabindex="1"></asp:TextBox>
	                                <label for="txtPostcode">Postcode</label> 
	                                <asp:TextBox ID="txtPostcode" runat="server" tabindex="2"></asp:TextBox>
               
	                                <label for="ddlTitle">Title</label> 
                                    <asp:DropDownList ID="ddlTitle" runat="server" TabIndex="3">
                                        <asp:ListItem>Mr</asp:ListItem>
                                        <asp:ListItem>Mrs</asp:ListItem>
                                        <asp:ListItem>Miss</asp:ListItem>
                                        <asp:ListItem>Ms</asp:ListItem>
                                        <asp:ListItem>Doctor</asp:ListItem>
                                        <asp:ListItem>Reverend</asp:ListItem>
                                    </asp:DropDownList>
	                    
	                                <label for="txtForename">Forename</label> 
	                                <asp:TextBox ID="txtForename" runat="server" tabindex="4"></asp:TextBox>
	                                <label for="txtSurname">Surname</label> 
	                                <asp:TextBox ID="txtSurname" runat="server" tabindex="5"></asp:TextBox>
            		                <br /> 
            		                <br />

                                    <p>Please choose your <i>gender</i> carefully.</p>

                                    <asp:RadioButton id="Radio1" 
                                                Text="Male" 
                                                TextAlign="Right"                                                
                                                Checked="False" 
                                                GroupName="RadioGroup1" 
                                                runat="server" /><br>

                                    <asp:RadioButton id="Radio2" 
                                                Text="Female" 
                                                TextAlign="Right"
                                                GroupName="RadioGroup1" 
                                                runat="server"/><br>


                                    <label for="txtAge"><strong>Age</strong> on day of run (minimum 13 years)</label> 
	                                <asp:TextBox ID="txtAge" runat="server" tabindex="8"></asp:TextBox>
 	                                <br />
	                                <br />
	                                <asp:Label ID="lblMessage" runat="server" ForeColor="#CC3300"></asp:Label>

                                </fieldset> 

                            </article>                          

                        </section>

                        <section class="grid_10 prefix_1">

                            <article>

			                    <header> <h3>&nbsp;</h3> </header>

                                <fieldset id="pt2"> 
            	    
	                                <legend><span>Step </span>2. Email</legend> 

                                    <label for="txtMail">

                                        <strong>Email</strong> <br />
                                        Please ensure your email address is correct.<br /> <br />
                                        The <strong>email address</strong> you use here should match the email address of the <strong>PayPal</strong> Account holder.

                                    </label>

	                                <asp:TextBox ID="txtMail" runat="server" tabindex="9"></asp:TextBox>
	                                <br />
	                                <br />
	                                <label for="txtTeam">
                                        <strong>Teams</strong><br />
                                        A team consists of <b>any</b> number of runners, and in addition to each runner being listed on 
                                        the results sheet, teams will be listed separately in finishing order 
                                        (average time of <b>all</b> runners). <br /> <br />
                                        Each team member should fill in a separate online entry form. <br /><br />
                                        Please enter your <strong>team name</strong> in the box below.
                                    </label> 
	                                <asp:TextBox ID="txtTeam" runat="server" tabindex="10"></asp:TextBox>
	                                <br />
	                                <br />

                                    <asp:Button ID="btnRegister" CssClass="button" runat="server" OnClick="btnRegister_Click" OnClientClick="return ValidateInfo(); aspnetForm.target ='_blank';" TabIndex="10" Text="Register online now" />

                                    <!-- 
                                    
                                    onclientclick="return ValidateInfo(); ReDirect();" 
                                    
                                    <input type="button" class="button" onclick="ReDirect();" value="ReadXml" />
                                    -->
                                </fieldset>

                            </article>

                        </section>

                        </ContentTemplate>

                        <Triggers>

                            <asp:AsyncPostBackTrigger ControlID="btnRegister" EventName="Click" />

                        </Triggers>

                    </asp:UpdatePanel>

            </section>

        </div>

    </form>

    </body>

</html>
